import React from "react";
import { render, screen } from "@testing-library/react";
import "@testing-library/jest-dom/extend-expect";
import userEvent from "@testing-library/user-event";

import Bot from "../components/Bot";

const waitingBot = {
  id: 1,
  ip: "192.168.0.144",
  platform: "Linux",
  status: "Waiting",
};

const workingBot = {
  id: 2,
  ip: "192.168.0.146",
  platform: "Linux",
  status: "Working",
};

describe("Bot", () => {
  test("renders a waiting bot", () => {
    render(<Bot bot={waitingBot} />);

    expect(screen.getByText(/Waiting/)).toBeInTheDocument();
  });

  test("renders a working bot", () => {
    render(<Bot bot={workingBot} />);

    expect(screen.getByText(/Working/)).toBeInTheDocument();
  });

  test("deletes a bot", async () => {
    const deleteBot = jest.fn();

    render(<Bot bot={waitingBot} deleteBot={deleteBot} />);

    await userEvent.click(screen.getByRole("button"));

    expect(deleteBot).toHaveBeenCalledTimes(1);
  });
});
