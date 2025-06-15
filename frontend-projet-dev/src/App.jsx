import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { AuthProvider } from "./context/AuthContext";
import { PosteProvider } from "./context/PosteContext"; // Importer le PosteProvider
import AuthPage from "./pages/AuthPage";
import HomePage from "./pages/HomePage";
import PosteListPage from "./pages/Poste/PosteListPage";
import PosteDetailsPage from "./pages/Poste/PosteDetailsPage";
import CreatePostePage from "./pages/Poste/CreatePostePage";
import EditPostePage from "./pages/Poste/EditPostePage";
import PrivateRoute from "./routes/PrivateRoute";
import PublicRoute from "./routes/PublicRoute";
import { ToastContainer } from "react-toastify";
import SidebarLayout from "./pages/SidebarLayout";
import "react-toastify/dist/ReactToastify.css";
import ApplyForPostePage from "./pages/Poste/ApplyForPostePage";
import PosteCandidaturesPage from "./pages/Poste/PosteCandidaturesPage";
import DepartementListPage from "./pages/Departement/DepartementListPage";
import DepartementCreatePage from "./pages/Departement/DepartementCreatePage";
import EditDepartementPage from "./pages/Departement/EditDepartementPage";
import DepartementDetailsPage from "./pages/Departement/DepartementDetailsPage";
import EquipeListPage from "./pages/Equipe/EquipeListPage";
import EquipeCreatePage from "./pages/Equipe/EquipeCreatePage";
import EquipeDetailsPage from "./pages/Equipe/EquipeDetailsPage";
import EditEquipePage from "./pages/Equipe/EditEquipePage";
import FormateursListPage from "./pages/Formateur/FormateursListPage";

export default function App() {
  return (
    <AuthProvider>
      <PosteProvider>
        {" "}
        {/* Envelopper l'ensemble des routes avec PosteProvider */}
        <Router>
          <Routes>
            {/* Route pour la page d'authentification sans SidebarLayout */}
            <Route
              path="/auth"
              element={
                <PublicRoute>
                  <AuthPage />
                </PublicRoute>
              }
            />

            {/* Route pour la page d'accueil avec SidebarLayout */}
            <Route
              path="/"
              element={
                <PrivateRoute>
                  <SidebarLayout /> {/* Sidebar avec Outlet */}
                </PrivateRoute>
              }
            >
              <Route index element={<HomePage />} />
              {/* Route pour la page des postes */}
              <Route path="postes" element={<PosteListPage />} />
              <Route path="/poste/:id" element={<PosteDetailsPage />} />
              <Route path="/poste/create" element={<CreatePostePage />} />
              <Route path="/poste/:id/edit" element={<EditPostePage />} />
              <Route path="/poste/:id/apply" element={<ApplyForPostePage />} />
              <Route
                path="/poste/:posteId/candidatures"
                element={<PosteCandidaturesPage />}
              />
              <Route path="departements" element={<DepartementListPage />} />

              <Route
                path="/departement/create"
                element={<DepartementCreatePage />}
              />
              <Route
                path="/departement/:id"
                element={<DepartementDetailsPage />}
              />
              <Route
                path="/departement/:id/edit"
                element={<EditDepartementPage />}
              />
              <Route path="/equipes" element={<EquipeListPage />} />

              <Route path="/equipe/create" element={<EquipeCreatePage />} />
              <Route path="/equipe/:id" element={<EquipeDetailsPage />} />
              <Route path="/equipe/:id/edit" element={<EditEquipePage />} />
              <Route path="/formateurs" element={<FormateursListPage />} />
            </Route>
          </Routes>
        </Router>
        <ToastContainer position="top-right" autoClose={3000} />
      </PosteProvider>{" "}
      {/* Fermer PosteProvider ici */}
    </AuthProvider>
  );
}
