import React from "react";
import "./App.css";
import Banner from "./components/Banner";
import Botnet from "./components/Botnet";
import BotList from "./components/BotList";

function App() {
  return (
    <div>
      <Banner />
      <Botnet />
      <BotList />
    </div>
  );
}

export default App;
