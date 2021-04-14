import "./App.css";
import { useState, useEffect } from "react";
import Recipe from "./components/Recipe";
import GetRecipes from "./hooks/GetRecipes";

function App() {
  const [search, setSearch] = useState("");
  const [query, setQuery] = useState("chicken");
  const [from, setFrom] = useState(0);
  const [to, setTo] = useState(10);

  let [loading, fetchedData, error, hasMore] = GetRecipes(query, from, to, [
    query,
    to,
  ]);

  const UpdateSearch = (e) => {
    setSearch(e.target.value);
  };

  const getSearch = (e) => {
    e.preventDefault();
    setQuery(search);
    setFrom(0);
    setTo(10);
    setSearch("");
  };

  const loadMore = () => {
    setTo(to + 10);
    setFrom(from + 10);
  };

  let nextContent = <div className="loading">Loading Recipes...</div>;

  if (!loading && hasMore) {
    nextContent = (
      <div className="load-more" onClick={loadMore}>
        Click For More Content
      </div>
    );
  } else if (!loading && !hasMore) {
    nextContent = <div className="no-more-content">No More Content</div>;
  }

  return (
    <div className="App">
      <form onSubmit={getSearch}>
        <input
          className="search-bar"
          type="text"
          onChange={UpdateSearch}
          placeholder="search here"
          value={search}
        />
        <button className="search-button" type="submit">
          Search
        </button>
      </form>
      <div className="recipes">
        {fetchedData.map((recipe) => (
          <Recipe
            key={recipe.recipe.label}
            label={recipe.recipe.label}
            image={recipe.recipe.image}
          />
        ))}
      </div>
      {nextContent}
    </div>
  );
}

export default App;
