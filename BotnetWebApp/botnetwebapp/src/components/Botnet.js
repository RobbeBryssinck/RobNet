import React, { useState, useEffect } from "react";
import axios from "axios";

const BotnetsUri = "https://localhost:44360/api/v1/Botnets/"

function Botnet() {
  const [botnet, setBotnet] = useState({});

  useEffect(() => {
    setBotnet({});
    axios.get(BotnetsUri + "1").then((res) => {
      const newBotnet = res.data;
      setBotnet(newBotnet);
    });
  }, [])

  return (
      <div>
          <h1>Botnet status</h1>
          <span>{botnet.status}</span>
      </div>
  );
}

export default Botnet;
