import { DynamicHtmlManager } from './dynamicHtml.js';

document.addEventListener('DOMContentLoaded', () => {
    const headerContainer = document.getElementById('layeoutHeader');
    const footerContainer = document.getElementById('layeoutFooter');
    if (headerContainer) {
        headerContainer.innerHTML = DynamicHtmlManager.GetLayoutHeaderModal();
        footerContainer.innerHTML = DynamicHtmlManager.GetLayoutFooterModal();
    }

    window.toggleMenu = function () {
        const nav = document.getElementById('mainMenu');
        nav.classList.toggle('open');
    };
});



   