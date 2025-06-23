/* eslint-disable no-unused-vars */
/* File: src/components/Paie/CreatePaie.jsx */
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getMe } from "../../api/auth";
import { getEmployes } from "../../api/employe";
import { getFormateurs } from "../../api/formateur";
import { createPaie } from "../../api/paie";
import {
  Box,
  Typography,
  Card,
  Button,
  MenuItem,
  Select,
  InputLabel,
  FormControl,
  TextField,
} from "@mui/material";
import { toast } from "react-toastify";
import AccessDeniedImage from "../../assets/accessDenied.png";
import "./CreatePaie.css";

const pastel = {
  primary: "#A7C7E7",
  secondary: "#B2E2B8",
  accent: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
};

const CreatePaie = () => {
  const [role, setRole] = useState(null);
  const [personnes, setPersonnes] = useState([]);
  const [typePersonne, setTypePersonne] = useState("employe");
  const [form, setForm] = useState({
    personneId: "",
    datePaie: new Date().toISOString().split("T")[0],
    montant: 0,
    description: "",
    avantages: "",
    retenues: "",
  });
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const me = await getMe();
        setRole(me.roles);

        if (me.roles.includes("Admin")) {
          const data = await getEmployes();
          setPersonnes(data);
        }
      } catch (error) {
        toast.error("Erreur de chargement des données");
      }
    };
    fetchData();
  }, []);

  useEffect(() => {
    const loadList = async () => {
      try {
        const list =
          typePersonne === "employe"
            ? await getEmployes()
            : await getFormateurs();
        setPersonnes(list);
      } catch (e) {
        toast.error("Erreur de chargement de la liste");
      }
    };
    loadList();
  }, [typePersonne]);

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async () => {
    try {
      const response = await createPaie(form);
      toast.success("Paie créée avec succès !");
      navigate(`/paie/${response.data.id}`);
    } catch (e) {
      toast.error("Erreur lors de la création de la paie");
    }
  };

  if (role === null) return <div>Chargement...</div>;

  if (!role.includes("Admin")) {
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

  return (
    <Box className="paie-create-container">
      <Card className="paie-form-card">
        <Typography variant="h5" className="paie-form-title">
          Créer une nouvelle paie
        </Typography>

        <FormControl fullWidth sx={{ mt: 2 }}>
          <InputLabel>Type</InputLabel>
          <Select
            value={typePersonne}
            label="Type"
            onChange={(e) => setTypePersonne(e.target.value)}
          >
            <MenuItem value="employe">Employé</MenuItem>
            <MenuItem value="formateur">Formateur</MenuItem>
          </Select>
        </FormControl>

        <FormControl fullWidth sx={{ mt: 2 }}>
          <InputLabel>Personne</InputLabel>
          <Select
            value={form.personneId}
            name="personneId"
            onChange={handleChange}
          >
            {personnes.map((p) => (
              <MenuItem key={p.id} value={p.id}>
                {p.nom} {p.prenom}
              </MenuItem>
            ))}
          </Select>
        </FormControl>

        <TextField
          fullWidth
          label="Date de paie"
          type="date"
          name="datePaie"
          sx={{ mt: 2 }}
          value={form.datePaie}
          onChange={handleChange}
        />

        <TextField
          fullWidth
          label="Montant"
          type="number"
          name="montant"
          sx={{ mt: 2 }}
          value={form.montant}
          onChange={handleChange}
        />

        <TextField
          fullWidth
          label="Description"
          name="description"
          sx={{ mt: 2 }}
          value={form.description}
          onChange={handleChange}
        />

        <TextField
          fullWidth
          label="Avantages"
          name="avantages"
          sx={{ mt: 2 }}
          value={form.avantages}
          onChange={handleChange}
        />

        <TextField
          fullWidth
          label="Retenues"
          name="retenues"
          sx={{ mt: 2, mb: 2 }}
          value={form.retenues}
          onChange={handleChange}
        />

        <Button
          variant="contained"
          onClick={handleSubmit}
          style={{ backgroundColor: pastel.secondary }}
        >
          Valider la paie
        </Button>
      </Card>
    </Box>
  );
};

export default CreatePaie;
