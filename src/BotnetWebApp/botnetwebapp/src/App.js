import React from "react";
import "./App.css";
import Banner from "./components/Banner";
import Botnet from "./components/Botnet";

import "bootstrap/dist/css/bootstrap.min.css";
import { Row, Container } from "react-bootstrap";

const path = require("path");
require("dotenv").config({ path: path.resolve(__dirname, "../.env") });

function App() {
  return (
    <Container>
      <Row>
        <Banner />
      </Row>
      <Botnet />
    </Container>
  );
}

export default App;
