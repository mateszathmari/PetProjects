import "./App.css";
import { useState, useEffect } from "react";
import Recipe from "./components/Recipe";
import GetRecipes from "./hooks/GetRecipes";

function App() {
  const [search, setSearch] = useState("");
  const [query, setQuery] = useState("chicken");
  const [from, setFrom] = useState(0);
  const [to, setTo] = useState(10);

  let [isLoading, fetchedData] = GetRecipes(query, from, to, [query, to]);

  const UpdateSearch = (e) => {
    setSearch(e.target.value);
  };

  const getSearch = (e) => {
    e.preventDefault();
    setQuery(search);
    setSearch("");
  };

  const loadMore = () => {
    setTo(to + 10);
    setFrom(from + 10);
  };

  let content = <div className="App">Loading Recipes...</div>;

  if (!isLoading) {
    content = (
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
            <Recipe key={recipe.recipe.label} image={recipe.recipe.image} />
          ))}
        </div>
        <div className="loadMore" onClick={loadMore}>
          Click For More Content
        </div>
      </div>
    );
  }

  return content;
}

export default App;
