import { createStore, action } from "easy-peasy";

const store = createStore({
  search: {
    from: 0,
    to: 10,
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
  },
});

export default store;
