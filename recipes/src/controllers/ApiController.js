import axios from "axios";

export default function sendAjax(
  endpoint,
  method,
  params = null,
  body = null,
  onSuccess = null
) {
  let headers = null;
  if (method === "POST" || method === "PUT") {
    headers = { "Content-type": "application/json" };
  }
  let uri = endpoint;
  uri += !!params ? `?${params}` : "";
  axios({
    method: method,
    url: uri,
    headers: headers,
    data: body,
  })
    .then((response) => {
      if (!response.status === 200) {
        throw new Error("Failed to fetch.");
      }
      return response;
    })
    .then((data) => {
      console.log(data);
    })
    .catch((err) => {});
}
