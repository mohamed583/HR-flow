import React from "react";
import "./CandidatureList.css";
import API_URL from "../../api/apiClient";

const CandidatureList = ({ candidatures }) => {
  return (
    <div className="candidature-list-container">
      <h2>Candidatures</h2>
      {candidatures.length === 0 ? (
        <p>Aucune candidature pour ce poste.</p>
      ) : (
        <ul className="candidature-list">
          {candidatures.map((candidature) => (
            <li key={candidature.id} className="candidature-item">
              <p>
                <strong>Nom :</strong> {candidature.candidat.nom}{" "}
                {candidature.candidat.prenom}
              </p>
              <p>
                <strong>CV :</strong>{" "}
                <a
                  href={`${API_URL}/${candidature.cvPath}`}
                  target="_blank"
                  rel="noopener noreferrer"
                >
                  Afficher le CV
                </a>
              </p>
              <p>
                <strong>Statut :</strong> {getStatusLabel(candidature.status)}
              </p>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

// Petite fonction utilitaire pour afficher le statut
const getStatusLabel = (status) => {
  switch (status) {
    case 0:
      return "En attente";
    case 1:
      return "Acceptée";
    case 2:
      return "Refusée";
    default:
      return "Inconnu";
  }
};

export default CandidatureList;
