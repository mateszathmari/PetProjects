import React from "react";
import { Link } from "react-router-dom";
import { useStoreActions } from "easy-peasy";

const Recipe = ({ recipe }) => {
  const setRecipe = useStoreActions((state) => state.actualView.setRecipe);

  const details = (e) => {
    setRecipe(recipe);
  };

  return (
    <Link to={`/recipe-details`}>
      <div className="recipe" onClick={details}>
        <img className="recipe-image" src={recipe.image} alt="Food" />
        <div className="title-text">{recipe.label}</div>
      </div>
    </Link>
  );
};

export default Recipe;
