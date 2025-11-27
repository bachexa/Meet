import { on } from '../../core/bus.js';
import { fetchJSON } from '../../core/api.js';
import { DynamicHtmlManager } from '../shared/dynamicHtml.js';

let aborter = null;

function destroy() {
    if (aborter) {
        aborter.abort();
        aborter = null;
    }
}

async function mountProducts(lang) {
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

        const heroMap = {
            home: '/images/home.jpg',
            business: '/images/business.png',
            enterprise: '/images/enterprise.png',
            education: '/images/education.png'
        };

        // დაიწყე hero ფოტოთი პირველ panel-ზე
        const firstPanel = container.querySelector('.ms-panel.is-visible');
        if (firstPanel) {
            const firstPanelName = firstPanel.dataset.panel;
            hero.src = heroMap[firstPanelName] ?? '/images/home.jpg';
        }

        tabs.forEach(tab => {
            tab.addEventListener('click', () => {
                tabs.forEach(t => t.classList.remove('is-active'));
                tab.classList.add('is-active');

                const panelName = tab.dataset.tab;
                panels.forEach(p => {
                    if (p.dataset.panel === panelName) {
                        p.classList.add('is-visible');
                        p.removeAttribute('hidden');
                    } else {
                        p.classList.remove('is-visible');
                        p.setAttribute('hidden', '');
                    }
                });

                hero.src = heroMap[panelName] ?? '/images/home.jpg';
            });
        });

    } catch (e) {
        if (e.name === 'AbortError') return;
        console.error(e);
        container.innerHTML = `<p>Couldn’t load product section. Please try again.</p>`;
    }
}

// ენის ცვლილება
on('languageChanged', ({ lang }) => mountProducts(lang));

// პირველი გაშვება
mountProducts('en');

export { mountProducts };