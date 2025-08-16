import { DynamicHtmlManager } from './dynamicHtml.js';
import { fetchJSON } from './backCalls.js';

let sliderRequestSeq = 0;       // increments per request to ignore stale responses
let sliderAbortController = null;

async function renderSliderByLang(lang) {
    const container = document.querySelector('.slider');
    if (!container) return;

    // cancel previous request if still running
    if (sliderAbortController) sliderAbortController.abort();
    sliderAbortController = new AbortController();

    const seq = ++sliderRequestSeq;

    try {
        const slides = await fetchJSON('/api/slider', { lang }, { signal: sliderAbortController.signal });
        if (seq !== sliderRequestSeq) return; // stale response, ignore

        container.innerHTML = DynamicHtmlManager.GetSliderModal(slides || []);
        initializeSlider();
    } catch (err) {
        if (err.name === 'AbortError') return; // was superseded by a newer lang change
        console.error(err);
        container.innerHTML = `
      <div class="slider-error">
        <p>Couldn’t load the slider. Please try again.</p>
      </div>`;
    }
}

// React only to language changes (no DOMContentLoaded initial render here)
document.addEventListener('languageChanged', (e) => {
    const newLang = e?.detail?.lang || 'en';
    renderSliderByLang(newLang);
});

// slider init unchanged
function initializeSlider() {
    const wrapper = document.getElementById('slidesWrapper');
    const dots = document.querySelectorAll('.dot');
    if (!wrapper || dots.length === 0) return;

    let currentIndex = 0;
    let timerId = null;

    function showSlide(index) {
        wrapper.style.transform = `translateX(-${index * 100}vw)`;
        dots.forEach((dot, i) => dot.classList.toggle('active', i === index));
        currentIndex = index;
    }

    function startAuto() {
        stopAuto();
        timerId = setInterval(() => {
            currentIndex = (currentIndex + 1) % dots.length;
            showSlide(currentIndex);
        }, 15000);
    }

    function stopAuto() {
        if (timerId) {
            clearInterval(timerId);
            timerId = null;
        }
    }

    dots.forEach((dot) => {
        dot.addEventListener('click', () => {
            const index = parseInt(dot.getAttribute('data-index'), 10);
            showSlide(index);
            startAuto();
        });
    });

    showSlide(currentIndex);
    startAuto();

    const sliderEl = document.querySelector('.slider');
    if (sliderEl) {
        sliderEl.addEventListener('mouseenter', stopAuto);
        sliderEl.addEventListener('mouseleave', startAuto);
    }
}
