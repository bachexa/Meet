import { t } from '../../core/i18n.js';

export class DynamicHtmlManager {
    static GetAuthHtml(lang) {
        return `
      <div class="auth-overlay" id="authOverlay" role="dialog" aria-modal="true">
        <div class="auth-card" id="authCard">
          <button class="close-btn" type="button" aria-label="Close">&times;</button>

          <div class="auth-section login-section">
            <input type="text" placeholder="${t('auth.usernamePlaceholder', null, lang)}" class="auth-input" />
            <input type="password" placeholder="${t('auth.passwordPlaceholder', null, lang)}" class="auth-input" />
            <button class="auth-action-btn" type="button">${t('auth.signInBtn', null, lang)}</button>
            <div class="auth-separator"><span>or</span></div>
            <button class="auth-btn google" type="button">
              <img src="/images/google-icon.png" alt="Google" />
              ${t('auth.googleSignIn', null, lang)}
            </button>
            <button class="auth-btn microsoft" type="button">
              <img src="/images/microsoft-icon.png" alt="Microsoft" />
              ${t('auth.msSignIn', null, lang)}
            </button>
            <p class="auth-link">
              ${t('auth.registerPrompt', null, lang)}
              <a href="#" id="showRegister">${t('auth.registerLink', null, lang)}</a>
            </p>
          </div>
        </div>
      </div>`;
    }

    static GetLayoutHeaderModal(lang) {
        return `
      <header class="ms-header">
        <div class="ms-container">
          <div class="ms-left">
            <a href="/" style="text-decoration:none;">
              <span style="font-family:'Segoe UI',sans-serif;font-size:20px;font-weight:600;color:#1a1a1a;">
                <span style="color:#0078d4;">Meet</span>Desk
              </span>
            </a>
            <div class="ms-divider"></div>
          </div>

          <!-- Mobile toggle button (no inline onclick) -->
          <button class="ms-toggle" type="button" aria-label="Toggle menu">☰</button>

          <nav class="ms-nav" id="mainMenu">
            <a href="#">${t('layoutHeader.products', null, lang)}</a>
            <a href="#">${t('layoutHeader.features', null, lang)}</a>
            <a href="#">${t('layoutHeader.pricing', null, lang)}</a>
            <a href="#">${t('layoutHeader.solutions', null, lang)}</a>
            <a href="#">${t('layoutHeader.resources', null, lang)}</a>
            <a href="#">${t('layoutHeader.support', null, lang)}</a>
          </nav>

          <div class="ms-buttons">
            <button class="ms-btn" type="button">${t('layoutHeader.downloadMetDesk', null, lang)}</button>
            <button class="ms-btn signin-btn" type="button">${t('layoutHeader.signIn', null, lang)}</button>

            <div class="lang-dropdown">
              <button class="ms-btn lang-toggle" type="button" aria-label="Change language">🌐</button>
              <div class="lang-menu" id="langMenu">
                <button class="lang-btn" data-lang="ka">GEO</button>
                <button class="lang-btn" data-lang="en">ENG</button>
              </div>
            </div>
          </div>
        </div>
      </header>`;
    }

    static GetLayoutFooterModal(lang) {
        return `
      <footer class="meetdesk-footer">
        <div class="meetdesk-footer-container">
          <div class="footer-links">
            <a href="#">${t('layoutHeader.products', null, lang)}</a>
            <a href="#">${t('layoutHeader.features', null, lang)}</a>
            <a href="#">${t('layoutHeader.pricing', null, lang)}</a>
            <a href="#">${t('layoutHeader.solutions', null, lang)}</a>
            <a href="#">${t('layoutHeader.support', null, lang)}</a>
            <a href="/Home/Privacy">${t('layoutFooter.secondText', null, lang)}</a>
          </div>
          <div class="footer-bottom-text">
            ${t('layoutFooter.firtsText', null, lang)}
          </div>
        </div>
      </footer>`;
    }

    static GetSliderModal(slides = []) {
        const slidesHtml = slides.map((s, i) => {
            const header = s.headerText ?? s.HeaderText ?? '';
            const text = s.paragraphText ?? s.ParagraphText ?? '';
            const img = s.img ?? s.Img ?? '';
            const btn = s.sliderButton ?? s.sliderButton ?? '';
            return `
        <div class="slide">
          <div class="hero-content">
            <h1>${header}</h1>
            <p>${text}</p>
            <a href="#" class="download-btn">${btn}</a>
          </div>
          <div class="hero-image">
            <img src="${img}" alt="Slide ${i + 1}">
          </div>
        </div>`;
        }).join('');

        const dotsHtml = slides.map((_, i) =>
            `<span class="dot${i === 0 ? ' active' : ''}" data-index="${i}"></span>`
        ).join('');

        return `
      <div class="slides-wrapper" id="slidesWrapper">${slidesHtml}</div>
      <div class="slider-dots">${dotsHtml}</div>`;
    }

    static GetDiscoverSectionModalFromModel(section) {
        if (!section) return '';
        const headerHtml = `
      <div class="discover-head">
        <h2>${section.discoverHeader ?? ''}</h2>
      </div>`;
        const cardsHtml = (section.cards || []).map(card => `
      <article class="discover-card">
        <div class="card-media">
          <img src="${card.img ?? ''}" alt="${card.discoverCardHeader ?? 'Discover item'}">
        </div>
        <div class="card-body">
          <h3>${card.discoverCardHeader ?? ''}</h3>
          <p>${card.discoverCardHeaderDescription ?? ''}</p>
          <a href="#" class="card-cta"><span class="cta-dot">➜</span>${card.discoverCardButton ?? ''}</a>
        </div>
      </article>`).join('');

        return `${headerHtml}<div class="discover-grid">${cardsHtml}</div>`;
    }
}
