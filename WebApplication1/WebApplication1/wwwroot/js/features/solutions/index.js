import { on } from '../../core/bus.js';
import { fetchJSON } from '../../core/api.js';
import { DynamicHtmlManager } from '../shared/dynamicHtml.js';

let aborter = null;
let sliderTimer = null; // used by autoplay
function destroy() {
    if (aborter) { aborter.abort(); aborter = null; }
    if (sliderTimer) { clearInterval(sliderTimer); sliderTimer = null; }
}


function initSolutionsSlider(container) {
    const root = container.querySelector('.solutions-image .solutions-slider');
    if (!root) return;

    const track = root.querySelector('.solutions-track');
    const slides = Array.from(root.querySelectorAll('.solutions-slide'));
    const dotsWrap = root.querySelector('.solutions-dots');
    if (!track || slides.length === 0) return;

    const count = slides.length;
    let index = 0;

    // Build dots
    dotsWrap.innerHTML = '';
    const dots = slides.map((_, i) => {
        const b = document.createElement('button');
        b.type = 'button';
        b.className = 'solutions-dot';
        b.setAttribute('aria-label', `Go to slide ${i + 1}`);
        b.addEventListener('click', (e) => {
            e.stopPropagation();          // don't let swipe handler eat the click
            index = i;
            update();
            restartAuto();                // optional: keep autoplay alive after click
        }, { signal: aborter.signal });
        dotsWrap.appendChild(b);
        return b;
    });

    // Prevent swipe starting from the dots
    dotsWrap.addEventListener('pointerdown', (e) => e.stopPropagation(), { signal: aborter.signal });
    dotsWrap.addEventListener('pointerup', (e) => e.stopPropagation(), { signal: aborter.signal });

    function update() {
        track.style.transform = `translateX(-${index * 100}%)`;
        dots.forEach((d, i) => {
            d.classList.toggle('active', i === index);
            d.setAttribute('aria-current', i === index ? 'true' : 'false');
        });
        slides.forEach((s, i) => s.setAttribute('aria-hidden', i === index ? 'false' : 'true'));
    }

    function next() { index = (index + 1) % count; update(); }
    function prev() { index = (index - 1 + count) % count; update(); }

    // --- Autoplay helpers (THIS IS THE PART YOU NEED) ---
    function startAuto() {
        if (count <= 1) return;              // nothing to auto-advance
        if (sliderTimer) clearInterval(sliderTimer);
        sliderTimer = setInterval(next, 5000);
    }
    function stopAuto() {
        if (sliderTimer) { clearInterval(sliderTimer); sliderTimer = null; }
    }
    function restartAuto() { stopAuto(); startAuto(); }

    // Keyboard
    root.addEventListener('keydown', (e) => {
        if (e.key === 'ArrowRight') { e.preventDefault(); next(); restartAuto(); }
        else if (e.key === 'ArrowLeft') { e.preventDefault(); prev(); restartAuto(); }
    }, { signal: aborter.signal });

    // Hover/focus pauses
    root.addEventListener('mouseenter', stopAuto, { signal: aborter.signal });
    root.addEventListener('mouseleave', startAuto, { signal: aborter.signal });
    root.addEventListener('focusin', stopAuto, { signal: aborter.signal });
    root.addEventListener('focusout', startAuto, { signal: aborter.signal });

    // Swipe (ignore when starting on dots)
    let startX = null, trackingId = null;
    root.addEventListener('pointerdown', (e) => {
        if (e.button !== 0) return;
        if (e.target.closest('.solutions-dots')) return;
        startX = e.clientX;
        trackingId = e.pointerId;
        root.setPointerCapture(trackingId);
    }, { signal: aborter.signal });

    root.addEventListener('pointerup', (e) => {
        if (trackingId !== e.pointerId || startX === null) return;
        const dx = e.clientX - startX;
        const threshold = 40;
        if (dx > threshold) prev();
        else if (dx < -threshold) next();
        try { root.releasePointerCapture(trackingId); } catch { }
        startX = null; trackingId = null;
        restartAuto();
    }, { signal: aborter.signal });

    // Initial paint + autoplay
    update();
    startAuto();

    // Resize keeps alignment correct
    window.addEventListener('resize', update, { signal: aborter.signal });
}






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
        initSolutionsSlider(container); // NEW
    } catch (e) {
        if (e.name === 'AbortError') return;
        console.error(e);
        container.innerHTML = `<div class="discover-error"><p>Couldn’t load Discover content. Please try again.</p></div>`;
    }
}

on('languageChanged', ({ lang }) => mount(lang));
