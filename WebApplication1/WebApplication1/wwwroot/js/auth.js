import { DynamicHtmlManager } from './dynamicHtml.js';

document.addEventListener('DOMContentLoaded', () => {
    const signInButton = document.querySelector('.signin-btn');

    if (signInButton) {
        signInButton.addEventListener('click', () => {
            // თუ უკვე არსებობს authOverlay — წაშალე
            const existingOverlay = document.getElementById('authOverlay');
            if (existingOverlay) {
                existingOverlay.remove();
            }

            // HTML ბლოკი
            const overlayHtml = DynamicHtmlManager.GetAuthHtml('en');

            // დინამიური DOM ელემენტის შექმნა
            const div = document.createElement('div');
            div.innerHTML = overlayHtml;
            document.body.appendChild(div); // ვამატებთ მთელ გვერდზე

            // გათიშვის ღილაკი
            const closeBtn = div.querySelector('.close-btn');
            if (closeBtn) {
                closeBtn.addEventListener('click', () => {
                    div.remove();
                });
            }
        });
    }
});
