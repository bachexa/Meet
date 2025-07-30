document.addEventListener('DOMContentLoaded', () => {
    const wrapper = document.getElementById('slidesWrapper');
    const dots = document.querySelectorAll('.dot');
    let currentIndex = 0;
    const totalSlides = dots.length;

    function showSlide(index) {
        // ✅ აქ % → vw-ით შეცვლე
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

    // ✅ ავტომატური გადასვლა ყოველ 5 წამში
    setInterval(() => {
        currentIndex = (currentIndex + 1) % totalSlides;
        showSlide(currentIndex);
    }, 15000);

    showSlide(currentIndex);
});
