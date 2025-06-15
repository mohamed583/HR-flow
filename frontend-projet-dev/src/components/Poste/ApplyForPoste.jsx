// src/components/Poste/ApplyForPoste.jsx
import React, { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { applyForPoste } from "../../api/poste";
import "./ApplyForPoste.css"; // Style minimaliste et professionnel
import { toast } from "react-toastify";

const ApplyForPoste = () => {
  const [cvFile, setCvFile] = useState(null);
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(false);
  const { id } = useParams();
  const navigate = useNavigate();

  const handleFileChange = (e) => {
    setCvFile(e.target.files[0]);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!cvFile) {
      setError("Veuillez télécharger votre CV.");
      return;
    }

    try {
      await applyForPoste(id, cvFile);
      setSuccess(true);
      toast.success("Candidature prise en compte");
      // Redirige vers la page des candidatures de l'utilisateur
      navigate("/candidatures");
      // eslint-disable-next-line no-unused-vars
    } catch (error) {
      toast.error("Erreur lors de la candidature");
    }
  };

  return (
    <div className="apply-for-poste-container">
      <h2>Postuler pour ce Poste</h2>
      {error && <div className="error">{error}</div>}
      {success && (
        <div className="success">Candidature envoyée avec succès !</div>
      )}
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="cvFile">Télécharger votre CV</label>
          <input
            type="file"
            id="cvFile"
            name="cvFile"
            onChange={handleFileChange}
            required
          />
        </div>
        <button type="submit">Postuler</button>
      </form>
    </div>
  );
};

export default ApplyForPoste;
