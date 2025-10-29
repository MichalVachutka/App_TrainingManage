// import React, { useEffect, useState } from "react";
// import { useParams, Link } from "react-router-dom";
// import { apiGet, apiDelete } from "../utils/api";
// import {
//   FaTags,
//   FaCoins,
//   FaCalendarAlt,
//   FaPeopleCarry,
//   FaUserSlash
// } from "react-icons/fa";

// export default function ExpenseDetail() {
//   const { id } = useParams();
//   const [detail, setDetail] = useState(null);
//   const [error, setError]   = useState("");

//   useEffect(() => {
//     apiGet(`/api/expenses/${id}/detail`)
//       .then(d => setDetail({
//         ...d,
//         expense: { ...d.expense, date: new Date(d.expense.date) }
//       }))
//       .catch(err => setError(err.toString()));
//   }, [id]);

//   const handleRemove = async (personId) => {
//     if (!window.confirm("Opravdu odebrat účastníka?")) return;
//     try {
//       await apiDelete(`/api/expenses/${id}/participants/${personId}`);
//       setDetail(d => {
//         const shares = d.participantShares.filter(s => s.personId !== personId);
//         const total  = shares.reduce((sum, s) => sum + s.shareAmount, 0);
//         return { ...d, participantShares: shares, totalShares: total };
//       });
//     } catch (err) {
//       alert(err.toString());
//     }
//   };

//   if (error)   return <div className="alert alert-danger mt-4">{error}</div>;
//   if (!detail) return (
//     <div className="text-center my-5">
//       <div className="spinner-border" />
//     </div>
//   );

//   const { expense, participantShares, totalShares } = detail;

//   return (
//     <div className="container mt-4">

//       {/* Hlavní karta výdaje */}
//       <div className="card mb-4">
//         <div className="card-header expense-header">
//           <h4>Výdaj #{expense.id}</h4>
//         </div>
//         <div className="card-body">
//           <dl className="row mb-4 expense-dl">
//             <dt className="col-sm-3">
//               <FaTags className="me-1 text-secondary" /> Typ
//             </dt>
//             <dd className="col-sm-9">{expense.type}</dd>

//             <dt className="col-sm-3">
//               <FaCoins className="me-1 text-secondary" /> Částka
//             </dt>
//             <dd className="col-sm-9">
//               {expense.totalAmount.toFixed(2)} Kč
//             </dd>

//             <dt className="col-sm-3">
//               <FaCalendarAlt className="me-1 text-secondary" /> Datum
//             </dt>
//             <dd className="col-sm-9">
//               {expense.date.toLocaleDateString("cs-CZ")}
//             </dd>
//           </dl>

//           <Link to="/expenses" className="btn btn-secondary">
//             ← Zpět na seznam
//           </Link>
//         </div>
//       </div>

//       {/* Podíly účastníků */}
//       <div className="card mb-4">
//         <div className="card-header">
//           <h5>
//             <FaPeopleCarry className="me-1" />
//             Podíly účastníků
//           </h5>
//         </div>
//         <div className="card-body p-0">
//           <table className="table table-hover mb-0">
//             <thead className="table-light">
//               <tr>
//                 <th>Jméno</th>
//                 <th className="text-end">
//                   <FaCoins />
//                 </th>
//                 <th className="text-center">
//                   <FaUserSlash />
//                 </th>
//               </tr>
//             </thead>
//             <tbody>
//               {participantShares.map(ps => (
//                 <tr key={ps.personId}>
//                   <td>{ps.personName}</td>
//                   <td className="text-end">
//                     {ps.shareAmount.toFixed(2)}
//                   </td>
//                   <td className="text-center">
//                     <button
//                       className="btn btn-sm btn-danger"
//                       onClick={() => handleRemove(ps.personId)}
//                     >
//                       Odebrat
//                     </button>
//                   </td>
//                 </tr>
//               ))}
//             </tbody>
//           </table>
//         </div>
//       </div>

//       {/* Souhrnná karta */}
//       <div className="card mb-4">
//         <div className="card-body d-flex justify-content-between">
//           <div>
//             Celkem účastníků: <strong>{participantShares.length}</strong>
//           </div>
//           <div>
//             Celkem částka: <strong>{totalShares.toFixed(2)} Kč</strong>
//           </div>
//         </div>
//       </div>

