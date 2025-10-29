// // dateUtils.js
// // ------------
// // funkce pro formátování datových řetězců.

// /**
//  * Převádí ISO časový řetězec na formátované datum.
//  *
//  * @param {string} str - ISO řetězec data (např. "2023-08-15T00:00:00Z").
//  * @param {boolean} [locale=false] - Pokud <true>, vrátí datum
//  *   ve formátu "15. srpna 2023" (cs-CZ). Jinak vrátí "YYYY-MM-DD".
//  * @returns {string} Formátované datum.
//  */
// export const dateStringFormatter = (str, locale = false) => {
//     const d = new Date(str);

//     if (locale) {
//         // Lokální formát: "den. měsíc rok" (např. "15. srpna 2023")
//         return d.toLocaleDateString("cs-CZ", {
//             year: "numeric",
//             month: "long",
//             day: "numeric",
//         });
//     }

//     // ISO formát: "YYYY-MM-DD"
//     const year = d.getFullYear();
//     const month = String(d.getMonth() + 1).padStart(2, '0');
//     const day = String(d.getDate()).padStart(2, '0');

//     return [year, month, day].join("-");
// };

// export default dateStringFormatter;

// dateUtils.js
// ------------
// funkce pro formátování datových řetězců.

/**
 * Převádí ISO časový řetězec na formátované datum (případně datum+čas).
 *
 * @param {string} str - ISO řetězec data (např. "2023-08-15T14:30:00Z").
 * @param {boolean} [locale=false] - Pokud <true>, vrátí lokalizovaný formát ("15. srpna 2023").
 *                                   Jinak ISO "YYYY-MM-DD" (resp. "YYYY-MM-DD HH:mm").
 * @param {boolean} [includeTime=false] - Pokud <true>, přidá k datu i hodiny a minuty.
 * @returns {string} Formátované datum (případně datum+čas).
 */
export const dateStringFormatter = (
  str,
  locale = false,
  includeTime = false
) => {
  const d = new Date(str);

  if (locale) {
    // Základní formát: den. měsíc (long) rok
    const options = {
      year:  "numeric",
      month: includeTime ? "2-digit" : "long",
      day:   "2-digit"
    };

    // Přidat hodiny a minuty, pokud je potřeba
    if (includeTime) {
      options.hour   = "2-digit";
      options.minute = "2-digit";
    }

    // Když includeTime=true, použije toLocaleString; jinak toLocaleDateString
    return includeTime
      ? d.toLocaleString("cs-CZ", options)
      : d.toLocaleDateString("cs-CZ", options);
  }

  // ISO formát: "YYYY-MM-DD" (nebo "YYYY-MM-DD HH:mm")
  const year  = d.getFullYear();
  const month = String(d.getMonth() + 1).padStart(2, "0");
  const day   = String(d.getDate()).padStart(2, "0");
  let result  = [year, month, day].join("-");

  if (includeTime) {
    const hour   = String(d.getHours()).padStart(2, "0");
    const minute = String(d.getMinutes()).padStart(2, "0");
    result += ` ${hour}:${minute}`;
  }

  return result;
};

export default dateStringFormatter;
