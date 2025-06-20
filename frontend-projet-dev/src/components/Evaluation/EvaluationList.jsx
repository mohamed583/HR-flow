/* COMPONENT FILE: components/Evaluation/EvaluationList.jsx */

/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import {
  Box,
  Typography,
  Card,
  Button,
  Divider,
  Grid,
  MenuItem,
  FormControl,
  Select,
  InputLabel,
} from "@mui/material";
import { getMe } from "../../api/auth";
import {
  getMesEvaluations,
  getAllEvaluations,
  deleteEvaluation,
  lancerCampagne,
} from "../../api/evaluation";
import { getEmployes, getListeEquipe } from "../../api/employe";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { FiEye, FiCheck, FiTrash2, FiPlusCircle, FiPlay } from "react-icons/fi";
import "./EvaluationList.css";

const pastel = {
  blue: "#A7C7E7",
  green: "#B2E2B8",
  pink: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
  text: "#333",
};

const EvaluationList = () => {
  const [user, setUser] = useState(null);
  const [myEvaluations, setMyEvaluations] = useState([]);
  const [allEvaluations, setAllEvaluations] = useState([]);
  const [employesMap, setEmployesMap] = useState({});
  const [responsablesMap, setResponsablesMap] = useState({});
  const [selectedEmploye, setSelectedEmploye] = useState("");
  const [employesList, setEmployesList] = useState([]);
  const [equipeIds, setEquipeIds] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      const me = await getMe();
      setUser(me);

      const allEmployes = await getEmployes();
      const responsablesMapping = {};
      allEmployes.forEach((e) => {
        responsablesMapping[e.id] = `${e.prenom} ${e.nom}`;
      });
      setResponsablesMap(responsablesMapping);

      let filteredEmployes = [];
      if (me.roles.includes("Admin")) {
        filteredEmployes = allEmployes;
      } else if (me.roles.includes("Manager")) {
        filteredEmployes = await getListeEquipe();
        setEquipeIds(filteredEmployes.map((e) => e.id));
      }
      setEmployesList(filteredEmployes);

      const employesMapping = {};
      filteredEmployes.forEach((e) => {
        employesMapping[e.id] = `${e.prenom} ${e.nom}`;
      });
      setEmployesMap(employesMapping);

      if (me.roles.includes("Employe")) {
        const res = await getMesEvaluations();
        setMyEvaluations(res.data);
      }

      if (me.roles.includes("Admin") || me.roles.includes("Manager")) {
        const res = await getAllEvaluations();
        setAllEvaluations(res.data);
      }
    };
    fetchData();
  }, []);

  const handleDelete = async (id) => {
    if (window.confirm("Confirmer la suppression de l'évaluation ?")) {
      await deleteEvaluation(id);
      setAllEvaluations((prev) => prev.filter((e) => e.id !== id));
      toast.success("Évaluation supprimée");
    }
  };

  const handleLancerCampagne = async () => {
    await lancerCampagne();
    toast.success("Campagne lancée");
  };

  if (!user) return <div>Chargement...</div>;

  const evaluationsFiltrees = allEvaluations.filter((e) => {
    if (selectedEmploye !== "") {
      return e.employeId === selectedEmploye;
    }
    if (user.roles.includes("Manager") && !user.roles.includes("Admin")) {
      return equipeIds.includes(e.employeId);
    }
    return true;
  });

  return (
    <Box className="evaluation-container">
      {user.roles.includes("Employe") && (
        <>
          <Typography variant="h5" className="evaluation-title">
            Mes Évaluations
          </Typography>
          <Divider sx={{ mb: 2 }} />
          <Grid container spacing={2}>
            {myEvaluations.map((evalItem) => (
              <Grid item xs={12} md={6} key={evalItem.id}>
                <Card className="evaluation-card">
                  <Typography>
                    Responsable :{" "}
                    {responsablesMap[evalItem.responsableId] || "-"}
                  </Typography>
                  <Typography>
                    Date : {evalItem.dateEvaluation.slice(0, 10)}
                  </Typography>
                  <Box className="evaluation-actions">
                    <Button
                      className="btn-blue"
                      onClick={() => navigate(`/evaluation/${evalItem.id}`)}
                      startIcon={<FiEye />}
                    >
                      Détail
                    </Button>
                    {!evalItem.finaliseParEmploye &&
                      !evalItem.finaliseParManager && (
                        <Button
                          className="btn-green"
                          onClick={() =>
                            navigate(`/evaluation/${evalItem.id}/finalize`)
                          }
                        >
                          <FiCheck /> Finaliser
                        </Button>
                      )}
                  </Box>
                </Card>
              </Grid>
            ))}
          </Grid>
        </>
      )}

      {(user.roles.includes("Admin") || user.roles.includes("Manager")) && (
        <>
          <Box className="evaluation-header-actions">
            <Button
              className="btn-purple"
              onClick={() => navigate("/evaluation/create")}
              startIcon={<FiPlusCircle />}
            >
              Créer une évaluation
            </Button>
            {user.roles.includes("Admin") && (
              <Button
                className="btn-yellow"
                onClick={handleLancerCampagne}
                startIcon={<FiPlay />}
              >
                Lancer une campagne
              </Button>
            )}
          </Box>

          <FormControl fullWidth sx={{ mb: 3 }}>
            <InputLabel>Choisir un employé</InputLabel>
            <Select
              value={selectedEmploye}
              label="Choisir un employé"
              onChange={(e) => setSelectedEmploye(e.target.value)}
            >
              <MenuItem value="">Tous les employés</MenuItem>
              {employesList.map((emp) => (
                <MenuItem key={emp.id} value={emp.id}>
                  {emp.prenom} {emp.nom}
                </MenuItem>
              ))}
            </Select>
          </FormControl>

          <Typography variant="h5" className="evaluation-title">
            Évaluations de{" "}
            {selectedEmploye
              ? employesMap[selectedEmploye]
              : "tous les employés"}
          </Typography>
          <Divider sx={{ mb: 2 }} />
          <Grid container spacing={2}>
            {evaluationsFiltrees.map((evalItem) => (
              <Grid item xs={12} md={6} key={evalItem.id}>
                <Card className="evaluation-card">
                  <Typography>
                    Employé : {employesMap[evalItem.employeId] || "-"}
                  </Typography>
                  <Typography>
                    Responsable :{" "}
                    {responsablesMap[evalItem.responsableId] || "-"}
                  </Typography>
                  <Typography>
                    Date : {evalItem.dateEvaluation.slice(0, 10)}
                  </Typography>
                  <Box className="evaluation-actions">
                    <Button
                      className="btn-blue"
                      onClick={() => navigate(`/evaluation/${evalItem.id}`)}
                      startIcon={<FiEye />}
                    >
                      Détail
                    </Button>
                    {evalItem.finaliseParEmploye &&
                      !evalItem.finaliseParManager && (
                        <Button
                          className="btn-green"
                          onClick={() =>
                            navigate(`/evaluation/${evalItem.id}/finalize`)
                          }
                        >
                          <FiCheck /> Finaliser
                        </Button>
                      )}
                    {user.roles.includes("Admin") && (
                      <Button
                        className="btn-pink"
                        onClick={() => handleDelete(evalItem.id)}
                        startIcon={<FiTrash2 />}
                      >
                        Supprimer
                      </Button>
                    )}
                  </Box>
                </Card>
              </Grid>
            ))}
          </Grid>
        </>
      )}
    </Box>
  );
};

export default EvaluationList;
