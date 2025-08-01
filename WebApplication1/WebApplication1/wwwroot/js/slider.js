import { DynamicHtmlManager } from './dynamicHtml.js';

const sliderDataFromCont = document.querySelector('.sliderCont');
const sliderData = sliderDataFromCont
    ? JSON.parse(sliderDataFromCont.getAttribute('data-slider'))
    : [];

function renderSliderByLang(lang) {
    const sliderContainer = document.querySelector('.slider');
    if (!sliderContainer || !sliderData.length) return;

    const filtered = sliderData.filter(slide => slide.Language === lang);
    sliderContainer.innerHTML = DynamicHtmlManager.GetSliderModal(filtered);
    initializeSlider();
}

// 🟡 This listener must be outside DOMContentLoaded
document.addEventListener('languageChanged', (e) => {
    const newLang = e.detail.lang || 'en';
    renderSliderByLang(newLang);
});

document.addEventListener('DOMContentLoaded', () => {
    const initialLang = window.currentLang || 'en';
    renderSliderByLang(initialLang);
});

function initializeSlider() {
    const wrapper = document.getElementById('slidesWrapper');
    const dots = document.querySelectorAll('.dot');
    if (!wrapper || dots.length === 0) return;

    let currentIndex = 0;

    function showSlide(index) {
        wrapper.style.transform = `translateX(-${index * 100}vw)`;
        dots.forEach((dot, i) => {
            dot.classList.toggle('active', i === index);
        });
        currentIndex = index;
    }

    dots.forEach(dot => {
        dot.addEventListener('click', () => {
            const index = parseInt(dot.getAttribute('data-index'));
            showSlide(index);
        });
    });

    setInterval(() => {
        currentIndex = (currentIndex + 1) % dots.length;
        showSlide(currentIndex);
    }, 15000);

    showSlide(currentIndex);
}
