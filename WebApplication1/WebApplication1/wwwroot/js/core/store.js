const state = { lang: 'en', user: null };
const subs = new Set();

export function getState() { return state; }
export function setState(patch) {
    Object.assign(state, patch);
    subs.forEach(fn => fn(state));
}
export function subscribe(fn) { subs.add(fn); return () => subs.delete(fn); }
