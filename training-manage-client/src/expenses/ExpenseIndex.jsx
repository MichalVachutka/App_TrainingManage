// import React, { useEffect, useState } from "react";
// import { Link } from "react-router-dom";
// import { apiGet, apiDelete } from "../utils/api";
// import "../styles/ExpensesStyle.css";
// import { FaChevronDown, FaChevronUp } from "react-icons/fa";

// export default function ExpenseIndex() {
//   const [expenses, setExpenses] = useState([]);
//   const [error, setError] = useState("");
//   const [expandedId, setExpandedId] = useState(null);

//   useEffect(() => {
//     apiGet("/api/expenses")
//       .then(setExpenses)
//       .catch(err => setError(err.toString()));
//   }, []);

//   const handleDelete = async (id) => {
//     if (!window.confirm("Opravdu smazat tento výdaj?")) return;
//     try {
//       await apiDelete(`/api/expenses/${id}`);
//       setExpenses(es => es.filter(e => e.id !== id));
//     } catch (e) {
//       alert(e.toString());
//     }
//   };

//   const toggleRow = id => {
//     setExpandedId(prev => (prev === id ? null : id));
//   };

//   if (error) {
//     return <div className="alert alert-danger mt-4">Chyba: {error}</div>;
//   }

//   return (
//     <div className="container mt-4">
//       <h3 className="mb-3">Seznam výdajů</h3>

//       <div className="table-responsive mb-4">
//         <table className="table table-striped table-bordered mb-0">
//           <thead className="table-light">
//             <tr>
//               <th style={{ width: "5%" }}>#</th>
//               <th style={{ width: "25%" }}>Typ</th>
//               <th style={{ width: "20%" }}>Částka</th>
//               <th style={{ width: "25%" }}>Datum</th>
//               <th style={{ width: "25%" }} className="text-center">Akce</th>
//             </tr>
//           </thead>
//           <tbody>
//             {expenses.map((e, idx) => (
//               <React.Fragment key={e.id}>
//                 <tr>
//                   <td>{idx + 1}</td>
//                   <td>{e.type}</td>
//                   <td>{e.totalAmount.toFixed(2)} Kč</td>
//                   <td>{new Date(e.date).toLocaleDateString("cs-CZ")}</td>
//                   <td className="text-center">
//                     <div className="d-none d-md-flex justify-content-center gap-2">
//                       <Link
//                         to={`/expenses/show/${e.id}`}
//                         className="btn btn-sm btn-primary"
//                       >
//                         Detail
//                       </Link>
//                       <button
//                         className="btn btn-sm btn-danger"
//                         onClick={() => handleDelete(e.id)}
//                       >
//                         Smazat
//                       </button>
//                     </div>
//                     <span
//                       className="d-md-none text-secondary"
//                       style={{ cursor: "pointer", fontSize: "1.2rem" }}
//                       onClick={() => toggleRow(e.id)}
//                     >
//                       {expandedId === e.id ? <FaChevronUp /> : <FaChevronDown />}
//                     </span>
//                   </td>
//                 </tr>

//                 {expandedId === e.id && (
//                   <tr className="action-row d-md-none">
//                     <td colSpan={5}>
//                       <div className="d-flex justify-content-center flex-wrap gap-2">
//                         <Link
//                           to={`/expenses/show/${e.id}`}
//                           className="btn btn-sm btn-primary"
//                         >
//                           Detail
//                         </Link>
//                         <button
//                           className="btn btn-sm btn-danger"
//                           onClick={() => handleDelete(e.id)}
//                         >
//                           Smazat
//                         </button>
//                       </div>
//                     </td>
//                   </tr>
//                 )}
//               </React.Fragment>
//             ))}
//           </tbody>
//         </table>
//       </div>

//       {/* Tlačítko “Nový výdaj” – pod tabulkou */}
//       <div className="expense-index-actions">
//         <Link to="/expenses/create" className="btn btn-success expense-create-btn">
//           + Nový výdaj
//         </Link>
//       </div>
//     </div>
//   );
// }










import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { apiGet, apiDelete } from "../utils/api";
import { useAuth } from "../components/AuthGate";
import "../styles/ExpensesStyle.css";
import { FaChevronDown, FaChevronUp } from "react-icons/fa";
import { toast } from "react-toastify";
import { confirmAlert } from 'react-confirm-alert';
import 'react-confirm-alert/src/react-confirm-alert.css';

