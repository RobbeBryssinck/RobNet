import React from "react";

import { Col } from "react-bootstrap";

function Banner({ name }) {
  return (
    <Col>
      <h1>{name}</h1>
    </Col>
  );
}

export default Banner;
