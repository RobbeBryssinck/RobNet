import React, { useState, useEffect } from "react";
import axios from "axios";
import Bot from "./Bot";
import ExploitMachineForm from "./ExploitMachineForm";
import AddBotForm from "./AddBotForm";
import useInterval from "../utils";

const BotsUri = "https://localhost:32853/api/v1/Bots/";
const ExploitsUri = "undefined";

function BotList({ botnetStatus, botnetId }) {
  const [botList, setBotList] = useState([]);

  useEffect(() => {
    setBotList([]);
    axios.get(BotsUri + botnetId).then((res) => {
      const newBotList = res.data;
      setBotList(newBotList);
    });
  }, []);

  useInterval(() => {
    axios.get(BotsUri + botnetId).then((res) => {
      const newBotList = res.data;
      setBotList(newBotList);
    });
  }, 1 * 1000);

  const addBot = (bot) => {
    axios.post(BotsUri, bot).then((res) => {
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
      <AddBotForm addBot={addBot} botnetId={botnetId} />
    </div>
  );
}

export default BotList;
