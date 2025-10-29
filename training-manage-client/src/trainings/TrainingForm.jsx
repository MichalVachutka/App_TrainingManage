
import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { apiGet, apiPost, apiPut } from "../utils/api";
import {
  Card,
  Row,
  Col,
  Form,
  Button,
  Alert,
  Spinner,
} from "react-bootstrap";

export default function TrainingForm() {
  const { id } = useParams();
  const navigate = useNavigate();

  // defaultní date-time ve formátu YYYY-MM-DDThh:mm
  const nowLocal = new Date().toISOString().substring(0, 16);
  const [training, setTraining] = useState({
    title: "",
    date: nowLocal,
    notes: "",
    rentCost: "450"
  });

  const [status, setStatus] = useState({
    loading: !!id,
    error: "",
    success: false,
  });

  useEffect(() => {
    if (!id) {
      setStatus(s => ({ ...s, loading: false }));
      return;
    }

    apiGet(`/api/trainings/${id}`)
      .then(dto => {
        setTraining({
          title: dto.title,
          date: new Date(dto.date).toISOString().substring(0, 16),
          notes: dto.notes || "",
          rentCost: dto.rentCost?.toString() ?? "450"
        });
      })
      .catch(err =>
        setStatus(s => ({ ...s, error: err.toString() }))
      )
      .finally(() =>
        setStatus(s => ({ ...s, loading: false }))
      );
  }, [id]);

  const handleChange = e => {
    const { name, value } = e.target;
    setTraining(t => ({ ...t, [name]: value }));
  };

  const handleSubmit = async e => {
    e.preventDefault();
    setStatus(s => ({ ...s, loading: true, error: "", success: false }));

    const payload = {
      title: training.title,
      date: training.date,       // YYYY-MM-DDThh:mm
      notes: training.notes,
      rentCost: parseFloat(training.rentCost)
    };

    try {
      if (id) {
        await apiPut(`/api/trainings/${id}`, payload);
      } else {
        await apiPost("/api/trainings", payload);
      }
      setStatus(s => ({ ...s, success: true }));
      setTimeout(() => navigate("/trainings"), 500);
    } catch (err) {
      setStatus(s => ({ ...s, error: err.toString() }));
    } finally {
      setStatus(s => ({ ...s, loading: false }));
    }
  };

  if (status.loading) {
    return (
      <div className="text-center my-4">
        <Spinner animation="border" />
      </div>
    );
  }

  return (
    <Card className="mb-4">
      <Card.Header as="h5">
        {id ? "Upravit trénink" : "Nový trénink"}
      </Card.Header>
      <Card.Body>
        {status.error && <Alert variant="danger">{status.error}</Alert>}
        {status.success && <Alert variant="success">Uloženo!</Alert>}

        <Form onSubmit={handleSubmit}>
          <Row className="mb-3">
            <Form.Group as={Col} md="6">
              <Form.Label>Datum a čas</Form.Label>
              <Form.Control
                type="datetime-local"
                name="date"
                required
                value={training.date}
                onChange={handleChange}
              />
            </Form.Group>
          </Row>

          <Row className="mb-3">
            <Form.Group as={Col} md="6">
              <Form.Label>Název</Form.Label>
              <Form.Control
                type="text"
                name="title"
                required
                value={training.title}
                onChange={handleChange}
              />
            </Form.Group>
          </Row>

          <Row className="mb-3">
            <Form.Group as={Col} md="4">
              <Form.Label>Cena pronájmu (Kč)</Form.Label>
              <Form.Control
                type="number"
                name="rentCost"
                step="0.01"
                required
                className="text-end"
                value={training.rentCost}
                onChange={handleChange}
              />
              <Form.Text className="text-muted">
                Zadejte 0, pokud cvičíte venku.
              </Form.Text>
            </Form.Group>
          </Row>

          <Form.Group className="mb-3">
            <Form.Label>Poznámka</Form.Label>
            <Form.Control
              as="textarea"
              rows={2}
              name="notes"
              value={training.notes}
              onChange={handleChange}
            />
          </Form.Group>

          <div className="mt-4 d-flex gap-2">
            <Button variant="secondary" onClick={() => navigate(-1)}>
              Zpět na seznam tréninků
            </Button>
            <Button type="submit" variant="primary">
              Uložit
            </Button>
          </div>
        </Form>
      </Card.Body>
    </Card>
  );
}


// Posledni uprava kdyztak pouzit predchozi kod ktery funguje


