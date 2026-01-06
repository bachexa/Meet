function parseBgUrl(bg) {
    if (!bg) return '';
    const m = String(bg).match(/url\\((['"]?)(.*?)\\1\\)/i);
    return m ? m[2] : '';
}

function extractModalData(triggerEl) {
    // 1) explicit overrides (optional if you later add data- ატრიბუტები)
    const ds = triggerEl.dataset || {};
    if (ds.modalTitle || ds.modalDesc || ds.modalImg) {
        return {
            title: ds.modalTitle || '',
            desc: ds.modalDesc || '',
            img: ds.modalImg || ''
        };
    }

    // Discover card
    const discover = triggerEl.closest('.discover-card');
    if (discover) {
        return {
            title: discover.querySelector('h3')?.textContent?.trim() || '',
            desc: discover.querySelector('p')?.textContent?.trim() || '',
            img: discover.querySelector('img')?.getAttribute('src') || ''
        };
    }

    // Resources card (plans-section-container cards)
    const resCard = triggerEl.closest('article.card');
    if (resCard) {
        const section = triggerEl.closest('section.plans-section-container');
        const bg = section ? getComputedStyle(section).backgroundImage : '';
        return {
            title: resCard.querySelector('h2')?.textContent?.trim() || '',
            desc: resCard.querySelector('p')?.textContent?.trim() || '',
            img: parseBgUrl(bg)
        };
    }

    // Plans ms-card
    const msCard = triggerEl.closest('.ms-card');
    if (msCard) {
        const panel = triggerEl.closest('.ms-panel') || document.querySelector('.ms-panel.is-visible');
        const hero = panel?.dataset?.hero || document.getElementById('ms-hero-img')?.getAttribute('src') || '';
        return {
            title: msCard.querySelector('.ms-card__title')?.textContent?.trim() || '',
            desc: msCard.querySelector('.ms-card__text')?.textContent?.trim() || '',
            img: hero
        };
    }

    // Solutions accordion
    const accItem = triggerEl.closest('.accordion-item');
    if (accItem) {
        const btn = accItem.querySelector('.accordion-header');
        const idx = Number(btn?.dataset?.slide ?? -1);
        const slider = triggerEl.closest('.solutions-container')?.querySelector('.solutions-track');
        const slideImg = (idx >= 0 && slider?.children?.[idx])
            ? slider.children[idx].querySelector('img')?.getAttribute('src')
            : '';
        return {
            title: btn?.textContent?.trim() || '',
            desc: accItem.querySelector('.accordion-content p')?.textContent?.trim() || '',
            img: slideImg || ''
        };
    }

    // Fallback: nearest article
    const article = triggerEl.closest('article');
    return {
        title: article?.querySelector('h2,h3')?.textContent?.trim() || triggerEl.textContent?.trim() || '',
        desc: article?.querySelector('p')?.textContent?.trim() || '',
        img: article?.querySelector('img')?.getAttribute('src') || ''
    };
}

function initInfoModal() {
    const overlay = document.getElementById('infoModal');
    if (!overlay) return;

    const card = overlay.querySelector('.md-info-card');
    const titleEl = document.getElementById('infoModalTitle');
    const descEl = document.getElementById('infoModalDesc');
    const imgEl = document.getElementById('infoModalImg');
    const closeBtn = overlay.querySelector('.md-info-close');
    const closeBtn2 = overlay.querySelector('[data-close-modal]');

    let lastFocus = null;

    function openModal(data, focusBackTo) {
        lastFocus = focusBackTo || null;

        titleEl.textContent = data.title || '';
        descEl.textContent = data.desc || '';

        if (data.img) {
            imgEl.src = data.img;
            imgEl.alt = data.title || 'Preview';
            card.classList.remove('no-image');
        } else {
            imgEl.removeAttribute('src');
            imgEl.alt = '';
            card.classList.add('no-image');
        }

        overlay.classList.add('is-open');
        overlay.setAttribute('aria-hidden', 'false');
        document.body.style.overflow = 'hidden';
        closeBtn?.focus();
    }

    function closeModal() {
        overlay.classList.remove('is-open');
        overlay.setAttribute('aria-hidden', 'true');
        document.body.style.overflow = '';
        if (lastFocus && typeof lastFocus.focus === 'function') lastFocus.focus();
    }

    // Close actions
    closeBtn?.addEventListener('click', closeModal);
    closeBtn2?.addEventListener('click', closeModal);

    overlay.addEventListener('click', (e) => {
        if (e.target === overlay) closeModal();
    });

    document.addEventListener('keydown', (e) => {
        if (e.key === 'Escape' && overlay.classList.contains('is-open')) closeModal();
    });

    // Event delegation for dynamic content
    document.addEventListener('click', (e) => {
        const trigger = e.target.closest('a.card-cta, a.download-btn, .plans-section-container a.btn');
        if (!trigger) return;

        e.preventDefault();
        openModal(extractModalData(trigger), trigger);
    });
}

initInfoModal();
