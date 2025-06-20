/* eslint-disable no-unused-vars */
import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getMe } from "../../api/auth";
import { getFormationById, updateFormation } from "../../api/formation";
import { getFormateurs } from "../../api/formateur";
import {
  Box,
  Typography,
  TextField,
  Button,
  MenuItem,
  Card,
} from "@mui/material";
import { toast } from "react-toastify";
import AccessDeniedImage from "../../assets/accessDenied.png"; // si tu as une image pour refus
import "./FormationEdit.css";

const pastel = {
  blue: "#A7C7E7",
  green: "#B2E2B8",
  pink: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
  text: "#333",
};

const FormationEdit = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const [user, setUser] = useState(null);
  const [formation, setFormation] = useState(null);
  const [formateurs, setFormateurs] = useState([]);
  const [formData, setFormData] = useState({
    id: 0,
    titre: "",
    description: "",
    dateDebut: "",
    dateFin: "",
    formateurId: "",
    cout: 0,
  });

  useEffect(() => {
    const fetchData = async () => {
      try {
        const me = await getMe();
        setUser(me);

        if (!me.roles.includes("Admin")) return;

        const fData = await getFormationById(id);
        setFormation(fData.data);

        setFormData({
          id: fData.data.id,
          titre: fData.data.titre,
          description: fData.data.description,
          dateDebut: fData.data.dateDebut.slice(0, 10),
          dateFin: fData.data.dateFin.slice(0, 10),
          formateurId: fData.data.formateurId,
          cout: fData.data.cout,
        });

        const formateursData = await getFormateurs();
        setFormateurs(formateursData);
      } catch {
        toast.error("Erreur lors du chargement");
      }
    };
    fetchData();
  }, [id]);

  if (user === null) return <div>Chargement...</div>;

  if (!user.roles.includes("Admin")) {
    return (
      <Box className="access-denied-container">
        <img src={AccessDeniedImage} alt="Accès refusé" />
        <Typography variant="h5" className="access-denied-message">
          Accès refusé
        </Typography>
        <Typography className="access-denied-message">
          Vous n'avez pas les autorisations nécessaires pour modifier cette
          page.
        </Typography>
      </Box>
    );
  }

  if (!formation) return <div>Chargement des données...</div>;

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((old) => ({ ...old, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    // validation simple
    if (
      !formData.titre ||
      !formData.description ||
      !formData.dateDebut ||
      !formData.dateFin ||
      !formData.formateurId
    ) {
      toast.error("Tous les champs doivent être remplis");
      return;
    }

    if (new Date(formData.dateFin) < new Date(formData.dateDebut)) {
      toast.error("La date de fin doit être après la date de début");
      return;
    }

    try {
      await updateFormation(id, {
        id: formData.id,
        titre: formData.titre,
        description: formData.description,
        dateDebut: formData.dateDebut,
        dateFin: formData.dateFin,
        formateurId: formData.formateurId,
        cout: formData.cout,
      });
      toast.success("Formation modifiée avec succès");
      navigate(`/formation/${id}`);
    } catch {
      toast.error("Erreur lors de la modification");
    }
  };

  return (
    <Box className="formation-edit-container">
      <Card className="formation-edit-card">
        <Typography variant="h5" sx={{ mb: 3, color: pastel.text }}>
          Modifier la formation
        </Typography>

        <form
          onSubmit={handleSubmit}
          className="formation-edit-form"
          noValidate
        >
          <TextField
            label="Titre"
            name="titre"
            value={formData.titre}
            onChange={handleChange}
            fullWidth
            required
            margin="normal"
          />

          <TextField
            label="Description"
            name="description"
            value={formData.description}
            onChange={handleChange}
            fullWidth
            multiline
            rows={4}
            required
            margin="normal"
          />

          <TextField
            label="Date de début"
            name="dateDebut"
            type="date"
            value={formData.dateDebut}
            onChange={handleChange}
            fullWidth
            required
            margin="normal"
            InputLabelProps={{ shrink: true }}
          />

          <TextField
            label="Date de fin"
            name="dateFin"
            type="date"
            value={formData.dateFin}
            onChange={handleChange}
            fullWidth
            required
            margin="normal"
            InputLabelProps={{ shrink: true }}
          />

          <TextField
            label="Formateur"
            name="formateurId"
            select
            value={formData.formateurId}
            onChange={handleChange}
            fullWidth
            required
            margin="normal"
          >
            {formateurs.map((f) => (
              <MenuItem key={f.id} value={f.id}>
                {f.nom} {f.prenom}
              </MenuItem>
            ))}
          </TextField>

          <TextField
            label="Coût (€)"
            name="cout"
            type="number"
            value={formData.cout}
            onChange={handleChange}
            fullWidth
            required
            margin="normal"
            inputProps={{ min: 0 }}
          />

          <Box sx={{ display: "flex", justifyContent: "flex-end", mt: 3 }}>
            <Button
              type="submit"
              variant="contained"
              sx={{
                backgroundColor: pastel.blue,
                color: pastel.text,
                fontWeight: "bold",
                borderRadius: "12px",
                textTransform: "none",
                "&:hover": {
                  backgroundColor: "#8db8dd",
                },
              }}
            >
              Enregistrer
            </Button>
          </Box>
        </form>
      </Card>
    </Box>
  );
};

export default FormationEdit;
