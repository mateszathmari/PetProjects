import React from "react";
import { useStoreState, useStoreActions } from "easy-peasy";

const Recipe = ({ label, image }) => {
  const setRecipeImage = useStoreActions((state) => state.actualView.setImg);
  const setLabel = useStoreActions((state) => state.actualView.setLabel);
  const details = (e) => {
    setRecipeImage(image);
    setLabel(label);
    window.location = "/recipe-details";
  };

  return (
    <div className="recipe" onClick={details}>
      <img className="recipe-image" src={image} alt="Food" />
      <div className="title-text">{label}</div>
    </div>
  );
};

export default Recipe;
