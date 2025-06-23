/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getPosteById } from "../../api/poste";
import { ToastContainer, toast } from "react-toastify";
import { deletePoste, updatePosteStatus } from "../../api/poste";
import { getMe } from "../../api/auth";
import "./PosteDetails.css";

const PosteDetails = () => {
  const { id } = useParams();
  const [poste, setPoste] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [isAdmin, setIsAdmin] = useState(false);
  const [isCandidat, setIsCandidat] = useState(false);
  const [status, setStatus] = useState(0);
  const [showModal, setShowModal] = useState(false); // État pour afficher le modal de confirmation
  const navigate = useNavigate();

  useEffect(() => {
    const fetchPosteDetails = async () => {
      try {
        const response = await getPosteById(id);
        if (response.success) {
          setPoste(response.data);
          setStatus(response.data.statutPoste);
        } else {
          setError("Poste non trouvé");
        }
      } catch (err) {
        setError("Erreur lors du chargement des détails du poste");
        toast.error("Erreur lors du chargement des détails");
      }
      setLoading(false);
    };

    fetchPosteDetails();
  }, [id]);

  useEffect(() => {
    const fetchUserRole = async () => {
      try {
        const userData = await getMe();
        if (userData.roles.includes("Admin")) {
          setIsAdmin(true);
        }
        if (userData.roles.includes("Candidat")) {
          setIsCandidat(true);
        }
      } catch (err) {
        console.error(
          "Erreur lors de la récupération des données utilisateur",
          err
        );
      }
    };

    fetchUserRole();
  }, []);

  const handleDelete = async () => {
    try {
      await deletePoste(id);
      toast.success("Poste supprimé avec succès");
      navigate("/postes");
    } catch (err) {
      toast.error("Erreur lors de la suppression du poste");
    }
  };

  const handleStatusChange = async (e) => {
    const newStatus = parseInt(e.target.value);
    try {
      await updatePosteStatus(id, newStatus);
      setStatus(newStatus);
      toast.success("Statut mis à jour");
    } catch (err) {
      toast.error("Erreur lors de la mise à jour du statut");
    }
  };

  // Fonction pour fermer le modal
  const closeModal = () => setShowModal(false);

  // Fonction pour confirmer la suppression
  const confirmDelete = () => {
    handleDelete();
    closeModal(); // Fermer le modal après confirmation
  };

  if (loading) return <div className="loading">Chargement...</div>;
  if (error) return <div>{error}</div>;

  return (
    <div className="poste-details-container">
      <h1 className="poste-title">Détails du Poste</h1>
      {poste && (
        <div className="poste-details-card">
          <p className="poste-info">
            <strong>Nom:</strong> {poste.nom}
          </p>
          <p className="poste-info">
            <strong>Description:</strong> {poste.description}
          </p>
          <p className="poste-info">
            <strong>Statut:</strong> {status === 0 ? "Ouvert" : "Fermé"}
          </p>

          {isAdmin && (
            <div className="admin-actions">
              <button
                className="btn btn-primary"
                onClick={() => navigate(`/poste/${poste.id}/edit`)}
              >
                <i className="fas fa-edit"></i> Modifier
              </button>
              <button
                className="btn btn-danger"
                onClick={() => setShowModal(true)} // Afficher le modal de confirmation
              >
                <i className="fas fa-trash"></i> Supprimer
              </button>
              <button
                className="btn btn-info"
                onClick={() => navigate(`/poste/${poste.id}/candidatures`)}
              >
                <i className="fas fa-list"></i> Afficher les candidatures
              </button>

              <div className="status-select">
                <label htmlFor="status">Changer le statut:</label>
                <select
                  id="status"
                  value={status}
                  onChange={handleStatusChange}
                >
                  <option value={0}>Ouvert</option>
                  <option value={1}>Fermé</option>
                </select>
              </div>
            </div>
          )}
          {isCandidat && (
            <button
              className="btn btn-success"
              onClick={() => navigate(`/poste/${poste.id}/apply`)}
            >
              Postuler
            </button>
          )}
        </div>
      )}

      {/* Modal de confirmation */}
      {showModal && (
        <div className="modal-overlay">
          <div className="modal-container">
            <h3>Êtes-vous sûr de vouloir supprimer ce poste ?</h3>
            <div className="modal-actions">
              <button className="btn btn-danger" onClick={confirmDelete}>
                Oui, supprimer
              </button>
              <button className="btn btn-secondary" onClick={closeModal}>
                Annuler
              </button>
            </div>
          </div>
        </div>
      )}

      <ToastContainer />
    </div>
  );
};

export default PosteDetails;
