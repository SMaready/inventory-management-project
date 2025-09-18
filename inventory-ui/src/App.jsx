import { useEffect, useState } from "react";

export default function App() {
  const [items, setItems] = useState([]);
  const [err, setErr] = useState("");


  //proxy server setting for this can be edited in vite.config.js file 
  //Target can be changed
  useEffect(() => {
    fetch("/weatherforecast") //Test enpoint
      .then(r => { if (!r.ok) throw new Error(`${r.status} ${r.statusText}`); return r.json(); })
      .then(data => setItems(data))
      .catch(e => setErr(e.message));
  }, []);



}
