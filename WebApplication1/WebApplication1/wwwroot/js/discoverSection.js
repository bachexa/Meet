import { DynamicHtmlManager } from './dynamicHtml.js';

async function fetchDiscover(lang) {
    const url = `/api/discoversection?lang=${encodeURIComponent(lang)}`;
    const res = await fetch(url, { headers: { 'Accept': 'application/json' } });
    if (!res.ok) throw new Error(`Failed to load discover: ${res.status}`);
    return await res.json(); // { DiscoverHeader, Language, Cards: [...] }
}

async function renderDiscoverByLang(lang) {
    const discoverSection = document.querySelector('.discover');
    if (!discoverSection) return;

    try {
        const sectionModel = await fetchDiscover(lang);
        discoverSection.innerHTML = DynamicHtmlManager.GetDiscoverSectionModalFromModel(sectionModel);
    } catch (err) {
        console.error(err);
        discoverSection.innerHTML = `
            <div class="discover-error">
                <p>Couldn’t load Discover content. Please try again.</p>
            </div>`;
    }
}

// First render
document.addEventListener('DOMContentLoaded', () => {
    const initialLang = window.currentLang || 'en';
    renderDiscoverByLang(initialLang);
});

// React to language changes from site.js
document.addEventListener('languageChanged', (e) => {
    const newLang = (e?.detail?.lang) || 'en';
    renderDiscoverByLang(newLang);
});
