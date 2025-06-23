/* eslint-disable no-unused-vars */
// Fichier : src/components/Employe/EditEmploye.jsx
import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import {
  Box,
  Button,
  Card,
  TextField,
  Typography,
  MenuItem,
  FormControlLabel,
  Checkbox,
} from "@mui/material";
import { getMe } from "../../api/auth";
import { getDetails, editEmploye } from "../../api/employe";
import { toast } from "react-toastify";
import "./EditEmploye.css";
import { getDepartements } from "../../api/departement";
import { getEquipeDetails, getEquipesByDepartement } from "../../api/equipe";

const pastelColors = {
  blue: "#A7C7E7",
  green: "#B2E2B8",
  pink: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
};

const contratLabels = ["CDD", "CDI", "Stagiaire", "Externe", "Alternant"];
const statutLabels = ["Actif", "Conge", "Formation", "Inactif"];

const EditEmploye = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [userRole, setUserRole] = useState(null);
  const [formData, setFormData] = useState({});
  const [departements, setDepartements] = useState([]);
  const [equipes, setEquipes] = useState([]);
  const [selectedDepartementId, setSelectedDepartementId] = useState(null);
  const [loadingEquipes, setLoadingEquipes] = useState(false);

  useEffect(() => {
    const fetchEquipes = async () => {
      if (!selectedDepartementId) {
        setEquipes([]);
        return;
      }
      setLoadingEquipes(true);
      try {
        const equipesData = await getEquipesByDepartement(
          selectedDepartementId
        );
        setEquipes(equipesData || []);
        // Si équipe actuelle ne fait plus partie du nouveau département, reset equipeId
        if (!equipesData.find((e) => e.id === formData.equipeId)) {
          setFormData((prev) => ({ ...prev, equipeId: "" }));
        }
      } catch (error) {
        toast.error("Erreur lors du chargement des équipes");
        setEquipes([]);
      }
      setLoadingEquipes(false);
    };
    fetchEquipes();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [selectedDepartementId]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const me = await getMe();
        if (!me.roles.includes("Admin")) {
          setUserRole("denied");
          return;
        }
        const data = await getDetails(id);

        const equipeDetails = await getEquipeDetails(data.equipeId);
        const departementId = equipeDetails.departementId;
        setSelectedDepartementId(departementId);

        // Récupère tous les départements
        const departementsData = await getDepartements();
        setDepartements(departementsData);

        // Récupère toutes les équipes du département sélectionné
        const equipesData = await getEquipesByDepartement(departementId);
        setEquipes(equipesData || []);

        setFormData({
          id: id,
          nom: data.nom,
          prenom: data.prenom,
          salaire: data.salaire,
          metier: data.metier,
          adresse: data.adresse,
          dateEmbauche: data.dateEmbauche.slice(0, 10),
          dateNaissance: data.dateNaissance.slice(0, 10),
          contrat: data.contrat,
          statut: data.statut,
          equipeId: data.equipeId,
          estManager: data.estManager,
        });
        setUserRole("admin");
      } catch (error) {
        toast.error("Erreur lors du chargement de l'employé");
      }
    };
    fetchData();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [id]);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: type === "checkbox" ? checked : value,
    }));
  };

  const handleSubmit = async () => {
    try {
      await editEmploye(id, formData);
      toast.success("Employé modifié avec succès");
      navigate(`/employe/${id}`);
    } catch (error) {
      toast.error("Erreur lors de la modification");
    }
  };

  if (userRole === null) return <div>Chargement...</div>;
  if (userRole === "denied") return <Typography>Accès refusé</Typography>;

  return (
    <Box className="edit-employe-container">
      <Card className="edit-card">
        <Typography variant="h5">Modifier Employé</Typography>

        <TextField
          label="Nom"
          name="nom"
          value={formData.nom || ""}
          onChange={handleChange}
          fullWidth
          margin="normal"
        />

        <TextField
          label="Prénom"
          name="prenom"
          value={formData.prenom || ""}
          onChange={handleChange}
          fullWidth
          margin="normal"
        />

        <TextField
          label="Métier"
          name="metier"
          value={formData.metier || ""}
          onChange={handleChange}
          fullWidth
          margin="normal"
        />
        <TextField
          label="Adresse"
          name="adresse"
          value={formData.adresse || ""}
          onChange={handleChange}
          fullWidth
          margin="normal"
        />
        <TextField
          label="Salaire"
          name="salaire"
          type="number"
          value={formData.salaire || ""}
          onChange={handleChange}
          fullWidth
          margin="normal"
        />

        <TextField
          label="Date d'embauche"
          type="date"
          name="dateEmbauche"
          value={formData.dateEmbauche || ""}
          onChange={handleChange}
          fullWidth
          margin="normal"
          InputLabelProps={{ shrink: true }}
        />

        <TextField
          label="Date de naissance"
          type="date"
          name="dateNaissance"
          value={formData.dateNaissance || ""}
          onChange={handleChange}
          fullWidth
          margin="normal"
          InputLabelProps={{ shrink: true }}
        />
        <TextField
          select
          label="Département"
          value={selectedDepartementId || ""}
          onChange={(e) => {
            setSelectedDepartementId(e.target.value);
            // Plus besoin de reset équipe ici, c'est fait dans useEffect fetchEquipes
          }}
          fullWidth
          margin="normal"
        >
          {departements.map((d) => (
            <MenuItem key={d.id} value={d.id}>
              {d.nom}
            </MenuItem>
          ))}
        </TextField>

        <TextField
          select
          label="Équipe"
          name="equipeId"
          value={formData.equipeId || ""}
          onChange={handleChange}
          fullWidth
          margin="normal"
          disabled={loadingEquipes || equipes.length === 0}
        >
          {loadingEquipes ? (
            <MenuItem disabled>Chargement...</MenuItem>
          ) : equipes.length === 0 ? (
            <MenuItem disabled>Aucune équipe disponible</MenuItem>
          ) : (
            equipes.map((e) => (
              <MenuItem key={e.id} value={e.id}>
                {e.nom}
              </MenuItem>
            ))
          )}
        </TextField>

        <TextField
          select
          label="Contrat"
          name="contrat"
          value={formData.contrat || 0}
          onChange={handleChange}
          fullWidth
          margin="normal"
        >
          {contratLabels.map((c, index) => (
            <MenuItem key={index} value={index}>
              {c}
            </MenuItem>
          ))}
        </TextField>

        <TextField
          select
          label="Statut"
          name="statut"
          value={formData.statut || 0}
          onChange={handleChange}
          fullWidth
          margin="normal"
        >
          {statutLabels.map((s, index) => (
            <MenuItem key={index} value={index}>
              {s}
            </MenuItem>
          ))}
        </TextField>

        <FormControlLabel
          control={
            <Checkbox
              checked={formData.estManager || false}
              onChange={handleChange}
              name="estManager"
              style={{ color: pastelColors.blue }}
            />
          }
          label="Est Manager"
        />

        <Box className="edit-buttons">
          <Button
            style={{ backgroundColor: pastelColors.green }}
            onClick={handleSubmit}
          >
            Sauvegarder
          </Button>
          <Button
            style={{ backgroundColor: pastelColors.pink }}
            onClick={() => navigate(-1)}
          >
            Annuler
          </Button>
        </Box>
      </Card>
    </Box>
  );
};

export default EditEmploye;
