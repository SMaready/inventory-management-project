import React from "react";

export default function Reports({ items = [], locations = [] }) {
  
  const totalSkus = items.length;
  const totalOnHand = items.reduce(
    (sum, item) => sum + (item.onHandQuantity ?? 0),
    0
  );
  const totalReserved = items.reduce(
    (sum, item) => sum + (item.reservedQuantity ?? 0),
    0
  );

  const distinctWarehouses = Array.from(
    new Set(
      locations
        .map((loc) => loc.warehouse)
        .filter((w) => w && typeof w === "string")
    )
  ).length;

  //Most quantity
  const topItems = [...items]
    .sort((a, b) => (b.onHandQuantity ?? 0) - (a.onHandQuantity ?? 0))
    .slice(0, 5);

  //low stock items
  const lowStockThreshold = 5;
  const lowStockItems = items.filter(
    (item) => (item.onHandQuantity ?? 0) <= lowStockThreshold
  );

  //sorted by warehouse
  const inventoryByWarehouseMap = new Map();

  items.forEach((item) => {
    const loc = locations.find((l) => l.id === item.locationId);
    const warehouseName = loc?.warehouse ?? "Unknown";

    const current = inventoryByWarehouseMap.get(warehouseName) ?? {
      totalQuantity: 0,
      itemCount: 0,
    };

    current.totalQuantity += item.onHandQuantity ?? 0;
    current.itemCount += 1;

    inventoryByWarehouseMap.set(warehouseName, current);
  });

  const inventoryByWarehouse = Array.from(
    inventoryByWarehouseMap.entries()
  ).map(([warehouse, stats]) => ({
    warehouse,
    ...stats,
  }));

  return (
    <div style={{ display: "flex", flexDirection: "column", gap: "20px" }}>
      {/* SUMMARY CARDS */}
      <section
        style={{
          display: "grid",
          gridTemplateColumns: "repeat(auto-fit, minmax(180px, 1fr))",
          gap: "16px",
        }}
      >
        <SummaryCard label="Total SKUs" value={totalSkus} />
        <SummaryCard label="Total On-Hand Quantity" value={totalOnHand} />
        <SummaryCard label="Total Reserved" value={totalReserved} />
        <SummaryCard label="Distinct Warehouses" value={distinctWarehouses} />
      </section>

    
      <section
        style={{
          borderRadius: 10,
          border: "1px solid #2b2b2b",
          background: "#141414",
          padding: "16px 18px",
        }}
      >
        <h3 style={{ marginTop: 0, marginBottom: 12 }}>Top 5 Most Stocked Items</h3>
        {topItems.length === 0 ? (
          <div style={{ color: "#888" }}>No items available.</div>
        ) : (
          <table
            style={{
              width: "100%",
              borderCollapse: "collapse",
              color: "#eaeaea",
              fontSize: 14,
            }}
          >
            <thead style={{ background: "#1d1d1d" }}>
              <tr>
                <th style={thStyle}>SKU</th>
                <th style={thStyle}>Name</th>
                <th style={thStyle}>On Hand</th>
                <th style={thStyle}>Location</th>
              </tr>
            </thead>
            <tbody>
              {topItems.map((item) => {
                const loc = locations.find((l) => l.id === item.locationId);
                const locLabel = loc
                  ? `${loc.warehouse} / ${loc.aisle} / ${loc.shelf} / ${loc.bin}`
                  : "-";

                return (
                  <tr key={item.id} style={{ borderTop: "1px solid #262626" }}>
                    <td style={tdStyle}>{item.sku}</td>
                    <td style={tdStyle}>{item.name}</td>
                    <td style={tdStyle}>{item.onHandQuantity ?? 0}</td>
                    <td style={tdStyle}>{locLabel}</td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        )}
      </section>

  
      <section
        style={{
          borderRadius: 10,
          border: "1px solid #2b2b2b",
          background: "#141414",
          padding: "16px 18px",
        }}
      >
        <h3 style={{ marginTop: 0, marginBottom: 12 }}>
          Low Stock Alerts (≤ {lowStockThreshold})
        </h3>
        {lowStockItems.length === 0 ? (
          <div style={{ color: "#4caf50" }}>No low stock items. All good!</div>
        ) : (
          <table
            style={{
              width: "100%",
              borderCollapse: "collapse",
              color: "#eaeaea",
              fontSize: 14,
            }}
          >
            <thead style={{ background: "#1d1d1d" }}>
              <tr>
                <th style={thStyle}>SKU</th>
                <th style={thStyle}>Name</th>
                <th style={thStyle}>On Hand</th>
                <th style={thStyle}>Location</th>
              </tr>
            </thead>
            <tbody>
              {lowStockItems.map((item) => {
                const loc = locations.find((l) => l.id === item.locationId);
                const locLabel = loc
                  ? `${loc.warehouse} / ${loc.aisle} / ${loc.shelf} / ${loc.bin}`
                  : "-";

                return (
                  <tr key={item.id} style={{ borderTop: "1px solid #262626" }}>
                    <td style={tdStyle}>{item.sku}</td>
                    <td style={tdStyle}>{item.name}</td>
                    <td style={tdStyle}>{item.onHandQuantity ?? 0}</td>
                    <td style={tdStyle}>{locLabel}</td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        )}
      </section>

      {/* INVENTORY BY WAREHOUSE */}
      <section
        style={{
          borderRadius: 10,
          border: "1px solid #2b2b2b",
          background: "#141414",
          padding: "16px 18px",
        }}
      >
        <h3 style={{ marginTop: 0, marginBottom: 12 }}>Inventory by Warehouse</h3>
        {inventoryByWarehouse.length === 0 ? (
          <div style={{ color: "#888" }}>No inventory data.</div>
        ) : (
          <table
            style={{
              width: "100%",
              borderCollapse: "collapse",
              color: "#eaeaea",
              fontSize: 14,
            }}
          >
            <thead style={{ background: "#1d1d1d" }}>
              <tr>
                <th style={thStyle}>Warehouse</th>
                <th style={thStyle}>Total Quantity</th>
                <th style={thStyle}>Item Count</th>
              </tr>
            </thead>
            <tbody>
              {inventoryByWarehouse.map((row) => (
                <tr key={row.warehouse} style={{ borderTop: "1px solid #262626" }}>
                  <td style={tdStyle}>{row.warehouse}</td>
                  <td style={tdStyle}>{row.totalQuantity}</td>
                  <td style={tdStyle}>{row.itemCount}</td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </section>
    </div>
  );
}

function SummaryCard({ label, value }) {
  return (
    <div
      style={{
        borderRadius: 10,
        border: "1px solid #2b2b2b",
        background: "#141414",
        padding: "14px 16px",
        display: "flex",
        flexDirection: "column",
        gap: 6,
      }}
    >
      <div style={{ fontSize: 13, color: "#aaa" }}>{label}</div>
      <div style={{ fontSize: 20, fontWeight: 600 }}>{value}</div>
    </div>
  );
}

const thStyle = {
  textAlign: "left",
  padding: "8px 10px",
  fontWeight: 600,
};

const tdStyle = {
  padding: "8px 10px",
};
