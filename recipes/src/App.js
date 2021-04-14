import "./App.css";
import { useState, useEffect } from "react";
import Recipe from "./components/Recipe";
import GetRecipes from "./hooks/GetRecipes";
import Header from "./components/Header";
import { useStoreState, useStoreActions } from "easy-peasy";
import NextContent from "./components/NextContent";

function App() {
  const from = useStoreState((state) => state.search.from);
  const setFrom = useStoreActions((actions) => actions.search.setFrom);
  const to = useStoreState((state) => state.search.to);
  const setTo = useStoreActions((actions) => actions.search.setTo);

  const [search, setSearch] = useState("");
  const [query, setQuery] = useState("chicken");

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

  // const loadMore = () => {
  //   setTo(to + 10);
  //   setFrom(from + 10);
  // };

  // let nextContent = <div className="loading">Loading Recipes...</div>;

  // if (!loading && hasMore) {
  //   nextContent = (
  //     <div className="load-more" onClick={loadMore}>
  //       Click For More Content
  //     </div>
  //   );
  // } else if (!loading && !hasMore) {
  //   nextContent = <div className="no-more-content">No More Content</div>;
  // }

  return (
    <div className="App">
      <Header />
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
      <NextContent loading={loading} hasMore={hasMore} />
    </div>
  );
}

export default App;
