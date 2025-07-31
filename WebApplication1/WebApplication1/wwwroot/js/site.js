import { DynamicHtmlManager } from './dynamicHtml.js';

document.addEventListener('DOMContentLoaded', () => {
    const headerContainer = document.getElementById('layeoutHeader');
    const footerContainer = document.getElementById('layeoutFooter');

    window.currentLang = "en";  // ✅ Default language

    function renderLayout(lang) {
        headerContainer.innerHTML = DynamicHtmlManager.GetLayoutHeaderModal(lang);
        footerContainer.innerHTML = DynamicHtmlManager.GetLayoutFooterModal(lang);
        bindLanguageButtons(); // გადამრთველების ხელახლა მიბმა
    }

    function bindLanguageButtons() {
        document.querySelectorAll('.lang-btn').forEach(btn => {
            btn.addEventListener('click', () => {
                const selectedLang = btn.getAttribute('data-lang');
                window.currentLang = selectedLang; // ✅ Make it global
                renderLayout(selectedLang);
            });
        });
    }

    renderLayout(window.currentLang); // პირველად გამოძახება



    window.toggleMenu = function () {
        const nav = document.getElementById('mainMenu');
        nav.classList.toggle('open');
    };
});
