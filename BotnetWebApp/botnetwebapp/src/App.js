import React from "react";
import "./App.css";
import Banner from "./components/Banner";
import BotList from "./components/BotList";
import AddBot from "./components/AddBot";
import BotnetCommands from "./components/BotnetCommands";

function App() {
  return (
    <div className="App">
      <Banner />
      <BotnetCommands />
      <BotList />
      <AddBot />
    </div>
  );
}

export default App;
