import React from "react";

function Bot({ bot, deleteBot }) {
  const handleDelete = () => {
    deleteBot(bot);
  };

  return (
    <tr>
      <td>{bot.id}</td>
      <td>{bot.ip}</td>
      <td>{bot.platform}</td>
      <td>{bot.status}</td>
      <td>{bot.sshName}</td>
      <td>
        <button>SSH</button>
      </td>
      <td>
        <button onClick={handleDelete}>Delete?</button>
      </td>
    </tr>
  );
}

export default Bot;
