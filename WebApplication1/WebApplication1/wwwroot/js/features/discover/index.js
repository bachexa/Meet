import { on } from '../../core/bus.js';
import { fetchJSON } from '../../core/api.js';
import { DynamicHtmlManager } from '../shared/dynamicHtml.js';

let aborter = null;
function destroy() { if (aborter) { aborter.abort(); aborter = null; } }

async function mount(lang) {
    const container = document.querySelector('.discover');
    if (!container) return;

    destroy();
    aborter = new AbortController();

    try {
        const model = await fetchJSON('/api/discoversection', { lang }, { signal: aborter.signal });
        container.innerHTML = DynamicHtmlManager.GetDiscoverSectionModalFromModel(model);
    } catch (e) {
        if (e.name === 'AbortError') return;
        console.error(e);
        container.innerHTML = `<div class="discover-error"><p>Couldn’t load Discover content. Please try again.</p></div>`;
    }
}

on('languageChanged', ({ lang }) => mount(lang));
