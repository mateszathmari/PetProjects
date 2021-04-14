import React from "react";
import styled, { css } from "styled-components";
import { useStoreState, useStoreActions } from "easy-peasy";
import magnifying_glass from "../pic/magnifying_glass.png";

export default function Search() {
  const search = useStoreState((state) => state.search.search);
  const setSearch = useStoreActions((actions) => actions.search.setSearch);
  const setQuery = useStoreActions((actions) => actions.search.setQuery);
  const setFrom = useStoreActions((actions) => actions.search.setFrom);
  const setTo = useStoreActions((actions) => actions.search.setTo);

  const UpdateSearch = (e) => {
    setSearch(e.target.value);
  };

  const getSearch = (e) => {
    e.preventDefault();
    setQuery(search);
    setFrom(0);
    setTo(10);
    setSearch("");
  };

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
        <form onSubmit={getSearch}>
          <input
            onChange={UpdateSearch}
            className="search-bar"
            type="text"
            value={search}
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
