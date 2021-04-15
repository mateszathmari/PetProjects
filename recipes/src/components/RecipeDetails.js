import { React } from "react";
import { useStoreState } from "easy-peasy";
import "../static/RecipeDetails.css";

export default function RecipeDetails() {
  const recipe = useStoreState((state) => state.actualView.recipe);

  let content = <div>loading</div>;

  if (recipe.label !== "") {
    content = (
      <div className="Recipe">
        <div className="Table">
          <table className="Column">
            <tr>
              <div className="Food-title">{recipe.label}</div>
            </tr>
            <tr>
              <div className="Total-time">{recipe.totalTime} mins</div>
            </tr>
            <tr>
              <img className="recipe-image" src={recipe.image} alt="Food" />
            </tr>
            <tr>
              <th>
                <label>Ingredients</label>
                <dl>
                  {recipe.ingredientLines.map((ingredientLine) => (
                    <dt>{ingredientLine}</dt>
                  ))}
                </dl>
              </th>
              <th>
                <label>Health labels</label>
                <dl>
                  {recipe.healthLabels.map((label) => (
                    <dt>{label}</dt>
                  ))}
                </dl>
              </th>
            </tr>
            <tr>
              <a href={recipe.url}>{recipe.url}</a>
            </tr>
          </table>
        </div>
      </div>
    );
  }

  return content;
}
