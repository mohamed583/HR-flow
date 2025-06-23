import React from "react";
import { useNavigate } from "react-router-dom";
import "./CandidatureList.css";
import API_URL from "../../api/apiClient";

const CandidatureList = ({ candidatures, matchScores }) => {
  const navigate = useNavigate();
  const getColor = (score) => {
    if (score >= 90) return "#A7C7E7"; // Bleu pastel
    if (score >= 75) return "#B2E2B8"; // Vert pastel
    if (score >= 60) return "#FFF9B0"; // Jaune pastel
    if (score >= 40) return "#FFD6A5"; // Orange pastel
    return "#F6C6D4"; // Rouge pastel
  };
  // Tri des candidatures en fonction du score IA
  const sortedCandidatures = [...candidatures].sort((a, b) => {
    const scoreA = matchScores[a.id] ?? -1; // Si pas de score, on met -1 pour le pousser à la fin
    const scoreB = matchScores[b.id] ?? -1;
    return scoreB - scoreA; // Tri décroissant
  });
  return (
    <div className="candidature-list-container">
      <h2>Candidatures</h2>
      {candidatures.length === 0 ? (
        <p>Aucune candidature pour ce poste.</p>
      ) : (
        <ul className="candidature-list">
          {sortedCandidatures.map((candidature) => {
            const score = matchScores[candidature.id];

            return (
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

                {score !== undefined && (
                  <p
                    style={{
                      backgroundColor: getColor(score),
                      padding: "6px 12px",
                      borderRadius: "12px",
                      display: "inline-block",
                      fontWeight: "bold",
                    }}
                  >
                    🔍 Score de correspondance : {score.toFixed(1)}%
                  </p>
                )}

                <div className="candidature-buttons">
                  <button
                    className="details-button"
                    onClick={() => navigate(`/candidature/${candidature.id}`)}
                  >
                    Détails
                  </button>
                </div>
              </li>
            );
          })}
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
