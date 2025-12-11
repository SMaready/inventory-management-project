import { useEffect, useState } from "react";

export default function UpdateItem({ id, onDone }) {
  const [form, setForm] = useState({
    sku: "",
    name: "",
    description: "",
    onHandQuantity: 0,
    status: 0,
    locationId: 0
  });

  //Load the existing item by ID
  useEffect(() => {
    if (!id) return;

    fetch(`/api/inventory/${id}`)
      .then(res => res.json())
      .then(data => {
        setForm({
          sku: data.sku,
          name: data.name,
          description: data.description,
          onHandQuantity: data.onHandQuantity,
          status: data.status,
          locationId: data.locationId
        });
      })
      .catch(err => {
        console.error("Error loading item:", err);
      });
  }, [id]);

  // Submit update
  const handleSubmit = async (e) => {
    e.preventDefault();

    const response = await fetch("/api/inventory", {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(form)
    });

    if (response.ok) {
      onDone();  // Go back to items page
    } else {
      alert("Update failed");
    }
  };

  return (
    <div style={{ padding: "20px" }}>
      <h1>Edit Inventory Item</h1>

      <form onSubmit={handleSubmit} style={{ display: "flex", flexDirection: "column", gap: "14px", maxWidth: "400px" }}>
        
        <label>SKU</label>
        <input
          value={form.sku}
          onChange={e => setForm({ ...form, sku: e.target.value })}
        />

        <label>Name</label>
        <input
          value={form.name}
          onChange={e => setForm({ ...form, name: e.target.value })}
        />

        <label>Description</label>
        <input
          value={form.description}
          onChange={e => setForm({ ...form, description: e.target.value })}
        />

        <label>On Hand Quantity</label>
        <input
          type="number"
          value={form.onHandQuantity}
          onChange={e => setForm({ ...form, onHandQuantity: parseInt(e.target.value) })}
        />

        <label>Status</label>
        <select
          value={form.status}
          onChange={e => setForm({ ...form, status: parseInt(e.target.value) })}
        >
          <option value={0}>New</option>
          <option value={1}>Reserved</option>
          <option value={2}>Shipped</option>
        </select>

        <label>Location ID</label>
        <input
          type="number"
          value={form.locationId}
          onChange={e => setForm({ ...form, locationId: parseInt(e.target.value) })}
        />

        <button type="submit" style={{ padding: "10px", marginTop: "10px" }}>
          Save Changes
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
