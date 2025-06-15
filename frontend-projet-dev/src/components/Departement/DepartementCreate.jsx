import React, { useState } from "react";
import { createDepartement } from "../../api/departement";
import { Box, Button, TextField, Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";
import "./DepartementCreate.css"; // Style pastel minimaliste
import { toast } from "react-toastify";

const DepartementCreate = () => {
  const [nom, setNom] = useState("");
  const navigate = useNavigate();

  const handleCreate = async () => {
    if (!nom.trim()) {
      toast.error("Le nom du département est requis !");
      return;
    }

    try {
      const data = await createDepartement({ nom });
      toast.success("Département créé avec succès !");
      navigate(`/departement/${data.id}`); // Redirige vers les détails
    } catch (error) {
      console.error(error);
      toast.error("Erreur lors de la création du département.");
    }
  };

  return (
    <Box className="departement-create-container">
      <Typography variant="h4" className="departement-create-title">
        Créer un Département
      </Typography>

      <TextField
        label="Nom du Département"
        variant="outlined"
        fullWidth
        value={nom}
        onChange={(e) => setNom(e.target.value)}
        className="departement-create-input"
        sx={{ mb: 2 }}
      />

      <Button onClick={handleCreate} className="departement-create-button">
        Créer
      </Button>
    </Box>
  );
};

export default DepartementCreate;
