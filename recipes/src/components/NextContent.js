import React from "react";
import { useStoreState, useStoreActions } from "easy-peasy";

export default function NextContent(hasMore) {
  const loadMore = useStoreActions((actions) => actions.search.loadMore);
  const loading = useStoreState((state) => state.search.loading);

  let content = <div className="loading">Loading Recipes...</div>;

  if (!loading && hasMore) {
    content = (
      <div className="load-more" onClick={loadMore}>
        Click For More Content
      </div>
    );
  } else if (!loading && !hasMore) {
    content = <div className="no-more-content">No More Content</div>;
  }
  return content;
}
