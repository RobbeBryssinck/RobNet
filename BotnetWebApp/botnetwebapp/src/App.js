import React from "react";
import "./App.css";
import Banner from "./components/Banner";
import BotList from "./components/BotList";
import AddBot from "./components/AddBot";
import BotnetCommands from "./components/BotnetCommands";

import { GlobalProvider } from "./context/GlobalState";

function App() {
  return (
    <GlobalProvider>
      <Banner />
      <BotnetCommands />
      <BotList />
      <AddBot />
    </GlobalProvider>
  );
}

export default App;
