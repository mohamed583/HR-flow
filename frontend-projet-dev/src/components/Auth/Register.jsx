import React, { useState, useContext } from "react";
import { TextField, Button, Box, Typography } from "@mui/material";
import { AuthContext } from "../../context/AuthContext";
import logo from "../../assets/logo.png";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";

export default function Register({ toggleForm }) {
  const { register } = useContext(AuthContext);
  const [form, setForm] = useState({
    nom: "",
    prenom: "",
    email: "",
    password: "",
    confirmPassword: "",
  });
  const [error, setError] = useState("");

  const handleChange = (e) => {
    setForm((f) => ({ ...f, [e.target.name]: e.target.value }));
  };
  const navigate = useNavigate();
  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");
    if (form.password !== form.confirmPassword) {
      setError("Les mots de passe ne correspondent pas");
      return;
    }
    try {
      await register(form);
      toast.success("Compte créé avec succés!");
      navigate("/");
    } catch (err) {
      setError(err.response?.data?.message || "Erreur lors de l'inscription");
      toast.error("Échec de la connexion " + err.response?.data?.message);
    }
  };

  return (
    <Box
      minHeight="100vh"
      display="flex"
      alignItems="center"
      justifyContent="center"
      sx={{
        background:
          "linear-gradient(135deg, #A7C7E7, #B2E2B8, #F6C6D4, #FFF1B0, #D5E0C6)",
        backgroundSize: "400% 400%",
        animation: "tieDye 15s ease infinite",
      }}
    >
      <Box
        maxWidth={400}
        width="100%"
        p={4}
        boxShadow={3}
        borderRadius={2}
        sx={{ backgroundColor: "#FFFFFFDD", position: "relative" }}
      >
        <img
          src={logo}
          alt="Logo"
          style={{
            position: "absolute",
            top: 16,
            left: 16,
            maxWidth: "20%",
            height: "auto",
          }}
        />
        <Typography variant="h5" mb={3} align="center" color="#A7C7E7">
          Inscription
        </Typography>
        <form onSubmit={handleSubmit}>
          <TextField
            label="Nom"
            variant="outlined"
            fullWidth
            margin="normal"
            name="nom"
            value={form.nom}
            onChange={handleChange}
            required
            sx={{ backgroundColor: "#E5E5E5", borderRadius: 1 }}
          />
          <TextField
            label="Prénom"
            variant="outlined"
            fullWidth
            margin="normal"
            name="prenom"
            value={form.prenom}
            onChange={handleChange}
            required
            sx={{ backgroundColor: "#E5E5E5", borderRadius: 1 }}
          />
          <TextField
            label="Email"
            variant="outlined"
            fullWidth
            margin="normal"
            name="email"
            type="email"
            value={form.email}
            onChange={handleChange}
            required
            sx={{ backgroundColor: "#E5E5E5", borderRadius: 1 }}
          />
          <TextField
            label="Mot de passe"
            variant="outlined"
            fullWidth
            margin="normal"
            name="password"
            type="password"
            value={form.password}
            onChange={handleChange}
            required
            sx={{ backgroundColor: "#E5E5E5", borderRadius: 1 }}
          />
          <TextField
            label="Confirmer mot de passe"
            variant="outlined"
            fullWidth
            margin="normal"
            name="confirmPassword"
            type="password"
            value={form.confirmPassword}
            onChange={handleChange}
            required
            sx={{ backgroundColor: "#E5E5E5", borderRadius: 1 }}
          />
          {error && (
            <Typography color="error" mb={2}>
              {error}
            </Typography>
          )}
          <Button
            type="submit"
            variant="contained"
            fullWidth
            sx={{
              mt: 2,
              backgroundColor: "#A7C7E7",
              "&:hover": { backgroundColor: "#89B2DA" },
            }}
          >
            Créer un compte
          </Button>
        </form>

        {/* LE LIEN SOUS LE BOUTON */}
        <Typography
          variant="body2"
          align="center"
          sx={{
            mt: 2,
            cursor: "pointer",
            color: "#A7C7E7",
            fontWeight: "bold",
          }}
          onClick={toggleForm}
        >
          Déjà un compte ? Connectez-vous ici
        </Typography>
      </Box>

      <style>
        {`
          @keyframes tieDye {
            0% { background-position: 0% 50%; }
            50% { background-position: 100% 50%; }
            100% { background-position: 0% 50%; }
          }
        `}
      </style>
    </Box>
  );
}
