import React, { useState } from "react";
import { Link } from "react-router-dom";
import { Button } from "react-bootstrap";
import {
  FaTrash,
  FaChevronDown,
  FaChevronUp,
  FaEye,
  FaEdit
} from "react-icons/fa";
import "../styles/PersonStyle.css";

export default function PersonTable({ items, deletePerson }) {
  const [expandedId, setExpandedId] = useState(null);
  const toggleRow = id => setExpandedId(prev => (prev === id ? null : id));

  return (
    <div className="table-responsive">
      <table className="table table-striped table-bordered mb-0">
        <thead>
          <tr>
            <th style={{ width: "10%" }}>#</th>
            <th style={{ width: "60%" }}>Jméno</th>
            <th style={{ width: "30%" }} className="text-center">
              Akce
            </th>
          </tr>
        </thead>
        <tbody>
          {items.map((item, idx) => (
            <React.Fragment key={item.id}>
              {/* hlavní řádek */}
              <tr>
                <td>{idx + 1}</td>
                <td>{item.name}</td>
                <td className="text-center">
                  {/* akce pro ≥md: jen ikony */}
                  <div className="d-none d-md-flex justify-content-center gap-2">
                    <Link
                      to={`/persons/show/${item.id}`}
                      className="btn btn-sm btn-info icon-btn"
                      title="Detail"
                    >
                      <FaEye />
                    </Link>
                    <Link
                      to={`/persons/edit/${item.id}`}
                      className="btn btn-sm btn-warning icon-btn"
                      title="Upravit"
                    >
                      <FaEdit />
                    </Link>
                    <Button
                      size="sm"
                      variant="danger"
                      className="icon-btn"
                      onClick={() => deletePerson(item.id)}
                      title="Odstranit"
                    >
                      <FaTrash />
                    </Button>
                  </div>

                  {/* toggle pro <md */}
                  <span
                    className="d-md-none text-secondary"
                    style={{ cursor: "pointer", fontSize: "1.2rem" }}
                    onClick={() => toggleRow(item.id)}
                  >
                    {expandedId === item.id 
                      ? <FaChevronUp /> 
                      : <FaChevronDown />}
                  </span>
                </td>
              </tr>

              {/* rozbalená akce pro <md */}
              {expandedId === item.id && (
                <tr className="action-row d-md-none">
                  <td colSpan={3}>
                    <div className="d-flex justify-content-center flex-wrap gap-2">
                      <Link
                        to={`/persons/show/${item.id}`}
                        className="btn btn-sm btn-info icon-btn"
                        title="Detail"
                      >
                        <FaEye />
                      </Link>
                      <Link
                        to={`/persons/edit/${item.id}`}
                        className="btn btn-sm btn-warning icon-btn"
                        title="Upravit"
                      >
                        <FaEdit />
                      </Link>
                      <Button
                        size="sm"
                        variant="danger"
                        className="icon-btn"
                        onClick={() => deletePerson(item.id)}
                        title="Odstranit"
                      >
                        <FaTrash />
                      </Button>
                    </div>
                  </td>
                </tr>
              )}
            </React.Fragment>
          ))}
        </tbody>
      </table>
    </div>
  );
}







