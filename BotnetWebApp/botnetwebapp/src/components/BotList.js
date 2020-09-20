import React, { useState, useEffect } from "react";
import axios from "axios";
import Bot from "./Bot";
import AddBotForm from "./AddBotForm";

const BotsUri = "https://localhost:44343/api/Bots/";
const ExploitsUri = "https://localhost:44343/api/Exploits/";

function BotList() {
  const [botList, setBotList] = useState([]);

  useEffect(() => {
    axios.get(BotsUri).then((res) => {
      const newBotList = res.data;
      setBotList(newBotList);
    });
  }, []);

  const addBot = (ip) => {
    /*
    const newBot = {
      id: 5,
      ip: ip,
      platform: "Mac OS",
      status: "Online",
    };
    */

    const exploit = { ip: ip };

    axios.post(ExploitsUri, exploit).then((res) => {
      const newBot = res.data;
      const newBotList = [...botList, newBot];
      setBotList(newBotList);
    });
  };

  return (
    <div className="BotList">
      <table id="botlist-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>IP address</th>
            <th>Platform</th>
            <th>Status</th>
            <th>Commands</th>
            <th>SSH</th>
            <th>Remove</th>
          </tr>
        </thead>
        <tbody>
          {botList.map((bot) => (
            <Bot key={bot.id} bot={bot} />
          ))}
        </tbody>
      </table>
      <AddBotForm addBot={addBot} />
    </div>
  );
}

export default BotList;
