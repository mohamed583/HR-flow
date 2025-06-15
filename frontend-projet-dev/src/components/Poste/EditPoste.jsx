/* eslint-disable no-unused-vars */
// src/components/Poste/EditPoste.jsx
import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { updatePoste, getPosteById } from "../../api/poste";
import { getMe } from "../../api/auth"; // Utiliser la fonction getMe pour vérifier le rôle
import "./EditPoste.css";

const EditPoste = () => {
  const [poste, setPoste] = useState({
    nom: "",
    description: "",
    statutPoste: 0, // Assurer que le statut est un entier
  });
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [isAdmin, setIsAdmin] = useState(false);
  const { id } = useParams(); // Récupérer l'id du poste depuis l'URL
  const navigate = useNavigate();

  useEffect(() => {
    // Vérifier le rôle de l'utilisateur
    const checkUserRole = async () => {
      try {
        const user = await getMe();
        if (!user.roles.includes("Admin")) {
          navigate("/poste"); // Rediriger si l'utilisateur n'a pas le rôle Admin
        } else {
          setIsAdmin(true);
        }
      } catch (error) {
        console.error("Erreur de vérification du rôle :", error);
      }
    };

    // Charger les détails du poste à partir de l'API
    const fetchPosteDetails = async () => {
      try {
        const posteData = await getPosteById(id);
        setPoste(posteData.data); // Remplir les informations du poste dans l'état
        setLoading(false);
      } catch (error) {
        setError("Erreur lors de la récupération du poste");
        setLoading(false);
      }
    };

    checkUserRole();
    fetchPosteDetails();
  }, [id, navigate]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    if (name === "statutPoste") {
      // Convertir la valeur en entier pour 'statutPoste'
      setPoste((prev) => ({
        ...prev,
        [name]: parseInt(value), // Force la conversion en entier
      }));
    } else {
      setPoste((prev) => ({
        ...prev,
        [name]: value,
      }));
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const posteToUpdate = {
      id: id, // Inclure l'id du poste
      dto: {
        nom: poste.nom,
        description: poste.description,
        statutPoste: poste.statutPoste, // Assurer que statutPoste est un entier
      },
    };

    try {
      await updatePoste(id, posteToUpdate);
      navigate(`/poste/${id}`); // Afficher les détails du poste après modification
    } catch (error) {
      setError("Erreur lors de la mise à jour du poste");
    }
  };

  if (loading) return <div>Chargement...</div>;
  if (error) return <div>{error}</div>;

  return (
    <div className="edit-poste-container">
      <h1>Modifier le poste</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="nom">Nom du Poste</label>
          <input
            type="text"
            id="nom"
            name="nom"
            value={poste.nom || ""}
            onChange={handleChange}
            required
          />
        </div>
        <div>
          <label htmlFor="description">Description</label>
          <textarea
            id="description"
            name="description"
            value={poste.description || ""}
            onChange={handleChange}
            required
          />
        </div>
        <div>
          <label htmlFor="statutPoste">Statut du Poste</label>
          <select
            id="statutPoste"
            name="statutPoste"
            value={poste.statutPoste || 0}
            onChange={handleChange}
            required
          >
            <option value={0}>Ouvert</option>
            <option value={1}>Fermé</option>
          </select>
        </div>
        <button type="submit">Mettre à jour le poste</button>
      </form>
    </div>
  );
};

export default EditPoste;
