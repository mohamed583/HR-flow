/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getMe } from "../../api/auth";
import { getDetails, getListeEquipe } from "../../api/employe";
import {
  getMesConges,
  getAllConges,
  updateCongeStatus,
  deleteConge,
} from "../../api/conge";
import { Box, Typography, Card, Button, Divider, Grid } from "@mui/material";
import { toast } from "react-toastify";
import { FiCheck, FiX, FiTrash2 } from "react-icons/fi";
import AccessDeniedImage from "../../assets/accessDenied.png";
import "./ListeConges.css";

const pastel = {
  blue: "#A7C7E7",
  green: "#B2E2B8",
  pink: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
  text: "#333",
};

const ListeConges = () => {
  const [user, setUser] = useState(null);
  const [mesConges, setMesConges] = useState([]);
  const [allConges, setAllConges] = useState([]);
  const [employeNames, setEmployeNames] = useState({});
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const me = await getMe();
        setUser(me);

        if (me.roles.includes("Employe")) {
          const res = await getMesConges();
          setMesConges(res);
        }

        if (me.roles.includes("Manager") || me.roles.includes("Admin")) {
          if (me.roles.includes("Admin")) {
            const res = await getAllConges();
            const names = {};
            for (const conge of res) {
              const details = await getDetails(conge.employeId);
              names[conge.employeId] = `${details.prenom} ${details.nom}`;
            }
            setEmployeNames(names);
            setAllConges(res);
          } else if (me.roles.includes("Manager")) {
            const res = await getAllConges();
            const filtered = res.filter((conge) => conge.employeId !== me.id);
            setAllConges(filtered);
            const equipe = await getListeEquipe();
            const names = {};
            for (const emp of equipe) {
              names[emp.id] = `${emp.prenom} ${emp.nom}`;
            }
            setEmployeNames(names);
          }
        }
      } catch (e) {
        toast.error("Erreur de chargement des congés.");
      }
    };
    fetchData();
  }, []);

  const statusLabel = (status) => {
    switch (status) {
      case 0:
        return "En cours";
      case 1:
        return "Approuvé";
      case 2:
        return "Rejeté";
      default:
        return "Inconnu";
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm("Confirmer la suppression du congé ?")) {
      try {
        await deleteConge(id);
        toast.success("Congé supprimé");
        setMesConges((prev) => prev.filter((c) => c.id !== id));
        setAllConges((prev) => prev.filter((c) => c.id !== id));
      } catch {
        toast.error("Erreur de suppression.");
      }
    }
  };

  const handleStatusChange = async (id, status) => {
    try {
      await updateCongeStatus(id, status);
      toast.success("Statut mis à jour");
      setAllConges((prev) =>
        prev.map((c) => (c.id === id ? { ...c, statusConge: status } : c))
      );
    } catch {
      toast.error("Erreur de mise à jour du statut");
    }
  };

  if (!user) return <div>Chargement...</div>;

  return (
    <Box
      className="conge-list-container"
      sx={{ maxWidth: 1200, margin: "auto", px: 2 }}
    >
      {user.roles.includes("Employe") && (
        <>
          {/* Bouton création aligné à droite avec MUI flex */}
          <Box display="flex" justifyContent="flex-end" mb={2}>
            <Button
              variant="contained"
              sx={{
                bgcolor: pastel.blue,
                color: pastel.text,
                borderRadius: 2,
                fontWeight: "bold",
                "&:hover": { bgcolor: "#91b7d7" },
                textTransform: "none",
              }}
              onClick={() => navigate("/conge/create")}
            >
              + Créer un congé
            </Button>
          </Box>

          <Typography variant="h5" className="title" gutterBottom>
            Mes congés
          </Typography>

          <Divider sx={{ mb: 3 }} />
          <Grid container spacing={3}>
            {mesConges.map((conge) => (
              <Grid item xs={12} md={6} key={conge.id}>
                <Card
                  className="conge-card"
                  sx={{
                    p: 3,
                    borderRadius: 3,
                    boxShadow: 3,
                    transition: "box-shadow 0.3s ease",
                    "&:hover": { boxShadow: 6 },
                  }}
                >
                  <Typography variant="body1" gutterBottom>
                    Type : {conge.type}
                  </Typography>
                  <Typography variant="body2" gutterBottom>
                    Début : {conge.dateDebut.slice(0, 10)}
                  </Typography>
                  <Typography variant="body2" gutterBottom>
                    Fin : {conge.dateFin.slice(0, 10)}
                  </Typography>
                  <Typography variant="body2" gutterBottom>
                    Statut : {statusLabel(conge.statusConge)}
                  </Typography>
                  {conge.statusConge === 0 && (
                    <Button
                      variant="outlined"
                      color="error"
                      startIcon={<FiTrash2 />}
                      sx={{ mt: 1 }}
                      onClick={() => handleDelete(conge.id)}
                    >
                      Supprimer
                    </Button>
                  )}
                </Card>
              </Grid>
            ))}
          </Grid>
        </>
      )}

      {(user.roles.includes("Manager") || user.roles.includes("Admin")) && (
        <>
          <Typography
            variant="h5"
            className="title"
            gutterBottom
            sx={{ mt: 4 }}
          >
            Tous les congés
          </Typography>
          <Divider sx={{ mb: 3 }} />
          <Grid container spacing={3}>
            {allConges.map((conge) => (
              <Grid item xs={12} md={6} key={conge.id}>
                <Card
                  className="conge-card"
                  sx={{
                    p: 3,
                    borderRadius: 3,
                    boxShadow: 3,
                    transition: "box-shadow 0.3s ease",
                    "&:hover": { boxShadow: 6 },
                  }}
                >
                  {/* Affichage du nom employé */}
                  {employeNames[conge.employeId] && (
                    <Typography
                      variant="subtitle2"
                      sx={{ mb: 1, fontWeight: "bold" }}
                    >
                      Employé : {employeNames[conge.employeId]}
                    </Typography>
                  )}

                  <Typography variant="body1" gutterBottom>
                    Type : {conge.type}
                  </Typography>
                  <Typography variant="body2" gutterBottom>
                    Début : {conge.dateDebut.slice(0, 10)}
                  </Typography>
                  <Typography variant="body2" gutterBottom>
                    Fin : {conge.dateFin.slice(0, 10)}
                  </Typography>
                  <Typography variant="body2" gutterBottom>
                    Statut : {statusLabel(conge.statusConge)}
                  </Typography>

                  {conge.statusConge === 0 && (
                    <Box sx={{ mt: 2, display: "flex", gap: 1 }}>
                      <Button
                        variant="contained"
                        color="success"
                        startIcon={<FiCheck />}
                        onClick={() => handleStatusChange(conge.id, "1")}
                      >
                        Approuver
                      </Button>
                      <Button
                        variant="contained"
                        color="error"
                        startIcon={<FiX />}
                        onClick={() => handleStatusChange(conge.id, "2")}
                      >
                        Rejeter
                      </Button>
                    </Box>
                  )}

                  {user.roles.includes("Admin") && (
                    <Button
                      variant="outlined"
                      color="warning"
                      startIcon={<FiTrash2 />}
                      sx={{ mt: 2 }}
                      onClick={() => handleDelete(conge.id)}
                    >
                      Supprimer
                    </Button>
                  )}
                </Card>
              </Grid>
            ))}
          </Grid>
        </>
      )}

      {!user.roles.includes("Employe") &&
        !user.roles.includes("Manager") &&
        !user.roles.includes("Admin") && (
          <Box
            className="access-denied-container"
            sx={{ textAlign: "center", py: 6 }}
          >
            <img
              src={AccessDeniedImage}
              alt="Accès refusé"
              style={{ maxWidth: 300 }}
            />
            <Typography
              className="access-denied-message"
              sx={{ color: pastel.text, mt: 2, fontWeight: "medium" }}
            >
              Accès refusé - Vous n'avez pas les autorisations requises.
            </Typography>
          </Box>
        )}
    </Box>
  );
};

export default ListeConges;
