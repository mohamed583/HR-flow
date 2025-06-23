/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import {
  Box,
  Typography,
  Button,
  Card,
  MenuItem,
  Select,
  FormControl,
  InputLabel,
} from "@mui/material";
import { getEmployes, getListeEquipe } from "../../api/employe";
import { getMe } from "../../api/auth";
import { toast } from "react-toastify";
import "./ListeCollegues.css";

const pastelColors = {
  blue: "#A7C7E7",
  green: "#B2E2B8",
  pink: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
};

const ListeCollegues = () => {
  const [userRole, setUserRole] = useState(null);
  const [employes, setEmployes] = useState([]);
  const [filter, setFilter] = useState("all");

  const fetchEmployes = async () => {
    try {
      const data =
        filter === "team" ? await getListeEquipe() : await getEmployes();
      const filtered = data.filter((e) => e.statut !== "Inactif");
      setEmployes(filtered);
    } catch (error) {
      toast.error("Erreur lors du chargement des collègues");
    }
  };

  useEffect(() => {
    const checkRoleAndFetch = async () => {
      try {
        const me = await getMe();
        if (!me.roles.includes("Employe")) {
          setUserRole("denied");
          return;
        }
        setUserRole("employe");
        fetchEmployes();
      } catch (error) {
        toast.error("Erreur d'autorisation");
      }
    };
    checkRoleAndFetch();
  }, [filter]);

  if (userRole === null) return <div>Chargement...</div>;
  if (userRole === "denied")
    return <Typography>Accès refusé - Employés uniquement</Typography>;

  return (
    <Box className="collegues-container">
      <Typography variant="h5" className="collegues-title">
        Liste de mes collègues
      </Typography>

      <FormControl className="collegues-select">
        <InputLabel>Filtrer</InputLabel>
        <Select
          value={filter}
          label="Filtrer"
          onChange={(e) => setFilter(e.target.value)}
        >
          <MenuItem value="all">Tous les employés</MenuItem>
          <MenuItem value="team">Mon équipe</MenuItem>
        </Select>
      </FormControl>

      <Box className="collegues-list">
        {employes.length === 0 ? (
          <Typography>Aucun collègue trouvé.</Typography>
        ) : (
          employes.map((emp) => (
            <Card key={emp.id} className="collegue-card">
              <Typography className="collegue-name">
                {emp.nom} {emp.prenom}
              </Typography>
              <Typography className="collegue-metier">{emp.metier}</Typography>
              <Typography
                className="collegue-statut"
                style={{ color: pastelColors.green }}
              >
                {emp.statut}
              </Typography>
            </Card>
          ))
        )}
      </Box>
    </Box>
  );
};

export default ListeCollegues;
