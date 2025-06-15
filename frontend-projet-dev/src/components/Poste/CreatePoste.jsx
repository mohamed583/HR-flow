// src/components/CreatePoste.jsx
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { createPoste } from "../../api/poste";
import { ToastContainer, toast } from "react-toastify";
import "./CreatePoste.css";

const CreatePoste = () => {
  const [nom, setNom] = useState("");
  const [description, setDescription] = useState("");
  const [statutPoste, setStatutPoste] = useState(0); // 0 = Ouvert, 1 = Fermé
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    const posteData = {
      information: {
        nom,
        description,
        statutPoste,
      },
    };

    try {
      const result = await createPoste(posteData);
      toast.success("Poste créé avec succès !");
      navigate(`/poste/${result.data.data.id}`); // Redirection vers les détails du poste
      // eslint-disable-next-line no-unused-vars
    } catch (err) {
      toast.error("Erreur lors de la création du poste");
    }
  };

  return (
    <div className="create-poste-container">
      <h1>Créer un nouveau Poste</h1>
      <form onSubmit={handleSubmit}>
        <label>
          Nom du poste:
          <input
            type="text"
            value={nom}
            onChange={(e) => setNom(e.target.value)}
            required
          />
        </label>
        <label>
          Description:
          <textarea
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            required
          />
        </label>
        <label>
          Statut du poste:
          <select
            value={statutPoste}
            onChange={(e) => setStatutPoste(Number(e.target.value))}
          >
            <option value={0}>Ouvert</option>
            <option value={1}>Fermé</option>
          </select>
        </label>
        <button type="submit">Créer le poste</button>
      </form>
      <ToastContainer />
    </div>
  );
};

export default CreatePoste;
