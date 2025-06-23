/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import { getMe } from "../../api/auth";
import { getMyEntretiens } from "../../api/entretien";
import { downloadCv } from "../../api/candidature";
import { Box, Typography, Card, Button, Divider } from "@mui/material";
import { toast } from "react-toastify";
import AccessDeniedImage from "../../assets/accessDenied.png";
import { FiDownload, FiCheck } from "react-icons/fi";
import { useNavigate } from "react-router-dom";
import "./MesEntretiens.css";

const pastelColors = {
  primary: "#A7C7E7",
  secondary: "#B2E2B8",
  accent: "#F6C6D4",
  background: "#F8F8F8",
  text: "#333",
  download: "#A7C7E7",
  finalize: "#B2E2B8",
};

const MesEntretiens = () => {
  const [userRole, setUserRole] = useState(null);
  const [entretiens, setEntretiens] = useState([]);
  const navigate = useNavigate();
  useEffect(() => {
    const fetchData = async () => {
      try {
        const me = await getMe();
        setUserRole(me.roles);
        if (me.roles.includes("Employe")) {
          const data = await getMyEntretiens();
          setEntretiens(data);
        }
      } catch (error) {
        toast.error("Erreur lors du chargement des entretiens.");
      }
    };
    fetchData();
  }, []);

  const handleDownload = async (candidatureId) => {
    try {
      const response = await downloadCv(candidatureId);
      const url = window.URL.createObjectURL(new Blob([response.data]));
      const link = document.createElement("a");
      link.href = url;
      link.setAttribute("download", "cv.pdf");
      document.body.appendChild(link);
      link.click();
    } catch (error) {
      toast.error("Erreur lors du téléchargement du CV.");
    }
  };

  const handleFinalize = (id) => {
    navigate(`/entretien/${id}/finalize`);
  };

  if (userRole === null) return <div>Chargement...</div>;

  if (!userRole.includes("Employe")) {
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
    <Box className="entretiens-container">
      <Typography variant="h4" className="page-title">
        Mes Entretiens
      </Typography>
      <Divider />

      {entretiens.length === 0 ? (
        <Typography>Aucun entretien en attente.</Typography>
      ) : (
        entretiens.map((e) => (
          <Card key={e.id} className="entretien-card">
            <Typography>
              Date : {new Date(e.dateEntretien).toLocaleString()}
            </Typography>
            <Typography>Status : {e.status}</Typography>
            <Box className="action-buttons">
              <Button
                variant="contained"
                onClick={() => handleDownload(e.candidatureId)}
                style={{ backgroundColor: pastelColors.download }}
              >
                <FiDownload style={{ marginRight: 6 }} /> Télécharger CV
              </Button>
              <Button
                variant="contained"
                onClick={() => handleFinalize(e.id)}
                style={{ backgroundColor: pastelColors.finalize }}
              >
                <FiCheck style={{ marginRight: 6 }} /> Finaliser
              </Button>
            </Box>
          </Card>
        ))
      )}
    </Box>
  );
};

export default MesEntretiens;
