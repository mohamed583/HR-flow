import React, { useState, useEffect } from "react";
import {
  Box,
  Drawer,
  List,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  AppBar,
  Toolbar,
  IconButton,
  Tooltip,
  Collapse,
} from "@mui/material";
import { Outlet, useNavigate } from "react-router-dom";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import LogoutIcon from "@mui/icons-material/Logout";
import MenuIcon from "@mui/icons-material/Menu";
import ExpandLessIcon from "@mui/icons-material/ExpandLess";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import logo from "../assets/logo.png";
import { getMe } from "../api/auth"; // Chemin d’accès correct

const drawerWidth = 240;

// Palette pastel
const pastelColors = {
  primary: "#A7C7E7",
  secondary: "#B2E2B8",
  accent: "#F6C6D4",
  background: "#E5E5E5",
  text: "#333",
};

const SidebarLayout = () => {
  const navigate = useNavigate();
  const [mobileOpen, setMobileOpen] = useState(window.innerWidth >= 600);
  const [isAdmin, setIsAdmin] = useState(false);
  const [organisationOpen, setOrganisationOpen] = useState(false);
  const [isCandidat, setIsCandidat] = useState(false);
  const [isEmploye, setIsEmploye] = useState(false);

  const handleDrawerToggle = () => {
    setMobileOpen(!mobileOpen);
  };

  const handleLogout = () => {
    console.log("Logout");
  };

  const handleProfile = () => {
    navigate("/profile");
  };

  const handleOrganisationToggle = () => {
    setOrganisationOpen(!organisationOpen);
  };

  const menuItems = [
    {
      text: "Acceuil",
      path: "/",
      icon: (
        <img
          src={"src/assets/accueil.png"}
          alt="Home"
          style={{ width: 24, height: 24 }}
        />
      ),
    },
    ...(isCandidat || isAdmin
      ? [
          {
            text: "Postes",
            path: "/Postes",
            icon: (
              <img
                src={"src/assets/job.png"}
                alt="Job"
                style={{ width: 24, height: 24 }}
              />
            ),
          },
        ]
      : []),
    ...(!isCandidat
      ? [
          {
            text: "Formations",
            path: "/formations",
            icon: (
              <img
                src={"src/assets/training.png"}
                alt="Formation"
                style={{ width: 24, height: 24 }}
              />
            ),
          },
        ]
      : []),
    ...(isEmploye
      ? [
          {
            text: "Inscriptions",
            path: "/mes-inscriptions",
            icon: (
              <img
                src={"src/assets/inscription.png"}
                alt="Inscription"
                style={{ width: 24, height: 24 }}
              />
            ),
          },
          {
            text: "Congés",
            path: "/conges",
            icon: (
              <img
                src={"src/assets/conge.png"}
                alt="Conge"
                style={{ width: 24, height: 24 }}
              />
            ),
          },
          {
            text: "Evaluations",
            path: "/evaluations",
            icon: (
              <img
                src={"src/assets/evaluation.png"}
                alt="Evaluation"
                style={{ width: 24, height: 24 }}
              />
            ),
          },
        ]
      : []),
  ];

  const organisationItems = [
    { text: "Départements", path: "/departements" },
    { text: "Équipes", path: "/equipes" },
    { text: "Employés", path: "/employes" },
    { text: "Formateurs", path: "/formateurs" },
  ];

  useEffect(() => {
    // Vérifie si l’utilisateur est admin
    const fetchUserRole = async () => {
      try {
        const user = await getMe();
        if (user && user.roles.includes("Admin")) {
          setIsAdmin(true);
        }
        if (user.roles.includes("Candidat")) {
          setIsCandidat(true);
        }
        if (user.roles.includes("Employe")) {
          setIsEmploye(true);
        }
      } catch (error) {
        console.error("Erreur lors de la récupération du rôle :", error);
      }
    };
    fetchUserRole();
  }, []);

  const drawerContent = (
    <Box sx={{ textAlign: "center" }}>
      <Toolbar />
      <List>
        {menuItems.map((item) => (
          <ListItemButton
            key={item.text}
            onClick={() => {
              navigate(item.path);
              if (window.innerWidth < 600) setMobileOpen(false);
            }}
            sx={{
              borderRadius: 2,
              my: 0.5,
              mx: 1,
              transition: "background-color 0.3s, transform 0.2s",
              "&:hover": {
                backgroundColor: pastelColors.secondary,
                transform: "translateX(4px)",
              },
            }}
          >
            <ListItemIcon sx={{ color: pastelColors.text }}>
              {item.icon}
            </ListItemIcon>
            <ListItemText
              primary={item.text}
              primaryTypographyProps={{
                fontWeight: 500,
              }}
            />
          </ListItemButton>
        ))}

        {/* Bouton Organisation (visible uniquement pour admin) */}
        {isAdmin && (
          <>
            <ListItemButton
              onClick={handleOrganisationToggle}
              sx={{
                borderRadius: 2,
                my: 0.5,
                mx: 1,
                transition: "background-color 0.3s, transform 0.2s",
                "&:hover": {
                  backgroundColor: pastelColors.secondary,
                  transform: "translateX(4px)",
                },
              }}
            >
              <ListItemIcon>
                <img
                  src={"src/assets/organisation.png"}
                  alt="Organisation"
                  style={{ width: 24, height: 24 }}
                />
              </ListItemIcon>
              <ListItemText
                primary="Organisation"
                primaryTypographyProps={{ fontWeight: 500 }}
              />
              {organisationOpen ? <ExpandLessIcon /> : <ExpandMoreIcon />}
            </ListItemButton>

            <Collapse in={organisationOpen} timeout="auto" unmountOnExit>
              <List component="div" disablePadding>
                {organisationItems.map((subItem) => (
                  <ListItemButton
                    key={subItem.text}
                    onClick={() => {
                      navigate(subItem.path);
                      if (window.innerWidth < 600) setMobileOpen(false);
                    }}
                    sx={{
                      pl: 4,
                      borderRadius: 2,
                      my: 0.5,
                      mx: 1,
                      transition: "background-color 0.3s, transform 0.2s",
                      "&:hover": {
                        backgroundColor: pastelColors.secondary,
                        transform: "translateX(4px)",
                      },
                    }}
                  >
                    <ListItemText
                      primary={subItem.text}
                      primaryTypographyProps={{ fontWeight: 500 }}
                    />
                  </ListItemButton>
                ))}
              </List>
            </Collapse>
          </>
        )}
      </List>
    </Box>
  );

  useEffect(() => {
    const handleResize = () => {
      if (window.innerWidth >= 600) {
        setMobileOpen(true);
      } else {
        setMobileOpen(false);
      }
    };

    window.addEventListener("resize", handleResize);
    handleResize();

    return () => window.removeEventListener("resize", handleResize);
  }, []);

  return (
    <Box sx={{ display: "flex" }}>
      {/* AppBar */}
      <AppBar
        position="fixed"
        elevation={0}
        sx={{
          backgroundColor: pastelColors.primary,
          color: pastelColors.text,
          zIndex: 1300,
          boxShadow: "0 2px 4px rgba(0, 0, 0, 0.1)",
        }}
      >
        <Toolbar sx={{ justifyContent: "space-between" }}>
          <IconButton
            color="inherit"
            edge="start"
            onClick={handleDrawerToggle}
            sx={{ mr: 2 }}
          >
            <MenuIcon />
          </IconButton>

          <Box
            sx={{
              display: "flex",
              alignItems: "center",
              cursor: "pointer",
            }}
            onClick={() => navigate("/")}
          >
            <img
              src={logo}
              alt="Logo"
              style={{
                height: 40,
                width: "auto",
                borderRadius: 8,
              }}
            />
          </Box>

          <Box>
            <Tooltip title="Profil">
              <IconButton
                onClick={handleProfile}
                sx={{ color: pastelColors.text }}
              >
                <AccountCircleIcon />
              </IconButton>
            </Tooltip>

            <Tooltip title="Déconnexion">
              <IconButton
                onClick={handleLogout}
                sx={{ color: pastelColors.text }}
              >
                <LogoutIcon />
              </IconButton>
            </Tooltip>
          </Box>
        </Toolbar>
      </AppBar>

      {/* Drawer */}
      <Drawer
        variant="persistent"
        open={mobileOpen}
        sx={{
          width: drawerWidth,
          flexShrink: 0,
          position: "fixed",
          height: "100vh",
          [`& .MuiDrawer-paper`]: {
            width: drawerWidth,
            boxSizing: "border-box",
            backgroundColor: pastelColors.background,
            color: pastelColors.text,
            position: "fixed",
            height: "100vh",
            borderRight: "1px solid #ccc",
            boxShadow: "2px 0 5px -2px rgba(0,0,0,0.1)",
          },
        }}
      >
        {drawerContent}
      </Drawer>

      {/* Contenu principal */}
      <Box
        component="main"
        sx={{
          flexGrow: 1,
          bgcolor: "#fff",
          p: 3,
          minHeight: "100vh",
          borderTopLeftRadius: { xs: 0, sm: 16 },
          borderBottomLeftRadius: { xs: 0, sm: 16 },
          boxShadow: "0 2px 8px rgba(0, 0, 0, 0.05)",
          transition: "margin-left 0.3s",
          ml: mobileOpen ? `${drawerWidth}px` : 0,
        }}
      >
        <Toolbar />
        <Outlet />
      </Box>
    </Box>
  );
};

export default SidebarLayout;
