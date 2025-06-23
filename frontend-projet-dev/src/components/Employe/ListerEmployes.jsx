import React, { useEffect, useState } from "react";
import { Box, Typography, Card, Button, Divider } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { getDepartements } from "../../api/departement";
import { getEquipesByDepartement } from "../../api/equipe";
import { getEmployesByEquipe } from "../../api/employe";
import { getMe } from "../../api/auth";
import AccessDeniedImage from "../../assets/accessDenied.png";
import "./ListerEmployes.css";

const pastelColors = {
  primary: "#A7C7E7",
  secondary: "#B2E2B8",
  accent: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
  text: "#333",
};

const ListerEmployes = () => {
  const [departements, setDepartements] = useState([]);
  const [loading, setLoading] = useState(true);
  const [isAdmin, setIsAdmin] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const me = await getMe();
        if (!me.roles.includes("Admin")) {
          setIsAdmin(false);
          return;
        }
        setIsAdmin(true);
        const deps = await getDepartements();
        const depsWithEquipes = await Promise.all(
          deps.map(async (dep) => {
            const equipes = await getEquipesByDepartement(dep.id);
            const equipesWithEmployes = await Promise.all(
              equipes.map(async (equipe) => {
                const employes = await getEmployesByEquipe(equipe.id);
                return { ...equipe, employes };
              })
            );
            return { ...dep, equipes: equipesWithEmployes };
          })
        );
        setDepartements(depsWithEquipes);
        setLoading(false);
      } catch (err) {
        console.error("Erreur lors du chargement des employés :", err);
      }
    };

    fetchData();
  }, []);

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

  if (loading) return <div>Chargement...</div>;

  return (
    <Box className="employes-container">
      <Typography variant="h4" sx={{ color: pastelColors.text, mb: 4 }}>
        Liste des employés
      </Typography>
      {departements.map((dep) => (
        <Box key={dep.id} className="departement-section">
          <Typography variant="h5" className="departement-title">
            Département : {dep.nom}
          </Typography>
          <Divider sx={{ mb: 2 }} />
          {dep.equipes.length === 0 ? (
            <Typography>Aucune équipe</Typography>
          ) : (
            dep.equipes.map((equipe) => (
              <Card key={equipe.id} className="equipe-card">
                <Typography variant="h6" className="equipe-title">
                  Équipe : {equipe.nom}
                </Typography>
                <Divider sx={{ my: 1 }} />
                {equipe.employes.length === 0 ? (
                  <Typography>Aucun employé</Typography>
                ) : (
                  equipe.employes.map((emp) => (
                    <Box
                      key={emp.id}
                      display="flex"
                      justifyContent="space-between"
                      alignItems="center"
                      className="employe-item"
                    >
                      <Typography>
                        {emp.nom} {emp.prenom} ({emp.metier}) Status:{" "}
                        {emp.statut}
                      </Typography>
                      <Button
                        variant="contained"
                        size="small"
                        sx={{ backgroundColor: pastelColors.primary }}
                        onClick={() => navigate(`/employe/${emp.id}`)}
                      >
                        Détails
                      </Button>
                    </Box>
                  ))
                )}
              </Card>
            ))
          )}
        </Box>
      ))}
    </Box>
  );
};

export default ListerEmployes;
