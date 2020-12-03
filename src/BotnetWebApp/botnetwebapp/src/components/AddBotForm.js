import React, { useState } from "react";
import "bootstrap/dist/css/bootstrap.min.css";

import { Button, Form, Row, Col, Modal } from "react-bootstrap";

function AddBotForm({ addBot, botnetId }) {
  const [ip, setIp] = useState("");
  const [platform, setPlatform] = useState("Linux");
  const [showModal, setShowModal] = useState(false);

  const handleCloseModal = () => setShowModal(false);
  const handleShowModal = () => setShowModal(true);

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!ip || !platform) return;

    const bot = {
      ip: ip,
      platform: platform,
      botnetId: botnetId,
    };
    addBot(bot);

    setIp("");
    setPlatform("");
    handleCloseModal();
  };

  return (
    <>
      <Button onClick={handleShowModal}>Add bot</Button>{" "}
      <Modal show={showModal} onHide={handleCloseModal}>
        <Modal.Header closeButton>
          <Modal.Title>Add bot to botnet</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form onSubmit={handleSubmit}>
            <Form.Group as={Row}>
              <Form.Label column>IP</Form.Label>
              <Col>
                <Form.Control
                  type="text"
                  placeholder="IP Address"
                  value={ip}
                  onChange={(e) => setIp(e.target.value)}
                />
              </Col>
            </Form.Group>
            <Form.Group as={Row}>
              <Form.Label column>Platform</Form.Label>
              <Col>
                <Form.Control as="select">
                  <option value="Linux" onClick={() => setPlatform("Linux")}>
                    Linux
                  </option>
                  <option
                    value="Windows"
                    onClick={() => setPlatform("Windows")}
                  >
                    Windows
                  </option>
                </Form.Control>
              </Col>
            </Form.Group>
            <Button type="submit">Submit</Button>
          </Form>
        </Modal.Body>
      </Modal>
    </>
  );
}

export default AddBotForm;
