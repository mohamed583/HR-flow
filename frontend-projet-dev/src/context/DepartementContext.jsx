import { createContext, useState, useContext } from "react";

const DepartementContext = createContext();

export const DepartementProvider = ({ children }) => {
  const [departements, setDepartements] = useState([]);

  return (
    <DepartementContext.Provider value={{ departements, setDepartements }}>
      {children}
    </DepartementContext.Provider>
  );
};

export const useDepartement = () => useContext(DepartementContext);
