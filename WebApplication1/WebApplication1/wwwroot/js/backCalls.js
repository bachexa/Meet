export async function fetchJSON(path, params = {}, options = {}) {
    const url = new URL(path, window.location.origin);

    // add query params
    Object.entries(params).forEach(([key, value]) => {
        if (value !== undefined && value !== null) {
            url.searchParams.set(key, value);
        }
    });

    const res = await fetch(url.toString(), {
        headers: { 'Accept': 'application/json', ...(options.headers || {}) },
        ...options
    });

    if (!res.ok) {
        throw new Error(`Failed to load: ${res.status}`);
    }

    return res.json();
}