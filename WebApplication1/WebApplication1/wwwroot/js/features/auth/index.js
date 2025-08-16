import { on } from '../../core/bus.js';
import { getState } from '../../core/store.js';
import { DynamicHtmlManager } from '../shared/dynamicHtml.js';

function closeAuthOverlay() {
    const el = document.getElementById('authOverlay');
    if (el) el.remove();
    if ('prevOverflow' in document.body.dataset) {
        document.body.style.overflow = document.body.dataset.prevOverflow;
        delete document.body.dataset.prevOverflow;
    }
}

function openAuthOverlay(lang = getState().lang) {
    closeAuthOverlay();
    const html = DynamicHtmlManager.GetAuthHtml(lang); // translations handled inside
    const wrap = document.createElement('div');
    wrap.innerHTML = html.trim();
    const node = wrap.firstElementChild || wrap;
    document.body.appendChild(node);

    document.body.dataset.prevOverflow = document.body.style.overflow || '';
    document.body.style.overflow = 'hidden';

    node.querySelector('.close-btn')?.addEventListener('click', closeAuthOverlay);
    node.addEventListener('click', (e) => { if (e.target === node) closeAuthOverlay(); });
    const onKey = (e) => { if (e.key === 'Escape') { closeAuthOverlay(); document.removeEventListener('keydown', onKey); } };
    document.addEventListener('keydown', onKey);
}

document.addEventListener('click', (e) => {
    const btn = e.target.closest('.signin-btn');
    if (btn) openAuthOverlay();
});

on('languageChanged', ({ lang }) => {
    if (document.getElementById('authOverlay')) openAuthOverlay(lang);
});

export { openAuthOverlay, closeAuthOverlay };
