/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import { getDepartements } from "../../api/departement";
import { createEquipe } from "../../api/equipe";
import { getMe } from "../../api/auth";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import "./EquipeCreate.css";

const EquipeCreate = () => {
  const [nom, setNom] = useState("");
  const [departementId, setDepartementId] = useState("");
  const [departements, setDepartements] = useState([]);
  const [userRole, setUserRole] = useState(null);

  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const me = await getMe();
        setUserRole(me.roles);
        if (!me.roles.includes("Admin")) {
          toast.error("Accès refusé.");
          navigate("/"); // Redirige si non Admin
          return;
        }

        const data = await getDepartements();
        setDepartements(data);
      } catch (error) {
        toast.error("Erreur lors du chargement des données.");
      }
    };

    fetchData();
  }, [navigate]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!nom || !departementId) {
      toast.error("Veuillez remplir tous les champs.");
      return;
    }

    try {
      const equipeData = { nom, departementId: parseInt(departementId) };
      const createdEquipe = await createEquipe(equipeData);

      toast.success("Équipe créée avec succès !");
      navigate(`/equipe/${createdEquipe.id}`);
    } catch (error) {
      toast.error("Erreur lors de la création de l'équipe.");
    }
  };

  if (userRole === null) {
    return <div>Chargement...</div>;
  }

  return (
    <div className="equipe-create-container">
      <h2 className="equipe-create-title">Créer une Équipe</h2>
      <form onSubmit={handleSubmit} className="equipe-create-form">
        <label className="equipe-create-label">
          Nom de l'équipe :
          <input
            type="text"
            value={nom}
            onChange={(e) => setNom(e.target.value)}
            className="equipe-create-input"
            placeholder="Entrez le nom de l'équipe"
          />
        </label>

        <label className="equipe-create-label">
          Département :
          <select
            value={departementId}
            onChange={(e) => setDepartementId(e.target.value)}
            className="equipe-create-select"
          >
            <option value="">Sélectionner un département</option>
            {departements.map((dept) => (
              <option key={dept.id} value={dept.id}>
                {dept.nom}
              </option>
            ))}
          </select>
        </label>

        <button type="submit" className="equipe-create-button">
          Créer l'Équipe
        </button>
      </form>
    </div>
  );
};

export default EquipeCreate;
