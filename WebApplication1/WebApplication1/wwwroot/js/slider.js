import { DynamicHtmlManager } from './dynamicHtml.js';

document.addEventListener('DOMContentLoaded', () => {

    const lang = window.currentLang || 'en';

    const sliderDataFromCont = document.querySelector('.sliderCont');
    const sliderData = JSON.parse(sliderDataFromCont.getAttribute('data-slider'));

    const filtered = sliderData.filter(item => item.Language === lang);

    // Step 1: ჩასმა დინამიურად
    const sliderContainer = document.querySelector('.slider');
    sliderContainer.innerHTML = DynamicHtmlManager.GetSliderModal(filtered);

    // Step 2: სლაიდერის ინიციალიზაცია
    const wrapper = document.getElementById('slidesWrapper');
    const dots = document.querySelectorAll('.dot');
    let currentIndex = 0;
    const totalSlides = dots.length;

    function showSlide(index) {
        wrapper.style.transform = `translateX(-${index * 100}vw)`; // ან %, თუ width 100%-ზე გაქვს
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
        currentIndex = (currentIndex + 1) % totalSlides;
        showSlide(currentIndex);
    }, 15000);

    showSlide(currentIndex);
});
