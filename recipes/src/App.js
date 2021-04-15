import "./App.css";
import Recipe from "./components/Recipe";
import GetRecipes from "./hooks/GetRecipes";
import Header from "./components/Header";
import React from "react";
import { useStoreState, useStoreActions } from "easy-peasy";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import NextContent from "./components/NextContent";
import RecipeDetails from "./components/RecipeDetails";
import NavigationBar from "./components/NavigationBar";
import About from "./components/About";

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
        <NavigationBar />
        <Switch>
          <Route exact path="/">
            <Header />
            <div className="recipes">
              {fetchedData.map((recipe) => (
                <Recipe key={recipe.recipe.url} recipe={recipe.recipe} />
              ))}
            </div>
            <NextContent />
          </Route>
          <Route path="/about" component={About} />

          <Route path="/recipe-details">
            <RecipeDetails />
          </Route>
        </Switch>
      </Router>
    </div>
  );
}

export default App;
