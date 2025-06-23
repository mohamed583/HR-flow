/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import {
  Box,
  Button,
  Card,
  TextField,
  Typography,
  MenuItem,
  FormControlLabel,
  Checkbox,
} from "@mui/material";
import { useNavigate, useParams } from "react-router-dom";
import {
  getCandidatureDetails,
  transformCandidatEnEmploye,
} from "../../api/candidature";

import { getEquipesByDepartement } from "../../api/equipe";
import { getDepartements } from "../../api/departement";
import { getMe } from "../../api/auth";
import { toast } from "react-toastify";
import "./TransformEmploye.css";

const pastelColors = {
  blue: "#A7C7E7",
  green: "#B2E2B8",
  pink: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
};

const contrats = ["CDD", "CDI", "Stagiaire", "Externe", "Alternant"];

const TransformEmploye = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [userRole, setUserRole] = useState(null);
  const [formData, setFormData] = useState({});
  const [departements, setDepartements] = useState([]);
  const [equipes, setEquipes] = useState([]);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: type === "checkbox" ? checked : value,
    }));
  };

  const fetchEquipes = async (departementId) => {
    const res = await getEquipesByDepartement(departementId);
    setEquipes(res);
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const me = await getMe();
        if (!me.roles.includes("Admin")) {
          setUserRole("denied");
          return;
        }

        const candidature = await getCandidatureDetails(id);
        const deps = await getDepartements();

        setDepartements(deps);
        setFormData({
          candidatureId: candidature.id,
          candidatId: candidature.candidatId,
          nom: candidature.candidat.nom,
          prenom: candidature.candidat.prenom,
          email: "",
          password: "",
          metier: "",
          salaire: "",
          adresse: "",
          dateNaissance: "",
          contrat: 0,
          estManager: false,
          equipeId: "",
        });
        setUserRole("admin");
      } catch (error) {
        toast.error("Erreur de chargement.");
      }
    };

    fetchData();
  }, [id]);

  const handleSubmit = async () => {
    try {
      await transformCandidatEnEmploye(formData);
      toast.success("Transformation réussie !");
      navigate(`/employes`);
    } catch (error) {
      toast.error("Erreur lors de la transformation.");
    }
  };

  if (userRole === null) return <div>Chargement...</div>;
  if (userRole === "denied") return <Typography>Accès refusé</Typography>;

  return (
    <Box className="transform-employe-container">
      <Card className="transform-card">
        <Typography variant="h5" className="form-title">
          Transformer Candidat en Employé
        </Typography>

        <TextField
          label="Nom"
          value={formData.nom || ""}
          disabled
          fullWidth
          margin="normal"
        />

        <TextField
          label="Prénom"
          value={formData.prenom || ""}
          disabled
          fullWidth
          margin="normal"
        />

        <TextField
          label="Email"
          name="email"
          value={formData.email || ""}
          onChange={handleChange}
          fullWidth
          margin="normal"
        />

        <TextField
          label="Mot de passe"
          name="password"
          type="password"
          value={formData.password || ""}
          onChange={handleChange}
          fullWidth
          margin="normal"
        />

        <TextField
          label="Métier"
          name="metier"
          value={formData.metier || ""}
          onChange={handleChange}
          fullWidth
          margin="normal"
        />

        <TextField
          label="Adresse"
          name="adresse"
          value={formData.adresse || ""}
          onChange={handleChange}
          fullWidth
          margin="normal"
        />

        <TextField
          label="Salaire"
          name="salaire"
          type="number"
          value={formData.salaire || ""}
          onChange={handleChange}
          fullWidth
          margin="normal"
        />

        <TextField
          label="Date de naissance"
          type="date"
          name="dateNaissance"
          value={formData.dateNaissance || ""}
          onChange={handleChange}
          fullWidth
          margin="normal"
          InputLabelProps={{ shrink: true }}
        />

        <TextField
          select
          label="Contrat"
          name="contrat"
          value={formData.contrat}
          onChange={handleChange}
          fullWidth
          margin="normal"
        >
          {contrats.map((c, index) => (
            <MenuItem key={index} value={index}>
              {c}
            </MenuItem>
          ))}
        </TextField>

        <TextField
          select
          label="Département"
          onChange={async (e) => {
            const depId = e.target.value;
            setFormData((prev) => ({ ...prev, equipeId: "" }));
            await fetchEquipes(depId);
          }}
          fullWidth
          margin="normal"
        >
          {departements.map((d) => (
            <MenuItem key={d.id} value={d.id}>
              {d.nom}
            </MenuItem>
          ))}
        </TextField>

        <TextField
          select
          label="Équipe"
          name="equipeId"
          value={formData.equipeId}
          onChange={handleChange}
          fullWidth
          margin="normal"
        >
          {equipes.map((e) => (
            <MenuItem key={e.id} value={e.id}>
              {e.nom}
            </MenuItem>
          ))}
        </TextField>

        <FormControlLabel
          control={
            <Checkbox
              checked={formData.estManager}
              onChange={handleChange}
              name="estManager"
              style={{ color: pastelColors.blue }}
            />
          }
          label="Est Manager"
        />

        <Box className="form-buttons">
          <Button
            style={{ backgroundColor: pastelColors.green }}
            onClick={handleSubmit}
          >
            Transformer
          </Button>
          <Button
            style={{ backgroundColor: pastelColors.pink }}
            onClick={() => navigate(-1)}
          >
            Annuler
          </Button>
        </Box>
      </Card>
    </Box>
  );
};

export default TransformEmploye;
