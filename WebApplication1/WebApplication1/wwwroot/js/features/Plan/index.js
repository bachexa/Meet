import { on } from '../../core/bus.js';
import { fetchJSON } from '../../core/api.js';
import { DynamicHtmlManager } from '../shared/dynamicHtml.js';

let aborter = null;

function destroy() {
    if (aborter) { aborter.abort(); aborter = null; }
}

async function mountPlans(lang) {
    const container = document.querySelector('.plans-section-container');
    if (!container) return;

    destroy();
    aborter = new AbortController();

    try {
        const section = await fetchJSON('/api/PlansSection', { lang }, { signal: aborter.signal });
        container.innerHTML = DynamicHtmlManager.RenderPlansSectionFromModel(section ?? {});
    } catch (e) {
        if (e.name === 'AbortError') return;
        console.error(e);
        container.innerHTML = `<div class="plans-error"><p>Couldn’t load plans section. Please try again.</p></div>`;
    }
}

on('languageChanged', ({ lang }) => mountPlans(lang));
mountPlans('en');