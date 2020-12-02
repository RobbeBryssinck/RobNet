import React from "react";
import axios from "axios";
import {
  render,
  screen,
  waitForElement,
  waitForElementToBeRemoved,
} from "@testing-library/react";
import "@testing-library/jest-dom/extend-expect";
import userEvent from "@testing-library/user-event";

import BotList from "../components/BotList";

jest.mock("axios");

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

describe("BotList", () => {
  test("checks botlist for bots", async () => {
    axios.get.mockImplementation(() => Promise.resolve({ data: bots }));
    const container = render(<BotList />);

    await waitForElement(() => container.queryAllByPlaceholderText("Bot IP"));
    const items = await screen.findAllByRole("row");

    expect(items).toHaveLength(4);
  });

  test("delete bot from botlist", async () => {
    axios.get.mockImplementation(() => Promise.resolve({ data: bots }));
    axios.delete.mockImplementation(() => Promise.resolve());
    render(<BotList />);

    await waitForElement(() => screen.getByText(/192.168.0.144/));
    userEvent.click(screen.getAllByRole("button")[0]);
    await waitForElementToBeRemoved(() => screen.getByText(/192.168.0.144/));
    const items = await screen.findAllByRole("row");

    expect(items).toHaveLength(3);
  });
});
