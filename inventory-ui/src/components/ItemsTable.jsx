import React from "react";

function fmtDate(iso) {
  if (!iso) return "-";
  const d = new Date(iso);
  return isNaN(d) ? iso : d.toLocaleString();
}

const statusLabel = (s) => {
  if (s === 0 || s === "New") return "New";
  return String(s ?? "-");
};

export default function ItemsTable({
  rows = [],
  locations = [],
  onEditItem,
  onDeleteItem,
}) {
  const columns = [
    { key: "sku", label: "SKU" },
    { key: "name", label: "Name" },
    { key: "status", label: "Status" },
    { key: "onHandQuantity", label: "On Hand" },
    { key: "reservedQuantity", label: "Reserved" },
    { key: "damagedQuantity", label: "Damaged" },
    { key: "location", label: "Location" },
    { key: "id", label: "ID" },
    { key: "createdOn", label: "Created" },
    { key: "createdBy", label: "Created By" },
    { key: "actions", label: "Actions" },
  ];

  const locationsById = Object.fromEntries(
    locations.map((loc) => [loc.id, loc])
  );

  return (
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
            {columns.map((c) => (
              <th
                key={c.key}
                style={{
                  textAlign: "left",
                  padding: "12px 14px",
                  fontWeight: 600,
                }}
              >
                {c.label}
              </th>
            ))}
          </tr>
        </thead>

        <tbody>
          {rows.length === 0 ? (
            <tr>
              <td
                colSpan={columns.length}
                style={{
                  padding: 20,
                  textAlign: "center",
                  color: "#888",
                }}
              >
                No items yet
              </td>
            </tr>
          ) : (
            rows.map((r, i) => {
              const loc = locationsById[r.locationId];
              const locLabel = loc
                ? `${loc.warehouse} / ${loc.aisle} / ${loc.shelf} / ${loc.bin}`
                : "-";

              return (
                <tr key={r.id ?? i} style={{ borderTop: "1px solid #262626" }}>
                  <td style={{ padding: "10px 14px" }}>{r.sku}</td>
                  <td style={{ padding: "10px 14px" }}>{r.name}</td>
                  <td style={{ padding: "10px 14px" }}>
                    {statusLabel(r.status)}
                  </td>
                  <td style={{ padding: "10px 14px" }}>{r.onHandQuantity}</td>
                  <td style={{ padding: "10px 14px" }}>{r.reservedQuantity}</td>
                  <td style={{ padding: "10px 14px" }}>
                    {r.damagedQuantity ?? 0}
                  </td>
                  <td style={{ padding: "10px 14px" }}>{locLabel}</td>
                  <td style={{ padding: "10px 14px" }}>{r.id}</td>
                  <td style={{ padding: "10px 14px" }}>
                    {fmtDate(r.createdOn)}
                  </td>
                  <td style={{ padding: "10px 14px" }}>{r.createdBy}</td>

                  {/* ACTIONS */}
                  <td style={{ padding: "10px 14px", whiteSpace: "nowrap" }}>
                    <button
                      style={{
                        padding: "6px 10px",
                        marginRight: "6px",
                        background: "#333",
                        color: "#fff",
                        border: "1px solid #555",
                        borderRadius: 6,
                        cursor: "pointer",
                      }}
                      onClick={() => onEditItem && onEditItem(r.id)}
                    >
                      Edit
                    </button>

                    <button
                      style={{
                        padding: "6px 10px",
                        background: "#7a1f1f",
                        color: "#fff",
                        border: "1px solid #aa3a3a",
                        borderRadius: 6,
                        cursor: "pointer",
                      }}
                      onClick={() =>
                        onDeleteItem && onDeleteItem(r.sku, r.name)
                      }
                    >
                      Delete
                    </button>
                  </td>
                </tr>
              );
            })
          )}
        </tbody>
      </table>
    </div>
  );
}
