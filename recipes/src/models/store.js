import { createStore, action } from "easy-peasy";

const store = createStore({
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
    recipe: {},
    setRecipe: action((state, recipe) => {
      state.recipe = recipe;
    }),
  },
});

export default store;
