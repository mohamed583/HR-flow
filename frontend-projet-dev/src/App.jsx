import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { AuthProvider } from "./context/AuthContext";
import { PosteProvider } from "./context/PosteContext"; // Importer le PosteProvider
import AuthPage from "./pages/Auth/AuthPage";
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
import CreateFormateurPage from "./pages/Formateur/CreateFormateurPage";
import FormateurDetailsPage from "./pages/Formateur/FormateurDetailsPage";
import FormateurEditPage from "./pages/Formateur/FormateurEditPage";
import ChangeLoginPage from "./pages/Auth/ChangeLoginPage";
import FormationsListPage from "./pages/Formation/FormationsListPage";
import FormationDetailsPage from "./pages/Formation/FormationDetailsPage";
import CreateFormationPage from "./pages/Formation/CreateFormationPage";
import MesInscriptionsPage from "./pages/Inscription/MesInscriptionsPage";
import FormationEditPage from "./pages/Formation/FormationEditPage";
import CongeCreatePage from "./pages/Conge/CongeCreatePage";
import ListeCongesPage from "./pages/Conge/ListeCongesPage";
import EvaluationListPage from "./pages/Evaluation/EvaluationListPage";
import CreateEvaluationPage from "./pages/Evaluation/CreateEvaluationPage";
import EvaluationDetailsPage from "./pages/Evaluation/EvaluationDetailsPage";
import EvaluationFinalisationPage from "./pages/Evaluation/EvaluationFinalisationPage";
import ListePaiesPage from "./pages/Paie/ListePaiesPage";
import PaieDetailsPage from "./pages/Paie/PaieDetailsPage";
import CreatePaiePage from "./pages/Paie/CreatePaiePage";
import ListeCandidaturesPage from "./pages/Candidature/ListeCandidaturesPage";
import CandidatureDetailsPage from "./pages/Candidature/CandidatureDetailsPage";
import CreateEntretienPage from "./pages/Entretien/CreateEntretienPage";
import EntretienDetailsPage from "./pages/Entretien/EntretienDetailsPage";
import MesEntretiensPage from "./pages/Entretien/MesEntretiensPage";
import FinaliserEntretienPage from "./pages/Entretien/FinaliserEntretienPage";
import ListerEmployesPage from "./pages/Employe/ListerEmployesPage";
import EmployeDetailsPage from "./pages/Employe/EmployeDetailsPage";
import EditEmployePage from "./pages/Employe/EmployeEditPage";
import TransformEmployePage from "./pages/Employe/TransformEmployePage";
import ListeColleguesPage from "./pages/Employe/ListeColleguesPage";
import ProfilePage from "./pages/Profile/ProfilePage";
import SplashScreen from "./components/SplashScreen";
import { useState } from "react";

export default function App() {
  const [isSplashVisible, setIsSplashVisible] = useState(true); // 👈 splash visible au début

  if (isSplashVisible) {
    return <SplashScreen onComplete={() => setIsSplashVisible(false)} />;
  }
  return (
    <AuthProvider>
      <PosteProvider>
        {" "}
        {/* Envelopper l'ensemble des routes avec PosteProvider */}
        <Router>
          <Routes>
            {/* Route pour la page d'authentification sans SidebarLayout */}
            <Route path="/auth" element={<AuthPage />} />

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
              <Route
                path="/formateur/create"
                element={<CreateFormateurPage />}
              />
              <Route path="/formateur/:id" element={<FormateurDetailsPage />} />
              <Route
                path="/formateur/:id/edit"
                element={<FormateurEditPage />}
              />
              <Route
                path="/auth/:id/change-login"
                element={<ChangeLoginPage />}
              />
              <Route path="/formations" element={<FormationsListPage />} />
              <Route
                path="/formation/create"
                element={<CreateFormationPage />}
              />
              <Route path="/formation/:id" element={<FormationDetailsPage />} />
              <Route
                path="/mes-inscriptions"
                element={<MesInscriptionsPage />}
              />
              <Route
                path="/formation/:id/edit"
                element={<FormationEditPage />}
              />
              <Route path="/conge/create" element={<CongeCreatePage />} />
              <Route path="/conges" element={<ListeCongesPage />} />
              <Route path="/evaluations" element={<EvaluationListPage />} />
              <Route
                path="/evaluation/create"
                element={<CreateEvaluationPage />}
              />
              <Route
                path="/evaluation/:id"
                element={<EvaluationDetailsPage />}
              />
              <Route
                path="/evaluation/:id/finalize"
                element={<EvaluationFinalisationPage />}
              />
              <Route path="/paies" element={<ListePaiesPage />} />
              <Route path="/paie/:id" element={<PaieDetailsPage />} />
              <Route path="/paie/create" element={<CreatePaiePage />} />
              <Route path="/candidatures" element={<ListeCandidaturesPage />} />
              <Route
                path="/candidature/:id"
                element={<CandidatureDetailsPage />}
              />
              <Route
                path="/entretien/:candidatureId/create"
                element={<CreateEntretienPage />}
              />
              <Route path="/entretien/:id" element={<EntretienDetailsPage />} />
              <Route path="/mes-entretiens" element={<MesEntretiensPage />} />
              <Route
                path="/entretien/:id/finalize"
                element={<FinaliserEntretienPage />}
              />
              <Route path="/employes" element={<ListerEmployesPage />} />
              <Route path="/employe/:id" element={<EmployeDetailsPage />} />
              <Route path="/employe/:id/edit" element={<EditEmployePage />} />
              <Route
                path="/employe/create/:id"
                element={<TransformEmployePage />}
              />
              <Route path="/mes-collegues" element={<ListeColleguesPage />} />
              <Route path="/profile" element={<ProfilePage />} />
            </Route>
          </Routes>
        </Router>
        <ToastContainer position="top-right" autoClose={3000} />
      </PosteProvider>{" "}
      {/* Fermer PosteProvider ici */}
    </AuthProvider>
  );
}
