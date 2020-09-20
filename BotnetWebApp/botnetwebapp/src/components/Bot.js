import React from "react";

function Bot({ bot }) {
  return (
    <tr>
      <td>{bot.id}</td>
      <td>{bot.ip}</td>
      <td>{bot.platform}</td>
      <td>{bot.status}</td>
      <td>
        <button>Commands</button>
      </td>
      <td>
        <button>SSH</button>
      </td>
      <td>
        <button>Delete?</button>
      </td>
    </tr>
  );
}

export default Bot;
