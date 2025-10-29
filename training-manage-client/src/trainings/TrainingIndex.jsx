
// import React, { useEffect, useState } from "react";
// import { apiGet, apiDelete }      from "../utils/api";
// import { useNavigate }            from "react-router-dom";
// import TrainingTable              from "./TrainingTable";
// import TrainingFilter             from "./TrainingFilter";
// import "../styles/TrainingStyle.css";
// import { motion } from "framer-motion";

// export default function TrainingIndex() {
//   // všechna data
//   const [allTrainings, setAllTrainings] = useState([]);
//   // to, co se právě zobrazuje
//   const [trainings,    setTrainings]    = useState([]);
//   const [error,        setError]        = useState(null);
//   const navigate                        = useNavigate();

//   // 1) Na mountu stáhneme všechno
//   useEffect(() => {
//     apiGet("/api/trainings")
//       .then(data => {
//         setAllTrainings(data);
//         setTrainings(data);
//         setError(null);
//       })
//       .catch(err => setError(err.message));
//   }, []);

//   // 2) Klientská aplikace filtru
//   const applyFilter = filter => {
//     if (!filter) {
//       setTrainings(allTrainings);
//       return;
//     }

//     let result = allTrainings;

//     if (filter.dateFrom) {
//       const from = new Date(filter.dateFrom);
//       result = result.filter(t => new Date(t.date) >= from);
//     }
//     if (filter.dateTo) {
//       const to = new Date(filter.dateTo);
//       result = result.filter(t => new Date(t.date) <= to);
//     }
//     if (filter.title) {
//       const term = filter.title.toLowerCase();
//       result = result.filter(t =>
//         t.title.toLowerCase().includes(term)
//       );
//     }
//     if (filter.notes) {
//       const term = filter.notes.toLowerCase();
//       result = result.filter(t =>
//         (t.notes || "").toLowerCase().includes(term)
//       );
//     }
//     if (filter.limit) {
//       result = result.slice(0, filter.limit);
//     }

//     setTrainings(result);
//   };

//   // 3) Smazání
//   const deleteTraining = async id => {
//     try {
//       await apiDelete(`/api/trainings/${id}`);
//       setAllTrainings(prev => prev.filter(t => t.id !== id));
//       setTrainings(prev    => prev.filter(t => t.id !== id));
//     } catch (err) {
//       alert(err.message);
//     }
//   };

//   // 4) Přesměrování na vytvoření
//   const createTraining = () => {
//     navigate("/trainings/create");
//   };

//   return (
  
//     <div className="card shadow-sm">
//       <div className="card-body">
//         <h1 className="card-title">Seznam tréninků</h1>

//         {error && (
//           <div className="alert alert-danger mb-3">
//             Chyba: {error}
//           </div>
//         )}

//         <TrainingFilter onFilter={applyFilter} />

//         <TrainingTable
//           items={trainings}
//           deleteTraining={deleteTraining}
//         />

//         <button
//           className="btn btn-success mt-3"
//           onClick={createTraining}
//         >
//           Vytvořit nový trénink
//         </button>
//       </div>
//     </div>
  
//   );
// }






// import React, { useEffect, useState } from "react";
// import { apiGet, apiDelete }      from "../utils/api";
// import { useNavigate }            from "react-router-dom";
// import TrainingTable              from "./TrainingTable";
// import TrainingFilter             from "./TrainingFilter";
// import "../styles/TrainingStyle.css";
// import { motion }                 from "framer-motion";
// import { useAuth }                from "../components/AuthGate";

// export default function TrainingIndex() {
//   const { role } = useAuth();
//   const [allTrainings, setAllTrainings] = useState([]);
//   const [trainings,    setTrainings]    = useState([]);
//   const [error,        setError]        = useState(null);
//   const navigate                      = useNavigate();

//   // 1) Na mountu stáhneme všechno
//   useEffect(() => {
//     apiGet("/api/trainings")
//       .then(data => {
//         setAllTrainings(data);
//         setTrainings(data);
//         setError(null);
//       })
//       .catch(err => setError(err.message));
//   }, []);

//   // 2) Klientská logika filtru
//   const applyFilter = filter => {
//     if (!filter) {
//       setTrainings(allTrainings);
//       return;
//     }

//     let result = allTrainings;

//     if (filter.dateFrom) {
//       const from = new Date(filter.dateFrom);
//       result = result.filter(t => new Date(t.date) >= from);
//     }
//     if (filter.dateTo) {
//       const to = new Date(filter.dateTo);
//       result = result.filter(t => new Date(t.date) <= to);
//     }
//     if (filter.title) {
//       const term = filter.title.toLowerCase();
//       result = result.filter(t =>
//         t.title.toLowerCase().includes(term)
//       );
//     }
//     if (filter.notes) {
//       const term = filter.notes.toLowerCase();
//       result = result.filter(t =>
//         (t.notes || "").toLowerCase().includes(term)
//       );
//     }
//     if (filter.limit) {
//       result = result.slice(0, filter.limit);
//     }

//     setTrainings(result);
//   };

//   // 3) Smazání – jen pro admina, jinak alert
//   const deleteTraining = async id => {
//     if (role !== "admin") {
//       alert("Nemáte oprávnění smazat trénink.");
//       return;
//     }
//     if (!window.confirm("Opravdu smazat tento trénink?")) return;

//     try {
//       await apiDelete(`/api/trainings/${id}`);
//       setAllTrainings(prev => prev.filter(t => t.id !== id));
//       setTrainings(prev    => prev.filter(t => t.id !== id));
//     } catch (err) {
//       alert(err.message);
//     }
//   };

