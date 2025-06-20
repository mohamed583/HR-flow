/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import { getMesInscriptions, deleteInscription } from "../../api/inscription";
import { getFormationById } from "../../api/formation";
import { getMe } from "../../api/auth";
import { Box, Card, Typography, Button } from "@mui/material";
import { toast } from "react-toastify";
import { FiTrash2 } from "react-icons/fi";
import "./MesInscriptions.css";

const pastel = {
  blue: "#A7C7E7",
  green: "#B2E2B8",
  pink: "#F6C6D4",
  yellow: "#FFF1B0",
  gray: "#E5E5E5",
  violet: "#D5C6E0",
  text: "#333",
};

const MesInscriptions = () => {
  const [inscriptions, setInscriptions] = useState([]);
  const [user, setUser] = useState(null);
  const [formations, setFormations] = useState({});

  useEffect(() => {
    const fetchData = async () => {
      try {
        const me = await getMe();
        setUser(me);

        if (me.roles.includes("Employe")) {
          const res = await getMesInscriptions();
          setInscriptions(res.data);

          const formationMap = {};
          for (const i of res.data) {
            const f = await getFormationById(i.formationId);
            formationMap[i.formationId] = f.data;
          }
          setFormations(formationMap);
        }
      } catch {
        toast.error("Erreur lors du chargement des inscriptions.");
      }
    };

    fetchData();
  }, []);

  const handleDelete = async (id) => {
    if (window.confirm("Voulez-vous vraiment annuler cette inscription ?")) {
      try {
        await deleteInscription(id);
        setInscriptions(inscriptions.filter((i) => i.id !== id));
        toast.success("Inscription annulée.");
      } catch {
        toast.error("Erreur lors de la suppression.");
      }
    }
  };

  if (!user) return <div>Chargement...</div>;

  if (!user.roles.includes("Employe")) {
    return (
      <Box className="access-denied-container">
        <Typography variant="h5">Accès refusé</Typography>
        <Typography>Cette section est réservée aux employés.</Typography>
      </Box>
    );
  }

  return (
    <Box className="mes-inscriptions-container">
      <Typography variant="h5" className="section-title">
        Mes Inscriptions
      </Typography>

      {inscriptions.length === 0 ? (
        <Typography>Aucune inscription trouvée.</Typography>
      ) : (
        inscriptions.map((inscription) => {
          const formation = formations[inscription.formationId];
          return (
            <Card key={inscription.id} className="inscription-card">
              <Typography className="formation-title">
                {formation?.titre}
              </Typography>
              <Typography>Statut : {inscription.statut}</Typography>
              <Typography>
                Date de début :{" "}
                {new Date(formation?.dateDebut).toLocaleDateString()}
              </Typography>
              <Typography>
                Date de fin :{" "}
                {new Date(formation?.dateFin).toLocaleDateString()}
              </Typography>

              {inscription.statut === "EnCours" && (
                <Button
                  onClick={() => handleDelete(inscription.id)}
                  variant="outlined"
                  className="cancel-button"
                >
                  <FiTrash2 style={{ marginRight: "5px" }} />
                  Annuler
                </Button>
              )}
            </Card>
          );
        })
      )}
    </Box>
  );
};

export default MesInscriptions;
