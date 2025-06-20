/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getMe } from "../../api/auth";
import { getFormationById, deleteFormation } from "../../api/formation";
import {
  getInscriptionsByFormation,
  getMesInscriptions,
  getFormationsEtInscriptions,
  approveInscription,
  rejectInscription,
  deleteInscription,
  postulerFormation,
} from "../../api/inscription";
import { getFormateurDetails } from "../../api/formateur";
import { Box, Typography, Card, Button, Divider } from "@mui/material";
import { FiEdit, FiTrash2, FiCheck, FiX } from "react-icons/fi";
import { toast } from "react-toastify";
import AccessDeniedImage from "../../assets/accessDenied.png";
import "./FormationDetails.css";

const colors = {
  blue: "#A7C7E7",
  green: "#B2E2B8",
  pink: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
  text: "#333",
};

const FormationDetails = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [user, setUser] = useState(null);
  const [formation, setFormation] = useState(null);
  const [formateur, setFormateur] = useState(null);
  const [inscriptions, setInscriptions] = useState([]);
  const [userAlreadyApplied, setUserAlreadyApplied] = useState(false);

  useEffect(() => {
    const loadData = async () => {
      try {
        const me = await getMe();
        setUser(me);

        const response = await getFormationById(id);
        setFormation(response.data);

        if (!me.roles.includes("Formateur") && !me.roles.includes("Candidat")) {
          const fData = await getFormateurDetails(response.data.formateurId);
          setFormateur(fData);
          console.log(fData);
        }

        if (me.roles.includes("Admin")) {
          const i = await getInscriptionsByFormation(id);
          setInscriptions(i.data);
          const mes = await getMesInscriptions();
          const exist = mes.data.find((i) => i.formationId === parseInt(id));
          if (exist) setUserAlreadyApplied(true);
        } else if (me.roles.includes("Formateur")) {
          const data = await getFormationsEtInscriptions();
          const myFormation = data.data.find((f) => f.id === parseInt(id));
          setInscriptions(
            myFormation?.inscriptions?.filter((i) => i.statut === "Approuve") ||
              []
          );
        } else if (me.roles.includes("Employe")) {
          const mes = await getMesInscriptions();
          const exist = mes.data.find((i) => i.formationId === parseInt(id));
          if (exist) setUserAlreadyApplied(true);
        }
      } catch (err) {
        toast.error("Erreur de chargement");
      }
    };
    loadData();
  }, [id]);

  const handlePostuler = async () => {
    try {
      await postulerFormation(id);
      toast.success("Demande envoyée");
      setUserAlreadyApplied(true);
    } catch {
      toast.error("Erreur lors de la demande");
    }
  };

  const handleDelete = async () => {
    if (window.confirm("Confirmer la suppression de la formation ?")) {
      try {
        await deleteFormation(id);
        toast.success("Formation supprimée");
        navigate("/formations");
      } catch {
        toast.error("Erreur suppression formation");
      }
    }
  };

  const handleApprove = async (inscriptionId) => {
    await approveInscription(inscriptionId);
    toast.success("Inscription approuvée");
    setInscriptions((prev) =>
      prev.map((i) =>
        i.id === inscriptionId ? { ...i, statut: "Approuve" } : i
      )
    );
  };

  const handleReject = async (inscriptionId) => {
    await rejectInscription(inscriptionId);
    toast.success("Inscription rejetée");
    setInscriptions((prev) =>
      prev.map((i) => (i.id === inscriptionId ? { ...i, statut: "Rejete" } : i))
    );
  };

  const handleDeleteInscription = async (inscriptionId) => {
    if (window.confirm("Confirmer la suppression ?")) {
      await deleteInscription(inscriptionId);
      setInscriptions((prev) => prev.filter((i) => i.id !== inscriptionId));
      toast.success("Supprimé");
    }
  };

  if (!formation || !user || (!user.roles.includes("Formateur") && !formateur))
    return <div>Chargement...</div>;
  const canPostuler = new Date(formation.dateDebut) > new Date();
  if (user.roles.includes("Candidat"))
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
    <Box className="formation-detail-container">
      <Card className="formation-card-detail">
        <Typography variant="h5">{formation.titre}</Typography>
        <Typography>{formation.description}</Typography>
        {!user.roles.includes("Formateur") && (
          <Typography>
            Formateur : {formateur.nom} {formateur.prenom}
          </Typography>
        )}
        <Typography>
          Date de début : {new Date(formation.dateDebut).toLocaleDateString()}
        </Typography>
        <Typography>
          Date de fin : {new Date(formation.dateFin).toLocaleDateString()}
        </Typography>
        {user.roles.includes("Admin") && (
          <Typography>Coût : {formation.cout} €</Typography>
        )}

        {user.roles.includes("Admin") && (
          <Box className="action-btns" sx={{ display: "flex", gap: 2, mt: 2 }}>
            <Button
              onClick={() => navigate(`/formation/${id}/edit`)}
              variant="outlined"
              sx={{
                border: `2px solid ${colors.blue}`,
                backgroundColor: colors.blue,
                color: colors.text,
                fontWeight: "bold",
                textTransform: "none",
                borderRadius: "12px",
                "&:hover": {
                  backgroundColor: "#8db8dd",
                },
              }}
            >
              <FiEdit style={{ marginRight: "5px" }} />
              Modifier
            </Button>
            <Button
              onClick={handleDelete}
              variant="outlined"
              sx={{
                border: `2px solid ${colors.pink}`,
                backgroundColor: colors.pink,
                color: colors.text,
                fontWeight: "bold",
                textTransform: "none",
                borderRadius: "12px",
                "&:hover": {
                  backgroundColor: "#e3a7b8",
                },
              }}
            >
              <FiTrash2 style={{ marginRight: "5px" }} />
              Supprimer
            </Button>
          </Box>
        )}

        {user.roles.includes("Employe") &&
          canPostuler &&
          !userAlreadyApplied && (
            <Button
              onClick={handlePostuler}
              variant="outlined"
              sx={{
                mt: 3,
                border: `2px solid ${colors.green}`,
                backgroundColor: colors.green,
                color: colors.text,
                fontWeight: "bold",
                textTransform: "none",
                borderRadius: "12px",
                "&:hover": {
                  backgroundColor: "#9cd7aa",
                },
              }}
            >
              Postuler
            </Button>
          )}
      </Card>
      {user.roles.includes("Admin") || user.roles.includes("Formateur") ? (
        <>
          <Typography variant="h6" sx={{ marginTop: 3 }}>
            Inscriptions
          </Typography>
          <Divider sx={{ marginBottom: 2 }} />

          <Box className="inscriptions-list">
            {inscriptions.length === 0 ? (
              <Typography>Aucune inscription</Typography>
            ) : (
              inscriptions.map((i) => (
                <Card
                  key={i.id}
                  className="inscription-card"
                  sx={{ mb: 2, p: 2 }}
                >
                  <Typography>Employé : {i.nomEmploye}</Typography>
                  <Typography>Statut : {i.statut}</Typography>
                  {user.roles.includes("Admin") && (
                    <Box sx={{ display: "flex", gap: 1, mt: 1 }}>
                      <Button
                        onClick={() => handleApprove(i.id)}
                        variant="outlined"
                        sx={{
                          minWidth: "40px",
                          border: `2px solid ${colors.green}`,
                          backgroundColor: colors.green,
                          color: colors.text,
                          "&:hover": { backgroundColor: "#9cd7aa" },
                        }}
                      >
                        <FiCheck />
                      </Button>
                      <Button
                        onClick={() => handleReject(i.id)}
                        variant="outlined"
                        sx={{
                          minWidth: "40px",
                          border: `2px solid ${colors.yellow}`,
                          backgroundColor: colors.yellow,
                          color: colors.text,
                          "&:hover": { backgroundColor: "#e9de96" },
                        }}
                      >
                        <FiX />
                      </Button>
                      <Button
                        onClick={() => handleDeleteInscription(i.id)}
                        variant="outlined"
                        sx={{
                          minWidth: "40px",
                          border: `2px solid ${colors.pink}`,
                          backgroundColor: colors.pink,
                          color: colors.text,
                          "&:hover": { backgroundColor: "#e3a7b8" },
                        }}
                      >
                        <FiTrash2 />
                      </Button>
                    </Box>
                  )}
                </Card>
              ))
            )}
          </Box>
        </>
      ) : (
        user.roles.includes("Employe") &&
        userAlreadyApplied && (
          <Typography sx={{ mt: 3, fontStyle: "italic", color: "green" }}>
            ✅ Vous avez déjà postulé à cette formation.
          </Typography>
        )
      )}
    </Box>
  );
};

export default FormationDetails;
