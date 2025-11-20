import React from "react";

function fmtDate(iso) {
    if (!iso) return "-";
    const d = new Date(iso);
    return isNaN(d) ? iso : d.toLocaleString();
  }
  
  const typeLabel = (t) => {
   
    return t === 0 ? "Storage" : String(t);
  };
  
  export default function LocationsTable({ rows = [] }) {
    const columns = [
      { key: "warehouse", label: "Warehouse" },
      { key: "aisle",     label: "Aisle" },
      { key: "shelf",     label: "Shelf" },
      { key: "bin",       label: "Bin" },
      { key: "type",      label: "Type" },
      { key: "id",        label: "ID" },
      { key: "createdOn", label: "Created" },
      { key: "createdBy", label: "Created By" },
    ];
  
    return (
      <div style={{ overflowX: "auto", borderRadius: 10, border: "1px solid #2b2b2b", background: "#141414" }}>
        <table style={{ width: "100%", borderCollapse: "collapse", color: "#eaeaea" }}>
          <thead style={{ background: "#1d1d1d" }}>
            <tr>
              {columns.map(c => (
                <th key={c.key} style={{ textAlign: "left", padding: "12px 14px", fontWeight: 600 }}>
                  {c.label}
                </th>
              ))}
            </tr>
          </thead>
          <tbody>
            {rows.length === 0 ? (
              <tr>
                <td colSpan={columns.length} style={{ padding: 20, textAlign: "center", color: "#888" }}>
                  No locations yet
                </td>
              </tr>
            ) : rows.map((r, i) => (
              <tr key={r.id ?? i} style={{ borderTop: "1px solid #262626" }}>
                <td style={{ padding: "10px 14px" }}>{r.warehouse ?? "-"}</td>
                <td style={{ padding: "10px 14px" }}>{r.aisle ?? "-"}</td>
                <td style={{ padding: "10px 14px" }}>{r.shelf ?? "-"}</td>
                <td style={{ padding: "10px 14px" }}>{r.bin ?? "-"}</td>
                <td style={{ padding: "10px 14px" }}>{typeLabel(r.type)}</td>
                <td style={{ padding: "10px 14px" }}>{r.id ?? "-"}</td>
                <td style={{ padding: "10px 14px" }}>{fmtDate(r.createdOn)}</td>
                <td style={{ padding: "10px 14px" }}>{r.createdBy ?? "-"}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
  }
  