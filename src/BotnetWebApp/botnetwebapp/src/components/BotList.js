import React, { useState, useEffect } from "react";
import axios from "axios";
import Bot from "./Bot";
import ExploitMachineForm from "./ExploitMachineForm";
import AddBotForm from "./AddBotForm";
import useInterval from "../utils";

import { Col, Row, Table, Card, Button, Modal } from "react-bootstrap";

const BotsUri = "https://localhost:32853/api/v1/Bots/";
const ExploitsUri = "undefined";

function BotList({ botnetId }) {
  const [botList, setBotList] = useState([]);
  const [showAddBot, setShowAddBot] = useState(false);

  const handleCloseAddBot = () => setShowAddBot(false);
  const handleShowAddBot = () => setShowAddBot(true);

  useEffect(() => {
    setBotList([]);
    axios.get(BotsUri + botnetId).then((res) => {
      const newBotList = res.data;
      setBotList(newBotList);
    });
  }, []);

  useInterval(() => {
    axios.get(BotsUri + botnetId).then((res) => {
      const newBotList = res.data;
      setBotList(newBotList);
    });
  }, 1 * 1000);

  const addBot = (bot) => {
    axios.post(BotsUri, bot).then((res) => {
      const newBot = res.data;
      const newBotList = [...botList, newBot];
      setBotList(newBotList);
    });
  };

  const exploitMachine = (ip) => {
    const machine = { ip: ip };

    axios.post(ExploitsUri, machine).then((res) => {
      const newBot = res.data;
      const newBotList = [...botList, newBot];
      setBotList(newBotList);
    });
  };

  const deleteBot = (bot) => {
    const id = bot.id;

    axios.delete(BotsUri + id).then(() => {
      const newBotList = botList.filter((filterBot) => filterBot.id !== bot.id);
      setBotList(newBotList);
    });
  };

  return (
    <Card className="shadow" body>
      <Row>
        <Col>
          <h2>Bots</h2>
        </Col>
      </Row>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>ID</th>
            <th>IP address</th>
            <th>Platform</th>
            <th>Status</th>
            <th>Remove</th>
          </tr>
        </thead>
        <tbody>
          {botList.map((bot) => (
            <Bot key={bot.id} bot={bot} deleteBot={deleteBot} />
          ))}
        </tbody>
      </Table>
      <AddBotForm addBot={addBot} botnetId={botnetId} />
      <ExploitMachineForm exploitMachine={exploitMachine} />
    </Card>
  );
}

export default BotList;
