import React from "react";

const Recipe = ({ label, image }) => {
  return (
    <div className="recipe">
      <img className="recipe-image" src={image} alt="image" />
      <div className="title-text">{label}</div>
    </div>
  );
};

export default Recipe;
