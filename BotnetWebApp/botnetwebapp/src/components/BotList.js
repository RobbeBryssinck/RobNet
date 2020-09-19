import React, { useContext } from "react";
import { GlobalContext } from "../context/GlobalState";
import Bot from "./Bot";

function BotList() {
  const { bots } = useContext(GlobalContext);

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
          {bots.map((bot) => (
            <Bot key={bot.id} bot={bot} />
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default BotList;
