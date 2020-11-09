import React from "react";

function BotnetCommands({ id, createBotnetJob }) {
  const cryptoJob = {
    botnetId: id,
    commandId: 1,
    command: "Crypto mining",
    commandArgument: "",
  };
  const ddosJob = {
    botnetId: id,
    commandId: 2,
    command: "DDoS",
    commandArgument: "",
  };
  const fileSortJob = {
    botnetId: id,
    commandId: 3,
    command: "File sort",
    commandArgument: "",
  };

  return (
    <div style={{ marginTop: "20px" }}>
      <button
        className="grey-button"
        onClick={() => createBotnetJob(cryptoJob)}
      >
        Crypto mining
      </button>
      <button className="grey-button" onClick={() => createBotnetJob(ddosJob)}>
        DDoS
      </button>
      <button
        className="grey-button"
        onClick={() => createBotnetJob(fileSortJob)}
      >
        File sort
      </button>
    </div>
  );
}

export default BotnetCommands;
