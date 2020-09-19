import React, { createContext, useReducer } from "react";
import AppReducer from "./AppReducer";

// Initial state
const initialState = {
  bots: [
    {
      id: 1,
      ip: "192.168.0.120",
      platform: "Linux",
      status: "Online",
      tasks: "None",
    },
    {
      id: 2,
      ip: "192.168.0.121",
      platform: "Windows",
      status: "Offline",
      tasks: "None",
    },
    {
      id: 3,
      ip: "192.168.0.122",
      platform: "Linux",
      status: "Online",
      tasks: "None",
    },
  ],
};

// Create context
export const GlobalContext = createContext(initialState);

// Provided component
export const GlobalProvider = ({ children }) => {
  const [state, dispatch] = useReducer(AppReducer, initialState);

  return (
    <GlobalContext.Provider
      value={{
        bots: state.bots,
      }}
    >
      {children}
    </GlobalContext.Provider>
  );
};
