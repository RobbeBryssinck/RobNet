import React from "react";

function BotnetCurrentCommand({ status, command }) {
  if (status === "Working") {
    return (
      <>
        <b>Current command: </b>
        <span>{command} </span>
      </>
    );
  } else {
    return <></>;
  }
}

export default BotnetCurrentCommand;
