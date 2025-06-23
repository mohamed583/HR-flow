/* eslint-disable no-unused-vars */
import React, { useState, useContext } from "react";
import {
  TextField,
  Button,
  Checkbox,
  FormControlLabel,
  Box,
  Typography,
} from "@mui/material";
import { AuthContext } from "../../context/AuthContext";
import logo from "../../assets/logo.png";
import { toast } from "react-toastify";
import { getMe } from "../../api/auth";
import { getEmployes } from "../../api/employe";
import { useNavigate } from "react-router-dom";

export default function Login({ toggleForm }) {
  const { login } = useContext(AuthContext);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [rememberMe, setRememberMe] = useState(false);
  const [error, setError] = useState("");

  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");
    try {
      await login({ email, password });

      // ✅ Étape 1 : Récupérer l'utilisateur connecté
      const user = await getMe();

      // ✅ Étape 2 : Récupérer tous les employés et filtrer par ID
      if (user.roles.includes("Employe")) {
        const allEmployes = await getEmployes();
        const employe = allEmployes.find((e) => e.id === user.id);

        // ✅ Étape 3 : Vérifier s’il est inactif
        if (employe && employe.statut === "Inactif") {
          localStorage.removeItem("accessToken");
          localStorage.removeItem("refreshToken");
          toast.error("Votre compte est bloqué !");
          return;
        }
      }
      toast.success("Connexion réussie !");
      // ✅ Redirection normale
      navigate("/");
    } catch (err) {
      setError(err.response?.data?.message || "Erreur de connexion");
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
          "linear-gradient(135deg, #A7C7E7, #B2E2B8, #F6C6D4, #FFF1B0, #D5C6E0)",
        backgroundSize: "1000% 1000%",
        animation: "tieDye 15s ease infinite",
        margin: 0, // Ajouté pour éviter toute marge
        padding: 0, // Ajouté pour éviter tout padding
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
          Connexion
        </Typography>
        <form onSubmit={handleSubmit}>
          <TextField
            label="Email"
            variant="outlined"
            fullWidth
            margin="normal"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
            type="email"
            sx={{ backgroundColor: "#E5E5E5", borderRadius: 1 }}
          />
          <TextField
            label="Mot de passe"
            variant="outlined"
            fullWidth
            margin="normal"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
            sx={{ backgroundColor: "#E5E5E5", borderRadius: 1 }}
          />
          <FormControlLabel
            control={
              <Checkbox
                checked={rememberMe}
                onChange={(e) => setRememberMe(e.target.checked)}
                sx={{ color: "#B2E2B8" }}
              />
            }
            label="Se souvenir de moi"
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
            Se connecter
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
          Pas de compte ? Inscrivez-vous ici
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
