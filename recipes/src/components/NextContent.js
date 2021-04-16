import React from "react";
import { useStoreState, useStoreActions } from "easy-peasy";

export default function NextContent() {
  const loadMore = useStoreActions((actions) => actions.search.loadMore);
  const loading = useStoreState((state) => state.search.loading);
  const error = useStoreState((state) => state.search.error);
  const hasMore = useStoreState((state) => state.search.hasMore);

  let content = <div className="loading">Loading Recipes...</div>;

  if (!loading && hasMore) {
    content = (
      <div className="load-more" onClick={loadMore}>
        Click For More Content
      </div>
    );
  } else if (!loading && !hasMore) {
    content = <div className="no-content">Please search for recipes</div>;
  } else if (error) {
    content = <div className="error">Some error has occur...</div>;
  }
  return content;
}
