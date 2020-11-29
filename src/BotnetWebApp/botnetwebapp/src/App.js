import React from "react";
import "./App.css";
import Banner from "./components/Banner";
import Botnet from "./components/Botnet";

const path = require("path");
require("dotenv").config({ path: path.resolve(__dirname, "../.env") });

function App() {
  return (
    <div>
      <Banner />
      <Botnet />
    </div>
  );
}

export default App;
