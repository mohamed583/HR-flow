import React, { createContext, useContext, useState, useEffect } from "react";
import { getPostes as fetchPostes } from "../api/poste";

const PosteContext = createContext();

export const usePosteContext = () => useContext(PosteContext);

export const PosteProvider = ({ children }) => {
  const [postes, setPostes] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const getPostes = async () => {
      try {
        const postesData = await fetchPostes();
        setPostes(postesData);
      } catch (error) {
        console.error("Erreur lors de la récupération des postes", error);
      } finally {
        setLoading(false);
      }
    };

    getPostes();
  }, []);

  return (
    <PosteContext.Provider value={{ postes, loading }}>
      {children}
    </PosteContext.Provider>
  );
};
