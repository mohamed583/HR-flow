// src/pages/CreatePostePage.jsx
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getMe } from "../../api/auth"; // On importe la fonction getMe
import CreatePoste from "../../components/Poste/CreatePoste";

const CreatePostePage = () => {
  const [userRole, setUserRole] = useState(null);
  const navigate = useNavigate();

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
    <div className="create-poste-page">
      <CreatePoste />
    </div>
  );
};

export default CreatePostePage;
