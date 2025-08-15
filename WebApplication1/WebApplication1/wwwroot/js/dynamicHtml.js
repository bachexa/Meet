import { translations } from './translations.js';

export class DynamicHtmlManager {
    static GetAuthHtml(lang) {
        const t = translations[lang].auth;
        return `<div class="auth-overlay" id="authOverlay">
            <div class="auth-card" id="authCard">
                <button class="close-btn" type="button" aria-label="Close">&times;</button>

                <!-- Login Form -->
                <div class="auth-section login-section">
                    <input type="text" placeholder=${t.usernamePlaceholder} class="auth-input" />
                    <input type="password" placeholder=${t.passwordPlaceholder} class="auth-input" />
                    <button class="auth-action-btn" type="button">${t.signInBtn}</button>
                    <div class="auth-separator"><span>or</span></div>
                    <button class="auth-btn google" type="button">
                        <img src="/images/google-icon.png" alt="Google" />
                        ${t.googleSignIn}
                    </button>
                    <button class="auth-btn microsoft" type="button">
                        <img src="/images/microsoft-icon.png" alt="Microsoft" />
                       ${t.msSignIn}
                    </button>
                    <p class="auth-link">${t.registerPrompt} <a href="#" id="showRegister">${t.registerLink}</a></p>
                </div>
            </div>
        </div>`;
    }

    // შეგიძლია სხვა მეთოდებიც დაამატო:
    static GetRegisterHtml() {
        return `<div class="register-form"> ... </div>`;
    }

    static GetAnotherModal() {
        return `<div class="modal"> ... </div>`;
    }

    static GetLayoutHeaderModal(lang) {
        const t = translations[lang].layoutHeader;
        return `<header class="ms-header">
                <div class="ms-container">
                    <!-- Left: Logo + Brand -->
                    <div class="ms-left">
                        <a href="/" style="text-decoration: none;">
                <span style="font-family: 'Segoe UI', sans-serif; font-size: 20px; font-weight: 600; color: #1a1a1a;">
                    <span style="color: #0078d4;">Meet</span>Desk
                </span>
                </a>
                <div class="ms-divider"></div>
            
                </div>

                <!-- Mobile toggle button -->
                <button class="ms-toggle" onclick="toggleMenu()">☰</button>

                <!-- Center: Nav -->
                <nav class="ms-nav" id="mainMenu">
                    <a href="#">${t.products}</a>
                    <a href="#">${t.features}</a>
                    <a href="#">${t.pricing}</a>
                    <a href="#">${t.solutions}</a>
                    <a href="#">${t.resources}</a>
                    <a href="#">${t.support}</a>
                </nav>

                <!-- Right: Buttons -->
                

               <div class="ms-buttons">
                <button class="ms-btn" type="button">${t.downloadMetDesk}</button>
                <button class="ms-btn signin-btn" type="button">${t.signIn}</button>

                    <!-- Language Dropdown styled as button -->
                    <div class="lang-dropdown">
                        <button class="ms-btn lang-toggle" type="button" aria-label="Change language">
                            <svg xmlns="" width="20" height="20" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="1.5"
                                      d="M12 3c2.364 0 4.5.857 6.088 2.27M12 3c-2.364 0-4.5.857-6.088 2.27M12 3v3m6.088-0.73C19.046 6.64 20 9.204 20 12c0 2.796-.954 5.36-1.912 6.73M17 12H7m11.088 6.73C16.5 20.143 14.364 21 12 21c-2.364 0-4.5-.857-6.088-2.27M12 21v-3M5.912 18.73C4.954 17.36 4 14.796 4 12c0-2.796.954-5.36 1.912-6.73M7 12c0-3.866 2.239-7 5-7s5 3.134 5 7c0 3.866-2.239 7-5 7s-5-3.134-5-7Z"/>
                            </svg>
                        </button>
                        <div class="lang-menu" id="langMenu">
                            <button class="lang-btn" data-lang="ka">GEO</button>
                            <button class="lang-btn" data-lang="en">ENG</button>
                        </div>
                    </div>
                </div>
               </div>
            </div>
            </header>`;
    }

            static GetLayoutFooterModal(lang) {
                const t = translations[lang].layoutFooter;
                const m = translations[lang].layoutHeader;
                return `
                <footer class="meetdesk-footer">
                    <div class="meetdesk-footer-container">
                        <!-- Footer Links -->
                        <div class="footer-links">
                            <a href="#">${m.products}</a>
                            <a href="#">${m.features}</a>
                            <a href="#">${m.pricing}</a>
                            <a href="#">${m.solutions}</a>
                            <a href="#">${m.support}</a>
                            <a href="/Home/Privacy">${t.secondText}</a>
                        </div>

                        <!-- Footer Description -->
                        <div class="footer-bottom-text">
                            ${t.firtsText}
                        </div>
                    </div>
                </footer>`;
            }


    static GetSliderModal(filteredSlides) {
        const slidesHtml = filteredSlides.map((slide, index) => {
            const header = slide.headerText ?? slide.HeaderText ?? '';
            const text = slide.paragraphText ?? slide.ParagraphText ?? '';
            const img = slide.img ?? slide.Img ?? '';
            const sliderButton = slide.sliderButton ?? slide.sliderButton ?? '';

            return `
              <div class="slide">
                <div class="hero-content">
                  <h1>${header}</h1>
                  <p>${text}</p>
                  <a href="#" class="download-btn">${sliderButton}</a>
                </div>
                <div class="hero-image">
                  <img src="${img}" alt="Slide ${index + 1}">
                </div>
              </div>
            `;
            }).join('');

            const dotsHtml = filteredSlides.map((_, i) =>
                `<span class="dot${i === 0 ? ' active' : ''}" data-index="${i}"></span>`
            ).join('');

                return `
            <div class="slides-wrapper" id="slidesWrapper">
              ${slidesHtml}
            </div>
            <div class="slider-dots">${dotsHtml}</div>
          `;
         }

    static GetDiscoverSectionModalFromModel(section) {
        if (!section) return '';

        const headerHtml = `
        <div class="discover-head">
            <h2>${section.discoverHeader ?? ''}</h2>
        </div>
    `;
        const cardsHtml = (section.cards || [])
            .map(card => `
            <article class="discover-card">
                <div class="card-media">
                    <img src="${card.img ?? ''}" alt="${card.discoverCardHeader ?? 'Discover item'}">
                </div>
                <div class="card-body">
                    <h3>${card.discoverCardHeader ?? ''}</h3>
                    <p>${card.discoverCardHeaderDescription ?? ''}</p>
                    <a href="#" class="card-cta">
                        <span class="cta-dot">➜</span>
                        ${card.discoverCardButton ?? ''}
                    </a>
                </div>
            </article>
        `)
            .join('');

        return `
        ${headerHtml}
        <div class="discover-grid">
            ${cardsHtml}
        </div>
    `;
    }

    
}
