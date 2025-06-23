/* eslint-disable no-unused-vars */
/* File: src/components/Paie/ListePaies.jsx */
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getMesPaies, getAllPaies, genererTousPaies } from "../../api/paie";
import { getMe } from "../../api/auth";
import { getEmployes } from "../../api/employe";
import { getFormateurs } from "../../api/formateur";
import {
  Box,
  Typography,
  Button,
  Card,
  Grid,
  Select,
  MenuItem,
  FormControl,
  InputLabel,
} from "@mui/material";
import { FiArrowRight, FiPlus, FiSend } from "react-icons/fi";
import { toast } from "react-toastify";
import "./ListePaies.css";

const pastel = {
  primary: "#A7C7E7",
  secondary: "#B2E2B8",
  accent: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
};

const ListePaies = () => {
  const [mesPaies, setMesPaies] = useState([]);
  const [allPaies, setAllPaies] = useState([]);
  const [roles, setRoles] = useState([]);
  const [employes, setEmployes] = useState([]);
  const [formateurs, setFormateurs] = useState([]);
  const [filterType, setFilterType] = useState("all");
  const [selectedPersonne, setSelectedPersonne] = useState("all");
  const navigate = useNavigate();

  useEffect(() => {
    const loadData = async () => {
      try {
        const me = await getMe();
        setRoles(me.roles);

        const mes = await getMesPaies();
        setMesPaies(mes.data);

        if (me.roles.includes("Admin")) {
          const all = await getAllPaies();
          setAllPaies(all.data);
          const emps = await getEmployes();
          const forms = await getFormateurs();
          setEmployes(emps);
          setFormateurs(forms);
        }
      } catch (error) {
        toast.error("Erreur de chargement des paies");
      }
    };
    loadData();
  }, []);

  const handleGenererTous = async () => {
    try {
      await genererTousPaies();
      toast.success("Paies générées avec succès");
    } catch (e) {
      toast.error("Erreur lors de la génération des paies");
    }
  };

  const filteredPaies = allPaies.filter((p) => {
    if (filterType === "all") return true;

    if (filterType === "employe") {
      const isEmploye = employes.some((e) => e.id === p.personneId);
      if (!isEmploye) return false;
      return selectedPersonne === "all" || p.personneId === selectedPersonne;
    }

    if (filterType === "formateur") {
      const isFormateur = formateurs.some((f) => f.id === p.personneId);
      if (!isFormateur) return false;
      return selectedPersonne === "all" || p.personneId === selectedPersonne;
    }

    return true;
  });

  return (
    <Box className="paie-container">
      <Typography variant="h5" className="paie-title">
        Mes Paies
      </Typography>
      <Grid container spacing={2} className="paie-section">
        {mesPaies.map((paie) => (
          <Grid item xs={12} md={6} key={paie.id}>
            <Card className="paie-card">
              <Typography>{paie.description}</Typography>
              <Typography>
                {new Date(paie.datePaie).toLocaleDateString()}
              </Typography>
              <Typography>Montant : {paie.montant} DT</Typography>
              <Button
                variant="contained"
                style={{ backgroundColor: pastel.primary }}
                onClick={() => navigate(`/paie/${paie.id}`)}
              >
                Détails <FiArrowRight size={16} />
              </Button>
            </Card>
          </Grid>
        ))}
      </Grid>

      {roles.includes("Admin") && (
        <>
          <Typography variant="h5" sx={{ mt: 4 }} className="paie-title">
            Toutes les Paies
          </Typography>
          <Box className="admin-actions">
            <Button
              variant="contained"
              style={{ backgroundColor: pastel.secondary }}
              onClick={() => navigate("/paie/create")}
            >
              <FiPlus /> Créer une paie
            </Button>
            <Button
              variant="contained"
              style={{ backgroundColor: pastel.violet }}
              onClick={handleGenererTous}
            >
              <FiSend /> Lancer paies pour tous
            </Button>
          </Box>

          <FormControl fullWidth sx={{ mt: 2 }}>
            <InputLabel>Type de filtre</InputLabel>
            <Select
              value={filterType}
              label="Type de filtre"
              onChange={(e) => {
                setFilterType(e.target.value);
                setSelectedPersonne("all");
              }}
            >
              <MenuItem value="all">Toutes les paies</MenuItem>
              <MenuItem value="employe">Paies des employés</MenuItem>
              <MenuItem value="formateur">Paies des formateurs</MenuItem>
            </Select>
          </FormControl>

          {(filterType === "employe" || filterType === "formateur") && (
            <FormControl fullWidth sx={{ mt: 2, mb: 3 }}>
              <InputLabel>Filtrer par</InputLabel>
              <Select
                value={selectedPersonne}
                label="Filtrer par"
                onChange={(e) => setSelectedPersonne(e.target.value)}
              >
                <MenuItem value="all">Tous</MenuItem>
                {(filterType === "employe" ? employes : formateurs).map((p) => (
                  <MenuItem key={p.id} value={p.id}>
                    {p.nom} {p.prenom}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
          )}

          <Grid container spacing={2} className="paie-section">
            {filteredPaies.map((paie) => (
              <Grid item xs={12} md={6} key={paie.id}>
                <Card className="paie-card">
                  <Typography>{paie.nomComplet}</Typography>
                  <Typography>{paie.description}</Typography>
                  <Typography>
                    {new Date(paie.datePaie).toLocaleDateString()}
                  </Typography>
                  <Typography>Montant : {paie.montant} DT</Typography>
                  <Button
                    variant="contained"
                    style={{ backgroundColor: pastel.primary }}
                    onClick={() => navigate(`/paie/${paie.id}`)}
                  >
                    Détails <FiArrowRight size={16} />
                  </Button>
                </Card>
              </Grid>
            ))}
          </Grid>
        </>
      )}
    </Box>
  );
};

export default ListePaies;
