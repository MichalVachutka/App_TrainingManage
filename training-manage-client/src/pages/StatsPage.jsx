import React, { useEffect, useState } from "react";
import { apiGet } from "../utils/api";
import "../styles/StatisticsStyle.css";

import {
  Form,
  Spinner,
  Alert,
  Container,
  Row,
  Col
} from "react-bootstrap";

import PersonStats           from "../stats/PersonStats";
import IncomeVsExpenseStats  from "../stats/IncomeVsExpenseStats";
import IncomeExpensePieStats from "../stats/IncomeExpensePieStats";
import ExpenseBreakdownStats from "../stats/ExpenseBreakdownStats";
import ParticipationStats    from "../stats/ParticipationStats";

export default function StatsPage() {
  const [people, setPeople]         = useState([]);
  const [selectedId, setSelectedId] = useState(null);
  const [error, setError]           = useState("");

  useEffect(() => {
    apiGet("/api/people")
      .then(setPeople)
      .catch(err => setError(err.toString()));
  }, []);

  if (error) {
    return <Alert variant="danger">{error}</Alert>;
  }
  if (!people.length) {
    return (
      <div className="text-center my-5">
        <Spinner animation="border" />
      </div>
    );
  }

  return (
    <Container className="mt-4">
      <h3>Statistiky plateb</h3>

      <Form.Group className="mb-3" controlId="selectPerson">
        <Form.Label>Vyber osobu</Form.Label>
        <Form.Select
          defaultValue=""
          onChange={e => setSelectedId(+e.target.value)}
        >
          <option value="" disabled>– zvolte osobu –</option>
          {people.map(p => (
            <option key={p.id} value={p.id}>
              {p.name}
            </option>
          ))}
        </Form.Select>
      </Form.Group>

      {selectedId && (
        <>
          <Row className="mt-4">
            <Col>
              <h5>Měsíční platby</h5>
              <PersonStats personId={selectedId} />
            </Col>
          </Row>

          <Row className="mt-4">
            <Col>
              <h5>Příjmy vs. výdaje</h5>
              <IncomeVsExpenseStats personId={selectedId} />
            </Col>
          </Row>

          {/* --- ZDE BÝVAJÍ DVA PODOBNÉ GRAFY V PODÉLNÉ VĚTVI --- */}
          <div className="pie-charts mt-4">
            <div className="pie-chart">
              <h5>Poměr celkových příjmů a výdajů</h5>
              <IncomeExpensePieStats personId={selectedId} />
            </div>
            <div className="pie-chart">
              <h5>Rozložení výdajů</h5>
              <ExpenseBreakdownStats personId={selectedId} />
            </div>
          </div>

          <Row className="mt-4 mb-5">
            <Col>
              <h5>Účast na trénincích</h5>
              <ParticipationStats personId={selectedId} />
            </Col>
          </Row>
        </>
      )}
    </Container>
  );
}
