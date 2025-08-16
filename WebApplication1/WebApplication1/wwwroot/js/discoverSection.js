import { DynamicHtmlManager } from './dynamicHtml.js';
import { fetchJSON } from './backCalls.js';

let discoverRequestSeq = 0;
let discoverAbortController = null;

async function renderDiscoverByLang(lang) {
    const container = document.querySelector('.discover');
    if (!container) return;

    if (discoverAbortController) discoverAbortController.abort();
    discoverAbortController = new AbortController();

    const seq = ++discoverRequestSeq;

    try {
        const model = await fetchJSON('/api/discoversection', { lang }, { signal: discoverAbortController.signal });
        if (seq !== discoverRequestSeq) return; // stale, ignore

        container.innerHTML = DynamicHtmlManager.GetDiscoverSectionModalFromModel(model);
    } catch (err) {
        if (err.name === 'AbortError') return;
        console.error(err);
        container.innerHTML = `
      <div class="discover-error">
        <p>Couldn’t load Discover content. Please try again.</p>
      </div>`;
    }
}

document.addEventListener('languageChanged', (e) => {
    const newLang = e?.detail?.lang || 'en';
    renderDiscoverByLang(newLang);
});
