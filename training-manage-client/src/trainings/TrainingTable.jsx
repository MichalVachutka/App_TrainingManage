
// import React, { useState } from "react";
// import { Link } from "react-router-dom";
// import { Button } from "react-bootstrap";
// import { useAuth } from "../components/AuthGate";
// import {
//   FaTrash,
//   FaChevronDown,
//   FaChevronUp,
//   FaEye,
//   FaEdit
// } from "react-icons/fa";

// export default function TrainingTable({ items, deleteTraining }) {
//   const [expandedId, setExpandedId] = useState(null);
//   const { role } = useAuth();                  // Role z AuthContext
//   const toggleRow = id =>
//     setExpandedId(prev => (prev === id ? null : id));

//   return (
//     <div className="table-responsive">
//       <table className="table table-striped table-bordered mb-0">
//         <thead className="table-light">
//           <tr>
//             <th style={{ width: "5%" }}>#</th>
//             <th style={{ width: "15%" }}>Datum</th>
//             <th style={{ width: "40%" }}>Název</th>
//             <th style={{ width: "15%" }} className="note-col">Poznámka</th>
//             <th style={{ width: "25%" }} className="text-center">Akce</th>
//           </tr>
//         </thead>
//         <tbody>
//           {items.map((training, idx) => (
//             <React.Fragment key={training.id}>
//               {/* hlavní řádek */}
//               <tr>
//                 <td>{idx + 1}</td>
//                 <td>{training.date?.slice(0, 10)}</td>
//                 <td>{training.title}</td>
//                 <td className="note-col">{training.notes || ""}</td>
//                 <td className="text-center">
//                   {/* tlačítka pro ≥md */}
//                   <div className="d-none d-md-flex justify-content-center gap-2">
//                     <Link
//                       to={`/trainings/show/${training.id}`}
//                       className="btn btn-sm btn-info icon-btn"
//                       title="Detail"
//                     >
//                       <FaEye />
//                     </Link>
//                     <Link
//                       to={`/trainings/edit/${training.id}`}
//                       className="btn btn-sm btn-warning icon-btn"
//                       title="Upravit"
//                       onClick={e => {
//                         if (role !== "admin") {
//                           e.preventDefault();
//                           alert("Nemáte oprávnění upravovat trénink.");
//                         }
//                       }}
//                     >
//                       <FaEdit />
//                     </Link>
//                     <Button
//                       size="sm"
//                       variant="danger"
//                       className="icon-btn"
//                       onClick={() => deleteTraining(training.id)}
//                       title="Odstranit"
//                     >
//                       <FaTrash />
//                     </Button>
//                   </div>

//                   {/* toggle ikona na <md */}
//                   <span
//                     className="d-md-none text-secondary"
//                     style={{ cursor: "pointer", fontSize: "1.2rem" }}
//                     onClick={() => toggleRow(training.id)}
//                   >
//                     {expandedId === training.id
//                       ? <FaChevronUp />
//                       : <FaChevronDown />
//                     }
//                   </span>
//                 </td>
//               </tr>

//               {/* rozbalený řádek s akcemi jen na <md */}
//               {expandedId === training.id && (
//                 <tr className="action-row d-md-none">
//                   <td colSpan={5}>
//                     <div className="d-flex justify-content-center flex-wrap gap-2">
//                       <Link
//                         to={`/trainings/show/${training.id}`}
//                         className="btn btn-sm btn-info icon-btn"
//                         title="Detail"
//                       >
//                         <FaEye />
//                       </Link>
//                       <Link
//                         to={`/trainings/edit/${training.id}`}
//                         className="btn btn-sm btn-warning icon-btn"
//                         title="Upravit"
//                         onClick={e => {
//                           if (role !== "admin") {
//                             e.preventDefault();
//                             alert("Nemáte oprávnění upravovat trénink.");
//                           }
//                         }}
//                       >
//                         <FaEdit />
//                       </Link>
//                       <Button
//                         size="sm"
//                         variant="danger"
//                         className="icon-btn"
//                         onClick={() => deleteTraining(training.id)}
//                         title="Odstranit"
//                       >
//                         <FaTrash />
//                       </Button>
//                     </div>
//                   </td>
//                 </tr>
//               )}
//             </React.Fragment>
//           ))}
//         </tbody>
//       </table>
//     </div>
//   );
// }







