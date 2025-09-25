import React from "react";
import "./Sidebar.css";

export default function Sidebar({ collapsed, setCollapsed }) {
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
          <li>
            <a href="#">
              <span className="link-icon">🏠</span>
              <span className="link-text">Dashboard</span>
            </a>
          </li>
          <li>
            <a href="#">
              <span className="link-icon">📦</span>
              <span className="link-text">Items</span>
            </a>
          </li>
          <li>
            <a href="#">
              <span className="link-icon">📍</span>
              <span className="link-text">Locations</span>
            </a>
          </li>
          <li>
            <a href="#">
              <span className="link-icon">📊</span>
              <span className="link-text">Reports</span>
            </a>
          </li>
        </ul>
      </nav>
    </aside>
  );
}
