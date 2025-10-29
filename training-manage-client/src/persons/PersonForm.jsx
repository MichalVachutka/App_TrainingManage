
// // PersonForm.jsx
// // ----------------
// // Formulář pro vytvoření nebo úpravu osoby.
// // – Načte existující data při editaci (parametr id z URL).
// // – Volá POST nebo PUT podle režimu.

// import React, { useEffect, useState } from "react";
// import { useNavigate, useParams } from "react-router-dom";
// import { apiGet, apiPost, apiPut } from "../utils/api";
// import InputField from "../components/InputField";
// import InputCheck from "../components/InputCheck";
// import FlashMessage from "../components/FlashMessage";
// import "../styles/PersonStyle.css";

// /**
//  * Formulář osoby.
//  * URL:
//  *   /persons/create → create mode
//  *   /persons/edit/:id → edit mode
//  */
// export default function PersonForm() {
//   const navigate = useNavigate();          // hook pro přesměrování
//   const { id } = useParams();              // id osoby (pro edit)

//   // Stav formuláře (DTO PersonDto)
//   const [person, setPerson] = useState({
//     name: "",
//     identificationNumber: 0,
//     age: 0,
//     telephone: "",
//     email: "",
//     hidden: false,
//   });

//   // Stav odeslání a výsledku
//   const [sent, setSent] = useState(false);
//   const [success, setSuccess] = useState(false);
//   const [error, setError] = useState(null);

//   // Načíst existující osobu při editu
//   useEffect(() => {
//     if (id) {
//       apiGet(`/api/people/${id}`)
//         .then((data) => setPerson(data))
//         .catch((e) => setError(e.toString()));
//     }
//   }, [id]);

//   /**
//    * Odeslání formuláře (POST nebo PUT podle režimu).
//    */
//   const handleSubmit = (e) => {
//     e.preventDefault();
//     const request = id
//       ? apiPut(`/api/people/${id}`, person)
//       : apiPost("/api/people", person);

//     request
//       .then(() => {
//         setSent(true);
//         setSuccess(true);
//         navigate("/persons");
//       })
//       .catch((e) => {
//         setError(e.toString());
//         setSent(true);
//         setSuccess(false);
//       });
//   };

//   return (
//     <div className="card shadow-sm">
//       <div className="card-body">
//         <h1 className="card-title">{id ? "Upravit" : "Vytvořit"} osobu</h1>
//         <hr />

//         {error && <div className="alert alert-danger">{error}</div>}
//         {sent && <FlashMessage theme={success ? "success" : "danger"}
//                                 text={success ? "Uloženo úspěšně" : "Chyba při ukládání"} />}

//         <form onSubmit={handleSubmit}>
//           {/* Jméno */}
//           <InputField
//             required type="text" label="Jméno" prompt="Zadejte celé jméno"
//             value={person.name}
//             handleChange={e => setPerson({ ...person, name: e.target.value })}
//           />

//           {/* IČO */}
//           <InputField
//             required type="number" label="IČO" prompt="Zadejte IČO"
//             value={person.identificationNumber}
//             handleChange={e => setPerson({
//               ...person,
//               identificationNumber: parseInt(e.target.value, 10)
//             })}
//           />

//           {/* Věk */}
//           <InputField
//             required type="number" label="Věk" prompt="Zadejte věk osoby"
//             value={person.age}
//             handleChange={e => setPerson({
//               ...person,
//               age: parseInt(e.target.value, 10)
//             })}
//           />

//           {/* Telefon */}
//           <InputField
//             required type="text" label="Telefon" prompt="Zadejte telefonní číslo"
//             value={person.telephone}
//             handleChange={e => setPerson({ ...person, telephone: e.target.value })}
//           />

//           {/* Email */}
//           <InputField
//             required type="email" label="Email" prompt="Zadejte email"
//             value={person.email}
//             handleChange={e => setPerson({ ...person, email: e.target.value })}
//           />

//           {/* Soft‑delete checkbox */}
//           <InputCheck
//             type="checkbox" label="Skryto"
//             checked={person.hidden}
//             handleChange={e =>
//              setPerson({ ...person, hidden: e.target.checked }) 
//             }
//           />

//           {/* Tlačítka */}
//           <div className="mt-4 d-flex gap-2 person-form-actions">
//             <button type="button"
//             className="btn btn-secondary"
//             onClick={() => navigate(-1)}
//           >
//             Zpět na seznam osob
//           </button>
//             <button type="submit" className="btn btn-primary">Uložit</button>
//           </div>
//         </form>
//       </div>
//     </div>
//   );
// }


