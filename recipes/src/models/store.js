import { createStore, action } from "easy-peasy";

const store = createStore({
  search: {
    from: 0,
    to: 10,
    loading: true,
    search: "",
    query: "",
    setFrom: action((state, number) => {
      state.from = number;
    }),
    setTo: action((state, number) => {
      state.to = number;
    }),
    loadMore: action((state) => {
      state.from = state.from + 10;
      state.to = state.to + 10;
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
  },
});

export default store;
