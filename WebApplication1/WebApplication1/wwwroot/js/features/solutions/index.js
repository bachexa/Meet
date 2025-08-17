import { on } from '../../core/bus.js';
import { fetchJSON } from '../../core/api.js';
import { DynamicHtmlManager } from '../shared/dynamicHtml.js';

let aborter = null;
function destroy() { if (aborter) { aborter.abort(); aborter = null; } }


function initAccordionA11y(container) {
    const items = container.querySelectorAll('.accordion-item');
    items.forEach((item, i) => {
        const header = item.querySelector('.accordion-header');
        const panel = item.querySelector('.accordion-content');
        const id = `solutions-acc-panel-${i}`;
        panel.id = id;
        header.setAttribute('aria-controls', id);
        header.setAttribute('aria-expanded', item.classList.contains('active') ? 'true' : 'false');
        panel.setAttribute('role', 'region');
        panel.setAttribute('aria-labelledby', header.id || `solutions-acc-header-${i}`);
        if (!header.id) header.id = `solutions-acc-header-${i}`;
    });
}

function wireAccordion(container) {
    container.addEventListener('click', (e) => {
        const header = e.target.closest('.accordion-header');
        if (!header || !container.contains(header)) return;

        const item = header.closest('.accordion-item');
        item.classList.toggle('active');
        header.setAttribute('aria-expanded', item.classList.contains('active') ? 'true' : 'false');
    }, { signal: aborter.signal });
}



async function mount(lang) {
    const container = document.querySelector('.solutions');
    if (!container) return;

    destroy();
    aborter = new AbortController();

    try {
        //const model = await fetchJSON('/api/discoversection', { lang }, { signal: aborter.signal });
        container.innerHTML = DynamicHtmlManager.GetSolutionsSectionModalFromModel('');
        initAccordionA11y(container);
        wireAccordion(container);
    } catch (e) {
        if (e.name === 'AbortError') return;
        console.error(e);
        container.innerHTML = `<div class="discover-error"><p>Couldn’t load Discover content. Please try again.</p></div>`;
    }
}

on('languageChanged', ({ lang }) => mount(lang));
