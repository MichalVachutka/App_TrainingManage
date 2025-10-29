import React, { useEffect, useState } from "react";
import { apiGet } from "../utils/api";
import { Line } from "react-chartjs-2";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
} from "chart.js";

ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
);

export default function PersonStatistics({ personId }) {
  const [stats, setStats] = useState(null);

  useEffect(() => {
    apiGet(`/api/people/${personId}/stats`)
      .then((data) => setStats(data))
      .catch((err) => console.error(err));
  }, [personId]);

  if (!stats) {
    return <div>Načítám statistiky…</div>;
  }

  const data = {
    labels: stats.labels,
    datasets: [
      {
        label: "Platby za měsíc",
        data: stats.values,
        fill: false,
        borderColor: "#007bff",
        backgroundColor: "#007bff"
      }
    ]
  };

  const options = {
    responsive: true,
    plugins: {
      legend: { position: "top" },
      title: { display: true, text: "Měsíční součet plateb" }
    }
  };

  return <Line data={data} options={options} />;
}