//   // 4) Přesměrování na vytvoření – jen pro admina
//   const createTraining = () => {
//     if (role !== "admin") {
//       alert("Nemáte oprávnění vytvořit nový trénink.");
//       return;
//     }
//     navigate("/trainings/create");
//   };

//   return (
//     <div className="card shadow-sm">
//       <div className="card-body">
//         <h1 className="card-title">Seznam tréninků</h1>

//         {error && (
//           <div className="alert alert-danger mb-3">
//             Chyba: {error}
//           </div>
//         )}

//         <TrainingFilter onFilter={applyFilter} />

//         <TrainingTable
//           items={trainings}
//           deleteTraining={deleteTraining}
//         />

//         <button
//           className="btn btn-success mt-3"
//           onClick={createTraining}
//         >
//           Vytvořit nový trénink
//         </button>
//       </div>
//     </div>
//   );
// }









import React, { useEffect, useState } from "react";
import { apiGet, apiDelete }      from "../utils/api";
import { useNavigate }            from "react-router-dom";
import TrainingTable              from "./TrainingTable";
import TrainingFilter             from "./TrainingFilter";
import "../styles/TrainingStyle.css";
import { motion }                 from "framer-motion";
import { useAuth }                from "../components/AuthGate";
import { toast } from "react-toastify";
import { confirmAlert } from 'react-confirm-alert';
import 'react-confirm-alert/src/react-confirm-alert.css'; 


export default function TrainingIndex() {
  const { role } = useAuth();
  const [allTrainings, setAllTrainings] = useState([]);
  const [trainings,    setTrainings]    = useState([]);
  const [error,        setError]        = useState(null);
  const navigate                      = useNavigate();

  // 1) Na mountu stáhneme všechno
  useEffect(() => {
    apiGet("/api/trainings")
      .then(data => {
        setAllTrainings(data);
        setTrainings(data);
        setError(null);
      })
      .catch(err => setError(err.message));
  }, []);

  // 2) Klientská logika filtru
  const applyFilter = filter => {
    if (!filter) {
      setTrainings(allTrainings);
      return;
    }

    let result = allTrainings;

    if (filter.dateFrom) {
      const from = new Date(filter.dateFrom);
      from.setHours(0, 0, 0, 0); // začátek dne
      result = result.filter(t => new Date(t.date) >= from);
    }
    if (filter.dateTo) {
      const to = new Date(filter.dateTo);
      to.setHours(23, 59, 59, 999); // konec dne
      result = result.filter(t => new Date(t.date) <= to);
    }
    if (filter.title) {
      const term = filter.title.toLowerCase();
      result = result.filter(t =>
        t.title.toLowerCase().includes(term)
      );
    }
    if (filter.notes) {
      const term = filter.notes.toLowerCase();
      result = result.filter(t =>
        (t.notes || "").toLowerCase().includes(term)
      );
    }
    if (filter.limit) {
      result = result.slice(0, filter.limit);
    }

    setTrainings(result);
  };

  // 3) Smazání – jen pro admina, jinak alert
  // const deleteTraining = async id => {
  //   if (role !== "admin") {
  //     toast.error("Nemáte oprávnění smazat trénink.");
  //     return;
  //   }
  //   if (!window.confirm("Opravdu smazat tento trénink?")) return;
  //   toast.success("Trénink byl úspěšně smazán…");

  //   try {
  //     await apiDelete(`/api/trainings/${id}`);
  //     setAllTrainings(prev => prev.filter(t => t.id !== id));
  //     setTrainings(prev    => prev.filter(t => t.id !== id));
  //   } catch (err) {
  //     alert(err.message);
  //   }
  // };
  const deleteTraining = id => {
  if (role !== "admin") {
    toast.error("Nemáte oprávnění smazat trénink.");
    return;
  }

  confirmAlert({
    title: "Potvrdit smazání",
    message: "Opravdu chcete smazat tento trénink?",
    buttons: [
      {
        label: "Ano, smazat",
        onClick: () => {
          // Potřebujeme volat async funkci z onClick → vytvoříme si vnitřní async funkci
          const doDelete = async () => {
            try {
              await apiDelete(`/api/trainings/${id}`);
              setAllTrainings(prev => prev.filter(t => t.id !== id));
              setTrainings(prev => prev.filter(t => t.id !== id));
              toast.success("Trénink byl úspěšně smazán.");
            } catch (err) {
              toast.error(`Chyba při mazání tréninku: ${err.message}`);
            }
          };

          doDelete(); // Spuštění async funkce
        }
      },
      {
        label: "Zrušit",
        onClick: () => {
          toast.info("Mazání bylo zrušeno.");
        }
      }
    ]
  });
};

    

  // 4) Přesměrování na vytvoření – jen pro admina
  const createTraining = () => {
    if (role !== "admin") {
      toast.error("Nemáte oprávnění vytvořit nový trénink.");
      return;
    }
    navigate("/trainings/create");
  };

  return (
    <div className="card shadow-sm">
      <div className="card-body">
        <h1 className="card-title">Seznam tréninků</h1>

        {error && (
          <div className="alert alert-danger mb-3">
            Chyba: {error}
          </div>
        )}

        <TrainingFilter onFilter={applyFilter} />

        <TrainingTable
          items={trainings}
          deleteTraining={deleteTraining}
        />

        <button
          className="btn btn-success mt-3"
          onClick={createTraining}
        >
          Vytvořit nový trénink
        </button>
      </div>
    </div>
  );
}
