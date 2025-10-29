// import React, { useEffect, useState } from "react";
// import { apiGet } from "../utils/api";
// import {
//   Chart as ChartJS,
//   CategoryScale,
//   LinearScale,
//   PointElement,
//   BarElement,
//   Legend,
//   Tooltip
// } from "chart.js";
// import { Bar } from "react-chartjs-2";

// ChartJS.register(CategoryScale, LinearScale, PointElement, BarElement, Legend, Tooltip);

// export default function IncomeVsExpenseStats({ personId }) {
//   const [data, setData] = useState(null);

//   useEffect(() => {
//     apiGet(`/api/people/${personId}/stats/income-vs-expense`)
//       .then(setData)
//       .catch(console.error);
//   }, [personId]);

//   if (!data) return <div>Načítám Příjmy vs. Výdaje…</div>;

//   const chartData = {
//     labels: data.labels,
//     datasets: [
//       {
//         label: "Příjmy",
//         data: data.income,
//         backgroundColor: "#28a745"
//       },
//       {
//         label: "Výdaje",
//         data: data.expense,
//         backgroundColor: "#dc3545"
//       }
//     ]
//   };

//   return <Bar data={chartData} options={{ responsive: true }} />;
// }
// src/components/IncomeVsExpenseStats.jsx

import React, { useEffect, useState } from "react";
import { apiGet } from "../utils/api";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  BarElement,
  Title,
  Tooltip,
  Legend
} from "chart.js";
import { Bar } from "react-chartjs-2";

ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  BarElement,
  Title,
  Tooltip,
  Legend
);

export default function IncomeVsExpenseStats({ personId }) {
  const [data, setData] = useState(null);

  useEffect(() => {
    apiGet(`/api/people/${personId}/stats/income-vs-expense`)
      .then(setData)
      .catch(console.error);
  }, [personId]);

  if (!data) return <div>Načítám Příjmy vs. Výdaje…</div>;

  const chartData = {
    labels: data.labels,
    datasets: [
      {
        label: "Příjmy",
        data: data.income,
        backgroundColor: "#28a745"
      },
      {
        label: "Výdaje",
        data: data.expense,
        backgroundColor: "#dc3545"
      }
    ]
  };

  const options = {
    responsive: true,
    maintainAspectRatio: false,
    animations: {
      tension: {
        duration: 1000,
        easing: "easeOutQuart",
        from: 0.5,
        to: 0
      },
      numbers: {
        duration: 800,
        easing: "linear",
        // postupné vykreslování sloupců
        from: ctx => 0,
        delay: ctx => ctx.dataIndex * 100
      }
    },
    plugins: {
      title: {
        display: true,
        text: "Příjmy vs. Výdaje",
        font: { size: 16 }
      },
      legend: {
        position: "bottom"
      },
      tooltip: {
        mode: "index",
        intersect: false
      }
    },
    scales: {
      y: {
        beginAtZero: true
      }
    }
  };

  return (
    <div className="stats-chart-container">
      <Bar data={chartData} options={options} />
    </div>
  );
}
