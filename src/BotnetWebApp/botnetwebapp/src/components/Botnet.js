import React, { useState, useEffect } from "react";
import axios from "axios";
import BotnetCurrentCommand from "./BotnetCurrentCommand";
import BotnetCommands from "./BotnetCommands";
import BotList from "./BotList";
import useInterval from "../utils";

import { Col, Row, Card } from "react-bootstrap";

const BotnetsUri = "https://localhost:32853/api/v1/Botnets/";
const BotnetJobsUri = "https://localhost:32851/api/v1/Botnetjob/";

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
  }, [id]);

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
      setStatusColor("black");
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
      <Card className="shadow" body>
        <Row>
          <Col>
            <h2>Botnet</h2>
          </Col>
        </Row>
        <Row>
          <Col>
            <h4>Status</h4>
          </Col>
        </Row>
        <Row>
          <Col>
            <Row>
              <Col>
                <b>ID: </b>
                <span>{id} </span>
              </Col>
            </Row>
            <Row>
              <Col>
                <b>Status: </b>
                <span style={{ color: statusColor }}>{status} </span>
              </Col>
            </Row>
            <Row>
              <Col>
                <BotnetCurrentCommand status={status} command={command} />
              </Col>
            </Row>
          </Col>
        </Row>
        <br />
        <Row>
          <Col>
            <BotnetCommands
              id={id}
              botnetStatus={status}
              createBotnetJob={createBotnetJob}
              cancelBotnetJob={cancelBotnetJob}
            />
          </Col>
        </Row>
      </Card>
      <br />
      <BotList botnetId={id} />
    </>
  );
}

export default Botnet;
