// src/utils/authorization.js
import { ADMIN_PASSWORD } from "./password";

export function authorization(actionFn) {
  const entered = window.prompt("Zadejte heslo pro tuto akci:");
  if (entered === ADMIN_PASSWORD) {
    // akce vrací promise, takže zachováš možnost .catch()
    return Promise.resolve().then(actionFn);
  } else {
    alert("Špatné heslo, akce zrušena.");
    return Promise.reject(new Error("Unauthorized"));
  }
}