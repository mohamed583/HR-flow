import React, { useState } from "react";
import Login from "../components/Auth/Login";
import Register from "../components/Auth/Register";

export default function AuthPage() {
  const [isLogin, setIsLogin] = useState(true);

  const toggleForm = () => {
    setIsLogin(!isLogin);
  };

  return (
    <>
      {isLogin ? (
        <Login toggleForm={toggleForm} />
      ) : (
        <Register toggleForm={toggleForm} />
      )}
    </>
  );
}
