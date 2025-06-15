/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import {
  getDepartementDetails,
  updateDepartement,
} from "../../api/departement";
import { getMe } from "../../api/auth";
import { useNavigate, useParams } from "react-router-dom";
import { Box, Button, TextField, Typography } from "@mui/material";
import { toast } from "react-toastify";
import AccessDeniedImage from "../../assets/accessDenied.png";
import "./EditDepartement.css";

const pastelColors = {
  primary: "#A7C7E7",
  secondary: "#B2E2B8",
  accent: "#F6C6D4",
  background: "#F8F8F8",
  text: "#333",
};

const EditDepartement = () => {
  const { id } = useParams();
  const [departement, setDepartement] = useState(null);
  const [nom, setNom] = useState("");
  const [userRole, setUserRole] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const me = await getMe();
        setUserRole(me.roles);
        if (!me.roles.includes("Admin")) return;

        const data = await getDepartementDetails(id);
        setDepartement(data);
        setNom(data.nom);
      } catch (error) {
        toast.error("Erreur lors du chargement du département.");
      }
    };

    fetchData();
  }, [id]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await updateDepartement(id, { nom });
      toast.success("Département modifié avec succès !");
      navigate(`/departement/${id}`);
    } catch (error) {
      toast.error("Erreur lors de la modification.");
    }
  };

  if (userRole === null) return <div>Chargement...</div>;

  if (!userRole.includes("Admin")) {
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
          Vous n'avez pas les autorisations nécessaires pour modifier ce
          département.
        </Typography>
      </Box>
    );
  }

  return (
    <Box className="edit-departement-container">
      <Typography variant="h4" className="edit-departement-title">
        Modifier le Département
      </Typography>

      <form onSubmit={handleSubmit} className="edit-departement-form">
        <TextField
          label="Nom du département"
          variant="outlined"
          value={nom}
          onChange={(e) => setNom(e.target.value)}
          fullWidth
          required
          sx={{
            "& .MuiOutlinedInput-root": {
              borderRadius: "20px",
              backgroundColor: "#fff",
            },
          }}
        />

        <Button type="submit" className="edit-departement-button">
          Enregistrer les modifications
        </Button>
      </form>
    </Box>
  );
};

export default EditDepartement;
