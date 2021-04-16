import React from "react";

export default function Login() {
  const login = (e) => {
    e.preventDefault();
    console.log(e.target.username.value);
    console.log(e.target.password.value);
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
