import React, { useEffect, useState } from "react";
import { apiGet, apiPost } from "../utils/api";

export default function RegistrationForm({ trainingId, onDone }) {
  const [persons, setPersons]     = useState([]);
  const [personId, setPersonId]   = useState("");
  const [payment, setPayment]     = useState("");
  const [note, setNote]           = useState("");
  const [error, setError]         = useState("");

  // Načíst seznam všech osob pro dropdown
  useEffect(() => {
    apiGet("/api/people")
      .then(setPersons)
      .catch(err => setError(err.toString()));
  }, []);

  const handleSubmit = async e => {
    e.preventDefault();
    if (!personId) {
      setError("Vyberte osobu.");
      return;
    }

    try {
      await apiPost("/api/registrations", {
        trainingId: parseInt(trainingId, 10),
        personId:   parseInt(personId, 10),
        payment:    parseFloat(payment) || 0,
        note
      });

      // vyčistit formulář a reload detailu
      setPersonId("");
      setPayment("");
      setNote("");
      setError("");
      onDone();
    } catch (err) {
      setError(err.toString());
    }
  };

  return (
    <form onSubmit={handleSubmit} className="mb-4 row g-3">
      {/* Výběr osoby */}
      <div className="col-md-4">
        <label className="form-label">Osoba</label>
        <select
          className="form-select"
          value={personId}
          onChange={e => setPersonId(e.target.value)}
          required
        >
          <option value="">-- vyber --</option>
          {persons.map(p => (
            <option key={p.id} value={p.id}>
              {p.name} ({p.identificationNumber})
            </option>
          ))}
        </select>
      </div>

      {/* Částka platby */}
      <div className="col-md-4">
        <label className="form-label">Částka (Kč)</label>
        <input
          type="number"
          step="0.01"
          className="form-control text-end"
          value={payment}
          onChange={e => setPayment(e.target.value)}
          required
        />
      </div>

      {/* Poznámka */}
      <div className="col-md-4">
        <label className="form-label">Poznámka</label>
        <input
          type="text"
          className="form-control"
          value={note}
          onChange={e => setNote(e.target.value)}
          placeholder="volitelně"
        />
      </div>

      {/* Chyba */}
      {error && (
        <div className="col-12 alert alert-danger">
          {error}
        </div>
      )}

      {/* Tlačítko */}
      <div className="col-12">
        <button type="submit" className="btn btn-success">
          Přidat platbu
        </button>
      </div>
    </form>
  );
}




