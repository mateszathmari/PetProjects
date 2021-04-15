import { Redirect, React } from "react";
import { Link } from "react-router-dom";
import logo from "../pic/logo.png";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";

export default function NavigationBar() {
  return (
    <div className="Navigation-bar">
      {/* <div onClick={<Redirect to="/" />}>Home</div> */}
      <img src={logo} alt="logo" width="80px" />
      <Link className="Navigation-text" to={"/"}>
        Home{" "}
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
