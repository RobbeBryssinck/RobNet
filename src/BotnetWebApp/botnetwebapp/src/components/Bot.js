import React from "react";

import { Button } from "react-bootstrap";

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
        <Button variant="danger" onClick={handleDelete}>
          Delete
        </Button>
      </td>
    </tr>
  );
}

function BotStatus({ bot }) {
  if (bot.status === "Working") {
    return <td style={{ color: "darkgreen" }}>{bot.status}</td>;
  } else {
    return <td style={{ color: "black" }}>{bot.status}</td>;
  }
}

export default Bot;
