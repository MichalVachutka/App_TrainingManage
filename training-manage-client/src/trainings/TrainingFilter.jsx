// src/trainings/TrainingFilter.jsx

// import React, { useState, useEffect } from "react";
// import { apiGet } from "../utils/api";
// import InputSelect from "../components/InputSelect";
// import InputField from "../components/InputField";

// /**
//  * Formulář pro zadání filtru tréninků.
//  *
//  * Props:
//  * - onFilter(filter|null) – funkce, která dostane aktuální filtr
//  *   (nebo null při resetu) a vyvolá načtení dat v nadřazené komponentě.
//  */
// export default function TrainingFilter({ onFilter }) {
//   // prázdný filtr (null znamená bez omezení)
//   const empty = {
//     buyerId: null,
//     sellerId: null,
//     product: "",
//     minPrice: null,
//     maxPrice: null,
//     limit: null,
//   };

//   const [persons, setPersons] = useState([]);
//   const [filter, setFilter] = useState(empty);

//   // jednorázově načteme osoby pro selecty
//   useEffect(() => {
//     apiGet("/api/people").then(setPersons).catch(console.error);
//   }, []);

//   // obecný handler všech polí
//   const handleChange = (e) => {
//     const { name, value } = e.target;
//     setFilter((f) => ({ ...f, [name]: value === "" ? null : value }));
//   };

//   const apply = (e) => {
//     e.preventDefault();
//     onFilter(filter);
//   };

//   const reset = (e) => {
//     e.preventDefault();
//     setFilter(empty);
//     onFilter(null);
//   };

//   return (
//     <form className="mb-3" onSubmit={apply}>
//       <div className="row g-2">
//         <div className="col-md">
//           <InputSelect
//             label="Kupující"
//             name="buyerId"
//             items={persons}
//             prompt="Všichni"
//             value={filter.buyerId || ""}
//             handleChange={handleChange}
//           />
//         </div>
//         <div className="col-md">
//           <InputSelect
//             label="Prodávající"
//             name="sellerId"
//             items={persons}
//             prompt="Všichni"
//             value={filter.sellerId || ""}
//             handleChange={handleChange}
//           />
//         </div>
//         <div className="col-md">
//           <InputField
//             type="text"
//             label="Produkt"
//             name="product"
//             prompt="Hledat"
//             value={filter.product}
//             handleChange={handleChange}
//           />
//         </div>
//         <div className="col-md">
//           <InputField
//             type="number"
//             label="Min. cena"
//             name="minPrice"
//             value={filter.minPrice || ""}
//             handleChange={handleChange}
//           />
//         </div>
//         <div className="col-md">
//           <InputField
//             type="number"
//             label="Max. cena"
//             name="maxPrice"
//             value={filter.maxPrice || ""}
//             handleChange={handleChange}
//           />
//         </div>
//         <div className="col-md">
//           <InputField
//             type="number"
//             label="Limit"
//             name="limit"
//             value={filter.limit || ""}
//             handleChange={handleChange}
//           />
//         </div>
//         <div className="col-auto align-self-end">
//           <button className="btn btn-primary me-1">Filtrovat</button>
//           <button className="btn btn-secondary" onClick={reset}>Obnovit</button>
//         </div>
//       </div>
//     </form>
//   );
// }

// src/trainings/TrainingFilter.jsx
// src/trainings/TrainingFilter.jsx

// src/trainings/TrainingFilter.jsx

import React, { useState } from "react";
import "../styles/TrainingStyle.css";

export default function TrainingFilter({ onFilter }) {
  const empty = {
    dateFrom: "",
    dateTo:   "",
    title:    "",
    notes:    "",
    limit:    ""
  };
  const [filter, setFilter] = useState(empty);

  const handleChange = e => {
    const { name, value } = e.target;
    setFilter(f => ({ ...f, [name]: value }));
  };

  const applyFilter = () => {
    const isEmpty = Object.values(filter).every(v => v === "");
    onFilter(
      isEmpty
        ? null
        : {
            dateFrom: filter.dateFrom || null,
            dateTo:   filter.dateTo   || null,
            title:    filter.title    || null,
            notes:    filter.notes    || null,
            limit:    filter.limit
              ? parseInt(filter.limit, 10)
              : null
          }
    );
  };

  const resetFilter = () => {
    setFilter(empty);
    onFilter(null);
  };

  return (
    <div className="training-filter mb-4">
      {/* formulář se vstupy */}
      <form className="row g-3 align-items-end training-filter-form justify-content-center">
        <div className="col-6 col-sm-4 col-md-2">
          <label htmlFor="dateFrom" className="form-label">Od data</label>
          <input
            type="date"
            id="dateFrom"
            name="dateFrom"
            className="form-control"
            value={filter.dateFrom}
            onChange={handleChange}
          />
        </div>

        <div className="col-6 col-sm-4 col-md-2">
          <label htmlFor="dateTo" className="form-label">Do data</label>
          <input
            type="date"
            id="dateTo"
            name="dateTo"
            className="form-control"
            value={filter.dateTo}
            onChange={handleChange}
          />
        </div>

        <div className="col-6 col-sm-4 col-md-2">
          <label htmlFor="title" className="form-label">Název</label>
          <input
            type="text"
            id="title"
            name="title"
            className="form-control"
            placeholder="Hledat"
            value={filter.title}
            onChange={handleChange}
          />
        </div>

        <div className="col-6 col-sm-4 col-md-2">
          <label htmlFor="notes" className="form-label">Poznámka</label>
          <input
            type="text"
            id="notes"
            name="notes"
            className="form-control"
            placeholder="Hledat"
            value={filter.notes}
            onChange={handleChange}
          />
        </div>

        <div className="col-6 col-sm-4 col-md-2">
          <label htmlFor="limit" className="form-label">Limit</label>
          <input
            type="number"
            id="limit"
            name="limit"
            className="form-control"
            placeholder="0"
            value={filter.limit}
            onChange={handleChange}
            min="1"
          />
        </div>
      </form>

      {/* tlačítka mimo form, uprostřed */}
      <div className="training-filter-buttons d-flex justify-content-center mt-3">
        <button
          type="button"
          className="btn btn-primary me-2"
          onClick={applyFilter}
        >
          Filtrovat
        </button>
        <button
          type="button"
          className="btn btn-secondary"
          onClick={resetFilter}
        >
          Obnovit
        </button>
      </div>
    </div>
  );
}




