import { on } from '../../core/bus.js';
import { fetchJSON } from '../../core/api.js';
import { DynamicHtmlManager } from '../shared/dynamicHtml.js';

let aborter = null;
function destroy() { if (aborter) { aborter.abort(); aborter = null; } }

// Tab/hero initializer (delegated; runs once per mount container)
function initPlans(root) {
    if (!root || root.dataset.plansInit === '1') return;
    root.dataset.plansInit = '1';

    const heroImg = root.querySelector('#ms-hero-img');

    const activate = (tab) => {
        if (!tab) return;
        const tabs = root.querySelectorAll('[role="tab"]');
        const panels = root.querySelectorAll('[role="tabpanel"]');

        // deactivate
        tabs.forEach(t => {
            t.classList.remove('is-active');
            t.setAttribute('aria-selected', 'false');
            t.setAttribute('tabindex', '-1');
        });
        panels.forEach(p => { p.hidden = true; p.classList.remove('is-visible'); });

        // activate chosen
        tab.classList.add('is-active');
        tab.setAttribute('aria-selected', 'true');
        tab.removeAttribute('tabindex');

        const id = tab.dataset.tab;
        const panel = root.querySelector(`[data-panel="${id}"]`);
        if (panel) {
            panel.hidden = false;
            panel.classList.add('is-visible');

            // swap hero using data attributes
            if (heroImg) {
                const src = panel.dataset.hero || heroImg.dataset.fallback;
                const alt = panel.dataset.heroAlt || '';
                if (src) heroImg.setAttribute('src', src);
                heroImg.setAttribute('alt', alt);
            }
        }
    };

    // Click (delegated)
    root.addEventListener('click', (e) => {
        const tab = e.target.closest('.ms-tab[role="tab"]');
        if (!tab || !root.contains(tab)) return;
        activate(tab);
    });

    // Keyboard (delegated)
    root.addEventListener('keydown', (e) => {
        const tgt = e.target;
        if (!tgt || !tgt.matches('.ms-tab[role="tab"]')) return;

        const tabs = Array.from(root.querySelectorAll('[role="tab"]'));
        const i = tabs.indexOf(tgt);
        if (i < 0) return;

        if (e.key === 'ArrowRight') {
            e.preventDefault();
            const n = tabs[(i + 1) % tabs.length];
            n.focus(); activate(n);
        } else if (e.key === 'ArrowLeft') {
            e.preventDefault();
            const p = tabs[(i - 1 + tabs.length) % tabs.length];
            p.focus(); activate(p);
        }
    });

    // Initialize to active (or first) tab
    const initial = root.querySelector('.ms-tab.is-active') || root.querySelector('[role="tab"]');
    if (initial) activate(initial);
}

async function mount(lang) {
    const container = document.querySelector('.ms-plans');
    if (!container) return;

    destroy();
    aborter = new AbortController();

    try {
        // const model = await fetchJSON('/api/discoversection', { lang }, { signal: aborter.signal });
        container.innerHTML = DynamicHtmlManager.GetPlansSectionModalFromModel("");

        // IMPORTANT: wire up the freshly injected DOM
        initPlans(container);
    } catch (e) {
        if (e.name === 'AbortError') return;
        console.error(e);
        container.innerHTML = `<div class="discover-error"><p>Couldn’t load Discover content. Please try again.</p></div>`;
    }
}

on('languageChanged', ({ lang }) => mount(lang));
// also call mount once at startup if needed
// mount(currentLang);
