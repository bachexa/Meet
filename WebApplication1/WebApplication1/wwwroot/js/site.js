import { DynamicHtmlManager } from './dynamicHtml.js';

document.addEventListener('DOMContentLoaded', () => {
    const headerContainer = document.getElementById('layeoutHeader');
    const footerContainer = document.getElementById('layeoutFooter');
    if (headerContainer) {
        headerContainer.innerHTML = DynamicHtmlManager.GetLayoutHeaderModal("en");
        footerContainer.innerHTML = DynamicHtmlManager.GetLayoutFooterModal("en");
    }

    window.toggleMenu = function () {
        const nav = document.getElementById('mainMenu');
        nav.classList.toggle('open');
    };
});



   