import { on } from '../../core/bus.js';
import { fetchJSON } from '../../core/api.js';
import { DynamicHtmlManager } from '../shared/dynamicHtml.js';

let aborter = null;
function destroy() {
    if (aborter) { aborter.abort(); aborter = null; }
}

async function mountProducts(lang = 'en') {
    const container = document.querySelector('.ms-plans');
    if (!container) return;

    destroy();
    aborter = new AbortController();

    try {
        const section = await fetchJSON('/api/ProductSection', { lang }, { signal: aborter.signal });
        container.innerHTML = DynamicHtmlManager.RenderProductSectionFromModel(section ?? {}, lang);

        const tabs = container.querySelectorAll('.ms-tab');
        const panels = container.querySelectorAll('.ms-panel');
        const hero = container.querySelector('#ms-hero-img');

        const firstTab = container.querySelector('.ms-tab.is-active');
        if (firstTab && hero && !hero.src) {
            const heroSrc = firstTab.getAttribute('data-hero') || '';
            if (heroSrc) hero.src = heroSrc;
        }

        tabs.forEach(tab => {
            tab.addEventListener('click', () => {
                tabs.forEach(t => t.classList.remove('is-active'));
                tab.classList.add('is-active');

                const panelName = tab.getAttribute('data-tab');
                panels.forEach(p => {
                    if (p.getAttribute('data-panel') === panelName) {
                        p.classList.add('is-visible');
                        p.removeAttribute('hidden');
                    } else {
                        p.classList.remove('is-visible');
                        p.setAttribute('hidden', '');
                    }
                });

                const dataHero = tab.getAttribute('data-hero') || '';
                if (hero && dataHero) hero.src = dataHero;
            });
        });

    } catch (e) {
        if (e.name === 'AbortError') return;
        console.error(e);
        container.innerHTML = `<p>Couldn’t load product section. Please try again.</p>`;
    }
}

on('languageChanged', ({ lang }) => mountProducts(lang));
mountProducts('en');

export { mountProducts };