// src/components/IncomeExpensePieStats.jsx
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

export default function IncomeExpensePieStats({ personId }) {
  const [stats, setStats] = useState(null);

  useEffect(() => {
    apiGet(`/api/people/${personId}/stats/income-vs-expense`)
      .then(data => {
        // data.income a data.expense jsou pole měsíčních hodnot
        const totalIncome  = data.income.reduce((sum, x) => sum + x, 0);
        const totalExpense = data.expense.reduce((sum, x) => sum + x, 0);
        setStats({ totalIncome, totalExpense });
      })
      .catch(console.error);
  }, [personId]);

  if (!stats) return <div>Načítám poměr příjmů a výdajů…</div>;

  const chartData = {
    labels: ["Příjmy", "Výdaje"],
    datasets: [{
      data: [stats.totalIncome, stats.totalExpense],
      backgroundColor: ["#28a745", "#dc3545"]
    }]
  };

  const options = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      title: {
        display: true,
        text: "Poměr celkových příjmů a výdajů"
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
