import React from "react";
import { Link } from "react-router-dom";
import logo from "../pic/logo.png";
import developer from "../pic/developer.png";

export default function About() {
  return (
    <div className="About">
      <img src={logo} alt="logo" width="200px" />
      <a href={"https://github.com/mateszathmari"}>
        <img
          className="dev-pic"
          src={developer}
          alt="developer"
          width="100px"
        />
      </a>
    </div>
  );
}
