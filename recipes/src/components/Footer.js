import { Redirect, React } from "react";
import { Link } from "react-router-dom";
import logo from "../pic/logo.png";
import easyPeasy from "../pic/easy-peasy.png";
import github from "../pic/github.png";
import htmlCss from "../pic/html-css.png";
import react from "../pic/React.png";
import vs from "../pic/vs.png";

export default function Footer() {
  return (
    <div className="Footer">
      <img className="logo" src={easyPeasy} alt="logo" height="40px" />
      <img className="logo" src={htmlCss} alt="logo" height="40px" />
      <img className="logo" src={github} alt="logo" height="40px" />
      <img className="logo" src={react} alt="logo" height="40px" />
      <img className="logo" src={vs} alt="logo" height="40px" />
    </div>
  );
}
