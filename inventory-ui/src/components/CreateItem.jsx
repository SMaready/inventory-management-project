import { useState } from "react";

const statusText = (value) => {
  if (value === 0) return "New";
  if (value === 1) return "Reserved";
  if (value === 2) return "Shipped";
  return String(value);
};

export default function CreateItem({ onDone }) {
  const [form, setForm] = useState({
    sku: "",
    name: "",
    description: "",
    onHandQuantity: 0,
    status: 0,
    locationId: 0
  });

  const handleSubmit = async (e) => {
    e.preventDefault();

    //Build confirmation message
    const summary = `
Please confirm the new inventory item:

SKU: ${form.sku}
Name: ${form.name}
Description: ${form.description}
On-hand Quantity: ${form.onHandQuantity}
Status: ${statusText(form.status)}
Location ID: ${form.locationId}
    `.trim();

    const ok = window.confirm(summary);
    if (!ok) return; //cancelling

    const response = await fetch("/api/inventory", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(form)
    });

    if (response.ok) {
      alert("Item created successfully.");
      onDone();
    } else {
      alert("Failed to create item");
    }
  };

  return (
    <div style={{ padding: "20px" }}>
      <h1>Add New Inventory Item</h1>

      <form
        onSubmit={handleSubmit}
        style={{ display: "flex", flexDirection: "column", gap: "14px", maxWidth: "400px" }}
      >
        <label>SKU</label>
        <input
          value={form.sku}
          onChange={(e) => setForm({ ...form, sku: e.target.value })}
        />

        <label>Name</label>
        <input
          value={form.name}
          onChange={(e) => setForm({ ...form, name: e.target.value })}
        />

        <label>Description</label>
        <input
          value={form.description}
          onChange={(e) => setForm({ ...form, description: e.target.value })}
        />

        <label>On Hand Quantity</label>
        <input
          type="number"
          value={form.onHandQuantity}
          onChange={(e) =>
            setForm({ ...form, onHandQuantity: parseInt(e.target.value || "0", 10) })
          }
        />

        <label>Status</label>
        <select
          value={form.status}
          onChange={(e) =>
            setForm({ ...form, status: parseInt(e.target.value, 10) })
          }
        >
          <option value={0}>New</option>
          <option value={1}>Reserved</option>
          <option value={2}>Shipped</option>
        </select>

        <label>Location ID</label>
        <input
          type="number"
          value={form.locationId}
          onChange={(e) =>
            setForm({ ...form, locationId: parseInt(e.target.value || "0", 10) })
          }
        />

        <button type="submit" style={{ padding: "10px", marginTop: "10px" }}>
          Add Item
        </button>

        <button
          type="button"
          onClick={onDone}
          style={{
            background: "#555",
            color: "white",
            padding: "10px",
            marginTop: "5px",
            border: "none",
            cursor: "pointer"
          }}
        >
          Cancel
        </button>
      </form>
    </div>
  );
}
