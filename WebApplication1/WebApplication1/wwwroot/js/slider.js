import { DynamicHtmlManager } from './dynamicHtml.js';

async function fetchSlides(lang) {
    // If your app may live under a virtual dir, consider using a base from a server-provided global
    const url = `/api/slider?lang=${encodeURIComponent(lang)}`;
    const res = await fetch(url, { headers: { 'Accept': 'application/json' } });
    if (!res.ok) throw new Error(`Failed to load slides: ${res.status}`);
    return await res.json();
}

async function renderSliderByLang(lang) {
    const sliderContainer = document.querySelector('.slider');
    if (!sliderContainer) return;

    try {
        const slides = await fetchSlides(lang);
        sliderContainer.innerHTML = DynamicHtmlManager.GetSliderModal(slides || []);
        initializeSlider();
    } catch (err) {
        console.error(err);
        sliderContainer.innerHTML = `
            <div class="slider-error">
                <p>Couldn’t load the slider. Please try again.</p>
            </div>`;
    }
}

// Listen for language changes (fired by site.js)
document.addEventListener('languageChanged', (e) => {
    const newLang = (e.detail && e.detail.lang) || 'en';
    renderSliderByLang(newLang);
});

// Initial render
document.addEventListener('DOMContentLoaded', () => {
    const initialLang = window.currentLang || 'en';
    renderSliderByLang(initialLang);
});

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

    dots.forEach(dot => {
        dot.addEventListener('click', () => {
            const index = parseInt(dot.getAttribute('data-index'), 10);
            showSlide(index);
            startAuto(); // restart cycle after manual nav
        });
    });

    showSlide(currentIndex);
    startAuto();

    // Optional: pause on hover
    const sliderEl = document.querySelector('.slider');
    if (sliderEl) {
        sliderEl.addEventListener('mouseenter', stopAuto);
        sliderEl.addEventListener('mouseleave', startAuto);
    }
}
