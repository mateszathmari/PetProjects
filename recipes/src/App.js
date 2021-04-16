import "./App.css";
import Recipe from "./components/Recipe";
import GetRecipes from "./hooks/getRecipes";
import Header from "./components/Header";
import React from "react";
import { useStoreState, useStoreActions } from "easy-peasy";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import NextContent from "./components/NextContent";
import RecipeDetails from "./components/RecipeDetails";
import NavigationBar from "./components/NavigationBar";
import About from "./components/About";
import Home from "./components/Home";

function App() {
  const from = useStoreState((state) => state.search.from);
  const to = useStoreState((state) => state.search.to);
  const query = useStoreState((state) => state.search.query);
  const setQuery = useStoreActions((actions) => actions.search.setQuery);
  let [fetchedData] = GetRecipes(query, from, to, [query, to]);

  return (
    <div className="App">
      <Router>
        <NavigationBar />
        <Switch>
          <Route exact path="/">
            <Home />
          </Route>
          <Route path="/about" component={About} />

          <Route path="/recipe-details">
            <RecipeDetails />
          </Route>
          <Route path="/recipes">
            <Header />
            <div className="recipes">
              {fetchedData.map((recipe) => (
                <Recipe key={recipe.recipe.url} recipe={recipe.recipe} />
              ))}
            </div>
            <NextContent />
          </Route>
        </Switch>
      </Router>
    </div>
  );
}

export default App;
