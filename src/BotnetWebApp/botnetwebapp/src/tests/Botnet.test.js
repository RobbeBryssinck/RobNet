import React from "react";
import axios from "axios";
import { render, screen } from "@testing-library/react";
import "@testing-library/jest-dom/extend-expect";

import Botnet from "../components/Botnet";

jest.mock("axios");

describe("Botnet", () => {
  test("display message if botnet does not exist", async () => {
    axios.get.mockImplementation(() => Promise.reject(new Error()));
    render(<Botnet />);

    const message = await screen.findByText(
      /The requested botnet is not available or does not exist./
    );

    expect(message).toBeInTheDocument();
  });
});
