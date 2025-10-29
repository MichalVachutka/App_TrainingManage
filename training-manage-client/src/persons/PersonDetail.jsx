// // src/persons/PersonDetail.jsx
// import React, { useEffect, useState } from "react";
// import { useParams, useNavigate } from "react-router-dom";
// import { apiGet } from "../utils/api";
// import {
//   FaIdBadge,
//   FaBirthdayCake,
//   FaPhone,
//   FaEnvelope,
//   FaEyeSlash,
//   FaExternalLinkAlt
// } from "react-icons/fa";
// import "../styles/PersonStyle.css";

// export default function PersonDetail() {
//   const { id } = useParams();
//   const navigate = useNavigate();
//   const [detail, setDetail] = useState(null);
//   const [error, setError]   = useState("");

//   useEffect(() => {
//     apiGet(`/api/people/${id}/detail`)
//       .then(d => {
//         console.debug("PersonDetail response:", d); // <-- zkontroluj to v konzoli
//         setDetail(d);
//       })
//       .catch(err => {
//         console.error("PersonDetail error:", err);
//         setError(err.toString());
//       });
//   }, [id]);

//   if (error) {
//     return <div className="alert alert-danger">{error}</div>;
//   }

//   if (!detail) {
//     return (
//       <div className="text-center my-5">
//         <div className="spinner-border" />
//       </div>
//     );
//   }

//   const {
//     person,
//     personTransactions: txs = [],
//     totalPaid,
//     paidThisYear,
//     totalRentShare,
//     totalEquipmentShare,
//     balance
//   } = detail;

//   /**
//    * Robustní detekce cílové stránky pro transakci.
//    * Podporujeme různé tvary:
//    * - t.trainingId  nebo t.training?.id
//    * - t.expenseId   nebo t.expense?.id
//    * - fallback: t.id -> /transactions/show/:id (pokud existuje komponenta)
//    */
//   const getTxLink = (t) => {
    
//     if (!t) return null;

//     // přímé id pole
//     if (t.trainingId != null && String(t.trainingId) !== "") return `/trainings/show/${t.trainingId}`;
//     if (t.expenseId  != null && String(t.expenseId)  !== "") return `/expenses/show/${t.expenseId}`;

//     // často backend vrací nested object
//     if (t.training && (t.training.id != null)) return `/trainings/show/${t.training.id}`;
//     if (t.expense  && (t.expense.id  != null)) return `/expenses/show/${t.expense.id}`;

//     // někdy typ/target pole (fallbacky)
//     if (t.type === "training" && t.targetId) return `/trainings/show/${t.targetId}`;
//     if (t.type === "expense"  && t.targetId) return `/expenses/show/${t.targetId}`;

//     // fallback na transakční stránku, pokud tu máte
//     if (t.id != null) return `/transactions/show/${t.id}`;

//     return null;
//   };

//   return (
//     <div className="container mt-4">
//       {/* Osobní karta */}
//       <div className="card mb-4">
//         <div className="card-header person-header">
//           <h4>
//             {person.name} ({person.identificationNumber})
//           </h4>
//         </div>
//         <div className="card-body row">
//           <div className="col-md-6">
//             <dl className="row mb-0 person-dl">
//               <dt className="col-sm-3 d-flex align-items-center">
//                 <FaIdBadge className="me-1 text-secondary" /> Id
//               </dt>
//               <dd className="col-sm-9">{person.id}</dd>

//               <dt className="col-sm-3 d-flex align-items-center">
//                 <FaBirthdayCake className="me-1 text-secondary" /> Věk
//               </dt>
//               <dd className="col-sm-9">{person.age}</dd>

//               <dt className="col-sm-3 d-flex align-items-center">
//                 <FaPhone className="me-1 text-secondary" /> Telefon
//               </dt>
//               <dd className="col-sm-9">{person.telephone}</dd>

//               <dt className="col-sm-3 d-flex align-items-center">
//                 <FaEnvelope className="me-1 text-secondary" /> Email
//               </dt>
//               <dd className="col-sm-9">{person.email}</dd>

//               <dt className="col-sm-3 d-flex align-items-center">
//                 <FaEyeSlash className="me-1 text-secondary" /> Skryto
//               </dt>
//               <dd className="col-sm-9">
//                 {person.hidden ? "Ano" : "Ne"}
//               </dd>
//             </dl>
//           </div>

//           {/* Metriky */}
//           <div className="col-md-6">
//             <div className="row g-3">
//               {[
//                 { label: "Celkem zaplaceno", value: totalPaid },
//                 { label: "Zaplatil letos",   value: paidThisYear },
//                 { label: "Podíl na nájmu",      value: totalRentShare },
//                 { label: "Podíl na vybavení",   value: totalEquipmentShare }
//               ].map((card, i) => (
//                 <div key={i} className="col-6">
//                   <div className="card text-center card-metric">
//                     <div className="card-header">{card.label}</div>
//                     <div className="card-body">
//                       <h5 className="card-title">
//                         {card.value.toFixed(2)} Kč
//                       </h5>
//                     </div>
//                   </div>
//                 </div>
//               ))}
//             </div>
//           </div>
//         </div>

