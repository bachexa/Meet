import { DynamicHtmlManager } from './dynamicHtml.js';
import { fetchJSON } from './backCalls.js';

async function renderDiscoverByLang(lang) {
    const discoverSection = document.querySelector('.discover');
    if (!discoverSection) return;

    try {
        const sectionModel = await fetchJSON('/api/discoversection', { lang });
        discoverSection.innerHTML =
            DynamicHtmlManager.GetDiscoverSectionModalFromModel(sectionModel);
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
