import { getState } from './store.js';
import { translations } from '../i18n/translations.js';

function deepGet(obj, path) {
    return path.split('.').reduce((o, k) => (o && k in o ? o[k] : undefined), obj);
}
function fmt(str, vars) {
    if (!vars) return str;
    return String(str).replace(/\{(\w+)\}/g, (_, k) => (k in vars ? vars[k] : `{${k}}`));
}
export function t(key, vars, lang = getState().lang) {
    const fromLang = deepGet(translations[lang] || {}, key);
    if (fromLang !== undefined) return fmt(fromLang, vars);
    const fromEn = deepGet(translations.en || {}, key);
    if (fromEn !== undefined) {
        console.warn(`[i18n] Missing "${key}" in "${lang}", using "en".`);
        return fmt(fromEn, vars);
    }
    console.warn(`[i18n] Missing key "${key}" in "${lang}" and "en".`);
    return key;
}
export const ns = (prefix) => (key, vars, lang) => t(`${prefix}.${key}`, vars, lang);
