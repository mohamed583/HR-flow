/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getMe, changeLoginInfo } from "../../api/auth";
import { Box, Typography, TextField, Button, Paper } from "@mui/material";
import { toast } from "react-toastify";
import AccessDeniedImage from "../../assets/accessDenied.png";
import "./ChangeLogin.css";

const ChangeLogin = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [loginInfo, setLoginInfo] = useState({ newEmail: "", newPassword: "" });
  const [isAdmin, setIsAdmin] = useState(false);

  useEffect(() => {
    (async () => {
      try {
        const me = await getMe();
        if (me.roles.includes("Admin")) setIsAdmin(true);
      } catch {
        toast.error("Erreur authentification");
      }
    })();
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setLoginInfo((p) => ({ ...p, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await changeLoginInfo(id, loginInfo);
      toast.success("Infos de connexion modifiées.");
      navigate(`/formateur/${id}`);
    } catch {
      toast.error("Erreur de modification");
    }
  };

  if (!isAdmin)
    return (
      <Box className="access-denied">
        <img src={AccessDeniedImage} alt="Accès refusé" />
        <Typography variant="h5">Accès refusé</Typography>
        <Typography>Vous devez être Admin.</Typography>
      </Box>
    );

  return (
    <Box className="change-login-container">
      <Paper className="change-login-paper">
        <Typography variant="h5" className="form-title">
          Changer les informations de connexion
        </Typography>
        <form onSubmit={handleSubmit}>
          <TextField
            label="Nouvel email"
            name="newEmail"
            value={loginInfo.newEmail}
            onChange={handleChange}
            fullWidth
            margin="normal"
            required
          />
          <TextField
            label="Nouveau mot de passe"
            name="newPassword"
            type="password"
            value={loginInfo.newPassword}
            onChange={handleChange}
            fullWidth
            margin="normal"
            required
          />
          <Box mt={2} className="buttons">
            <Button variant="outlined" onClick={() => navigate(-1)}>
              Annuler
            </Button>
            <Button type="submit" variant="contained" color="primary">
              Valider
            </Button>
          </Box>
        </form>
      </Paper>
    </Box>
  );
};

export default ChangeLogin;
