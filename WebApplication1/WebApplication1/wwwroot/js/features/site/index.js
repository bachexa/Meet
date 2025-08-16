import { emit } from '../../core/bus.js';
import { getState, setState } from '../../core/store.js';
import { DynamicHtmlManager } from '../shared/dynamicHtml.js';

function renderLayout(lang) {
    const header = document.getElementById('layeoutHeader');
    const footer = document.getElementById('layeoutFooter');
    if (!header || !footer) return;

    header.innerHTML = DynamicHtmlManager.GetLayoutHeaderModal(lang);
    footer.innerHTML = DynamicHtmlManager.GetLayoutFooterModal(lang);

    emit('languageChanged', { lang });
}

document.addEventListener('DOMContentLoaded', () => {
    renderLayout(getState().lang);

    // language change (delegated)
    document.addEventListener('click', (e) => {
        const btn = e.target.closest('.lang-btn');
        if (!btn) return;
        const lang = btn.getAttribute('data-lang');
        if (!lang || lang === getState().lang) return;
        setState({ lang });
        renderLayout(lang);
    });

    // language dropdown toggle + outside click close
    document.addEventListener('click', (e) => {
        const toggle = e.target.closest('.lang-toggle');
        if (toggle) {
            const menu = document.getElementById('langMenu');
            if (menu) menu.style.display = menu.style.display === 'block' ? 'none' : 'block';
            return;
        }
        const dropdown = document.querySelector('.lang-dropdown');
        if (dropdown && !dropdown.contains(e.target)) {
            const menu = document.getElementById('langMenu');
            if (menu) menu.style.display = 'none';
        }
    });

    // mobile menu toggle (replaces inline onclick)
    document.addEventListener('click', (e) => {
        if (e.target.closest('.ms-toggle')) {
            const menu = document.getElementById('mainMenu');
            if (menu) menu.classList.toggle('open');
        }
    });
});
