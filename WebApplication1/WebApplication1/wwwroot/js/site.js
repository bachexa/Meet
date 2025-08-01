import { DynamicHtmlManager } from './dynamicHtml.js';

document.addEventListener('DOMContentLoaded', () => {
    const headerContainer = document.getElementById('layeoutHeader');
    const footerContainer = document.getElementById('layeoutFooter');

    window.currentLang = "en";  // Default language

    function renderLayout(lang) {
        headerContainer.innerHTML = DynamicHtmlManager.GetLayoutHeaderModal(lang);
        footerContainer.innerHTML = DynamicHtmlManager.GetLayoutFooterModal(lang);
        bindLanguageButtons();
    }

    function bindLanguageButtons() {
        // Language switch buttons
        document.querySelectorAll('.lang-btn').forEach(btn => {
            btn.addEventListener('click', () => {
                const selectedLang = btn.getAttribute('data-lang');
                window.currentLang = selectedLang;
                renderLayout(selectedLang);
            });
        });

        // 🌐 toggle button
        const langToggle = document.querySelector('.lang-toggle');
        if (langToggle) {
            langToggle.addEventListener('click', () => {
                const menu = document.getElementById('langMenu');
                if (menu) {
                    menu.style.display = menu.style.display === 'block' ? 'none' : 'block';
                }
            });
        }
    }

    // Outside click hides the language dropdown
    document.addEventListener('click', function (event) {
        const dropdown = document.querySelector('.lang-dropdown');
        if (dropdown && !dropdown.contains(event.target)) {
            const menu = document.getElementById('langMenu');
            if (menu) menu.style.display = 'none';
        }
    });

    renderLayout(window.currentLang); // Initial load
});

