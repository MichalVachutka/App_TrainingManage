// src/trainings/ParticipantSummary.jsx

// import React from "react";

// export default function ParticipantSummary({ count, total }) {
//   return (
//     <div className="card participant-summary mb-4">
//       <div className="card-body d-flex justify-content-between">
//         <div>
//           Počet účastníků: <strong>{count}</strong>
//         </div>
//         <div>
//           Celkem vybráno: <strong>{total.toFixed(2)} Kč</strong>
//         </div>
//       </div>
//     </div>
//   );
// }

// src/trainings/ParticipantSummary.jsx

import React from "react";

export default function ParticipantSummary({ count, total }) {
  return (
    <div className="card participant-summary mb-4">
      <div className="card-body d-flex justify-content-between align-items-center">
        <div className="summary-item">
          <span className="summary-label">Počet účastníků</span>
          <span className="summary-value">{count}</span>
        </div>
        <div className="summary-item">
          <span className="summary-label">Celkem vybráno</span>
          <span className="summary-value">{total.toFixed(2)} Kč</span>
        </div>
      </div>
    </div>
  );
}

