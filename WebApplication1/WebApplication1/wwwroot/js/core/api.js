const DEFAULT_TIMEOUT = 10000;

export async function fetchJSON(path, params = {}, { headers, signal, timeout = DEFAULT_TIMEOUT, ...init } = {}) {
    const url = new URL(path, window.location.origin);
    for (const [k, v] of Object.entries(params)) if (v != null) url.searchParams.set(k, v);

    const controller = new AbortController();
    const t = setTimeout(() => controller.abort(), timeout);
    const usedSignal = signal ?? controller.signal;

    try {
        const res = await fetch(url.toString(), {
            headers: { Accept: 'application/json', ...(headers || {}) },
            signal: usedSignal,
            ...init,
        });
        if (!res.ok) throw new Error(`HTTP ${res.status}`);
        return await res.json();
    } finally {
        clearTimeout(t);
    }
}
