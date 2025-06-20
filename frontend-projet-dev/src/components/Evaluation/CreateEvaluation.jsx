/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import {
  Box,
  Button,
  Card,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  TextField,
  Typography,
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import { getMe } from "../../api/auth";
import { getEmployes, getListeEquipe } from "../../api/employe";
import { createEvaluation } from "../../api/evaluation";
import { toast } from "react-toastify";
import { FiSend } from "react-icons/fi";
import apiClient from "../../api/apiClient";
import "./CreateEvaluation.css";

const pastel = {
  blue: "#A7C7E7",
  green: "#B2E2B8",
  pink: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
  text: "#333",
};

const CreateEvaluation = () => {
  const [user, setUser] = useState(null);
  const [employes, setEmployes] = useState([]);
  const [formData, setFormData] = useState({
    employeId: "",
    dateEvaluation: "",
    description: "",
    objectifs: "",
  });

  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      const me = await getMe();
      setUser(me);

      let list = [];
      if (me.roles.includes("Admin")) {
        list = await getEmployes();
      } else if (me.roles.includes("Manager")) {
        list = await getListeEquipe();
      }
      setEmployes(list);
    };
    fetchData();
  }, []);

  if (!user) return <div>Chargement...</div>;

  if (!user.roles.includes("Admin") && !user.roles.includes("Manager")) {
    return (
      <Box className="access-denied-container">
        <Typography variant="h5">Accès refusé</Typography>
        <Typography>Vous n'avez pas les autorisations nécessaires.</Typography>
      </Box>
    );
  }

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await createEvaluation(formData);
      toast.success("Évaluation créée avec succès");
      navigate("/evaluations");
    } catch (error) {
      toast.error("Erreur lors de la création de l'évaluation");
    }
  };

  return (
    <Box className="create-evaluation-container">
      <Card className="create-evaluation-card">
        <Typography variant="h5" className="title">
          Nouvelle Évaluation
        </Typography>
        <form onSubmit={handleSubmit} className="form">
          <FormControl fullWidth className="form-item">
            <InputLabel>Employé</InputLabel>
            <Select
              name="employeId"
              value={formData.employeId}
              label="Employé"
              onChange={handleChange}
              required
            >
              {employes.map((e) => (
                <MenuItem key={e.id} value={e.id}>
                  {e.prenom} {e.nom}
                </MenuItem>
              ))}
            </Select>
          </FormControl>

          <TextField
            name="dateEvaluation"
            type="date"
            label="Date de l'évaluation"
            value={formData.dateEvaluation}
            onChange={handleChange}
            className="form-item"
            InputLabelProps={{ shrink: true }}
            required
          />

          <TextField
            name="description"
            label="Description"
            multiline
            rows={3}
            value={formData.description}
            onChange={handleChange}
            className="form-item"
            required
          />

          <TextField
            name="objectifs"
            label="Objectifs"
            multiline
            rows={3}
            value={formData.objectifs}
            onChange={handleChange}
            className="form-item"
            required
          />

          <Button
            type="submit"
            variant="contained"
            startIcon={<FiSend />}
            className="submit-button"
          >
            Créer
          </Button>
        </form>
      </Card>
    </Box>
  );
};

export default CreateEvaluation;
