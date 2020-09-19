import React from "react";

function BotList() {
  return (
    <div className="BotList">
      <table id="botlist-table">
        <tr>
          <th>ID</th>
          <th>Platform</th>
          <th>Status</th>
          <th>Commands</th>
          <th>SSH</th>
          <th>Remove</th>
        </tr>
        <tr>
          <td>1</td>
          <td>Linux</td>
          <td>Online</td>
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
        <tr>
          <td>2</td>
          <td>Windows</td>
          <td>Offline</td>
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
        <tr>
          <td>3</td>
          <td>Windows</td>
          <td>Online</td>
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
      </table>
    </div>
  );
}

export default BotList;
