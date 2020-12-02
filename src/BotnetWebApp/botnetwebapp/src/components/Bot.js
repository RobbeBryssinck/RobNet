import React from "react";

function Bot({ bot, deleteBot }) {
  const handleDelete = () => {
    deleteBot(bot);
  };

  return (
    <tr>
      <td>{bot.id}</td>
      <td placeholder="Bot IP">{bot.ip}</td>
      <td>{bot.platform}</td>
      <BotStatus bot={bot} />
      <td>
        <button onClick={handleDelete}>Delete?</button>
      </td>
    </tr>
  );
}

function BotStatus({ bot }) {
  if (bot.status === "Working") {
    return <td style={{ color: "darkgreen" }}>{bot.status}</td>;
  } else {
    return <td style={{ color: "darkred" }}>{bot.status}</td>;
  }
}

export default Bot;
