
import React, { Suspense, lazy } from "react";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
  NavLink,
  useLocation
} from "react-router-dom";

import { AnimatePresence, motion } from "framer-motion";
import { Navbar, Nav, Container, Spinner } from "react-bootstrap";
import { FaUser, FaDumbbell, FaMoneyBill } from "react-icons/fa";
import AuthGate from './components/AuthGate';
import Footer from "./components/Footer";
import "react-toastify/dist/ReactToastify.css";
import "bootswatch/dist/flatly/bootstrap.min.css";
import { ToastContainer } from "react-toastify";

const PersonIndex     = lazy(() => import("./persons/PersonIndex"));
const PersonDetail    = lazy(() => import("./persons/PersonDetail"));
const PersonForm      = lazy(() => import("./persons/PersonForm"));
const TrainingIndex   = lazy(() => import("./trainings/TrainingIndex"));
const TrainingDetail  = lazy(() => import("./trainings/TrainingDetail"));
const TrainingForm    = lazy(() => import("./trainings/TrainingForm"));
const ExpenseIndex    = lazy(() => import("./expenses/ExpenseIndex"));
const ExpenseDetail   = lazy(() => import("./expenses/ExpenseDetail"));
const ExpenseForm     = lazy(() => import("./expenses/ExpenseForm"));
const StatsPage       = lazy(() => import("./pages/StatsPage"));


function Loader() {
  return (
    <div className="text-center my-5">
      <Spinner animation="border" role="status" />
    </div>
  );
}

function AnimatedRoutes() {
  const location = useLocation();

  return (
    <AnimatePresence exitBeforeEnter initial={false}>
      <Routes location={location} key={location.pathname}>
        <Route index element={<Navigate to="/persons" replace />} />

        {/* Persons */}
        <Route path="/persons">
          <Route index element={<PersonIndex />} />
          <Route
            path="show/:id"
            element={
              <motion.div
                initial={{ opacity: 0, x: 20 }}
                animate={{ opacity: 1, x: 0 }}
                exit={{ opacity: 0, x: -20 }}
                transition={{ duration: 0.4, ease: "easeOut" }}
              >
                <PersonDetail />
              </motion.div>
            }
          />
          <Route path="create" element={<PersonForm />} />
          <Route path="edit/:id" element={<PersonForm />} />
        </Route>

        {/* Trainings */}
        <Route path="/trainings">
          <Route index element={<TrainingIndex />} />
          <Route
            path="show/:id"
            element={
              <motion.div
                initial={{ opacity: 0, y: 20 }}
                animate={{ opacity: 1, y: 0 }}
                exit={{ opacity: 0, y: -20 }}
                transition={{ duration: 0.5, ease: "easeOut" }}
              >
                <TrainingDetail />
              </motion.div>
            }
          />
          <Route path="create" element={<TrainingForm />} />
          <Route path="edit/:id" element={<TrainingForm />} />
        </Route>

        {/* Expenses */}
        <Route path="/expenses">
          <Route index element={<ExpenseIndex />} />
          <Route
            path="show/:id"
            element={
              <motion.div
                initial={{ opacity: 0, y: 20 }}
                animate={{ opacity: 1, y: 0 }}
                exit={{ opacity: 0, y: -20 }}
                transition={{ duration: 0.5, ease: "easeOut" }}
              >
                <ExpenseDetail />
              </motion.div>
            }
          />
          <Route path="create" element={<ExpenseForm />} />
          <Route path="edit/:id" element={<ExpenseForm />} />
        </Route>

        {/* Stats */}
        <Route
          path="/stats"
          element={
            <motion.div
              initial={{ opacity: 0, scale: 0.95 }}
              animate={{ opacity: 1, scale: 1 }}
              exit={{ opacity: 0, scale: 0.95 }}
              transition={{ duration: 0.4 }}
            >
              <StatsPage />
            </motion.div>
          }
        />

        {/* Fallback */}
        <Route path="*" element={<Navigate to="/persons" replace />} />
      </Routes>
    </AnimatePresence>
  );
}

export default function App() {
  return (
    <AuthGate>
    <Router>
      {/* Navbar */}
      <Navbar
        bg="primary"
        variant="dark"
        expand="lg"
        sticky="top"
        className="mb-4 shadow-sm"
      >
        <Container>
          <Navbar.Brand as={NavLink} to="/">
            TrainingManage
          </Navbar.Brand>
          <Navbar.Toggle aria-controls="mainNav" />
          <Navbar.Collapse id="mainNav">
            <Nav className="ms-auto gap-2">
              <Nav.Link as={NavLink} to="/persons">
                <FaUser className="me-1" /> People
              </Nav.Link>
              <Nav.Link as={NavLink} to="/trainings">
                <FaDumbbell className="me-1" /> Trainings
              </Nav.Link>
              <Nav.Link as={NavLink} to="/expenses">
                <FaMoneyBill className="me-1" /> Expenses
              </Nav.Link>
              <Nav.Link as={NavLink} to="/stats">
                Statistiky
              </Nav.Link>
            </Nav>
          </Navbar.Collapse>
        </Container>
      </Navbar>

      {/* Main content with animated routes */}
      <Container>
        <Suspense fallback={<Loader />}>
          <AnimatedRoutes />
        </Suspense>
      </Container>

      <ToastContainer
        position="top-center"
        autoClose={3000}
        hideProgressBar={false}
        newestOnTop
        closeOnClick
        pauseOnHover
      />

    </Router>  
    <Footer />
    </AuthGate>
  );
}