//     </div>
//   );
// }











import React, { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";
import { apiGet, apiDelete } from "../utils/api";
import { useAuth } from "../components/AuthGate";
import {
  FaTags,
  FaCoins,
  FaCalendarAlt,
  FaPeopleCarry,
  FaUserSlash
} from "react-icons/fa";
import { toast } from "react-toastify";

export default function ExpenseDetail() {
  const { id } = useParams();
  const { role } = useAuth();                // Role z AuthContext
  const [detail, setDetail] = useState(null);
  const [error, setError] = useState("");

  useEffect(() => {
    apiGet(`/api/expenses/${id}/detail`)
      .then(data =>
        setDetail({
          ...data,
          expense: { ...data.expense, date: new Date(data.expense.date) }
        })
      )
      .catch(err => setError(err.toString()));
  }, [id]);

  const handleRemove = async (personId) => {
    if (!window.confirm("Opravdu odebrat účastníka?")) return;

    try {
      await apiDelete(`/api/expenses/${id}/participants/${personId}`);
      setDetail(prev => {
        const shares = prev.participantShares.filter(s => s.personId !== personId);
        const total = shares.reduce((sum, s) => sum + s.shareAmount, 0);
        return { ...prev, participantShares: shares, totalShares: total };
      });
    } catch (err) {
      alert(err.toString());
    }
  };

  if (error) {
    return <div className="alert alert-danger mt-4">{error}</div>;
  }

  if (!detail) {
    return (
      <div className="text-center my-5">
        <div className="spinner-border" />
      </div>
    );
  }

  const { expense, participantShares, totalShares } = detail;

  return (
    <div className="container mt-4">
      {/* Hlavní karta výdaje */}
      <div className="card mb-4">
        <div className="card-header expense-header">
          <h4>Výdaj #{expense.id}</h4>
        </div>
        <div className="card-body">
          <dl className="row mb-4 expense-dl">
            <dt className="col-sm-3">
              <FaTags className="me-1 text-secondary" /> Typ
            </dt>
            <dd className="col-sm-9">{expense.type}</dd>

            <dt className="col-sm-3">
              <FaCoins className="me-1 text-secondary" /> Částka
            </dt>
            <dd className="col-sm-9">
              {expense.totalAmount.toFixed(2)} Kč
            </dd>

            <dt className="col-sm-3">
              <FaCalendarAlt className="me-1 text-secondary" /> Datum
            </dt>
            <dd className="col-sm-9">
              {expense.date.toLocaleDateString("cs-CZ")}
            </dd>
          </dl>

          <Link to="/expenses" className="btn btn-secondary">
            ← Zpět na seznam
          </Link>
        </div>
      </div>

      {/* Podíly účastníků */}
      <div className="card mb-4">
        <div className="card-header">
          <h5>
            <FaPeopleCarry className="me-1" />
            Podíly účastníků
          </h5>
        </div>
        <div className="card-body p-0">
          <table className="table table-hover mb-0">
            <thead className="table-light">
              <tr>
                <th>Jméno</th>
                <th className="text-end">
                  <FaCoins />
                </th>
                <th className="text-center">
                  <FaUserSlash />
                </th>
              </tr>
            </thead>
            <tbody>
              {participantShares.map(ps => (
                <tr key={ps.personId}>
                  <td>{ps.personName}</td>
                  <td className="text-end">{ps.shareAmount.toFixed(2)}</td>
                  <td className="text-center">
                    <button
                      className="btn btn-sm btn-danger"
                      onClick={() => {
                        if (role !== "admin") {
                          toast.error("Nemáte oprávnění odebrat účastníka.");
                          return;
                        }
                        handleRemove(ps.personId);
                      }}
                    >
                      Odebrat
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>

      {/* Souhrnná karta */}
      <div className="card mb-4">
        <div className="card-body d-flex justify-content-between">
          <div>
            Celkem účastníků: <strong>{participantShares.length}</strong>
          </div>
          <div>
            Celkem částka: <strong>{totalShares.toFixed(2)} Kč</strong>
          </div>
        </div>
      </div>
    </div>
  );
}
