import React from "react";
import "../styles/Footer.css";
import { FaInstagram, FaFacebook } from "react-icons/fa";

export default function Footer() {
  return (
    <footer className="footer">
      <img src="/obrazek-1.png" alt="Obrázek v patičce" className="footer-image" />

      <div className="social-icons">
        <a href="https://www.instagram.com" target="_blank" rel="noopener noreferrer">
          <FaInstagram className="icon" />
        </a>
        <a href="https://www.facebook.com" target="_blank" rel="noopener noreferrer">
          <FaFacebook className="icon" />
        </a>
      </div>

      <div className="footer-text">
        © {new Date().getFullYear()} TrainingManage – Všechna práva vyhrazena       
      </div>
      
       <span className="created-by">Vytvořil: <strong>Michal Vachutka</strong></span>
    </footer>
  );
}
