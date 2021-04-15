import "./App.css";
import Recipe from "./components/Recipe";
import GetRecipes from "./hooks/GetRecipes";
import Header from "./components/Header";
import React from "react";
import { useStoreState, useStoreActions } from "easy-peasy";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import NextContent from "./components/NextContent";
import RecipeDetails from "./components/RecipeDetails";

function App() {
  const from = useStoreState((state) => state.search.from);
  const to = useStoreState((state) => state.search.to);
  const query = useStoreState((state) => state.search.query);
  const setQuery = useStoreActions((actions) => actions.search.setQuery);

  if (query === "") {
    setQuery("chicken");
  }

  let [fetchedData] = GetRecipes(query, from, to, [query, to]);

  return (
    <div className="App">
      <Router>
        <Switch>
          <Route exact path="/">
            <Header />
            <div className="recipes">
              {fetchedData.map((recipe) => (
                <Recipe
                  key={recipe.recipe.label}
                  label={recipe.recipe.label}
                  image={recipe.recipe.image}
                />
              ))}
            </div>
            <NextContent />
          </Route>
          <Route path="/about">about</Route>
          <Route path="/recipe-details">
            <RecipeDetails />
          </Route>
        </Switch>
      </Router>
    </div>
  );
}

export default App;
