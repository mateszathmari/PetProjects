import { useEffect, React } from "react";
import Search from "./Search";
import logo from "../pic/logo.png";
import { Redirect } from "react-router";
import { useStoreState } from "easy-peasy";

export default function About() {
  const queryInProgress = useStoreState(
    (state) => state.search.queryInProgress
  );

  return (
    <div className="Home">
      <img src={logo} alt="logo" width="200px" />
      <Search />
      {queryInProgress !== "" ? <Redirect to="/recipes" /> : ""}
    </div>
  );
}
