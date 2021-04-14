import React from "react";

const Recipe = ({ key, image }) => {
  return (
    <div className="recipe">
      <img src={image} alt="asd" />
    </div>
  );
};

export default Recipe;
