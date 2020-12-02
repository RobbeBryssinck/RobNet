import React from "react";

import { Button, Form, Row, Col } from "react-bootstrap";

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
      <>
        <h4>Commands</h4>
        <Button onClick={() => createBotnetJob(cryptoJob)}>
          Crypto mining
        </Button>{" "}
        <Button onClick={() => createBotnetJob(ddosJob)}>DDoS</Button>{" "}
        <Button onClick={() => createBotnetJob(fileSortJob)}>File sort</Button>{" "}
      </>
    );
  } else if (botnetStatus === "Working") {
    return (
      <Button variant="danger" onClick={() => cancelBotnetJob()}>
        Cancel command
      </Button>
    );
  } else {
    return <></>;
  }
}

export default BotnetCommands;
