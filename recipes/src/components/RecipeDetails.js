import { Redirect, React } from "react";
import { useStoreState } from "easy-peasy";
import "../static/RecipeDetails.css";
import heinz from "../pic/A-Heinz.jpg";
import subWay from "../pic/A-Sub.jpg";
import clock from "../pic/clock.png";
import { Link } from "react-router-dom";

export default function RecipeDetails() {
  window.scrollTo(0, 0);
  const recipe = useStoreState((state) => state.actualView.recipe);

  let content = <div>loading</div>;

  if (recipe.label !== undefined) {
    content = (
      <div className="Recipe">
        <div className="Table">
          <table className="Column">
            <tr>
              <div className="Food-title">{recipe.label}</div>
            </tr>
            <tr>
              <div className="Total-time">
                <img className="clock" src={clock} alt="clock" width="14px" />
                {recipe.totalTime > 0 ? recipe.totalTime : ""}
              </div>
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
            <a className="Recipe-link" href={recipe.url}>
              For more information click
            </a>
          </table>
          <div className="Advert">
            <img src={subWay} alt="Food" width="300px" />
            <img src={heinz} alt="Food" width="300px" />
          </div>
        </div>
      </div>
    );
  } else {
    content = (
      <div>
        There are no recipe found... Go back to home page and search again
        <Link to={`/`}>Home</Link>
      </div>
    );
  }

  return content;
}
