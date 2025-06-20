/* eslint-disable no-unused-vars */
/* --- FICHIER: src/components/Formation/FormationList.jsx --- */
import React, { useEffect, useState } from "react";
import { getMe } from "../../api/auth";
import { getFormateurs } from "../../api/formateur";
import { getFormationsByFormateur, getFormations } from "../../api/formation";
import {
  Box,
  Typography,
  Card,
  MenuItem,
  Select,
  FormControl,
  InputLabel,
} from "@mui/material";
import { toast } from "react-toastify";
import "./FormationsList.css";
import AccessDeniedImage from "../../assets/accessDenied.png";

const colors = {
  blue: "#A7C7E7",
  green: "#B2E2B8",
  pink: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
  text: "#333",
};

const FormationList = () => {
  const [user, setUser] = useState(null);
  const [formations, setFormations] = useState([]);
  const [formateurs, setFormateurs] = useState([]);
  const [selectedFormateurId, setSelectedFormateurId] = useState("");

  const fetchFormationsFor = async (formateurId) => {
    try {
      const response = await getFormationsByFormateur(formateurId);
      setFormations(response);
    } catch (err) {
      toast.error("Erreur lors du chargement des formations");
    }
  };

  useEffect(() => {
    const init = async () => {
      try {
        const me = await getMe();
        setUser(me);

        if (me.roles.includes("Formateur")) {
          await fetchFormationsFor(me.id);
        } else if (me.roles.includes("Admin")) {
          const formateursList = await getFormateurs();
          setFormateurs(formateursList);
        } else if (me.roles.includes("Employe")) {
          const today = new Date();
          let all = [];

          const f = await getFormations();
          const filtered = f.filter(
            (formation) => new Date(formation.dateDebut) > today
          );
          all = [...all, ...filtered];

          setFormations(all);
        }
      } catch (err) {
        toast.error("Erreur utilisateur ou formations");
      }
    };
    init();
  }, []);

  const handleFormateurChange = (e) => {
    const id = e.target.value;
    setSelectedFormateurId(id);
    fetchFormationsFor(id);
  };

  if (!user) return <div>Chargement...</div>;
  if (user.roles.includes("Candidat")) {
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
    <Box className="formation-list-container">
      <Typography variant="h4" className="title">
        Liste des Formations
        {user.roles.includes("Admin") && (
          <Box display="flex" justifyContent="flex-end" mb={2}>
            <button
              className="create-button"
              onClick={() => (window.location.href = "/formation/create")}
            >
              + Créer une formation
            </button>
          </Box>
        )}
      </Typography>

      {user.roles.includes("Admin") && (
        <FormControl fullWidth className="select-formateur">
          <InputLabel>Choisir un formateur</InputLabel>
          <Select value={selectedFormateurId} onChange={handleFormateurChange}>
            {formateurs.map((formateur) => (
              <MenuItem key={formateur.id} value={formateur.id}>
                {formateur.nom} {formateur.prenom}
              </MenuItem>
            ))}
          </Select>
        </FormControl>
      )}

      <Box className="formation-cards">
        {formations.length === 0 ? (
          <Typography>Aucune formation disponible</Typography>
        ) : (
          formations.map((formation) => (
            <Card key={formation.id} className="formation-card">
              <Typography variant="h6">{formation.titre}</Typography>
              <Typography>{formation.description}</Typography>
              <Typography>
                Date début :{" "}
                {new Date(formation.dateDebut).toLocaleDateString()}
              </Typography>
              <Typography>
                Date fin : {new Date(formation.dateFin).toLocaleDateString()}
              </Typography>

              {user.roles.includes("Admin") && (
                <Typography>Coût : {formation.cout}€</Typography>
              )}

              <Box mt={2}>
                <button
                  className="details-button"
                  onClick={() =>
                    (window.location.href = `/formation/${formation.id}`)
                  }
                >
                  Détails
                </button>
              </Box>
            </Card>
          ))
        )}
      </Box>
    </Box>
  );
};

export default FormationList;
