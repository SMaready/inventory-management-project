import React, { useEffect, useState } from "react";
import Sidebar from "./components/Sidebar";
import "./App.css";

export default function App() {
  const [items, setItems] = useState([]);
  const [err, setErr] = useState("");
  const [collapsed, setCollapsed] = useState(false);

  //proxy server setting for this can be edited in vite.config.js file
  //Target can be changed
  useEffect(() => {
    fetch("/weatherforecast") //Test enpoint
      .then((r) => {
        if (!r.ok) throw new Error(`${r.status} ${r.statusText}`);
        return r.json();
      })
      .then((data) => setItems(data))
      .catch((e) => setErr(e.message));
  }, []);

  return (
    <div className={`app-layout ${collapsed ? "collapsed" : ""}`}>
      <Sidebar collapsed={collapsed} setCollapsed={setCollapsed} />
      <main className="app-main">
        <header className="app-header">
          <h1>Inventory</h1>
        </header>

        {err && <div className="error">Error: {err}</div>}

        <section className="content">
          <h2>Data</h2>
          <pre>{JSON.stringify(items, null, 2)}</pre>
        </section>
      </main>
    </div>
  );
}
