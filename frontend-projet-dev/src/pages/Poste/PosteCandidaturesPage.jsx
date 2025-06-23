import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getCandidaturesByPoste } from "../../api/poste";
import CandidatureList from "../../components/Poste/CandidatureList";
import { getMe } from "../../api/auth"; // On importe la fonction getMe
import { getCvMatchByPosteId } from "../../api/candidature";

const PosteCandidaturesPage = () => {
  const { posteId } = useParams();
  const [candidatures, setCandidatures] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [userRole, setUserRole] = useState(null);
  const navigate = useNavigate();
  const [matchScores, setMatchScores] = useState({});

  useEffect(() => {
    const fetchUserRole = async () => {
      try {
        const user = await getMe(); // On appelle l'API pour récupérer l'utilisateur
        setUserRole(user.roles); // On suppose que l'objet `user` contient une propriété `roles`
        // eslint-disable-next-line no-unused-vars
      } catch (error) {
        console.error(
          "Erreur lors de la récupération des informations utilisateur"
        );
        navigate("/auth"); // Rediriger vers la page de login si l'appel échoue
      }
    };
    fetchUserRole();
  }, [navigate]);

  useEffect(() => {
    const fetchCandidatures = async () => {
      try {
        const result = await getCandidaturesByPoste(posteId);
        if (result.success) {
          setCandidatures(result.data);
          // ✅ Fetch matching scores
          if (result.data.length > 0) {
            const matchResult = await getCvMatchByPosteId(posteId);
            if (matchResult.success) {
              const map = {};
              matchResult.data.forEach((m) => {
                map[m.candidatureId] = m.matchingScore;
              });
              setMatchScores(map);
            } else {
              setError("Erreur lors du chargement des scores.");
            }
          }
        } else {
          setError("Erreur lors du chargement des candidatures.");
        }
        // eslint-disable-next-line no-unused-vars
      } catch (err) {
        setError("Erreur lors du chargement des candidatures.");
      } finally {
        setLoading(false);
      }
    };

    fetchCandidatures();
  }, [posteId]);
  // Si les rôles ne sont pas encore récupérés, on attend (loading state)
  if (userRole === null) {
    return <div>Chargement...</div>;
  }
  // Si l'utilisateur n'est pas Admin, on le redirige
  if (!userRole.includes("Admin")) {
    navigate("/postes"); // Redirection vers la liste des postes si ce n'est pas un Admin
    return null;
  }
  return (
    <div>
      {loading ? (
        <p>Chargement des candidatures...</p>
      ) : error ? (
        <p style={{ color: "#F6C6D4" }}>{error}</p> // Rose pastel pour les erreurs
      ) : (
        <CandidatureList
          candidatures={candidatures}
          matchScores={matchScores}
        />
      )}
    </div>
  );
};

export default PosteCandidaturesPage;
