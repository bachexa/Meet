import { DynamicHtmlManager } from './dynamicHtml.js';

document.addEventListener('click', (e) => {
    if (e.target && e.target.classList.contains('signin-btn')) {
        const existingOverlay = document.getElementById('authOverlay');
        if (existingOverlay) {
            existingOverlay.remove();
        }

        const lang = window.currentLang || 'en';
        const overlayHtml = DynamicHtmlManager.GetAuthHtml(lang);

        const div = document.createElement('div');
        div.innerHTML = overlayHtml;
        document.body.appendChild(div);

        const closeBtn = div.querySelector('.close-btn');
        if (closeBtn) {
            closeBtn.addEventListener('click', () => {
                div.remove();
            });
        }
    }
});

