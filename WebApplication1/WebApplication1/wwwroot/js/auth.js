// auth.js
import { DynamicHtmlManager } from './dynamicHtml.js';

let currentLang = window.currentLang || 'en';

function openAuthOverlay(lang = currentLang) {
    // remove any existing overlay
    closeAuthOverlay();

    // build overlay HTML in the requested language
    const html = DynamicHtmlManager.GetAuthHtml(lang);

    // create a wrapper to parse HTML, then append the actual root element
    const wrapper = document.createElement('div');
    wrapper.innerHTML = html.trim();
    const node = wrapper.firstElementChild || wrapper;

    // make sure your template gives this root an id="authOverlay"
    // and optionally role="dialog" aria-modal="true"
    document.body.appendChild(node);

    // lock scroll while modal is open
    document.body.dataset.prevOverflow = document.body.style.overflow || '';
    document.body.style.overflow = 'hidden';

    // close on [x] button
    node.querySelector('.close-btn')?.addEventListener('click', () => closeAuthOverlay());

    // close when clicking the backdrop (assumes authOverlay is the backdrop)
    node.addEventListener('click', (e) => {
        if (e.target === node) closeAuthOverlay();
    });

    // close on Esc once
    const onKey = (e) => {
        if (e.key === 'Escape') {
            closeAuthOverlay();
            document.removeEventListener('keydown', onKey);
        }
    };
    document.addEventListener('keydown', onKey);
}

function closeAuthOverlay() {
    const el = document.getElementById('authOverlay');
    if (el) el.remove();
    // restore scroll
    if ('prevOverflow' in document.body.dataset) {
        document.body.style.overflow = document.body.dataset.prevOverflow;
        delete document.body.dataset.prevOverflow;
    }
}

// open on any .signin-btn (event delegation supports dynamic headers)
document.addEventListener('click', (e) => {
    const btn = e.target.closest('.signin-btn');
    if (!btn) return;
    openAuthOverlay();
});

// keep it in sync with language changes
document.addEventListener('languageChanged', (e) => {
    currentLang = e?.detail?.lang || 'en';
    // if overlay is open, re-render it in the new language
    if (document.getElementById('authOverlay')) {
        openAuthOverlay(currentLang);
    }
});

// optional: export for programmatic control
export { openAuthOverlay, closeAuthOverlay };
