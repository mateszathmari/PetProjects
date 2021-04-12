import logo from "./logo.svg";
import "./App.css";

function App() {
  const API_ID = "";
  const API_KEY = "";
  const EXAMPLEQUERRY = `https://api.edamam.com/search?q=chicken&app_id=${API_ID}&app_key=${API_KEY}`;

  const getInfo = () => {
    fetch(EXAMPLEQUERRY)
      .then((response) => {
        console.log(response);
      })
      .catch((err) => {
        console.error(err);
      });
  };

  getInfo();

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
