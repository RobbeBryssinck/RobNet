import React, { useState, useEffect } from "react";
import axios from "axios";
import BotnetCurrentCommand from "./BotnetCurrentCommand";
import BotnetCommands from "./BotnetCommands";
import BotList from "./BotList";
import useInterval from "../utils";

const BotnetsUri = process.env.REACT_APP_BOTNETS_URI;
const BotnetJobsUri = process.env.REACT_APP_BOTNETJOBS_URI;

function Botnet() {
  const [id, setId] = useState(1);
  const [status, setStatus] = useState();
  const [command, setCommand] = useState();
  const [botnetjobId, setBotnetjobId] = useState();
  const [statusColor, setStatusColor] = useState();

  useEffect(() => {
    axios.get(BotnetsUri + id).then((res) => {
      const newBotnet = res.data;
      setId(newBotnet.id);
      setStatus(newBotnet.status);
      setCommand(newBotnet.command);
    });
  }, []);

  useInterval(() => {
    axios.get(BotnetsUri + id).then((res) => {
      const newBotnet = res.data;
      setId(newBotnet.id);
      setStatus(newBotnet.status);
      setCommand(newBotnet.command);
      if (newBotnet.status === "Working") {
        axios.get(BotnetJobsUri + id).then((jobres) => {
          setBotnetjobId(jobres.data.id);
        });
      }
    });
  }, 1 * 1000);

  useEffect(() => {
    if (status === "Working") {
      setStatusColor("darkgreen");
    } else {
      setStatusColor("darkred");
    }
  }, [status]);

  const createBotnetJob = (botnetJob) => {
    axios.post(BotnetJobsUri, botnetJob).then((res) => {
      setId(res.data.botnetId);
      setStatus(res.data.status);
      setCommand(res.data.command);
    });
  };

  const cancelBotnetJob = () => {
    axios.delete(BotnetJobsUri + botnetjobId).then((res) => {
      setId(res.data.botnetId);
      setStatus("Waiting");
      setCommand(undefined);
    });
  };

  return (
    <>
      <div className="container">
        <h2>Botnet</h2>
        <b>ID: </b>
        <span>{id} </span>
        <b>Status: </b>
        <span style={{ color: statusColor }}>{status} </span>
        <BotnetCurrentCommand status={status} command={command} />
        <br />
        <BotnetCommands
          id={id}
          botnetStatus={status}
          createBotnetJob={createBotnetJob}
          cancelBotnetJob={cancelBotnetJob}
        />
      </div>
      <BotList botnetStatus={status} botnetId={id} />
    </>
  );
}

export default Botnet;
