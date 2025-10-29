// import React, { useEffect, useState, useMemo } from "react";
// import { apiGet, apiPost } from "../utils/api";

// export default function RegistrationMultiForm({ trainingId, onDone }) {
//   const [persons, setPersons]         = useState([]);
//   const [selectedIds, setSelectedIds] = useState([]);
//   const [payment, setPayment]         = useState("");
//   const [note, setNote]               = useState("");
//   const [error, setError]             = useState("");

//   // Načíst všechny osoby
//   useEffect(() => {
//     apiGet("/api/people")
//       .then(setPersons)
//       .catch(err => setError(err.toString()));
//   }, []);

//   // Filtrovaný seznam pro checkboxy
//   const filtered = useMemo(() => {
//     const term = payment; // nebo jiný stav filtru
//     return persons;       // případně filtrování podle jména
//   }, [persons, payment]);

//   const toggleOne = id =>
//     setSelectedIds(ids =>
//       ids.includes(id) ? ids.filter(x => x !== id) : [...ids, id]
//     );

//   const handleSubmit = async e => {
//     e.preventDefault();
//     if (!selectedIds.length || !payment) {
//       setError("Vyberte aspoň jednu osobu a zadejte částku.");
//       return;
//     }

//     try {
//       const amt = parseFloat(payment);
//       await Promise.all(
//         selectedIds.map(pid =>
//           apiPost("/api/registrations", {
//             trainingId: parseInt(trainingId, 10),
//             personId: pid,
//             payment: amt,
//             note
//           })
//         )
//       );
//       setSelectedIds([]);
//       setPayment("");
//       setNote("");
//       setError("");
//       onDone();
//     } catch (err) {
//       setError(err.toString());
//     }
//   };

//   return (
//     <form onSubmit={handleSubmit} className="mb-4">
//       {/* Zde doplňte filtr/osoby jako seznam checkboxů */}
//       {/* … podle předchozí ukázky … */}

//       <div className="mb-3">
//         <label>Částka (Kč)</label>
//         <input
//           type="number"
//           step="0.01"
//           className="form-control text-end"
//           value={payment}
//           onChange={e => setPayment(e.target.value)}
//           required
//         />
//       </div>
//       <div className="mb-3">
//         <label>Poznámka</label>
//         <input
//           type="text"
//           className="form-control"
//           value={note}
//           onChange={e => setNote(e.target.value)}
//         />
//       </div>
//       {error && <div className="alert alert-danger">{error}</div>}
//       <button type="submit" className="btn btn-success">
//         Přihlásit vybrané
//       </button>
//     </form>
//   );
// }
