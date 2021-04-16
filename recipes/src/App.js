import "./App.css";
import Recipe from "./components/Recipe";
import GetRecipes from "./hooks/getRecipes";
import Header from "./components/Header";
import React from "react";
import { useStoreState } from "easy-peasy";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import NextContent from "./components/NextContent";
import RecipeDetails from "./components/RecipeDetails";
import NavigationBar from "./components/NavigationBar";
import About from "./components/About";
import Home from "./components/Home";
import Footer from "./components/Footer";
import Login from "./components/Login";

function App() {
  const from = useStoreState((state) => state.search.from);
  const to = useStoreState((state) => state.search.to);
  const query = useStoreState((state) => state.search.query);
  let [fetchedData] = GetRecipes(query, from, to, [query, to]);

  return (
    <div className="App">
      <Router>
        <NavigationBar />
        <Switch>
          <Route exact path="/" component={Home} />
          <Route path="/about" component={About} />
          <Route path="/login" component={Login} />

          <Route path="/recipes">
            <Header />
            <div className="recipes">
              {fetchedData.map((recipe) => (
                <Recipe key={recipe.recipe.url} recipe={recipe.recipe} />
              ))}
            </div>
            <NextContent />
          </Route>
          <Route path="/recipe-details">
            <RecipeDetails />
          </Route>
        </Switch>
        <Footer />
      </Router>
    </div>
  );
}

export default App;
