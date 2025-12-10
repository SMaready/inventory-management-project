// App.jsx
import React, { useEffect, useState } from "react";
import Sidebar from "./components/Sidebar";
import LocationsTable from "./components/LocationTables";
import ItemsTable from "./components/ItemsTable";
import Dashboard from "./components/Dashboard";
import UpdateItem from "./components/UpdateItem";
import CreateItem from "./components/CreateItem";
import Reports from "./components/Report";

import "./App.css";

export default function App() {
  const [status, setStatus] = useState("idle"); // idle | loading | ok | error
  const [err, setErr] = useState("");

  const [locations, setLocations] = useState([]);
  const [items, setItems] = useState([]);

  const [selectedPage, setSelectedPage] = useState("dashboard"); 
  const [searchQuery, setSearchQuery] = useState("");
  const [collapsed, setCollapsed] = useState(false);

 
  const [selectedItemId, setSelectedItemId] = useState(null);

  //
  // Load Locations
  //
  useEffect(() => {
    setStatus("loading");
    setErr("");

    fetch("/api/locations")
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

  //
  // Load Items
  
  useEffect(() => {
    fetch("/api/inventory")
      .then((r) => {
        if (!r.ok) throw new Error(`${r.status} ${r.statusText}`);
        return r.json();
      })
      .then((data) => {
        
        console.log("Loaded items:", data);
        setItems(Array.isArray(data) ? data : []);
      })
      .catch((e) => {
        console.warn("Could not load items:", e.message || e);
      });
  }, []);

  //
  // Filtering
  //
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

  //
  // Page Title
  //
  const pageTitle =
    selectedPage === "items"
      ? "Items"
      : selectedPage === "updateItem"
      ? "Edit Item"
      : selectedPage === "dashboard"
      ? "Dashboard"
      : selectedPage === "reports"
      ? "Reports"
      : "Inventory";

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

        {/* Search Bar */}
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
  <>
    <button
      style={{
        padding: "10px 16px",
        marginBottom: "12px",
        background: "#333",
        color: "white",
        border: "1px solid #555",
        borderRadius: 6,
        cursor: "pointer",
      }}
      onClick={() => setSelectedPage("createItem")}
    >
      + Add New Item
    </button>

    <ItemsTable
      rows={filteredItems}
      locations={locations}
      onEditItem={(id) => {
        setSelectedItemId(id);
        setSelectedPage("updateItem");
      }}
      onDeleteItem={async (sku, name) => {
        const ok = window.confirm(
          `Delete this item?\n\nSKU: ${sku}\nName: ${name}`
        );
        if (!ok) return;

        try {
          const res = await fetch(
            `/api/inventory/${encodeURIComponent(sku)}`,
            {
              method: "DELETE",
            }
          );

          if (!res.ok) {
            alert("Failed to delete item.");
            return;
          }

          
          setItems((prev) => prev.filter((item) => item.sku !== sku));
        } catch (e) {
          console.error("Error deleting item:", e);
          alert("Error deleting item.");
        }
      }}
    />
  </>
)}


  {selectedPage === "dashboard" && (
    <Dashboard locations={locations} items={items} />
  )}

  {selectedPage === "updateItem" && (
    <UpdateItem
      id={selectedItemId}
      onDone={() => setSelectedPage("items")}
    />
  )}

  {selectedPage === "createItem" && (
    <CreateItem onDone={() => setSelectedPage("items")} />
  )}

{selectedPage === "reports" && (
  <Reports items={items} locations={locations} />
)}

</section>

      </main>
    </div>
  );
}
