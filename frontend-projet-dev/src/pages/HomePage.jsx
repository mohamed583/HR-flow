import React from "react";
import { Box, Typography, Grid, Paper } from "@mui/material";
// eslint-disable-next-line no-unused-vars
import { motion } from "framer-motion";

// Couleurs pastel pour le thème
const pastelColors = {
  primary: "#A7C7E7",
  secondary: "#B2E2B8",
  accent: "#F6C6D4",
  background: "#E5E5E5",
  text: "#333", // Gris foncé pour le texte
  yellow: "#FFF1B0",
  violet: "#D5C6E0",
  charcoal: "#333333", // Charcoal pour le titre
};

const HomePage = () => {
  return (
    <Box
      sx={{
        minHeight: "100vh", // Étend l'arrière-plan à toute la hauteur de la page
        mt: 0,
        padding: "20px",
        background: `linear-gradient(135deg, rgba(167, 199, 231, 0.8), rgba(213, 198, 224, 0.8))`, // Fond avec opacité
        display: "flex",
        flexDirection: "column",
        justifyContent: "center", // Centrer le contenu verticalement
      }}
    >
      <Box textAlign="center" mb={5}>
        <motion.div
          initial={{ opacity: 0, y: -50 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 1 }}
        >
          <Typography
            variant="h3"
            component="h1"
            sx={{
              color: pastelColors.charcoal, // Couleur charcoal pour le texte
              fontWeight: "bold",
              textShadow: "2px 2px 5px rgba(0, 0, 0, 0.3)", // Ombre pour améliorer la lisibilité
            }}
          >
            Bienvenue sur notre application RH
          </Typography>
        </motion.div>

        <motion.div
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ delay: 0.5, duration: 1 }}
        >
          <Typography variant="h6" sx={{ color: pastelColors.text }}>
            Simplifier la gestion des ressources humaines
          </Typography>
        </motion.div>
      </Box>

      <Grid container spacing={4} justifyContent="center">
        <Grid item xs={12} sm={6} md={4}>
          <motion.div
            initial={{ opacity: 0, y: 50 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 1 }}
          >
            <Paper
              sx={{
                p: 3,
                backgroundColor: pastelColors.secondary,
                borderRadius: 3,
                boxShadow: 3,
              }}
            >
              <Typography variant="h5" gutterBottom>
                Gestion des employés
              </Typography>
              <Typography>
                Gérez efficacement les données des employés, les processus
                d'intégration et de départ.
              </Typography>
            </Paper>
          </motion.div>
        </Grid>

        <Grid item xs={12} sm={6} md={4}>
          <motion.div
            initial={{ opacity: 0, y: 50 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 1, delay: 0.3 }}
          >
            <Paper
              sx={{
                p: 3,
                backgroundColor: pastelColors.yellow,
                borderRadius: 3,
                boxShadow: 3,
              }}
            >
              <Typography variant="h5" gutterBottom>
                Suivi des Formations
              </Typography>
              <Typography>
                Suivez facilement vos formations professionnelles.
              </Typography>
            </Paper>
          </motion.div>
        </Grid>

        <Grid item xs={12} sm={6} md={4}>
          <motion.div
            initial={{ opacity: 0, y: 50 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 1, delay: 0.6 }}
          >
            <Paper
              sx={{
                p: 3,
                backgroundColor: pastelColors.violet,
                borderRadius: 3,
                boxShadow: 3,
              }}
            >
              <Typography variant="h5" gutterBottom>
                Évaluations de performance
              </Typography>
              <Typography>
                Menez et gérez les évaluations de performance des employés de
                manière transparente.
              </Typography>
            </Paper>
          </motion.div>
        </Grid>
      </Grid>
    </Box>
  );
};

export default HomePage;
