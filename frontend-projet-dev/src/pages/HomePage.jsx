/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import { Box, Typography, Button, Grid, Paper } from "@mui/material";
import { motion } from "framer-motion";
import { getMe } from "../api/auth";
import { useNavigate } from "react-router-dom";

const pastelColors = {
  primary: "#A7C7E7",
  secondary: "#B2E2B8",
  accent: "#F6C6D4",
  background: "#E5E5E5",
  text: "#333",
  yellow: "#FFF1B0",
  violet: "#D5C6E0",
};

const HomePage = () => {
  const [role, setRole] = useState(null);
  const [user, setUser] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetch = async () => {
      const me = await getMe();
      setUser(me);
      if (me.roles.includes("Employe")) setRole("Employe");
      else if (me.roles.includes("Candidat")) setRole("Candidat");
      else if (me.roles.includes("Formateur")) setRole("Formateur");
    };
    fetch();
  }, []);

  const sectionVariant = {
    hidden: { opacity: 0, y: 40 },
    visible: { opacity: 1, y: 0, transition: { duration: 0.6 } },
  };

  const renderButtons = () => {
    switch (role) {
      case "Candidat":
        return (
          <Grid container spacing={4} justifyContent="center">
            <Grid item xs={12} md={5}>
              <motion.div variants={sectionVariant}>
                <Paper
                  elevation={3}
                  sx={{
                    p: 3,
                    borderRadius: 4,
                    background: pastelColors.secondary,
                  }}
                >
                  <Typography variant="h6" gutterBottom>
                    Découvrir les Postes
                  </Typography>
                  <Typography sx={{ color: pastelColors.text, mb: 2 }}>
                    Parcourez les offres d'emploi disponibles.
                  </Typography>
                  <Button
                    variant="contained"
                    sx={{ backgroundColor: pastelColors.primary }}
                    onClick={() => navigate("/postes")}
                  >
                    Voir les postes
                  </Button>
                </Paper>
              </motion.div>
            </Grid>
            <Grid item xs={12} md={5}>
              <motion.div variants={sectionVariant}>
                <Paper
                  elevation={3}
                  sx={{
                    p: 3,
                    borderRadius: 4,
                    background: pastelColors.accent,
                  }}
                >
                  <Typography variant="h6" gutterBottom>
                    Mes candidatures
                  </Typography>
                  <Typography sx={{ color: pastelColors.text, mb: 2 }}>
                    Suivez l'évolution de vos candidatures déposées.
                  </Typography>
                  <Button
                    variant="contained"
                    sx={{ backgroundColor: pastelColors.violet }}
                    onClick={() => navigate("/candidatures")}
                  >
                    Mes candidatures
                  </Button>
                </Paper>
              </motion.div>
            </Grid>
          </Grid>
        );

      case "Formateur":
        return (
          <Grid container justifyContent="center">
            <Grid item xs={12} md={6}>
              <motion.div variants={sectionVariant}>
                <Paper
                  elevation={3}
                  sx={{
                    p: 4,
                    borderRadius: 4,
                    background: pastelColors.violet,
                  }}
                >
                  <Typography variant="h6" gutterBottom>
                    Formations à animer
                  </Typography>
                  <Typography sx={{ color: pastelColors.text, mb: 2 }}>
                    Consultez les sessions à venir à gérer ou à animer.
                  </Typography>
                  <Button
                    variant="contained"
                    sx={{ backgroundColor: pastelColors.secondary }}
                    onClick={() => navigate("/formations")}
                  >
                    Voir les formations
                  </Button>
                </Paper>
              </motion.div>
            </Grid>
          </Grid>
        );

      case "Employe":
        return (
          <Grid container spacing={4} justifyContent="center">
            <Grid item xs={12} md={4}>
              <motion.div variants={sectionVariant}>
                <Paper
                  sx={{
                    p: 3,
                    borderRadius: 4,
                    background: pastelColors.secondary,
                  }}
                >
                  <Typography variant="h6" gutterBottom>
                    Congés
                  </Typography>
                  <Typography sx={{ color: pastelColors.text, mb: 2 }}>
                    Demandez et suivez vos congés facilement.
                  </Typography>
                  <Button
                    variant="contained"
                    sx={{ backgroundColor: pastelColors.primary }}
                    onClick={() => navigate("/conges")}
                  >
                    Accéder
                  </Button>
                </Paper>
              </motion.div>
            </Grid>
            <Grid item xs={12} md={4}>
              <motion.div variants={sectionVariant}>
                <Paper
                  sx={{
                    p: 3,
                    borderRadius: 4,
                    background: pastelColors.yellow,
                  }}
                >
                  <Typography variant="h6" gutterBottom>
                    Évaluations
                  </Typography>
                  <Typography sx={{ color: pastelColors.text, mb: 2 }}>
                    Consultez vos évaluations de performance.
                  </Typography>
                  <Button
                    variant="contained"
                    sx={{ backgroundColor: pastelColors.violet }}
                    onClick={() => navigate("/evaluations")}
                  >
                    Accéder
                  </Button>
                </Paper>
              </motion.div>
            </Grid>
            <Grid item xs={12} md={4}>
              <motion.div variants={sectionVariant}>
                <Paper
                  sx={{
                    p: 3,
                    borderRadius: 4,
                    background: pastelColors.accent,
                  }}
                >
                  <Typography variant="h6" gutterBottom>
                    Paies
                  </Typography>
                  <Typography sx={{ color: pastelColors.text, mb: 2 }}>
                    Consultez et visualisez vos paies.
                  </Typography>
                  <Button
                    variant="contained"
                    sx={{ backgroundColor: pastelColors.secondary }}
                    onClick={() => navigate("/paies")}
                  >
                    Accéder
                  </Button>
                </Paper>
              </motion.div>
            </Grid>
            <Grid item xs={12} md={4}>
              <motion.div variants={sectionVariant}>
                <Paper
                  sx={{
                    p: 3,
                    borderRadius: 4,
                    background: pastelColors.violet,
                  }}
                >
                  <Typography variant="h6" gutterBottom>
                    Inscriptions
                  </Typography>
                  <Typography sx={{ color: pastelColors.text, mb: 2 }}>
                    Gérez vos inscriptions aux sessions de formations internes.
                  </Typography>
                  <Button
                    variant="contained"
                    sx={{ backgroundColor: pastelColors.primary }}
                    onClick={() => navigate("/mes-inscriptions")}
                  >
                    Accéder
                  </Button>
                </Paper>
              </motion.div>
            </Grid>
          </Grid>
        );

      default:
        return (
          <Typography variant="body1" sx={{ mt: 5 }}>
            Chargement des données utilisateur...
          </Typography>
        );
    }
  };

  return (
    <Box
      sx={{
        minHeight: "100vh",
        background: `linear-gradient(135deg, rgba(167, 199, 231, 0.8), rgba(213, 198, 224, 0.8))`,
        padding: "40px 20px",
        textAlign: "center",
      }}
    >
      <motion.div
        initial={{ opacity: 0, y: -30 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.8 }}
      >
        {user && (
          <Typography
            variant="h4"
            sx={{
              color: pastelColors.text,
              fontWeight: "bold",
              mb: 3,
              textShadow: "1px 1px 4px rgba(0, 0, 0, 0.2)",
            }}
          >
            Bienvenue, {user.prenom} {user.nom}
          </Typography>
        )}
      </motion.div>

      <motion.div
        initial="hidden"
        animate="visible"
        variants={{
          visible: { transition: { staggerChildren: 0.2 } },
        }}
      >
        {renderButtons()}
      </motion.div>
    </Box>
  );
};

export default HomePage;
