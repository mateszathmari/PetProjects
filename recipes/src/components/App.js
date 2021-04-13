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
  const [drecipes, setdRecipes] = useState([]);
  const [search, setSearch] = useState("");
  const [query, setQuery] = useState("chicken");

  useEffect(() => {
    getRecipes();
  }, [query]);

  useEffect(() => {
    document.addEventListener("scroll", refreshEvent);
  }, []);

  const getRecipes = async (from = 0, to = 10) => {
    const response = await fetch(
      `https://api.edamam.com/search?q=${query}&app_id=${API_ID}&app_key=${API_KEY}&from=${from}&to=${to}`
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

  const refreshEvent = async (e) => {
    let app = document.getElementsByClassName("recipes");
    console.log(app[0].offsetHeight + "max");
    console.log(window.pageYOffset + " actual");
    if (app[0].offsetHeight === window.pageYOffset + 500) {
      setSearch(new Array(...recipes, ...recipes));
      //alert("reached the bottom");
    }
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
      <div className="recipes">
        {recipes.map((recipe) => (
          <Recipe key={recipe.recipe.label} image={recipe.recipe.image} />
        ))}
      </div>
    </div>
  );
}

export default App;
