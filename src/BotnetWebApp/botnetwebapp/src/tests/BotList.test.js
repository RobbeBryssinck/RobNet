import React from "react";
import axios from "axios";
import { render, screen, waitForElement } from "@testing-library/react";
import "@testing-library/jest-dom/extend-expect";

import BotList from "../components/BotList";

jest.mock("axios");

describe("BotList", () => {
  test("checks botlist for bots", async () => {
    const bots = [
      {
        id: 1,
        ip: "192.168.0.144",
        platform: "Linux",
        status: "Waiting",
      },
      {
        id: 2,
        ip: "192.168.0.146",
        platform: "Linux",
        status: "Waiting",
      },
      {
        id: 3,
        ip: "192.168.0.156",
        platform: "Windows",
        status: "Waiting",
      },
    ];

    axios.get.mockImplementation(() => Promise.resolve({ data: bots }));

    const container = render(<BotList />);

    await waitForElement(() => container.queryAllByPlaceholderText("Bot IP"));

    const items = await screen.findAllByRole("row");

    expect(items).toHaveLength(4);
  });
});
