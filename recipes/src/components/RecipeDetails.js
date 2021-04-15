import { React } from "react";
import { useStoreState } from "easy-peasy";

export default function RecipeDetails() {
  const recipe = useStoreState((state) => state.actualView.recipe);

  let content = <div>loading</div>;

  if (recipe.label !== "") {
    content = (
      <div>
        <div>{recipe.label}</div>
        <img src={recipe.image} alt="Food" />
      </div>
    );
  }

  return content;
}
