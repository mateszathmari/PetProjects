import "./App.css";
import { useState, useEffect } from "react";
import Recipe from "./Recipe";

function App() {
  const API_ID = "";
  const API_KEY = "";
  const EXAMPLEQUERRY = `https://api.edamam.com/search?q=chicken&app_id=${API_ID}&app_key=${API_KEY}`;

  //"https://api.edamam.com/search?q=chicken&app_id=${YOUR_APP_ID}&app_key=${YOUR_APP_KEY}&from=0&to=3&calories=591-722&health=alcohol-free"

  // const getInfo = () => {
  //   fetch(EXAMPLEQUERRY)
  //     .then((response) => {
  //       console.log(response.json());
  //     })
  //     .catch((err) => {
  //       console.error(err);
  //     });
  // };

  const [recipes, setRecipes] = useState([]);
  const [search, setSearch] = useState("");
  const [query, setQuery] = useState("chicken");

  useEffect(() => {
    getRecipes();
  }, [query]);

  const getRecipes = async () => {
    const response = await fetch(
      `https://api.edamam.com/search?q=${query}&app_id=${API_ID}&app_key=${API_KEY}&from=0&to=3`
    );
    const data = await response.json();
    setRecipes(data.hits);
  };

  const UpdateSearch = (e) => {
    setSearch(e.target.value);
  };

  const getSearch = (e) => {
    e.preventDefault();
    setQuery(search);
    setSearch("");
  };

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
      {recipes.map((recipe) => (
        <Recipe key={recipe.recipe.label} image={recipe.recipe.image} />
      ))}
    </div>
  );
}

export default App;
