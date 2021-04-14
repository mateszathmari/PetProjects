import React from "react";
import { useStoreState, useStoreActions } from "easy-peasy";

export default function NextContent(loading, hasMore) {
  const loadMore = useStoreActions((actions) => actions.search.loadMore);

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
