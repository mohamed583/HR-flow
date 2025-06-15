import React, { useEffect, useState } from "react";
import DepartementCreate from "../../components/Departement/DepartementCreate";
import { getMe } from "../../api/auth";
import { Box, Typography } from "@mui/material";
import AccessDeniedImage from "../../assets/accessDenied.png";

const DepartementCreatePage = () => {
  const [isAdmin, setIsAdmin] = useState(null);

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const user = await getMe();
        setIsAdmin(user.roles.includes("Admin"));
        // eslint-disable-next-line no-unused-vars
      } catch (error) {
        setIsAdmin(false);
      }
    };
    fetchUser();
  }, []);

  if (isAdmin === null) {
    return <div>Chargement...</div>;
  }

  if (!isAdmin) {
    return (
      <Box className="access-denied-container">
        <img src={AccessDeniedImage} alt="Accès refusé" />
        <Typography
          variant="h5"
          className="access-denied-message"
          sx={{ mb: 2 }}
        >
          Accès refusé
        </Typography>
        <Typography className="access-denied-message">
          Vous n'avez pas les autorisations nécessaires pour consulter cette
          page.
        </Typography>
      </Box>
    );
  }

  return <DepartementCreate />;
};

export default DepartementCreatePage;
