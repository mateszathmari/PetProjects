import { getDefaultNormalizer } from "@testing-library/dom";
import React from "react";
import axios from "axios";
import sendAjax from "../controllers/ApiController";

export default function Login() {
  const login = (e) => {
    e.preventDefault();
    // console.log(e.target.username.value);
    // console.log(e.target.password.value);

    let newLogin = {
      username: e.target.username.value,
      password: e.target.password.value,
    };

    let jsonData = JSON.stringify(newLogin);

    sendAjax(
      "https://localhost:44386/api/login",
      "POST",
      null,
      JSON.stringify(newLogin)
    );
  };

  return (
    <div className="Container">
      <form className="Login" onSubmit={login}>
        <input
          className="Login-element"
          type="text"
          name="username"
          placeholder="Username"
        />
        <input
          className="Login-element"
          type="password"
          name="password"
          placeholder="Password"
        />
        <button className="Login-element" type="submit">
          Login
        </button>
      </form>
    </div>
  );
}
