// App.jsx
import React, { useEffect, useState } from "react";
import Sidebar from "./components/Sidebar";
import LocationsTable from "./components/LocationTables";
import ItemsTable from "./components/ItemsTable"; 
import Dashboard from "./components/Dashboard";
import "./App.css";

export default function App() {
  const [status, setStatus] = useState("idle"); // idle | loading | ok | error
  const [err, setErr] = useState("");

 
  const [locations, setLocations] = useState([]);
  const [items, setItems] = useState([]);

  
  const [selectedPage, setSelectedPage] = useState("dashboard"); // "dashboard" | "items" | "locations" | "reports"

  
  const [searchQuery, setSearchQuery] = useState("");

  const [collapsed, setCollapsed] = useState(false);


  useEffect(() => {
    setStatus("loading");
    setErr("");

    const url = "/api/locations";
    fetch(url)
      .then((r) => {
        if (!r.ok) throw new Error(`${r.status} ${r.statusText}`);
        return r.json();
      })
      .then((data) => {
        const rows = Array.isArray(data?.items) ? data.items : data;
        setLocations(Array.isArray(rows) ? rows : []);
        setStatus("ok");
      })
      .catch((e) => {
        setErr(String(e.message || e));
        setStatus("error");
      });
  }, []);


  useEffect(() => {
    fetch("/api/items")
      .then((r) => {
        if (!r.ok) throw new Error(`${r.status} ${r.statusText}`);
        return r.json();
      })
      .then((data) => {
        const rows = Array.isArray(data?.items) ? data.items : data;
        setItems(Array.isArray(rows) ? rows : []);
      })
      .catch((e) => {
        console.warn("Could not load items:", e.message || e);
        // we just leave items as [] if this fails
      });
  }, []);

  // ---- Filtering for "Search Item / Location" use case ----
  const filteredLocations = locations.filter((loc) => {
    const text =
      `${loc.warehouse ?? ""} ` +
      `${loc.aisle ?? ""} ` +
      `${loc.shelf ?? ""} ` +
      `${loc.bin ?? ""} ` +
      `${loc.type ?? ""}`;
    return text.toLowerCase().includes(searchQuery.toLowerCase());
  });

  const filteredItems = items.filter((item) => {
    const text =
      `${item.sku ?? ""} ` +
      `${item.name ?? ""} ` +
      `${item.description ?? ""} ` +
      `${item.status ?? ""}`;
    return text.toLowerCase().includes(searchQuery.toLowerCase());
  });

  const pageTitle =
    selectedPage === "items"
      ? "Items"
      : selectedPage === "dashboard"
      ? "Dashboard"
      : selectedPage === "reports"
      ? "Reports"
      : "Inventory"; // locations

  return (
    <div className={`app-layout ${collapsed ? "collapsed" : ""}`}>
      <Sidebar
        collapsed={collapsed}
        setCollapsed={setCollapsed}
        selectedPage={selectedPage}
        onSelectPage={setSelectedPage}
      />

      <main className="app-main">
        <header className="app-header">
          <h1>{pageTitle}</h1>
        </header>

        {status === "loading" && (
          <div style={{ color: "#aaa" }}>⏳ Connecting…</div>
        )}
        {status === "ok" && (
          <div style={{ color: "#4caf50" }}>✅ Connected to API</div>
        )}
        {status === "error" && (
          <div style={{ color: "#f44336" }}>❌ Error: {err}</div>
        )}

        {/*  Search bar: maps to “Search Item / Location” in the diagrams */}
        <section className="toolbar">
          <input
            type="text"
            placeholder={
              selectedPage === "items"
                ? "Search by SKU / Name / Status"
                : "Search by warehouse / aisle / shelf / bin / type"
            }
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
          />
        </section>

        <section className="content">
          <h2>Data</h2>

          {selectedPage === "locations" && (
            <LocationsTable rows={filteredLocations} />
          )}

          {selectedPage === "items" && (
            <ItemsTable
              rows={filteredItems}
              locations={locations} // to show where each item is
            />
          )}

          {selectedPage === "dashboard" && (
            <Dashboard locations={locations} items={items}/>
          )}

          {selectedPage === "reports" && (
            <div style={{ color: "#aaa" }}>
              Reports placeholder (future work).
            </div>
          )}
        </section>
      </main>
    </div>
  );
}
