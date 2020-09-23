import React from "react";
import "./App.css";
import Banner from "./components/Banner";
import BotList from "./components/BotList";
import BotnetCommands from "./components/BotnetCommands";

function App() {
  return (
    <div>
      <Banner />
      <BotnetCommands />
      <BotList />
    </div>
  );
}

export default App;
