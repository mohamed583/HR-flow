/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getMe } from "../../api/auth";
import {
  Box,
  Typography,
  Card,
  Button,
  Divider,
  Grid,
  MenuItem,
  Select,
  FormControl,
  InputLabel,
} from "@mui/material";
import { getCandidatureDetails } from "../../api/candidature";
import { getEmployes } from "../../api/employe";
import { getAllConges } from "../../api/conge";
import { getAllInscriptions } from "../../api/inscription";
import { getAllEvaluations } from "../../api/evaluation";
import { getAllPaies } from "../../api/paie";
import { getEntretiens } from "../../api/entretien";
import { getDetails } from "../../api/employe";
import { getFormationById } from "../../api/formation";
import { toast } from "react-toastify";
import "./EmployeDetails.css";

const pastelColors = {
  blue: "#A7C7E7",
  green: "#B2E2B8",
  pink: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
};

const EmployeDetails = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const [userRole, setUserRole] = useState(null);
  const [employe, setEmploye] = useState(null);
  const [conges, setConges] = useState([]);
  const [inscriptions, setInscriptions] = useState([]);
  const [evaluations, setEvaluations] = useState([]);
  const [paies, setPaies] = useState([]);
  const [entretiens, setEntretiens] = useState([]);
  const [selectedList, setSelectedList] = useState("conges");
  const [subOption, setSubOption] = useState("");
  const [employes, setEmployes] = useState([]);
  const [candidaturesMap, setCandidaturesMap] = useState({});
  const [formationNames, setFormationNames] = useState({});
  const contratLabels = ["CDD", "CDI", "Stagiaire", "Externe", "Alternant"];
  const statutLabels = ["Actif", "Conge", "Formation", "Inactif"];
  const statutDecisionLabels = ["En cours", "Approuvé", "Rejeté"];
  useEffect(() => {
    const fetchData = async () => {
      try {
        const me = await getMe();
        if (!me.roles.includes("Admin")) {
          setUserRole("denied");
          return;
        }
        const allEmployes = await getEmployes();
        setEmployes(allEmployes);
        const details = await getDetails(id);
        setEmploye(details);

        const allConges = await getAllConges();
        setConges(allConges.filter((c) => c.employeId === id));

        const allInscriptions = await getAllInscriptions();
        const filteredInscriptions = allInscriptions.data.filter(
          (i) => i.employeId === id
        );
        setInscriptions(filteredInscriptions);

        // Fetch formation names
        const formationMap = {};
        for (const insc of filteredInscriptions) {
          try {
            const formation = await getFormationById(insc.formationId);
            formationMap[insc.formationId] = formation.data.titre;
          } catch {
            formationMap[insc.formationId] = "Nom inconnu";
          }
        }
        setFormationNames(formationMap);

        const allEvals = await getAllEvaluations();
        setEvaluations(allEvals.data);

        const allPaies = await getAllPaies();
        setPaies(allPaies.data.filter((p) => p.personneId === id));

        const allEntretiens = await getEntretiens();
        setEntretiens(allEntretiens);

        setUserRole("admin");
      } catch (error) {
        toast.error("Erreur lors du chargement des données");
      }
    };
    fetchData();
  }, [id]);
  useEffect(() => {
    if (selectedList === "evaluations") {
      setSubOption("employe");
    } else if (selectedList === "entretiens") {
      setSubOption("responsable");
    } else {
      setSubOption("");
    }
  }, [selectedList]);
  useEffect(() => {
    const fetchMissingCandidatures = async () => {
      const missing = entretiens.filter(
        (e) =>
          !e.newEmployeId &&
          e.candidatureId &&
          !candidaturesMap[e.candidatureId]
      );

      const fetchedCandidatures = {};
      for (const entretien of missing) {
        try {
          const details = await getCandidatureDetails(entretien.candidatureId);
          fetchedCandidatures[
            entretien.candidatureId
          ] = `${details.candidat.prenom} ${details.candidat.nom}`;
        } catch (error) {
          fetchedCandidatures[entretien.candidatureId] = "Candidat inconnu";
        }
      }

      setCandidaturesMap((prev) => ({ ...prev, ...fetchedCandidatures }));
    };

    if (selectedList === "entretiens") {
      fetchMissingCandidatures();
    }
  }, [entretiens, selectedList]);
  if (userRole === null) return <div>Chargement...</div>;
  if (userRole === "denied") return <div>Accès refusé</div>;
  const getFullName = (id) => {
    const emp = employes.find((e) => e.id === id);
    return emp ? `${emp.prenom} ${emp.nom}` : "Inconnu";
  };
  const filteredEvaluations =
    subOption === "responsable"
      ? evaluations.filter((e) => e.responsableId === id)
      : evaluations.filter((e) => e.employeId === id);

  const filteredEntretiens =
    subOption === "responsable"
      ? entretiens.filter((e) => e.employeId === id)
      : entretiens.filter((e) => e.newEmployeId === id);

  const renderList = () => {
    const handleDetail = (type, id) => {
      navigate(`/${type}/${id}`);
    };

    const cardStyle = {
      marginBottom: "1rem",
      padding: "1rem",
      backgroundColor: pastelColors.gray,
      borderRadius: "12px",
      boxShadow: "0 2px 8px rgba(0,0,0,0.1)",
    };

    const buttonStyle = {
      marginTop: "0.5rem",
      backgroundColor: pastelColors.blue,
    };

    switch (selectedList) {
      case "conges":
        return conges.map((c) => (
          <Box key={c.id} sx={cardStyle}>
            <Typography>
              <strong>{c.type}</strong> : {c.dateDebut.slice(0, 10)} →{" "}
              {c.dateFin.slice(0, 10)}
            </Typography>
            <Typography>
              <strong>Statut</strong> : {statutDecisionLabels[c.statusConge]}
            </Typography>
          </Box>
        ));
      case "inscriptions":
        return inscriptions.map((i) => (
          <Box key={i.id} sx={cardStyle}>
            <Typography>
              <strong>Formation</strong> :{" "}
              {formationNames[i.formationId] || "Chargement..."}
            </Typography>
            <Typography>
              <strong>Statut</strong> : {i.statut}
            </Typography>
          </Box>
        ));
      case "evaluations":
        return filteredEvaluations.map((e) => (
          <Box key={e.id} sx={cardStyle}>
            <Typography>
              <strong>Date</strong> :{" "}
              {new Date(e.dateEvaluation).toLocaleDateString("fr-FR")}
            </Typography>
            <Typography>
              <strong>Objectifs</strong> : {e.objectifs}
            </Typography>
            <Typography>
              {subOption === "employe" ? "Par" : "Pour"} :{" "}
              {subOption === "employe"
                ? getFullName(e.responsableId)
                : getFullName(e.employeId)}
            </Typography>
            <Button
              variant="contained"
              sx={buttonStyle}
              onClick={() => handleDetail("evaluation", e.id)}
            >
              Détail
            </Button>
          </Box>
        ));
      case "paies":
        return paies.map((p) => (
          <Box key={p.id} sx={cardStyle}>
            <Typography>
              <strong>Date</strong> :{" "}
              {new Date(p.datePaie).toLocaleDateString("fr-FR")}
            </Typography>
            <Typography>
              <strong>Description</strong> : {p.description}
            </Typography>
            <Typography>
              <strong>Montant</strong> : {p.montant} €
            </Typography>
            <Button
              variant="contained"
              sx={buttonStyle}
              onClick={() => handleDetail("paie", p.id)}
            >
              Détail
            </Button>
          </Box>
        ));
      case "entretiens":
        return filteredEntretiens.map((e) => {
          let nomAffiche;
          if (subOption === "employe") {
            nomAffiche = getFullName(e.employeId);
          } else {
            if (e.newEmployeId) {
              nomAffiche = getFullName(e.newEmployeId);
            } else {
              nomAffiche = candidaturesMap[e.candidatureId] || "Chargement...";
            }
          }

          return (
            <Box key={e.id} sx={cardStyle}>
              <Typography>
                <strong>Date</strong> :{" "}
                {new Date(e.dateEntretien).toLocaleDateString("fr-FR")}
              </Typography>
              <Typography>
                {subOption === "employe" ? "Par" : "Pour"} : {nomAffiche}
              </Typography>
              <Button
                variant="contained"
                sx={buttonStyle}
                onClick={() => handleDetail("entretien", e.id)}
              >
                Détail
              </Button>
            </Box>
          );
        });
      default:
        return null;
    }
  };

  return (
    <Box className="employe-detail-container">
      <Card className="employe-info-card">
        <Typography variant="h5">
          {employe.nom} {employe.prenom}
        </Typography>
        <Typography>Métier : {employe.metier}</Typography>
        <Typography>Salaire : {employe.salaire} €</Typography>
        <Typography>Adresse : {employe.adresse}</Typography>
        <Typography>
          Date Naissance : {employe.dateNaissance.slice(0, 10)}
        </Typography>
        <Typography>
          Date Embauche : {employe.dateEmbauche.slice(0, 10)}
        </Typography>
        <Typography>Contrat : {contratLabels[employe.contrat]}</Typography>
        <Typography>Statut : {statutLabels[employe.statut]}</Typography>

        <Box className="action-buttons">
          <Button
            variant="contained"
            style={{ backgroundColor: pastelColors.violet }}
            onClick={() => navigate(`/employe/${id}/edit`)}
          >
            Modifier
          </Button>
          <Button
            variant="contained"
            style={{ backgroundColor: pastelColors.pink }}
            onClick={() => navigate(`/auth/${id}/change-login`)}
          >
            Changer identifiants
          </Button>
        </Box>
      </Card>

      <Card className="list-card">
        <FormControl fullWidth sx={{ mb: 2 }}>
          <InputLabel>Choisir la liste</InputLabel>
          <Select
            value={selectedList}
            label="Choisir la liste"
            onChange={(e) => {
              setSelectedList(e.target.value);
              setSubOption("");
            }}
          >
            <MenuItem value="conges">Congés</MenuItem>
            <MenuItem value="inscriptions">Inscriptions</MenuItem>
            <MenuItem value="evaluations">Évaluations</MenuItem>
            <MenuItem value="paies">Paies</MenuItem>
            <MenuItem value="entretiens">Entretiens</MenuItem>
          </Select>
        </FormControl>

        {(selectedList === "evaluations" || selectedList === "entretiens") && (
          <FormControl fullWidth sx={{ mb: 2 }}>
            <InputLabel>Vue</InputLabel>
            <Select
              value={subOption}
              label="Vue"
              onChange={(e) => setSubOption(e.target.value)}
            >
              <MenuItem value="employe">Employé</MenuItem>
              <MenuItem value="responsable">Responsable</MenuItem>
            </Select>
          </FormControl>
        )}

        <ul>{renderList()}</ul>
      </Card>
    </Box>
  );
};

export default EmployeDetails;