//         <div className="card-footer d-flex gap-2 person-detail-actions">
//           <button className="btn btn-secondary" onClick={() => navigate(-1)}>← Zpět</button>
//           <button className="btn btn-primary" onClick={() => navigate(`/persons/edit/${person.id}`)}>Upravit</button>
//         </div>
//       </div>

//       {/* Přehled plateb */}
//       <div className="card mb-4">
//         <div className="card-header">Přehled plateb</div>

//         {txs.length === 0 ? (
//           <div className="p-3 text-muted">Žádné platby</div>
//         ) : (
//           <table className="table table-hover mb-0 table-transactions">
//             <thead>
//               <tr>
//                 <th>Datum</th>
//                 <th>Popis</th>
//                 <th className="text-end">Částka</th>
//                 <th className="text-center">Akce</th>
//               </tr>
//             </thead>
//             <tbody>
//               {txs.map((t, i) => {
//                 const rowClass = t.amount > 0
//                   ? "tx-income"
//                   : t.description?.startsWith("Rent share")
//                     ? "tx-rent-share"
//                     : t.description?.startsWith("Equipment")
//                       ? "tx-equipment-share"
//                       : "tx-expense";

//                 const link = getTxLink(t);

//                 return (
                  
//                   <tr key={i} className={rowClass}>
//                     <td>{new Date(t.date).toLocaleDateString("cs-CZ")}</td>
//                     <td>{t.description}</td>
//                     <td className="text-end">{t.amount.toFixed(2)}</td>
//                     <td className="text-center">
//                       {link ? (
//                         <button
//                           className="btn btn-sm btn-outline-secondary icon-btn"
//                           title="Detail transakce"
//                           onClick={() => {
//                             console.debug("navigating to", link, "transaction item:", t);
//                             navigate(link);
//                           }}
//                         >
//                           <FaExternalLinkAlt />
//                         </button>
//                       ) : (
//                         <button
//                           className="btn btn-sm btn-outline-secondary icon-btn"
//                           title="Detail není k dispozici"
//                           disabled
//                         >
//                           <FaExternalLinkAlt />
//                         </button>
//                       )}
//                     </td>
//                   </tr>
//                 );
//               })}
//             </tbody>
//           </table>
//         )}

//         <div className="card-footer text-end">
//           <strong>Zůstatek: {balance.toFixed(2)} Kč</strong>
//         </div>
//       </div>
//     </div>
//   );
// }


// src/persons/PersonDetail.jsx
import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { apiGet } from "../utils/api";
import { toast } from "react-toastify";
import {
  FaIdBadge,
  FaBirthdayCake,
  FaPhone,
  FaEnvelope,
  FaEyeSlash,
  FaExternalLinkAlt
} from "react-icons/fa";
import "../styles/PersonStyle.css";

