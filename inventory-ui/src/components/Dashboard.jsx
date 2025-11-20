import React from "react";

export default function Dashboard({ locations = [], items = [] }) {
 
  const uniqueWarehouses = Array.from(
    new Set(locations.map((l) => l.warehouse).filter(Boolean))
  );
  const totalWarehouses = uniqueWarehouses.length;
  const totalLocations = locations.length;
  const totalItems = items.length;

  const totalOnHand = items.reduce(
    (sum, item) => sum + (item.onHandQuantity || 0),
    0
  );

  
  const itemsPerWarehouse = uniqueWarehouses.map((wh) => {
    const count = items.filter((item) => {
      const loc = locations.find((l) => l.id === item.locationId);
      return loc && loc.warehouse === wh;
    }).length;

    const qty = items.reduce((sum, item) => {
      const loc = locations.find((l) => l.id === item.locationId);
      if (loc && loc.warehouse === wh) {
        return sum + (item.onHandQuantity || 0);
      }
      return sum;
    }, 0);

    return { warehouse: wh, itemCount: count, onHand: qty };
  });

  return (
    <div className="dashboard">
      
      <div
        style={{
          display: "grid",
          gridTemplateColumns: "repeat(auto-fit, minmax(180px, 1fr))",
          gap: "16px",
          marginBottom: "24px",
        }}
      >
        <StatCard label="Warehouses" value={totalWarehouses} />
        <StatCard label="Locations" value={totalLocations} />
        <StatCard label="Items" value={totalItems} />
        <StatCard label="Total On-Hand Qty" value={totalOnHand} />
      </div>

      
      <section>
        <h2 style={{ marginBottom: 12 }}>Items per Warehouse</h2>
        <div
          style={{
            overflowX: "auto",
            borderRadius: 10,
            border: "1px solid #2b2b2b",
            background: "#141414",
          }}
        >
          <table
            style={{
              width: "100%",
              borderCollapse: "collapse",
              color: "#eaeaea",
            }}
          >
            <thead style={{ background: "#1d1d1d" }}>
              <tr>
                <th
                  style={{
                    textAlign: "left",
                    padding: "10px 14px",
                    fontWeight: 600,
                  }}
                >
                  Warehouse
                </th>
                <th
                  style={{
                    textAlign: "left",
                    padding: "10px 14px",
                    fontWeight: 600,
                  }}
                >
                  # of Items
                </th>
                <th
                  style={{
                    textAlign: "left",
                    padding: "10px 14px",
                    fontWeight: 600,
                  }}
                >
                  Total On-Hand Qty
                </th>
              </tr>
            </thead>
            <tbody>
              {itemsPerWarehouse.length === 0 ? (
                <tr>
                  <td
                    colSpan={3}
                    style={{
                      padding: 20,
                      textAlign: "center",
                      color: "#888",
                    }}
                  >
                    No data yet
                  </td>
                </tr>
              ) : (
                itemsPerWarehouse.map((row) => (
                  <tr key={row.warehouse} style={{ borderTop: "1px solid #262626" }}>
                    <td style={{ padding: "10px 14px" }}>{row.warehouse}</td>
                    <td style={{ padding: "10px 14px" }}>{row.itemCount}</td>
                    <td style={{ padding: "10px 14px" }}>{row.onHand}</td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      </section>
    </div>
  );
}

function StatCard({ label, value }) {
  return (
    <div
      style={{
        padding: "16px 18px",
        borderRadius: 10,
        background: "#141414",
        border: "1px solid #2b2b2b",
      }}
    >
      <div style={{ fontSize: 12, color: "#aaa", marginBottom: 4 }}>{label}</div>
      <div style={{ fontSize: 24, fontWeight: 600 }}>{value}</div>
    </div>
  );
}
