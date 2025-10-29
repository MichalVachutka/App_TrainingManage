


import React, { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";
import { apiGet, apiDelete } from "../utils/api";
import "../styles/TrainingStyle.css";
import ParticipantSummary from "./ParticipantSummary";
import RegistrationForm from "../registrations/RegistrationForm";
import { dateStringFormatter } from "../utils/dateStringFormatter";
import { toast } from "react-toastify";
import { confirmAlert } from 'react-confirm-alert';
import 'react-confirm-alert/src/react-confirm-alert.css';

import {
  FaTag,
  FaCalendarAlt,
  FaStickyNote,
  FaUsers,
  FaMoneyBillWave,
  FaHome
} from "react-icons/fa";
import { motion } from "framer-motion";
import { useAuth } from "../components/AuthGate";

export default function TrainingDetail() {
  const { id } = useParams();
  const auth   = useAuth() || {};
  const role   = auth.role;
  const userId = auth.user?.id;

  const [detail, setDetail] = useState(null);
  const [error, setError]  = useState("");

  // 1) Načtení detailu při mountu nebo změně id
  useEffect(() => {
    loadDetail();
  }, [id]);

  // 2) (volitelně) debug auth objektu
  useEffect(() => {
    console.log("AUTH object:", auth);
  }, [auth]);

  function loadDetail() {
    apiGet(`/api/trainings/${id}/detail`)
      .then(d => {
        d.training.date = new Date(d.training.date);
        setDetail(d);
        setError("");
      })
      .catch(err => setError(err.toString()));
  }

  const handleUnregister = async (regId, regPersonId) => {
    if (role !== "admin" && userId !== regPersonId) {
      toast.error("Nemáte oprávnění odhlásit tohoto účastníka.");
      return;
    }
    if (!window.confirm("Opravdu odhlásit?")) return;
    await apiDelete(`/api/registrations/${regId}`);
    loadDetail();
  };

  if (error) {
    return <div className="alert alert-danger">{error}</div>;
  }
  if (!detail) {
    return (
      <div className="text-center my-5">
        <div className="spinner-border" />
      </div>
    );
  }

  const {
    training,
    registrations,
    participantCount,
    totalCollected,
    rentCost
  } = detail;

  return (
    <motion.div
      className="container mt-4"
      initial={{ opacity: 0, y: 20 }}
      animate={{ opacity: 1, y: 0 }}
      exit={{ opacity: 0, y: -20 }}
      transition={{ duration: 0.5, ease: "easeOut" }}
    >
      {/* Detail tréninku */}
      <div className="card mb-4">
        <div className="card-header training-header">
          <h4>Trénink #{training.id}</h4>
        </div>
        <div className="card-body">
          <dl className="row mb-4 training-dl">
            <dt className="col-sm-3">
              <FaTag className="me-1 text-secondary" /> Název
            </dt>
            <dd className="col-sm-9">{training.title}</dd>

            <dt className="col-sm-3">
              <FaCalendarAlt className="me-1 text-secondary" /> Datum
            </dt>
            <dd className="col-sm-9">
              {dateStringFormatter(training.date, true, true)}
            </dd>

            <dt className="col-sm-3">
              <FaStickyNote className="me-1 text-secondary" /> Poznámka
            </dt>
            <dd className="col-sm-9">
              {training.notes || <em>žádná</em>}
            </dd>

            <dt className="col-sm-3">
              <FaHome className="me-1 text-secondary" /> Cena nájmu
            </dt>
            <dd className="col-sm-9">
              {rentCost?.toFixed(2)} Kč
            </dd>
          </dl>

          <div className="d-flex gap-2 flex-wrap mb-3 training-detail-actions">
            <Link to="/trainings" className="btn btn-secondary">
              ← Zpět na seznam
            </Link>
            {/* {role === "admin" && (
              <Link
                to={`/trainings/edit/${training.id}`}
                className="btn btn-primary"
              >
                Upravit trénink
              </Link>
            )} */}

   <Link
     to={`/trainings/edit/${training.id}`}
     className="btn btn-primary"
     onClick={e => {
       if (role !== "admin") {
         e.preventDefault()
         toast.error("Nemáte oprávnění upravovat trénink.")
       }
     }}
   >
     Upravit trénink
   </Link>


          </div>
        </div>
      </div>

      {/* Formulář pro novou registraci */}
      <div className="card mb-4">
        <div className="card-header">
          <h5>Přihlásit nového účastníka</h5>
        </div>
        <div className="card-body">
          <RegistrationForm trainingId={id} onDone={loadDetail} />
        </div>
      </div>

      {/* Seznam registrací */}
      <div className="card mb-4">
        <div className="card-header">
          <h5>
            <FaUsers className="me-1" />
            Účastníci ({participantCount})
          </h5>
        </div>
        <div className="card-body p-0">
          <table className="table table-hover mb-0 responsive-reflow">
            <thead className="table-light">
              <tr>
                <th data-label="#">#</th>
                <th data-label="Jméno">Jméno</th>
                <th className="payment-col" data-label="Platba">
                  <FaMoneyBillWave />
                </th>
                <th data-label="Poznámka">Poznámka</th>
                <th data-label="Akce">Akce</th>
              </tr>
            </thead>
            <tbody>
              {registrations.map((r, i) => (
                <tr key={r.id}>
                  <td data-label="#">{i + 1}</td>
                  <td data-label="Jméno">
                    <Link to={`/persons/show/${r.personId}`}>
                      {r.personName}
                    </Link>
                  </td>
                  <td className="payment-col" data-label="Platba">
                    {r.payment.toFixed(2)}
                  </td>
                  <td data-label="Poznámka">{r.note || ""}</td>
                  <td data-label="Akce">
                    {/* {(role === "admin" || userId === r.personId) && (
                      <button
                        className="btn btn-sm btn-danger"
                        onClick={() => handleUnregister(r.id, r.personId)}
                      >
                        Odhlásit
                      </button>
                    )} */}
                           <button
         className="btn btn-sm btn-danger"
         onClick={() => handleUnregister(r.id, r.personId)}
       >
         Odhlásit
       </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>

      {/* Souhrn */}
      <ParticipantSummary count={participantCount} total={totalCollected} />
    </motion.div>
  );
}



