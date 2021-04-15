import { useEffect, React } from "react";
import { useStoreState } from "easy-peasy";

export default function RecipeDetails() {
  const image = useStoreState((state) => state.actualView.img);
  const label = useStoreState((state) => state.actualView.label);

  let content = <div>loading</div>;

  if (label != "") {
    content = (
      <div>
        <div>{label}</div>
        <img src={image} />
      </div>
    );
  }

  return content;
}
