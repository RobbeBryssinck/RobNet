import React from "react";

function BotnetCommands({
  id,
  botnetStatus,
  createBotnetJob,
  cancelBotnetJob,
}) {
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

  if (botnetStatus === "Waiting") {
    return (
      <div style={{ marginTop: "20px" }}>
        <button
          className="grey-button"
          onClick={() => createBotnetJob(cryptoJob)}
        >
          Crypto mining
        </button>
        <button
          className="grey-button"
          onClick={() => createBotnetJob(ddosJob)}
        >
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
  } else if (botnetStatus === "Working") {
    return (
      <div style={{ marginTop: "20px" }}>
        <button onClick={() => cancelBotnetJob()}>Cancel job</button>
      </div>
    );
  } else {
    return <></>;
  }
}

export default BotnetCommands;
