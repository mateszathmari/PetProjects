import { useEffect, useState } from "react";
import axios from "axios";

export default function GetRecipes(query, from, to, dependencies) {
  const API_ID = "8cfa623e";
  const API_KEY = "a3dc989b7a01df6e08dd2567b0af1abd";
  const EXAMPLEQUERRY = `https://api.edamam.com/search?q=chicken&app_id=${API_ID}&app_key=${API_KEY}`;
  //"https://api.edamam.com/search?q=chicken&app_id=${YOUR_APP_ID}&app_key=${YOUR_APP_KEY}&from=0&to=3&calories=591-722&health=alcohol-free"

  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);
  const [recipes, setRecipes] = useState([]);
  const [hasMore, setHasMore] = useState(false);

  useEffect(() => {
    setRecipes([]);
  }, [query]);

  useEffect(() => {
    setLoading(true);
    setError(false);
    let cancel;
    axios({
      method: "POST",
      url: `https://api.edamam.com/search?q=${query}&app_id=${API_ID}&app_key=${API_KEY}&from=${from}&to=${to}`,
    })
      .then((response) => {
        if (!response.status == 200) {
          throw new Error("Failed to fetch.");
        }
        console.log(response);
        return response;
      })
      .then((data) => {
        setLoading(false);
        setRecipes((prevBooks) => {
          return [...new Set([...prevBooks, ...data.data.hits])];
        });
      })
      .catch((err) => {
        console.log(err);
        setLoading(false);
      });
  }, dependencies);

  return [loading, recipes];
}
