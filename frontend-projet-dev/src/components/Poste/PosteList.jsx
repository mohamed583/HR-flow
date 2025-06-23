import React, { useEffect, useState } from "react";
import { getPostes } from "../../api/poste";
import { getMe } from "../../api/auth";
import { Link } from "react-router-dom";
import { ToastContainer, toast } from "react-toastify";
import "./PosteList.css"; // Fichier CSS pour la personnalisation des styles
import iconVide from "../../assets/vide.png";

const PosteList = () => {
  const [postes, setPostes] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [isAdmin, setIsAdmin] = useState(false);

  // Récupérer les informations de l'utilisateur pour déterminer les rôles
  useEffect(() => {
    const fetchUserRole = async () => {
      try {
        const userData = await getMe();
        if (userData.roles.includes("Admin")) {
          setIsAdmin(true);
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

  // Récupérer les postes en fonction du rôle de l'utilisateur
  useEffect(() => {
    const fetchPostes = async () => {
      try {
        const data = await getPostes();
        const filteredPostes = isAdmin
          ? data.data
          : data.data.filter((poste) => poste.statutPoste === 0);
        setPostes(filteredPostes);
        setLoading(false);
        // eslint-disable-next-line no-unused-vars
      } catch (err) {
        setError("Erreur lors du chargement des postes");
        setLoading(false);
        toast.error("Erreur lors du chargement des postes");
      }
    };

    fetchPostes();
  }, [isAdmin]);

  if (loading) return <div className="loading">Chargement...</div>;
  if (error) return <div>{error}</div>;

  return (
    <div className="poste-list-container">
      {/* Bouton de création visible uniquement pour les admins */}
      {isAdmin && (
        <div className="create-poste-button-container">
          <Link to="/poste/create" className="btn create-poste-btn">
            Créer un poste
          </Link>
        </div>
      )}

      {postes.length === 0 ? (
        <div className="no-postes-container animated fadeIn">
          <div className="no-postes-icon">
            <img src={iconVide} alt="Aucun poste" className="no-postes-img" />
          </div>
          <h1 className="no-postes-message">
            Pas de postes ouverts pour le moment
          </h1>
          <p className="no-postes-submessage">
            Reste à l'écoute, de nouvelles opportunités arrivent bientôt !
          </p>
        </div>
      ) : (
        <div>
          <h1 className="page-title">Liste des postes</h1>
          <table className="table animated fadeIn">
            <thead>
              <tr>
                <th>Nom</th>
                <th>Description</th>
                <th>Actions</th>
                {isAdmin && <th>Status</th>}
              </tr>
            </thead>
            <tbody>
              {postes.map((poste) => (
                <tr key={poste.id} className="table-row">
                  <td>{poste.nom}</td>
                  <td>{poste.description}</td>
                  <td>
                    <Link
                      to={`/poste/${poste.id}`}
                      className="btn"
                      style={{
                        backgroundColor: "#D5C6E0",
                        animation: "bounce 1s infinite",
                      }}
                    >
                      Détails
                    </Link>
                  </td>
                  {isAdmin && (
                    <td>
                      <strong>
                        {poste.statutPoste === 0 ? "Ouvert" : "Fermé"}
                      </strong>
                    </td>
                  )}
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
      <ToastContainer position="top-right" autoClose={3000} />
    </div>
  );
};

export default PosteList;