// src/trainings/TrainingTable.jsx
import React, { useState } from "react";
import { Link } from "react-router-dom";
import { Button } from "react-bootstrap";
import { useAuth } from "../components/AuthGate";
import "../styles/TrainingStyle.css";
import { toast } from "react-toastify";
import { confirmAlert } from 'react-confirm-alert';
import 'react-confirm-alert/src/react-confirm-alert.css';
import {
  FaTrash,
  FaChevronDown,
  FaChevronUp,
  FaEye,
  FaEdit
} from "react-icons/fa";

export default function TrainingTable({ items, deleteTraining }) {
  const [expandedId, setExpandedId] = useState(null);
  const { role } = useAuth();                  // Role z AuthContext
  const toggleRow = id =>
    setExpandedId(prev => (prev === id ? null : id));

  const today = new Date();

  return (
    <div className="table-responsive">
      <table className="table table-bordered mb-0">
        <thead className="table-light">
          <tr>
            <th style={{ width: "5%" }}>#</th>
            <th style={{ width: "15%" }}>Datum</th>
            <th style={{ width: "40%" }}>Název</th>
            <th style={{ width: "15%" }} className="note-col">Poznámka</th>
            <th style={{ width: "25%" }} className="text-center">Akce</th>
          </tr>
        </thead>
        <tbody>
          {items.map((training, idx) => {
            // const trainingDate = new Date(training.date?.slice(0, 10));
            // const isPast = trainingDate < today;
            const trainingDateTime = new Date(training.date); // vezme datum i čas
            const isPast = trainingDateTime < new Date(); // porovnání s aktuálním časem


            return (
              <React.Fragment key={training.id}>
                {/* hlavní řádek */}
                <tr className={isPast ? "table-cervena" : "table-zelena"}>
                  <td>{idx + 1}</td>
                  <td>{training.date?.slice(0, 10)}</td>
                  <td>{training.title}</td>
                  <td className="note-col">{training.notes || ""}</td>
                  <td className="text-center">
                    {/* tlačítka pro ≥md */}
                    <div className="d-none d-md-flex justify-content-center gap-2">
                      <Link
                        to={`/trainings/show/${training.id}`}
                        className="btn btn-sm btn-info icon-btn"
                        title="Detail"
                      >
                        <FaEye />
                      </Link>
                      <Link
                        to={`/trainings/edit/${training.id}`}
                        className="btn btn-sm btn-warning icon-btn"
                        title="Upravit"
                        onClick={e => {
                          if (role !== "admin") {
                            e.preventDefault();
                            toast.error("Nemáte oprávnění upravovat trénink.");
                          }
                        }}
                      >
                        <FaEdit />
                      </Link>
                      <Button
                        size="sm"
                        variant="danger"
                        className="icon-btn"
                        onClick={() => deleteTraining(training.id)}
                        title="Odstranit"
                      >
                        <FaTrash />
                      </Button>
                    </div>

                    {/* toggle ikona na <md */}
                    <span
                      className="d-md-none text-secondary"
                      style={{ cursor: "pointer", fontSize: "1.2rem" }}
                      onClick={() => toggleRow(training.id)}
                    >
                      {expandedId === training.id
                        ? <FaChevronUp />
                        : <FaChevronDown />
                      }
                    </span>
                  </td>
                </tr>

                {/* rozbalený řádek s akcemi jen na <md */}
                {expandedId === training.id && (
                  <tr className="action-row d-md-none">
                    <td colSpan={5}>
                      <div className="d-flex justify-content-center flex-wrap gap-2">
                        <Link
                          to={`/trainings/show/${training.id}`}
                          className="btn btn-sm btn-info icon-btn"
                          title="Detail"
                        >
                          <FaEye />
                        </Link>
                        <Link
                          to={`/trainings/edit/${training.id}`}
                          className="btn btn-sm btn-warning icon-btn"
                          title="Upravit"
                          onClick={e => {
                            if (role !== "admin") {
                              e.preventDefault();
                              alert("Nemáte oprávnění upravovat trénink.");
                            }
                          }}
                        >
                          <FaEdit />
                        </Link>
                        <Button
                          size="sm"
                          variant="danger"
                          className="icon-btn"
                          onClick={() => deleteTraining(training.id)}
                          title="Odstranit"
                        >
                          <FaTrash />
                        </Button>
                      </div>
                    </td>
                  </tr>
                )}
              </React.Fragment>
            );
          })}
        </tbody>
      </table>
    </div>
  );
}

