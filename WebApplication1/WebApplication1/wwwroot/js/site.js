// site.js
import { DynamicHtmlManager } from './dynamicHtml.js';

document.addEventListener('DOMContentLoaded', () => {
    const headerContainer = document.getElementById('layeoutHeader');
    const footerContainer = document.getElementById('layeoutFooter');

    window.currentLang = 'en';

    function renderLayout(lang) {
        headerContainer.innerHTML = DynamicHtmlManager.GetLayoutHeaderModal(lang);
        footerContainer.innerHTML = DynamicHtmlManager.GetLayoutFooterModal(lang);

        // Notify other modules
        document.dispatchEvent(new CustomEvent('languageChanged', { detail: { lang } }));
    }

    // Event delegation for .lang-btn created by re-renders
    document.addEventListener('click', (e) => {
        const btn = e.target.closest('.lang-btn');
        if (!btn) return;
        const selectedLang = btn.getAttribute('data-lang');
        if (!selectedLang || selectedLang === window.currentLang) return;
        window.currentLang = selectedLang;
        renderLayout(selectedLang);
    });

    // Toggle language menu
    document.addEventListener('click', (e) => {
        const toggle = e.target.closest('.lang-toggle');
        if (toggle) {
            const menu = document.getElementById('langMenu');
            if (menu) menu.style.display = menu.style.display === 'block' ? 'none' : 'block';
            return;
        }
        // click outside -> close
        const dropdown = document.querySelector('.lang-dropdown');
        if (dropdown && !dropdown.contains(e.target)) {
            const menu = document.getElementById('langMenu');
            if (menu) menu.style.display = 'none';
        }
    });

    renderLayout(window.currentLang); // initial render -> also emits `languageChanged`
});