export default function ExpenseIndex() {
  const { role } = useAuth();          // Role z AuthContext (sessionStorage)
  const [expenses, setExpenses] = useState([]);
  const [error, setError] = useState("");
  const [expandedId, setExpandedId] = useState(null);

  useEffect(() => {
    apiGet("/api/expenses")
      .then(setExpenses)
      .catch(err => setError(err.toString()));
  }, []);

  // const handleDelete = async (id) => {
  //   if (!window.confirm("Opravdu smazat tento výdaj?")) return;
  //   try {
  //     await apiDelete(`/api/expenses/${id}`);
  //     setExpenses(es => es.filter(e => e.id !== id));
  //   } catch (e) {
  //     alert(e.toString());
  //   }
  // };

  // const toggleRow = id => {
  //   setExpandedId(prev => (prev === id ? null : id));
  // };

  // if (error) {
  //   return <div className="alert alert-danger mt-4">Chyba: {error}</div>;
  // }
  const handleDelete = (id) => {
  confirmAlert({
    title: "Potvrzení",
    message: "Opravdu chcete smazat tento výdaj?",
    buttons: [
      {
        label: "Ano",
        onClick: async () => {
          try {
            await apiDelete(`/api/expenses/${id}`);
            setExpenses(prev => prev.filter(e => e.id !== id));
            toast.success("Výdaj byl úspěšně smazán.");
          } catch (e) {
            toast.error("Chyba při mazání: " + e.toString());
          }
        }
      },
      {
        label: "Ne",
        onClick: () => {} // nic nedělat
      }
    ]
  });
};


  return (
    <div className="container mt-4">
      <h3 className="mb-3">Seznam výdajů</h3>

      <div className="table-responsive mb-4">
        <table className="table table-striped table-bordered mb-0">
          <thead className="table-light">
            <tr>
              <th style={{ width: "5%" }}>#</th>
              <th style={{ width: "25%" }}>Typ</th>
              <th style={{ width: "20%" }}>Částka</th>
              <th style={{ width: "25%" }}>Datum</th>
              <th style={{ width: "25%" }} className="text-center">Akce</th>
            </tr>
          </thead>
          <tbody>
            {expenses.map((e, idx) => (
              <React.Fragment key={e.id}>
                <tr>
                  <td>{idx + 1}</td>
                  <td>{e.type}</td>
                  <td>{e.totalAmount.toFixed(2)} Kč</td>
                  <td>{new Date(e.date).toLocaleDateString("cs-CZ")}</td>
                  <td className="text-center">
                    <div className="d-none d-md-flex justify-content-center gap-2">
                      <Link
                        to={`/expenses/show/${e.id}`}
                        className="btn btn-sm btn-primary"
                        title="Detail"
                      >
                        Detail
                      </Link>
                      <button
                        className="btn btn-sm btn-danger"
                        title="Smazat"
                        onClick={() => {
                          if (role !== "admin") {
                            toast.error("Nemáte oprávnění mazat výdaje.");
                            return;
                          }
                          handleDelete(e.id);
                        }}
                      >
                        Smazat
                      </button>
                    </div>
                    <span
                      className="d-md-none text-secondary"
                      style={{ cursor: "pointer", fontSize: "1.2rem" }}
                      onClick={() => toggleRow(e.id)}
                    >
                      {expandedId === e.id ? <FaChevronUp /> : <FaChevronDown />}
                    </span>
                  </td>
                </tr>

                {expandedId === e.id && (
                  <tr className="action-row d-md-none">
                    <td colSpan={5}>
                      <div className="d-flex justify-content-center flex-wrap gap-2">
                        <Link
                          to={`/expenses/show/${e.id}`}
                          className="btn btn-sm btn-primary"
                          title="Detail"
                        >
                          Detail
                        </Link>
                        <button
                          className="btn btn-sm btn-danger"
                          title="Smazat"
                          onClick={() => {
                            if (role !== "admin") {
                              toast.error("Nemáte oprávnění mazat výdaje.");
                              return;
                            }
                            handleDelete(e.id);
                          }}
                        >
                          Smazat
                        </button>
                      </div>
                    </td>
                  </tr>
                )}
              </React.Fragment>
            ))}
          </tbody>
        </table>
      </div>

      <div className="expense-index-actions">
        <Link
          to="/expenses/create"
          className="btn btn-success expense-create-btn"
          title="Nový výdaj"
          onClick={e => {
            if (role !== "admin") {
              e.preventDefault();
              toast.error("Nemáte oprávnění vytvářet nové výdaje.");
            }
          }}
        >
          + Nový výdaj
        </Link>
      </div>
    </div>
  );
}
