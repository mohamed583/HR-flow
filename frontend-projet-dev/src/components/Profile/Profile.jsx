/* eslint-disable no-unused-vars */
/* Fichier : src/components/Profile/Profile.jsx */
import React, { useEffect, useState } from "react";
import {
  Box,
  Card,
  Typography,
  TextField,
  Button,
  MenuItem,
} from "@mui/material";
import { changeLoginInfo, getMe } from "../../api/auth";
import { getEmployes, changerStatut, getDetails } from "../../api/employe";
import { toast } from "react-toastify";
import "./Profile.css";

const pastelColors = {
  blue: "#A7C7E7",
  green: "#B2E2B8",
  pink: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
};

const statutLabels = ["Actif", "Conge", "Formation"];
const contratLabels = ["CDD", "CDI", "Stagiaire", "Externe", "Alternant"];

const Profile = () => {
  const [user, setUser] = useState(null);
  const [selectedStatut, setSelectedStatut] = useState(0);
  const [newPassword, setPassword] = useState("");
  const [newEmail, setEmail] = useState("");
  const [isEmploye, setIsEmploye] = useState(false);
  const [employe, setEmploye] = useState(null);
  useEffect(() => {
    const fetch = async () => {
      try {
        const me = await getMe();
        setUser(me);
        setEmail(me.email);
        setIsEmploye(me.roles.includes("Employe"));

        // Récupérer le statut depuis getEmployes()
        if (me.roles.includes("Employe")) {
          const employes = await getEmployes();
          const matched = employes.find((emp) => emp.id === me.id);
          if (matched) {
            const statusIndex = statutLabels.findIndex(
              (s) => s.toLowerCase() === matched.statut.toLowerCase()
            );
            const employeDetails = await getDetails(me.id);
            setEmploye(employeDetails);
            setSelectedStatut(statusIndex !== -1 ? statusIndex : 0);
          }
        }
      } catch (error) {
        toast.error("Erreur lors du chargement des données.");
      }
    };

    fetch();
  }, []);

  const handleStatutChange = async () => {
    try {
      await changerStatut({ statut: selectedStatut });
      toast.success("Statut mis à jour");
    } catch {
      toast.error("Erreur de mise à jour du statut");
    }
  };

  const handleLoginUpdate = async () => {
    try {
      await changeLoginInfo(user.id, { newEmail, newPassword });
      toast.success("Identifiants mis à jour");
    } catch {
      toast.error("Erreur de mise à jour des identifiants");
    }
  };

  if (!user) return <div>Chargement...</div>;

  return (
    <Box className="profile-container">
      {/* Statut (visible uniquement pour Employe) */}
      {isEmploye && (
        <Card className="profile-card">
          <Typography variant="h6">Statut actuel</Typography>
          <TextField
            select
            label="Statut"
            value={selectedStatut}
            onChange={(e) => setSelectedStatut(Number(e.target.value))}
            fullWidth
            margin="normal"
          >
            {statutLabels.map((s, i) => (
              <MenuItem key={i} value={i}>
                {s}
              </MenuItem>
            ))}
          </TextField>
          <Button
            variant="contained"
            style={{ backgroundColor: pastelColors.green }}
            onClick={handleStatutChange}
          >
            Mettre à jour le statut
          </Button>
        </Card>
      )}

      {/* Infos */}
      <Card className="profile-card">
        <Typography variant="h6">Mes informations</Typography>
        <Typography>
          <strong>Nom :</strong> {user.nom}
        </Typography>
        <Typography>
          <strong>Prénom :</strong> {user.prenom}
        </Typography>
        <Typography>
          <strong>Email :</strong> {user.email}
        </Typography>
        {isEmploye && employe && (
          <>
            <Typography>
              <strong>Métier :</strong> {employe.metier}
            </Typography>
            <Typography>
              <strong>Salaire :</strong> {employe.salaire} €
            </Typography>
            <Typography>
              <strong>Adresse :</strong> {employe.adresse}
            </Typography>
            <Typography>
              <strong>Date Naissance :</strong>{" "}
              {employe.dateNaissance.slice(0, 10)}
            </Typography>
            <Typography>
              <strong>Date Embauche :</strong>{" "}
              {employe.dateEmbauche.slice(0, 10)}
            </Typography>
            <Typography>
              <strong>Contrat :</strong> {contratLabels[employe.contrat]}
            </Typography>
          </>
        )}

        <Typography>
          <strong>Rôle :</strong> {user.roles.join(", ")}
        </Typography>
      </Card>

      {/* Modifier login */}
      <Card className="profile-card">
        <Typography variant="h6">Changer mes identifiants</Typography>
        <TextField
          label="Email"
          value={newEmail}
          onChange={(e) => setEmail(e.target.value)}
          fullWidth
          margin="normal"
          disabled={isEmploye}
        />
        <TextField
          label="Nouveau mot de passe"
          type="password"
          value={newPassword}
          onChange={(e) => setPassword(e.target.value)}
          fullWidth
          margin="normal"
        />
        <Button
          variant="contained"
          style={{ backgroundColor: pastelColors.violet }}
          onClick={handleLoginUpdate}
        >
          Mettre à jour mes identifiants
        </Button>
      </Card>
    </Box>
  );
};

export default Profile;
