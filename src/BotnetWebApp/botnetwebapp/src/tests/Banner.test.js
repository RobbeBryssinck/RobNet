import React from "react";
import { render, screen } from "@testing-library/react";
import "@testing-library/jest-dom/extend-expect";

import Banner from "../components/Banner";

describe("Banner", () => {
  test("checks banner for text", () => {
    render(<Banner name={"RobNet"} />);

    expect(screen.getByText(/RobNet/)).toBeInTheDocument();
  });
});
