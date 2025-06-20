/* --- FICHIER: src/components/Formation/CreateFormation.jsx --- */
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { createFormation } from "../../api/formation";
import { getFormateurs } from "../../api/formateur";
import { getMe } from "../../api/auth";
import {
  TextField,
  Button,
  Box,
  Typography,
  MenuItem,
  Select,
  InputLabel,
  FormControl,
} from "@mui/material";
import { toast } from "react-toastify";
import "./CreateFormation.css";

const CreateFormation = () => {
  const navigate = useNavigate();
  const [user, setUser] = useState(null);
  const [formateurs, setFormateurs] = useState([]);
  const [formData, setFormData] = useState({
    titre: "",
    description: "",
    dateDebut: "",
    dateFin: "",
    formateurId: "",
    cout: 0,
  });

  useEffect(() => {
    const fetchInit = async () => {
      try {
        const me = await getMe();
        if (!me.roles.includes("Admin")) {
          toast.error("Accès refusé");
          return navigate("/");
        }
        setUser(me);
        const formateursList = await getFormateurs();
        setFormateurs(formateursList);
      } catch {
        toast.error("Erreur lors du chargement des données.");
      }
    };
    fetchInit();
  }, [navigate]);

  const handleChange = (e) => {
    setFormData((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const created = await createFormation(formData);
      toast.success("Formation créée avec succès !");
      navigate(`/formations/${created.id}`);
    } catch {
      toast.error("Erreur lors de la création.");
    }
  };

  if (!user) return <div>Chargement...</div>;

  return (
    <Box className="create-formation-container">
      <Typography variant="h4" className="form-title">
        Créer une formation
      </Typography>
      <form onSubmit={handleSubmit} className="formation-form">
        <TextField
          label="Titre"
          name="titre"
          value={formData.titre}
          onChange={handleChange}
          required
          fullWidth
        />
        <TextField
          label="Description"
          name="description"
          value={formData.description}
          onChange={handleChange}
          multiline
          rows={4}
          required
          fullWidth
        />
        <TextField
          type="date"
          name="dateDebut"
          value={formData.dateDebut}
          onChange={handleChange}
          required
          fullWidth
          InputLabelProps={{ shrink: true }}
          label="Date de début"
        />
        <TextField
          type="date"
          name="dateFin"
          value={formData.dateFin}
          onChange={handleChange}
          required
          fullWidth
          InputLabelProps={{ shrink: true }}
          label="Date de fin"
        />
        <FormControl fullWidth required>
          <InputLabel>Formateur</InputLabel>
          <Select
            name="formateurId"
            value={formData.formateurId}
            onChange={handleChange}
          >
            {formateurs.map((f) => (
              <MenuItem key={f.id} value={f.id}>
                {f.nom} {f.prenom}
              </MenuItem>
            ))}
          </Select>
        </FormControl>
        <TextField
          label="Coût (€)"
          name="cout"
          type="number"
          value={formData.cout}
          onChange={handleChange}
          required
          fullWidth
        />
        <Button type="submit" className="submit-button">
          Créer
        </Button>
      </form>
    </Box>
  );
};

export default CreateFormation;
