import { React } from "react";
import { Link } from "react-router-dom";
import logo from "../pic/logo.png";

export default function NavigationBar() {
  return (
    <div className="Navigation-bar">
      <img src={logo} alt="logo" width="80px" />
      <Link className="Navigation-text" to={"/"}>
        Home
      </Link>
      <Link className="Navigation-text" to={"/recipes"}>
        Recipes
      </Link>
      <Link className="Navigation-text" to={"/about"}>
        About
      </Link>
    </div>
  );
}
