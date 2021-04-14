import React from "react";
import styled, { css } from "styled-components";
import magnifying_glass from "../pic/magnifying_glass.png";

export default function Search() {
  const Button = styled.button`
    background: transparent;
    border-radius: 4px;
    border: 9px solid red;
    color: palevioletred;
    margin: 0 0.5em;
    padding: 0.15em 0.5em;

    ${(props) =>
      props.primary &&
      css`
        background: red;
        color: white;
      `};
  `;
  return (
    <div className="search-box">
      <div className="search">
        <form>
          <input
            className="search-bar"
            type="text"
            placeholder="What are you looking for?"
          />
          <Button className="search-button" type="submit" primary>
            <div className="button-box">
              <img src={magnifying_glass} alt="search" width="13px" />
            </div>
          </Button>
        </form>
      </div>
    </div>
  );
}
