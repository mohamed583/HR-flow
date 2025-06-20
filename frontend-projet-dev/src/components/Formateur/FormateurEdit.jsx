import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getFormateurDetails, updateFormateur } from "../../api/formateur";
import { getMe } from "../../api/auth";
import { Box, Typography, TextField, Button, Paper } from "@mui/material";
import { toast } from "react-toastify";
import AccessDeniedImage from "../../assets/accessDenied.png";
import "./FormateurEdit.css";

const FormateurEdit = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [formateurData, setFormateurData] = useState({
    nom: "",
    prenom: "",
    domaine: "",
    description: "",
    salaire: 0,
  });
  const [userRole, setUserRole] = useState(null);
  const [isAdmin, setIsAdmin] = useState(false);

  useEffect(() => {
    (async () => {
      try {
        const me = await getMe();
        setUserRole(me.roles);
        if (me.roles.includes("Admin")) {
          setIsAdmin(true);
          const data = await getFormateurDetails(id);
          setFormateurData({
            id: id,
            nom: data.nom,
            prenom: data.prenom,
            domaine: data.domaine,
            description: data.description,
            salaire: data.salaire,
          });
        }
      } catch {
        toast.error("Erreur lors du chargement");
      }
    })();
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormateurData((prev) => ({
      ...prev,
      [name]: name === "salaire" ? Number(value) : value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await updateFormateur(id, formateurData);
      toast.success("Formateur mis à jour");
      navigate(`/formateur/${id}`);
    } catch {
      toast.error("Erreur lors de la mise à jour");
    }
  };

  if (userRole === null) return <div>Chargement…</div>;
  if (!isAdmin)
    return (
      <Box className="access-denied-container">
        <img src={AccessDeniedImage} alt="Accès refusé" />
        <Typography variant="h5" className="access-denied-message">
          Accès refusé
        </Typography>
        <Typography className="access-denied-message">
          Vous n'avez pas les autorisations nécessaires.
        </Typography>
      </Box>
    );

  return (
    <Box className="formateur‑edit‑container">
      <Paper className="formateur‑form‑paper">
        <Typography variant="h5" className="form‑title">
          Modifier le formateur
        </Typography>
        <form onSubmit={handleSubmit}>
          {["nom", "prenom", "domaine"].map((field) => (
            <TextField
              key={field}
              label={field.charAt(0).toUpperCase() + field.slice(1)}
              name={field}
              value={formateurData[field]}
              onChange={handleChange}
              fullWidth
              margin="normal"
              required
            />
          ))}
          <TextField
            label="Description"
            name="description"
            value={formateurData.description}
            onChange={handleChange}
            fullWidth
            margin="normal"
            multiline
            rows={3}
          />
          <TextField
            label="Salaire"
            name="salaire"
            type="number"
            value={formateurData.salaire}
            onChange={handleChange}
            fullWidth
            margin="normal"
            required
          />
          <Box mt={2} display="flex" justifyContent="space-between">
            <Button
              variant="contained"
              className="action-button reset"
              onClick={() => navigate(-1)}
            >
              Annuler
            </Button>
            <Button
              type="submit"
              variant="contained"
              className="action-button submit"
            >
              Enregistrer
            </Button>
          </Box>
        </form>
      </Paper>
    </Box>
  );
};

export default FormateurEdit;
