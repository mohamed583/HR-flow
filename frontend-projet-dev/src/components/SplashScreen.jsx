/* eslint-disable no-unused-vars */
// src/components/SplashScreen.jsx
import React, { useEffect, useState } from "react";
import { Box } from "@mui/material";
import { motion, AnimatePresence } from "framer-motion";
import logo from "../assets/hrflow-logo.png";

const SplashScreen = ({ onComplete }) => {
  const [show, setShow] = useState(true);

  useEffect(() => {
    const timer = setTimeout(() => {
      setShow(false);
      onComplete(); // Informe le parent que le splash est terminé
    }, 2000); // Durée d'affichage du splash screen

    return () => clearTimeout(timer);
  }, [onComplete]);

  return (
    <AnimatePresence>
      {show && (
        <motion.div
          initial={{ opacity: 1 }}
          animate={{ opacity: 0 }}
          exit={{ opacity: 0 }}
          transition={{ duration: 1.5 }}
          style={{
            position: "fixed",
            top: 0,
            left: 0,
            width: "100vw",
            height: "100vh",
            backgroundColor: "#FFF",
            zIndex: 9999,
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
          }}
        >
          <motion.img
            src={logo}
            alt="hrflow logo"
            initial={{ scale: 1 }}
            animate={{ scale: 1.05 }}
            transition={{ duration: 1 }}
            style={{
              width: "40vw", // 40% de la largeur de l’écran (responsive)
              maxWidth: "400px", // Max taille sur desktop
              height: "auto", // Conserve le ratio de l’image
            }}
          />
        </motion.div>
      )}
    </AnimatePresence>
  );
};

export default SplashScreen;
