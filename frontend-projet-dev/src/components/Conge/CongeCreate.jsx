/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getMe } from "../../api/auth";
import { createConge } from "../../api/conge";
import {
  Box,
  Typography,
  TextField,
  Button,
  MenuItem,
  Card,
} from "@mui/material";
import { toast } from "react-toastify";
import AccessDeniedImage from "../../assets/accessDenied.png";
import "./CongeCreate.css";

const pastel = {
  blue: "#A7C7E7",
  green: "#B2E2B8",
  pink: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
  text: "#333",
};

const congeTypes = [
  "conge paye",
  "conge sans solde",
  "conge maternite",
  "conge paternite",
  "conge maladie",
  "deces dun proche",
  "exception",
];

const CongeCreate = () => {
  const navigate = useNavigate();

  const [user, setUser] = useState(null);
  const [formData, setFormData] = useState({
    dateDebut: "",
    dateFin: "",
    type: "",
  });

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const me = await getMe();
        setUser(me);
      } catch {
        toast.error("Erreur lors de la récupération de l'utilisateur.");
      }
    };
    fetchUser();
  }, []);

  if (user === null) return <div>Chargement...</div>;

  if (!user.roles.includes("Employe")) {
    return (
      <Box className="access-denied-container">
        <img src={AccessDeniedImage} alt="Accès refusé" />
        <Typography variant="h5" className="access-denied-message">
          Accès refusé
        </Typography>
        <Typography className="access-denied-message">
          Vous n'avez pas les autorisations nécessaires pour créer un congé.
        </Typography>
      </Box>
    );
  }

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((old) => ({ ...old, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!formData.dateDebut || !formData.dateFin || !formData.type) {
      toast.error("Veuillez remplir tous les champs.");
      return;
    }

    if (new Date(formData.dateFin) < new Date(formData.dateDebut)) {
      toast.error("La date de fin doit être après la date de début.");
      return;
    }

    try {
      await createConge(formData);
      toast.success("Congé créé avec succès !");
      navigate("/conges"); // redirection vers la page liste congés
    } catch {
      toast.error("Erreur lors de la création du congé.");
    }
  };

  return (
    <Box className="conge-create-container">
      <Card className="conge-create-card">
        <Typography variant="h5" sx={{ mb: 3, color: pastel.text }}>
          Créer un nouveau congé
        </Typography>

        <form onSubmit={handleSubmit} className="conge-create-form" noValidate>
          <TextField
            label="Date de début"
            name="dateDebut"
            type="date"
            value={formData.dateDebut}
            onChange={handleChange}
            fullWidth
            required
            margin="normal"
            InputLabelProps={{ shrink: true }}
          />

          <TextField
            label="Date de fin"
            name="dateFin"
            type="date"
            value={formData.dateFin}
            onChange={handleChange}
            fullWidth
            required
            margin="normal"
            InputLabelProps={{ shrink: true }}
          />

          <TextField
            label="Type de congé"
            name="type"
            select
            value={formData.type}
            onChange={handleChange}
            fullWidth
            required
            margin="normal"
          >
            {congeTypes.map((type) => (
              <MenuItem key={type} value={type}>
                {type.charAt(0).toUpperCase() + type.slice(1)}
              </MenuItem>
            ))}
          </TextField>

          <Box sx={{ display: "flex", justifyContent: "flex-end", mt: 3 }}>
            <Button
              type="submit"
              variant="contained"
              sx={{
                backgroundColor: pastel.blue,
                color: pastel.text,
                fontWeight: "bold",
                borderRadius: "12px",
                textTransform: "none",
                "&:hover": {
                  backgroundColor: "#8db8dd",
                },
              }}
            >
              Créer
            </Button>
          </Box>
        </form>
      </Card>
    </Box>
  );
};

export default CongeCreate;
