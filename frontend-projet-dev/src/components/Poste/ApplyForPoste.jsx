/* eslint-disable no-unused-vars */
import React, { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { applyForPoste } from "../../api/poste";
import "./ApplyForPoste.css";
import { toast } from "react-toastify";

const ApplyForPoste = () => {
  const [cvFile, setCvFile] = useState(null);
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(false);
  const { id } = useParams();
  const navigate = useNavigate();

  const handleFileChange = (e) => {
    const file = e.target.files[0];
    if (file && file.type !== "application/pdf") {
      setError("Seuls les fichiers PDF sont autorisés.");
      setCvFile(null);
    } else {
      setError(null);
      setCvFile(file);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!cvFile) {
      setError("Veuillez télécharger votre CV au format PDF.");
      return;
    }

    try {
      await applyForPoste(id, cvFile);
      setSuccess(true);
      toast.success("Candidature prise en compte");
      navigate("/candidatures");
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
          <label htmlFor="cvFile">Télécharger votre CV (PDF uniquement)</label>
          <input
            type="file"
            id="cvFile"
            name="cvFile"
            accept=".pdf"
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
