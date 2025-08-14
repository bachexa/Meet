import { DynamicHtmlManager } from './dynamicHtml.js';

document.addEventListener('DOMContentLoaded', () => {
    const discoverSection = document.querySelector('.discover');

    function renderDiscover(lang) {
        if (discoverSection) {
            discoverSection.innerHTML = DynamicHtmlManager.GetDiscoverSectionModal(lang);
        }
    }

    // First render
    renderDiscover(window.currentLang || 'en');

    // Listen for language changes from site.js
    document.addEventListener('languageChanged', (e) => {
        renderDiscover(e.detail.lang);
    });
});
