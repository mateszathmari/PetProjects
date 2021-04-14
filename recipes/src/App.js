import "./App.css";
import { useState, useEffect } from "react";
import Recipe from "./components/Recipe";
import GetRecipes from "./hooks/GetRecipes";
import Header from "./components/Header";
import { useStoreState, useStoreActions } from "easy-peasy";
import NextContent from "./components/NextContent";
import Search from "./components/Search";

function App() {
  const from = useStoreState((state) => state.search.from);
  const setFrom = useStoreActions((actions) => actions.search.setFrom);
  const to = useStoreState((state) => state.search.to);
  const setTo = useStoreActions((actions) => actions.search.setTo);
  const search = useStoreState((state) => state.search.search);
  const setSearch = useStoreActions((actions) => actions.search.setSearch);
  const query = useStoreState((state) => state.search.query);
  const setQuery = useStoreActions((actions) => actions.search.setQuery);

  if (query === "") {
    setQuery("chicken");
  }

  let [fetchedData, error, hasMore] = GetRecipes(query, from, to, [query, to]);

  return (
    <div className="App">
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
      <NextContent hasMore={hasMore} />
    </div>
  );
}

export default App;
