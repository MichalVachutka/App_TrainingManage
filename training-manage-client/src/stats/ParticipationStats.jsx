import React, { useEffect, useState } from "react";
import { apiGet } from "../utils/api";
import "../styles/StatisticsStyle.css";

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
import { Line } from "react-chartjs-2";

ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend);

export default function ParticipationStats({ personId }) {
  const [data, setData] = useState(null);

  useEffect(() => {
    apiGet(`/api/people/${personId}/stats/participation`)
      .then(setData)
      .catch(console.error);
  }, [personId]);

  if (!data) return <div>Načítám účast…</div>;

  return (
    <Line
      data={{
        labels: data.labels,
        datasets: [{
          label: "Počet lekcí",
          data: data.values,
          borderColor: "#17a2b8",
          fill: false
        }]
      }}
      options={{ responsive: true, plugins: { title: { display: true, text: "Účast na trénincích" } } }}
    />
  );
}
