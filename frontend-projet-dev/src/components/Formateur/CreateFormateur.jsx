/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getMe } from "../../api/auth";
import { createFormateur } from "../../api/formateur";
import { Box, TextField, Button, Typography } from "@mui/material";
import { toast } from "react-toastify";
import "./CreateFormateur.css";
import AccessDeniedImage from "../../assets/accessDenied.png";

const pastelColors = {
  primary: "#A7C7E7",
  secondary: "#B2E2B8",
  accent: "#F6C6D4",
  background: "#F8F8F8",
  text: "#333",
};

const CreateFormateur = () => {
  const navigate = useNavigate();
  const [isAdmin, setIsAdmin] = useState(false);
  const [userRole, setUserRole] = useState(null);
  const [formateur, setFormateur] = useState({
    nom: "",
    prenom: "",
    email: "",
    domaine: "",
    description: "",
    salaire: "",
    password: "",
  });

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const me = await getMe();
        setUserRole(me.roles);
        setIsAdmin(me.roles.includes("Admin"));
      } catch (error) {
        toast.error("Erreur lors de la vérification des rôles.");
      }
    };
    fetchUser();
  }, []);

  const handleChange = (e) => {
    setFormateur({ ...formateur, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const id = await createFormateur(formateur);
      toast.success("Formateur créé avec succès !");
      navigate(`/formateurs/`);
    } catch (error) {
      toast.error("Erreur lors de la création du formateur.");
    }
  };

  if (userRole === null) return <div>Chargement...</div>;
  if (!isAdmin) {
    return (
      <Box className="access-denied-container">
        <img src={AccessDeniedImage} alt="Accès refusé" />
        <Typography
          variant="h5"
          className="access-denied-message"
          sx={{ mb: 2 }}
        >
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
    <Box className="formateur-form-container">
      <Typography variant="h4" className="form-title">
        Créer un Formateur
      </Typography>
      <form onSubmit={handleSubmit} className="formateur-form">
        <TextField
          name="nom"
          label="Nom"
          value={formateur.nom}
          onChange={handleChange}
          required
          fullWidth
        />
        <TextField
          name="prenom"
          label="Prénom"
          value={formateur.prenom}
          onChange={handleChange}
          required
          fullWidth
        />
        <TextField
          name="email"
          label="Email"
          value={formateur.email}
          onChange={handleChange}
          required
          fullWidth
          type="email"
        />
        <TextField
          name="domaine"
          label="Domaine"
          value={formateur.domaine}
          onChange={handleChange}
          required
          fullWidth
        />
        <TextField
          name="description"
          label="Description"
          value={formateur.description}
          onChange={handleChange}
          multiline
          rows={3}
          fullWidth
        />
        <TextField
          name="salaire"
          label="Salaire"
          value={formateur.salaire}
          onChange={handleChange}
          required
          fullWidth
          type="number"
        />
        <TextField
          name="password"
          label="Mot de passe"
          value={formateur.password}
          onChange={handleChange}
          required
          fullWidth
          type="password"
        />
        <Button className="submit-button" type="submit">
          Créer
        </Button>
      </form>
    </Box>
  );
};

export default CreateFormateur;
