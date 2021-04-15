import { createStore, action } from "easy-peasy";

const store = createStore({
  actualRecipe: {
    calories,
    dietLabels,
    image,
    ingredientLines,
    label,
    totalTime,
    url,
    setCalories: action((state, calories) => {
      state.calories = calories;
    }),
    setDietLabels: action((state, dietLabels) => {
      state.dietLabels = dietLabels;
    }),
    setImage: action((state, image) => {
      state.image = image;
    }),
    setIngredientLines: action((state, ingredientLines) => {
      state.ingredientLines = ingredientLines;
    }),
    setLabel: action((state, label) => {
      state.label = label;
    }),
    setTotalTime: action((state, totalTime) => {
      state.totalTime = totalTime;
    }),
    setUrl: action((state, url) => {
      state.url = url;
    }),
  },
  search: {
    from: 0,
    to: 12,
    loading: true,
    search: "",
    query: "",
    error: "",
    hasMore: false,
    setFrom: action((state, number) => {
      state.from = number;
    }),
    setTo: action((state, number) => {
      state.to = number;
    }),
    loadMore: action((state) => {
      state.from = state.from + 12;
      state.to = state.to + 12;
    }),
    setLoading: action((state, isLoading) => {
      state.loading = isLoading;
    }),
    setSearch: action((state, searchParameter) => {
      state.search = searchParameter;
    }),
    setQuery: action((state, queryParameter) => {
      state.query = queryParameter;
    }),
    setError: action((state, isError) => {
      state.error = isError;
    }),
    setHasMore: action((state, hasMoreValue) => {
      state.hasMore = hasMoreValue;
    }),
  },
  actualView: {
    label: "",
    img: "",
    setLabel: action((state, labelParameter) => {
      state.label = labelParameter;
    }),
    setImg: action((state, imgParameter) => {
      state.img = imgParameter;
    }),
  },
});

export default store;