import React, { useEffect, useState, useRef } from "react";
import { useNavigate, useParams }    from "react-router-dom";
import { apiGet, apiPost, apiPut }    from "../utils/api";
import InputField                    from "../components/InputField";
import InputCheck                    from "../components/InputCheck";
import FlashMessage                  from "../components/FlashMessage";
import "../styles/PersonStyle.css";
import { useAuth }                   from "../components/AuthGate";
import { toast } from "react-toastify";

export default function PersonForm() {
  const navigate = useNavigate();
  const { id }   = useParams();
  const { role } = useAuth();

  // Ref, abychom jednou zobrazili alert a dál už ne
  const alertedRef = useRef(false);

  useEffect(() => {
    if (
      role &&                // až role přijde
      role !== "admin" &&    // a není admin
      !alertedRef.current    // a ještě jsme nealertovali
    ) {
      alertedRef.current = true;
      // alert("Nemáte oprávnění upravovat nebo vytvářet osoby.");
      toast.error("Nemáte oprávnění upravovat osoby.");
      navigate("/persons");
    }
  }, [role, navigate]);

  const [person, setPerson] = useState({
    name: "",
    identificationNumber: 0,
    age: 0,
    telephone: "",
    email: "",
    hidden: false,
  });
  const [sent,    setSent]    = useState(false);
  const [success, setSuccess] = useState(false);
  const [error,   setError]   = useState(null);

  useEffect(() => {
    if (id) {
      apiGet(`/api/people/${id}`)
        .then(data => setPerson(data))
        .catch(e => setError(e.toString()));
    }
  }, [id]);

  const handleSubmit = (e) => {
    e.preventDefault();
    const req = id
      ? apiPut(`/api/people/${id}`, person)
      : apiPost("/api/people", person);

    req
      .then(() => {
        // setSent(true);
        setSuccess(true);
        toast.success("Úspěšně uloženo.")
        navigate("/persons");
      })
      .catch(e => {
        
        // setSent(true);
        setSuccess(false);
        toast.success("Ćhyba při ukládání")
        setError(e.toString());
      })
      .finally(() => {
      setSent(true);
      });
  };

  return (
    <div className="card shadow-sm">
      <div className="card-body">
        <h1 className="card-title">{id ? "Upravit" : "Vytvořit"} osobu</h1>
        <hr />

        {error && <div className="alert alert-danger">{error}</div>}

        {/* {sent && (
          <FlashMessage
            theme={success ? "success" : "danger"}
            text={success ? "Uloženo úspěšně" : "Chyba při ukládání"}
          />
        )} */}
        {/* {sent && (
          useEffect(() => {
            if (!sent) return;

            if (success) {
              toast.success("Uloženo úspěšně.");
            } else {
              toast.error("Chyba při ukládání.");
            }
          }, [sent, success])
        )} */}

        <form onSubmit={handleSubmit}>
          <InputField
            required type="text" label="Jméno" prompt="Zadejte celé jméno"
            value={person.name}
            handleChange={e => setPerson({ ...person, name: e.target.value })}
          />
          <InputField
            required type="number" label="IČO" prompt="Zadejte IČO"
            value={person.identificationNumber}
            handleChange={e => setPerson({
              ...person,
              identificationNumber: parseInt(e.target.value, 10) || 0
            })}
          />
          <InputField
            required type="number" label="Věk" prompt="Zadejte věk osoby"
            value={person.age}
            handleChange={e => setPerson({
              ...person,
              age: parseInt(e.target.value, 10) || 0
            })}
          />
          <InputField
            required type="text" label="Telefon" prompt="Zadejte telefonní číslo"
            value={person.telephone}
            handleChange={e => setPerson({ ...person, telephone: e.target.value })}
          />
          <InputField
            required type="email" label="Email" prompt="Zadejte email"
            value={person.email}
            handleChange={e => setPerson({ ...person, email: e.target.value })}
          />
          <InputCheck
            type="checkbox" label="Skryto"
            checked={person.hidden}
            handleChange={e => setPerson({ ...person, hidden: e.target.checked })}
          />
          <div className="mt-4 d-flex gap-2 person-form-actions">
            <button
              type="button"
              className="btn btn-secondary"
              onClick={() => navigate(-1)}
            >
              Zpět na seznam osob
            </button>
            <button type="submit" className="btn btn-primary">
              Uložit
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
