import React, { useEffect, useState } from "react";
import { useNavigate, useParams, Link } from "react-router-dom";
import ParticipantMultiSelect from "./ParticipantMultiSelect";
import { apiGet, apiPost, apiPut } from "../utils/api";
import { toast } from "react-toastify"; // přidej na začátek souboru


export default function ExpenseForm() {
  const { id }      = useParams();
  const isEdit      = Boolean(id);
  const navigate    = useNavigate();

  const [type, setType]                     = useState("");
  const [totalAmount, setTotalAmount]       = useState("");
  const [persons, setPersons]               = useState([]);
  const [participantIds, setParticipantIds] = useState([]);
  const [error, setError]                   = useState("");

  // Načíst všechny osoby pro multi-select
  useEffect(() => {
    apiGet("/api/people")
      .then(setPersons)
      .catch(err => setError(err.toString()));
  }, []);

  // Při editaci načteme taky stávající podíly
  useEffect(() => {
    if (!isEdit) return;
    apiGet(`/api/expenses/${id}/detail`)
      .then(d => {
        setType(d.expense.type);
        setTotalAmount(d.expense.totalAmount.toString());
        setParticipantIds(d.participantShares.map(s => s.personId));
      })
      .catch(err => setError(err.toString()));
  }, [id, isEdit]);

  const handleSubmit = async e => {
    e.preventDefault();
    setError("");

    try {
      let expId = id;

      if (isEdit) {
        // 1) Jen PUT /api/expenses bez participantIds
        await apiPut(`/api/expenses/${id}`, {
          type,
          totalAmount: parseFloat(totalAmount)
        });
      } else {
        // 1) POST /api/expenses bez participantIds
        const created = await apiPost("/api/expenses", {
          type,
          totalAmount: parseFloat(totalAmount)
        });
        expId = created.id;
        toast.success("Výdaj byl úspěšně vytvořen.");
      }

      // 2) Manuální přidání participantů (jednou) – backend to zpracuje a vytvoří záporné transakce
      if (participantIds.length) {
        const share = parseFloat(totalAmount) / participantIds.length;
        await Promise.all(
          participantIds.map(pid =>
            apiPost(`/api/expenses/${expId}/participants`, {
              personId:    pid,
              shareAmount: parseFloat(share.toFixed(2))
            })
          )
        );
      }

      // 3) Přesměrujeme na detail první osoby, kde se hned projeví nový zůstatek
      if (participantIds.length) {
        // navigate(`/persons/show/${participantIds[0]}`);
        navigate("/expenses");
      } 
    } catch (err) {
      setError(err.toString());
    }
  };

  return (
    <div className="container mt-4">
      <h3>{isEdit ? "Upravit výdaj" : "Nový výdaj"}</h3>
      {error && <div className="alert alert-danger mb-3">Chyba: {error}</div>}

      <form onSubmit={handleSubmit} className="row g-3">
        <div className="col-md-6">
          <label className="form-label">Typ výdaje</label>
          <input
            type="text"
            className="form-control"
            value={type}
            onChange={e => setType(e.target.value)}
            required
          />
        </div>

        <div className="col-md-6">
          <label className="form-label">Celková částka</label>
          <input
            type="number"
            className="form-control text-end"
            value={totalAmount}
            onChange={e => setTotalAmount(e.target.value)}
            required
            min="0"
            step="0.01"
          />
        </div>

        <div className="col-12">
          <label className="form-label">Účastníci</label>
          <ParticipantMultiSelect
            persons={persons}
            selectedIds={participantIds}
            onChange={setParticipantIds}
          />
        </div>

        <div className="col-12 d-flex gap-2 expense-form-actions">
          <Link to="/expenses" className="btn btn-secondary">
            Zpět na seznam
          </Link>
          <button type="submit" className="btn btn-primary">
            {isEdit ? "Uložit změny" : "Vytvořit výdaj"}
          </button>
          
        </div>
      </form>
    </div>
  );
}





