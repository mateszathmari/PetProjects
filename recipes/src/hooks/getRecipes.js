import { useEffect, useState } from "react";
import { useStoreActions } from "easy-peasy";
import axios from "axios";

export default function GetRecipes(query, from, to, dependencies) {
  const API_ID = "8cfa623e";
  const API_KEY = "a3dc989b7a01df6e08dd2567b0af1abd";
  const setHasMore = useStoreActions((actions) => actions.search.setHasMore);
  const setQueryInProgress = useStoreActions(
    (actions) => actions.search.setQueryInProgress
  );
  const setLoading = useStoreActions((actions) => actions.search.setLoading);
  const setError = useStoreActions((actions) => actions.search.setError);
  //"https://api.edamam.com/search?q=chicken&app_id=${YOUR_APP_ID}&app_key=${YOUR_APP_KEY}&from=0&to=3&calories=591-722&health=alcohol-free"

  const [recipes, setRecipes] = useState([]);

  useEffect(() => {
    setRecipes([]);
    setQueryInProgress(query);
  }, [query]);

  useEffect(() => {
    setLoading(true);
    setError(false);
    axios({
      method: "POST",
      url: `https://api.edamam.com/search?q=${query}&app_id=${API_ID}&app_key=${API_KEY}&from=${from}&to=${to}`,
    })
      .then((response) => {
        if (!response.status === 200) {
          throw new Error("Failed to fetch.");
        }
        return response;
      })
      .then((data) => {
        setLoading(false);
        if (data.data.count - to > 0) {
          setHasMore(true);
        } else {
          setHasMore(false);
        }
        setQueryInProgress("");
        setRecipes((prevBooks) => {
          return [...new Set([...prevBooks, ...data.data.hits])];
        });
      })
      .catch((err) => {
        setError(true);
        setLoading(false);
      });
  }, dependencies);

  return [recipes];
}
