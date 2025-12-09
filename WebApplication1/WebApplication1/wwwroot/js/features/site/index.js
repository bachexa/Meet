import { emit } from '../../core/bus.js';
import { getState, setState } from '../../core/store.js';
import { fetchJSON } from '../../core/api.js';
import { DynamicHtmlManager } from '../shared/dynamicHtml.js';

let headerAborter = null;

function destroyHeader() {
    if (headerAborter) {
        headerAborter.abort();
        headerAborter = null;
    }
}

async function renderLayout(lang) {
    const header = document.getElementById('layeoutHeader');
    const footer = document.getElementById('layeoutFooter');
    if (!header || !footer) return;

    destroyHeader();
    headerAborter = new AbortController();

    try {
        // GET /api/MainMenuSection?lang=en|ka
        const menuModel = await fetchJSON(
            '/api/MainMenuSection',
            { lang },
            { signal: headerAborter.signal }
        );

        // Build header from API model
        header.innerHTML = DynamicHtmlManager.GetLayoutHeaderFromModel(menuModel, lang);
        footer.innerHTML = DynamicHtmlManager.GetLayoutFooterModal(menuModel,lang);
    } catch (e) {
        if (e.name === 'AbortError') return;
        console.error(e);
        header.innerHTML = `<div class="header-error"><p>Couldn’t load header. Please try again.</p></div>`;
        footer.innerHTML = `<div class="footer-error"><p>Couldn’t load footer. Please try again.</p></div>`;
    }

    // Footer can still use your translation-based function
    

    // Inform other modules (plans, resources, etc.) that language changed
    emit('languageChanged', { lang });
}

document.addEventListener('DOMContentLoaded', () => {
    // initial render
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
