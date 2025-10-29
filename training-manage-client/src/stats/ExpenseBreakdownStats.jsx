// src/components/ExpenseBreakdownStats.jsx

import React, { useEffect, useState } from "react";
import { apiGet } from "../utils/api";
import "../styles/StatisticsStyle.css";

import {
  Chart as ChartJS,
  ArcElement,
  Tooltip,
  Legend,
  Title
} from "chart.js";
import { Pie } from "react-chartjs-2";

ChartJS.register(ArcElement, Tooltip, Legend, Title);

export default function ExpenseBreakdownStats({ personId }) {
  const [data, setData] = useState(null);

  useEffect(() => {
    apiGet(`/api/people/${personId}/stats/expense-breakdown`)
      .then(setData)
      .catch(console.error);
  }, [personId]);

  if (!data) return <div>Načítám rozložení výdajů…</div>;

  const colors = ["#007bff", "#ffc107", "#17a2b8", "#6c757d"];
  const chartData = {
    labels: data.categories,
    datasets: [
      {
        data: data.values,
        backgroundColor: colors.slice(0, data.categories.length),
      }
    ]
  };

  const options = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      title: {
        display: true,
        text: "Rozložení výdajů"
      },
      legend: {
        position: "bottom"
      }
    }
  };

  return (
    <div className="stats-chart-container">
      <Pie data={chartData} options={options} />
    </div>
  );
}
