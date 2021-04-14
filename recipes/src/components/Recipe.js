import React from "react";

const Recipe = ({ label, image }) => {
  return (
    <div className="recipe">
      <img className="recipe-image" src={image} alt="Food" />
      <div className="title-text">{label}</div>
    </div>
  );
};

export default Recipe;
