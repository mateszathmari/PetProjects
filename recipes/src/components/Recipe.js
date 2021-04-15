import React from "react";
import { Link } from "react-router-dom";
import { useStoreState, useStoreActions } from "easy-peasy";

const Recipe = ({ label, image }) => {
  const setRecipeImage = useStoreActions((state) => state.actualView.setImg);
  const setLabel = useStoreActions((state) => state.actualView.setLabel);

  const details = (e) => {
    setRecipeImage(image);
    setLabel(label);
  };

  return (
    <Link to={`/recipe-details`}>
      <div className="recipe" onClick={details}>
        <img className="recipe-image" src={image} alt="Food" />
        <div className="title-text">{label}</div>
      </div>
    </Link>
  );
};

export default Recipe;
