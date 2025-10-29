// import React, { useEffect, useState } from "react";
// import { apiDelete, apiGet } from "../utils/api";
// import PersonTable from "./PersonTable";
// import "../styles/PersonStyle.css";
// import { authorization } from "../utils/authorization";
// import { useAuth } from './AuthGate';

// export default function PersonIndex() {
//   const [persons, setPersons] = useState([]);
//   const [inputLimit, setInputLimit] = useState("");
//   const [limit, setLimit] = useState(0);

//   useEffect(() => {
//     apiGet("/api/people").then(data => setPersons(data));
//   }, []);

//   const deletePerson = async (id) => {
//     await apiDelete("/api/people/" + id);
//     setPersons(ps => ps.filter(p => p.id !== id));
//   };

//   const createPerson = () => {
//     window.location.href = "/persons/create";
//   };

//   const applyFilter = () => {
//     const n = parseInt(inputLimit, 10);
//     setLimit(isNaN(n) || n < 0 ? 0 : n);
//   };

//   const resetFilter = () => {
//     setInputLimit("");
//     setLimit(0);
//   };

//   const displayed = limit > 0 ? persons.slice(0, limit) : persons;

//   return (
//     <div className="card shadow-sm">
//       <div className="card-body">
//         <h1 className="card-title">Seznam osob</h1>

//         {/* Filtr */}
//         <div className="filter-controls">
//           <input
//             type="number"
//             min="0"
//             className="form-control"
//             placeholder="Max. počet"
//             value={inputLimit}
//             onChange={e => setInputLimit(e.target.value)}
//           />

//           {/* Tlačítka filtrovat/obnovit */}
//           <div className="person-filter-buttons">
//             <button className="btn btn-primary" onClick={applyFilter}>
//               Filtrovat
//             </button>
//             <button className="btn btn-secondary" onClick={resetFilter}>
//               Obnovit
//             </button>
//           </div>
//         </div>

//         {/* Tabulka osob */}
//         <PersonTable deletePerson={deletePerson} items={displayed} />

//         {/* Přidat osobu */}
//         {/* <button
//           className="btn btn-success mt-3 person-add-btn"
//           onClick={createPerson}
//         >
//           Přidat novou osobu
//         </button> */}
//          {/* Přidat osobu (vyžaduj heslo) */}
//           <button
//           className="btn btn-success mt-3 person-add-btn"
//           onClick={() =>
//             authorization(() => {
//               window.location.href = "/persons/create";
//             }).catch(() => {
//               // uživatel zrušil/neuspěl
//             })
//           }
//         >
//           Přidat novou osobu
//         </button>
//       </div>
//     </div>
//   );
// }




import React, { useEffect, useState } from "react";
import { apiDelete, apiGet }      from "../utils/api";
import PersonTable               from "./PersonTable";
import "../styles/PersonStyle.css";
import { authorization }         from "../utils/authorization";
import { useAuth }               from "../components/AuthGate";
import { toast } from "react-toastify";
import { confirmAlert } from 'react-confirm-alert';
import 'react-confirm-alert/src/react-confirm-alert.css';

export default function PersonIndex() {
  const { role, logout }            = useAuth();
  const [persons, setPersons]       = useState([]);
  const [inputLimit, setInputLimit] = useState("");
  const [limit, setLimit]           = useState(0);

  useEffect(() => {
    apiGet("/api/people").then(setPersons);
  }, []);

 const deletePerson = async (id) => {
  if (role !== "admin") {
    toast.error("Nemáte oprávnění smazat osobu.");
    return;
  }

  confirmAlert({
    title: 'Potvrdit smazání',
    message: 'Opravdu smazat tuto osobu?',
    buttons: [
      {
        label: 'Ano, smazat',
        onClick: async () => {
          try {
            await apiDelete(`/api/people/${id}`);
            setPersons(ps => ps.filter(p => p.id !== id));
            toast.success("Osoba byla smazána.");
          } catch (err) {
            console.error(err);
            toast.error("Chyba při mazání osoby.");
            if (err.message) setError(err.toString());
          }
        }
      },
      { label: 'Zrušit' }
    ]
  });
};

const createPerson = () => {
  if (role !== "admin") {
    toast.error("Nemáte oprávnění přidat novou osobu.");
    return;
  }
  confirmAlert({
    title: 'Vytvořit osobu',
    message: 'Chcete vytvořit novou osobu?',
    buttons: [
      {
        label: 'Ano',
        onClick: () => {
          // použijeme routu (pokud chceš použít authorization(), dej ho sem)
          window.location.href = "/persons/create";
        }
      },
      { label: 'Zrušit' }
    ]
  });
};

  const applyFilter = () => {
    const n = parseInt(inputLimit, 10);
    setLimit(isNaN(n) || n < 0 ? 0 : n);
  };

  const resetFilter = () => {
    setInputLimit("");
    setLimit(0);
  };

  const displayed = limit > 0 ? persons.slice(0, limit) : persons;

  return (
    <div className="card shadow-sm">
      <div className="card-body">
        <div className="d-flex justify-content-between align-items-center mb-3">
          <h1 className="card-title">Seznam osob</h1>
          <button className="btn btn-link" onClick={logout}>
            Odhlásit
          </button>
        </div>

        {/* filtr beze změny */}
        <div className="filter-controls">
          <input
            type="number"
            min="0"
            className="form-control"
            placeholder="Max. počet"
            value={inputLimit}
            onChange={e => setInputLimit(e.target.value)}
          />
          <div className="person-filter-buttons">
            <button className="btn btn-primary" onClick={applyFilter}>
              Filtrovat
            </button>
            <button className="btn btn-secondary" onClick={resetFilter}>
              Obnovit
            </button>
          </div>
        </div>

        {/* Předáme delete handler všem rolím, alert se teď zobrazí */}
        <PersonTable
          items={displayed}
          deletePerson={deletePerson}
        />

        {/* Přidat osoba s ochranou */}
        <button
          className="btn btn-success mt-3 person-add-btn"
          onClick={createPerson}
        >
          Přidat novou osobu
        </button>
      </div>
    </div>
  );
}






