// // src/components/AuthGate.jsx
// import React, { createContext, useContext, useState } from 'react';
// import '../styles/LoginStyle.css';

// // Tady si nadefinuj obě hesla
// const USER_PASSWORD  = 'hesloProUcastniky';
// const ADMIN_PASSWORD = 'testhesla';

// const AuthContext = createContext({
//   role: null,           // 'admin' | 'participant' | null
//   login: (_pw) => {},   // funkce pro zalogování
//   logout: () => {}      // odhlásit se
// });

// export function useAuth() {
//   return useContext(AuthContext);
// }

// export default function AuthGate({ children }) {
//   // Načteme roli ze sessionStorage (relativně bezpečnější než localStorage pro per-session)
//   const [role, setRole] = useState(
//     () => sessionStorage.getItem('role') || null
//   );
//   const [pwInput, setPwInput] = useState('');

//   const login = () => {
//     if (pwInput === ADMIN_PASSWORD) {
//       sessionStorage.setItem('role', 'admin');
//       setRole('admin');
//     }
//     else if (pwInput === USER_PASSWORD) {
//       sessionStorage.setItem('role', 'participant');
//       setRole('participant');
//     }
//     else {
//       alert('Špatné heslo.');
//     }
//   };

//   const logout = () => {
//     sessionStorage.removeItem('role');
//     setRole(null);
//     setPwInput('');
//   };

//   // Pokud ještě není přihlášen žádný uživatel, zobrazíme jednoduchý login formulář
//   if (!role) {
//     return (
//       <div style={{
//         height: '100vh', display: 'flex',
//         justifyContent: 'center', alignItems: 'center',
//         flexDirection: 'column'
//       }}>
//         <input
//           type="password"
//           placeholder="Zadejte heslo"
//           value={pwInput}
//           onChange={e => setPwInput(e.target.value)}
//         />
//         <button onClick={login} style={{ marginTop: 8 }}>
//           Přihlásit
//         </button>
//       </div>
//     );
//   }

//   // Po přihlášení vložíme do Contextu informace o roli
//   return (
//     <AuthContext.Provider value={{ role, login, logout }}>
//       {children}
//     </AuthContext.Provider>
//   );
// }


// import React, { createContext, useContext, useState } from 'react';
// import '../styles/LoginStyle.css';

// // const USER_PASSWORD  = 'hesloProUcastniky';
// // const ADMIN_PASSWORD = 'testhesla';

// const USER_PASSWORD  = 'user';
// const ADMIN_PASSWORD = 'admin';

// const AuthContext = createContext({ role: null, login: () => {}, logout: () => {} });

// export function useAuth() {
//   return useContext(AuthContext);
// }

// export default function AuthGate({ children }) {
//   const [role, setRole] = useState(() => sessionStorage.getItem('role') || null);
//   const [pwInput, setPwInput] = useState('');

//   const login = () => {
//     if (pwInput === ADMIN_PASSWORD) {
//       sessionStorage.setItem('role', 'admin');
//       setRole('admin');
//     } else if (pwInput === USER_PASSWORD) {
//       sessionStorage.setItem('role', 'participant');
//       setRole('participant');
//     } else {
//       alert('Špatné heslo.');
//     }
//   };

//   const logout = () => {
//     sessionStorage.removeItem('role');
//     setRole(null);
//     setPwInput('');
//   };

//   if (!role) {
//     return (
//       <div className="auth-container">
//         <div className="auth-box">
//           <input
//             type="password"
//             placeholder="Zadejte heslo"
//             value={pwInput}
//             onChange={e => setPwInput(e.target.value)}
//             className="auth-input"
//           />
//           <button onClick={login} className="auth-btn">
//             Přihlásit
//           </button>
//         </div>
//       </div>
//     );
//   }

//   return (
//     <AuthContext.Provider value={{ role, login, logout }}>
//       {children}
//     </AuthContext.Provider>
//   );
// }




import React, { createContext, useContext, useState } from 'react';
import '../styles/LoginStyle.css';
import { FaLock } from 'react-icons/fa';

// const USER_PASSWORD  = 'hesloProUcastniky';
// const ADMIN_PASSWORD = 'testhesla';

const USER_PASSWORD  = 'user';
const ADMIN_PASSWORD = 'admin';

const AuthContext = createContext({ role: null, login: () => {}, logout: () => {} });

export function useAuth() {
  return useContext(AuthContext);
}

export default function AuthGate({ children }) {
  const [role, setRole] = useState(() => sessionStorage.getItem('role') || null);
  const [pwInput, setPwInput] = useState('');

  const login = () => {
    if (pwInput === ADMIN_PASSWORD) {
      sessionStorage.setItem('role', 'admin');
      setRole('admin');
    } else if (pwInput === USER_PASSWORD) {
      sessionStorage.setItem('role', 'participant');
      setRole('participant');
    } else {
      alert('Špatné heslo.');
    }
  };

  const logout = () => {
    sessionStorage.removeItem('role');
    setRole(null);
    setPwInput('');
  };

if (!role) {
  return (
    <div className="auth-container">
      <div className="auth-box">
        <FaLock className="auth-icon" />
        <input
          type="password"
          placeholder="Zadejte heslo"
          value={pwInput}
          onChange={e => setPwInput(e.target.value)}
          className="auth-input"
        />
               <button
         onClick={login}
         className="auth-btn"
         disabled={!pwInput.trim()}          /* ← zde */
       >
          Přihlásit
        </button>
      </div>
    </div>
  );
}

  return (
    <AuthContext.Provider value={{ role, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
}
