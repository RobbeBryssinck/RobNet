import React from "react";
import "./App.css";
import Banner from "./components/Banner";
import BotnetCommands from "./components/BotnetCommands";
import Botnet from "./components/Botnet";
import BotList from "./components/BotList";

function App() {
  return (
    <div>
      <Banner />
      <BotnetCommands />
      <Botnet />
      <BotList />
    </div>
  );
}

export default App;