export default function PersonDetail() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [detail, setDetail] = useState(null);
  const [error, setError]   = useState("");

  useEffect(() => {
    apiGet(`/api/people/${id}/detail`)
      .then(d => {
        console.debug("PersonDetail response:", d);
        setDetail(d);
      })
      .catch(err => {
        console.error("PersonDetail error:", err);
        setError(err.toString());
      });
  }, [id]);

  if (error) {
    return <div className="alert alert-danger">{error}</div>;
  }

  if (!detail) {
    return (
      <div className="text-center my-5">
        <div className="spinner-border" />
      </div>
    );
  }

  const {
    person,
    personTransactions: txs = [],
    totalPaid,
    paidThisYear,
    totalRentShare,
    totalEquipmentShare,
    balance
  } = detail;

  /**
   * getTxLink: vrátí správný interní URL path pro transakci `t`,
   * nebo null pokud nelze spolehlivě určit cílovou stránku.
   *
   * Priorita:
   * 1) t.trainingId
   * 2) t.expenseId
   * 3) nested objects t.training?.id / t.expense?.id
   * 4) parsing description (např. "Výdaj #4" -> expense 4)
   * 5) null (NEVRACET /transactions/show/:id protože ta routa v appce není)
   */
  const getTxLink = (t) => {
    if (!t) return null;

    // 1) přímé id pole
    if (t.trainingId != null && String(t.trainingId) !== "") {
      return `/trainings/show/${t.trainingId}`;
    }
    if (t.expenseId != null && String(t.expenseId) !== "") {
      return `/expenses/show/${t.expenseId}`;
    }

    // 2) nested object (někdy backend vrací objekt)
    if (t.training && (t.training.id != null)) {
      return `/trainings/show/${t.training.id}`;
    }
    if (t.expense && (t.expense.id != null)) {
      return `/expenses/show/${t.expense.id}`;
    }

    // 3) parsing popisu (často formát "Výdaj #N" nebo "Rent share #N")
    if (typeof t.description === "string") {
      // výdaj => expense
      const expenseMatch = t.description.match(/Výdaj\s*#\s*(\d+)/i);
      if (expenseMatch) {
        return `/expenses/show/${expenseMatch[1]}`;
      }
      // rent share (pokud chcete, může odkazovat na expense nebo jinde)
      const rentMatch = t.description.match(/Rent\s*share\s*#\s*(\d+)/i);
      if (rentMatch) {
        // pokusíme se také odkazovat na expenses, pokud to dává smysl
        return `/expenses/show/${rentMatch[1]}`;
      }
      // další patterny lze snadno přidat zde...
    }

    // 4) Není možné spolehlivě určit cílovou stránku -> vrátit null
    return null;
  };

  const handleNavigateToTx = (t) => {
    const link = getTxLink(t);
    console.debug("Attempt navigate for transaction:", t, "resolved link:", link);

    if (!link) {
      toast.info("Detail nelze načíst (možná byl záznam smazán nebo chybí odkaz).");
      return;
    }

    // naviguj na dedikovaný route
    navigate(link);
  };

  return (
    <div className="container mt-4">
      {/* Osobní karta */}
      <div className="card mb-4">
        <div className="card-header person-header">
          <h4>
            {person.name} ({person.identificationNumber})
          </h4>
        </div>
        <div className="card-body row">
          <div className="col-md-6">
            <dl className="row mb-0 person-dl">
              <dt className="col-sm-3 d-flex align-items-center">
                <FaIdBadge className="me-1 text-secondary" /> Id
              </dt>
              <dd className="col-sm-9">{person.id}</dd>

              <dt className="col-sm-3 d-flex align-items-center">
                <FaBirthdayCake className="me-1 text-secondary" /> Věk
              </dt>
              <dd className="col-sm-9">{person.age}</dd>

              <dt className="col-sm-3 d-flex align-items-center">
                <FaPhone className="me-1 text-secondary" /> Telefon
              </dt>
              <dd className="col-sm-9">{person.telephone}</dd>

              <dt className="col-sm-3 d-flex align-items-center">
                <FaEnvelope className="me-1 text-secondary" /> Email
              </dt>
              <dd className="col-sm-9">{person.email}</dd>

              <dt className="col-sm-3 d-flex align-items-center">
                <FaEyeSlash className="me-1 text-secondary" /> Skryto
              </dt>
              <dd className="col-sm-9">
                {person.hidden ? "Ano" : "Ne"}
              </dd>
            </dl>
          </div>

          {/* Metriky */}
          <div className="col-md-6">
            <div className="row g-3">
              {[
                { label: "Celkem zaplaceno", value: totalPaid },
                { label: "Zaplatil letos",   value: paidThisYear },
                { label: "Podíl na nájmu",   value: totalRentShare },
                { label: "Podíl na vybavení", value: totalEquipmentShare }
              ].map((card, i) => (
                <div key={i} className="col-6">
                  <div className="card text-center card-metric">
                    <div className="card-header">{card.label}</div>
                    <div className="card-body">
                      <h5 className="card-title">
                        {card.value.toFixed(2)} Kč
                      </h5>
                    </div>
                  </div>
                </div>
              ))}
            </div>
          </div>
        </div>

        <div className="card-footer d-flex gap-2 person-detail-actions">
          <button className="btn btn-secondary" onClick={() => navigate(-1)}>← Zpět</button>
          <button className="btn btn-primary" onClick={() => navigate(`/persons/edit/${person.id}`)}>Upravit</button>
        </div>
      </div>

      {/* Přehled plateb */}
      <div className="card mb-4">
        <div className="card-header">Přehled plateb</div>

        {txs.length === 0 ? (
          <div className="p-3 text-muted">Žádné platby</div>
        ) : (
          <table className="table table-hover mb-0 table-transactions">
            <thead>
              <tr>
                <th>Datum</th>
                <th>Popis</th>
                <th className="text-end">Částka</th>
                <th className="text-center">Akce</th>
              </tr>
            </thead>
            <tbody>
              {txs.map((t, i) => {
                const rowClass = t.amount > 0
                  ? "tx-income"
                  : t.description?.startsWith("Rent share")
                    ? "tx-rent-share"
                    : t.description?.startsWith("Výdaj #")
                      ? "tx-equipment-share"
                      : "tx-expense";

                const link = getTxLink(t);

                return (
                  <tr key={i} className={rowClass}>
                    <td>{new Date(t.date).toLocaleDateString("cs-CZ")}</td>
                    <td>{t.description}</td>
                    <td className="text-end">{t.amount.toFixed(2)}</td>
                    <td className="text-center">
                      <button
                        className="btn btn-sm btn-outline-secondary icon-btn"
                        title={link ? "Detail transakce" : "Detail není k dispozici"}
                        onClick={() => handleNavigateToTx(t)}
                        disabled={!link}
                      >
                        <FaExternalLinkAlt />
                      </button>
                    </td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        )}

        <div className="card-footer text-end">
          <strong>Zůstatek: {balance.toFixed(2)} Kč</strong>
        </div>
      </div>
    </div>
  );
}

























