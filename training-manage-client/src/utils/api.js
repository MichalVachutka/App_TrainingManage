// src/utils/api.js
// ------------------
// Jednotný wrapper pro volání REST API pomocí fetch.
// Nastaví základní URL a zpracuje chybové stavy.


const API_URL = ""; 

/**
 * Zabalí fetch, zkontroluje status a vrátí JSON (kromě DELETE).
 * @param {string} url - relativní cesta k API (včetně query stringu).
 * @param {object} requestOptions - options pro fetch (method, headers, body).
 * @returns {Promise<any>} Rozparsovaný JSON, nebo undefined pro DELETE.
 * @throws Error pokud response.ok === false nebo při network error.
 */
const fetchData = (url, requestOptions) => {
    const apiUrl = `${API_URL}${url}`;

    return fetch(apiUrl, requestOptions)
        .then((response) => {
            if (!response.ok) {
                throw new Error(`Network response was not ok: ${response.status} ${response.statusText}`);
            }
            // DELETE endpointy obvykle nevrací tělo
            if (requestOptions.method !== 'DELETE') {
                return response.json();
            }
        })
        .catch((error) => {
            // Propagujeme chybu dál
            throw error;
        });
};

/**
 * GET request s volitelnými query parametry.
 * @param {string} url - cesta bez query stringu.
 * @param {object} [params] - objekt klíč:hodnota pro query.
 * @returns {Promise<any>} Data z API.
 */
export const apiGet = (url, params) => {
    // Odebereme hodnoty null/undefined
    const filteredParams = Object.fromEntries(
        Object.entries(params || {}).filter(([_, value]) => value != null)
    );

    // Sestrojíme query string
    const apiUrl = `${url}?${new URLSearchParams(filteredParams)}`;
    const requestOptions = { method: "GET" };

    return fetchData(apiUrl, requestOptions);
};

/**
 * POST request s JSON tělem.
 */
export const apiPost = (url, data) => {
    const requestOptions = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data),
    };

    return fetchData(url, requestOptions);
};

/**
 * PUT request pro aktualizaci.
 */
export const apiPut = (url, data) => {
    const requestOptions = {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data),
    };

    return fetchData(url, requestOptions);
};

/**
 * DELETE request (bez těla v odpovědi).
 */
export const apiDelete = (url) => {
    const requestOptions = { method: "DELETE" };
    return fetchData(url, requestOptions);
};

export default { apiGet, apiPost, apiPut, apiDelete };



