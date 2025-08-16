import { on } from '../../core/bus.js';
import { fetchJSON } from '../../core/api.js';
import { DynamicHtmlManager } from '../shared/dynamicHtml.js';

let timer = null;
let aborter = null;

function destroy() {
    if (timer) { clearInterval(timer); timer = null; }
    if (aborter) { aborter.abort(); aborter = null; }
}

function initializeSlider() {
    const wrapper = document.getElementById('slidesWrapper');
    const dots = document.querySelectorAll('.dot');
    if (!wrapper || dots.length === 0) return;

    let idx = 0;
    const show = (i) => {
        wrapper.style.transform = `translateX(-${i * 100}vw)`;
        dots.forEach((d, k) => d.classList.toggle('active', k === i));
        idx = i;
    };
    const start = () => {
        if (timer) clearInterval(timer);
        timer = setInterval(() => { idx = (idx + 1) % dots.length; show(idx); }, 15000);
    };
    const stop = () => { if (timer) { clearInterval(timer); timer = null; } };

    dots.forEach((dot) => dot.addEventListener('click', () => {
        const i = parseInt(dot.getAttribute('data-index'), 10);
        show(i); start();
    }));

    show(idx);
    start();

    const sliderEl = document.querySelector('.slider');
    sliderEl?.addEventListener('mouseenter', stop);
    sliderEl?.addEventListener('mouseleave', start);
}

async function mount(lang) {
    const container = document.querySelector('.slider');
    if (!container) return;

    destroy();
    aborter = new AbortController();

    try {
        const slides = await fetchJSON('/api/slider', { lang }, { signal: aborter.signal });
        container.innerHTML = DynamicHtmlManager.GetSliderModal(slides || []);
        initializeSlider();
    } catch (e) {
        if (e.name === 'AbortError') return;
        console.error(e);
        container.innerHTML = `<div class="slider-error"><p>Couldn’t load the slider. Please try again.</p></div>`;
    }
}

on('languageChanged', ({ lang }) => mount(lang));
