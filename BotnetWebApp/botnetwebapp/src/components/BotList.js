import React, { useState } from "react";
import Bot from "./Bot";
import AddBot from "./AddBotForm";

function BotList() {
  const [botList, setBotList] = useState([
    {
      id: 1,
      ip: "192.168.0.120",
      platform: "Linux",
      status: "Online",
      tasks: "None",
    },
    {
      id: 2,
      ip: "192.168.0.121",
      platform: "Windows",
      status: "Offline",
      tasks: "None",
    },
    {
      id: 3,
      ip: "192.168.0.122",
      platform: "Linux",
      status: "Online",
      tasks: "None",
    },
  ]);

  const addBot = (ip) => {
    const newBot = {
      id: 5,
      ip: ip,
      platform: "Mac OS",
      status: "Online",
      tasks: "None",
    };
    const newBotList = [...botList, newBot];
    console.log(newBotList);
    setBotList(newBotList);
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
            <th>Current tasks</th>
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
      <AddBot addBot={addBot} />
    </div>
  );
}

export default BotList;
