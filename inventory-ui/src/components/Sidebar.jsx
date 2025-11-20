import React from "react";
import "./Sidebar.css";

export default function Sidebar({
  collapsed,
  setCollapsed,
  selectedPage,
  onSelectPage,
}) {
  return (
    <aside className={`sidebar ${collapsed ? "is-collapsed" : ""}`}>
      <div className="brand">
        <div className="brand-left">
          <div className="brand-icon">I</div>
          <div className="brand-text">Inventory</div>
        </div>
        <button
          className="collapse-btn"
          onClick={() => setCollapsed(!collapsed)}
          aria-label={collapsed ? "Expand sidebar" : "Collapse sidebar"}
          title={collapsed ? "Expand" : "Collapse"}
        >
          {collapsed ? "›" : "‹"}
        </button>
      </div>

      <nav>
        <ul>
          <li className={selectedPage === "dashboard" ? "active" : ""}>
            <button type="button" onClick={() => onSelectPage("dashboard")}>
              <span className="link-icon">🏠</span>
              <span className="link-text">Dashboard</span>
            </button>
          </li>
          <li className={selectedPage === "items" ? "active" : ""}>
            <button type="button" onClick={() => onSelectPage("items")}>
              <span className="link-icon">📦</span>
              <span className="link-text">Items</span>
            </button>
          </li>
          <li className={selectedPage === "locations" ? "active" : ""}>
            <button type="button" onClick={() => onSelectPage("locations")}>
              <span className="link-icon">📍</span>
              <span className="link-text">Locations</span>
            </button>
          </li>
          <li className={selectedPage === "reports" ? "active" : ""}>
            <button type="button" onClick={() => onSelectPage("reports")}>
              <span className="link-icon">📊</span>
              <span className="link-text">Reports</span>
            </button>
          </li>
        </ul>
      </nav>
    </aside>
  );
}
