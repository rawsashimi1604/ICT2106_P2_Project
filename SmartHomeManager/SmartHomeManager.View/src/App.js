import React from "react";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";

import { NavBar } from "./components/NavBar";
import Home from "./pages/Home";
import Devices from "./pages/Devices";
import Profiles from "./pages/Profiles";
import Register from "./pages/account/Register";
import ForgetPassword from "./pages/account/ForgetPassword";
import Login from "./pages/account/Login";

export function App() {
  return (
    <>
      <Router>
        <NavBar />
        <Routes>
          <Route path="/" element={<Home />} />
                  <Route path="/devices" element={<Devices />} />
                  <Route path="/profiles" element={<Profiles />} />
                  <Route path="/register" element={<Register />} />
                  <Route path="/forgetpw" element={<ForgetPassword />} />
                  <Route path="/login" element={<Login />} />
        </Routes>
      </Router>
    </>
  );
}

export default App;
