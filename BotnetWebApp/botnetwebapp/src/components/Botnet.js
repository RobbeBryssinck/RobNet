import React, { useState, useEffect } from "react";
import axios from "axios";
import BotnetCurrentCommand from "./BotnetCurrentCommand";

const BotnetsUri = "https://localhost:44360/api/v1/Botnets/";

function Botnet() {
  const [botnet, setBotnet] = useState({});

  useEffect(() => {
    setBotnet({});
    axios.get(BotnetsUri + "1").then((res) => {
      const newBotnet = res.data;
      setBotnet(newBotnet);
    });
  }, []);

  return (
    <div className="container">
      <h2>Botnet</h2>
      <b>ID: </b>
      <span>{botnet.id} </span>
      <b>Status: </b>
      <span>{botnet.status} </span>
      <BotnetCurrentCommand status={botnet.status} command={botnet.command} />
    </div>
  );
}

export default Botnet;
