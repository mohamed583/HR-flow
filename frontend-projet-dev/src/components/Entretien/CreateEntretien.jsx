/* eslint-disable no-unused-vars */
// src/components/Entretien/CreateEntretien.jsx
import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import {
  Box,
  Typography,
  Card,
  Button,
  TextField,
  MenuItem,
} from "@mui/material";
import { toast } from "react-toastify";
import { getMe } from "../../api/auth";
import { getEmployes } from "../../api/employe";
import { createEntretien } from "../../api/entretien";
import AccessDeniedImage from "../../assets/accessDenied.png";
import "./CreateEntretien.css";

const pastelColors = {
  primary: "#A7C7E7",
  secondary: "#B2E2B8",
  accent: "#F6C6D4",
  background: "#F8F8F8",
  text: "#333",
};

const CreateEntretien = () => {
  const { candidatureId } = useParams();
  const navigate = useNavigate();
  const [isAdmin, setIsAdmin] = useState(false);
  const [employes, setEmployes] = useState([]);
  const [selectedEmploye, setSelectedEmploye] = useState("");
  const [date, setDate] = useState("");

  useEffect(() => {
    const fetchData = async () => {
      try {
        const me = await getMe();
        if (me.roles.includes("Admin")) {
          setIsAdmin(true);
          const data = await getEmployes();
          setEmployes(data);
        }
      } catch (error) {
        toast.error("Erreur lors du chargement.");
      }
    };

    fetchData();
  }, []);

  const handleSubmit = async () => {
    try {
      await createEntretien({
        model: {
          candidatureId,
          employeId: selectedEmploye,
          dateEntretien: date,
        },
      });
      toast.success("Entretien créé avec succès");
      navigate(-1);
    } catch (error) {
      toast.error("Erreur lors de la création de l'entretien");
    }
  };

  if (!isAdmin) {
    return (
      <Box className="access-denied-container">
        <img src={AccessDeniedImage} alt="Accès refusé" />
        <Typography variant="h5" className="access-denied-message">
          Accès refusé
        </Typography>
        <Typography className="access-denied-message">
          Vous n'avez pas les autorisations nécessaires pour consulter cette
          page.
        </Typography>
      </Box>
    );
  }

  return (
    <Box className="entretien-form-container">
      <Card className="entretien-form-card">
        <Typography variant="h5" className="form-title">
          Créer un Entretien
        </Typography>
        <TextField
          select
          label="Employé"
          fullWidth
          margin="normal"
          value={selectedEmploye}
          onChange={(e) => setSelectedEmploye(e.target.value)}
        >
          {employes.map((e) => (
            <MenuItem key={e.id} value={e.id}>
              {e.nom} {e.prenom}
            </MenuItem>
          ))}
        </TextField>
        <TextField
          type="datetime-local"
          label="Date Entretien"
          fullWidth
          margin="normal"
          InputLabelProps={{ shrink: true }}
          value={date}
          onChange={(e) => setDate(e.target.value)}
        />
        <Button
          variant="contained"
          style={{ backgroundColor: pastelColors.secondary, marginTop: 16 }}
          onClick={handleSubmit}
        >
          Créer l'entretien
        </Button>
      </Card>
    </Box>
  );
};

export default CreateEntretien;
