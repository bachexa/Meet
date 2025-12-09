import { t } from '../../core/i18n.js';

export class DynamicHtmlManager {


    // in DynamicHtmlManager
    static GetAuthHtmlFromModel(section) {
        if (!section) return '';

        const auth = section.authCart ?? section.AuthCart ?? {};

        const usernamePlaceholder = auth.usernamePlaceholder ?? auth.UsernamePlaceholder ?? 'Username';
        const passwordPlaceholder = auth.passwordPlaceholder ?? auth.PasswordPlaceholder ?? 'Password';
        const signIn = auth.signIn ?? auth.SignIn ?? 'Sign in';
        const googleSignIn = auth.googleSignIn ?? auth.GoogleSignIn ?? 'Sign in with Google';
        const msSignIn = auth.msSignIn ?? auth.MsSignIn ?? 'Sign in with Microsoft';
        const registerPrompt = auth.registerPrompt ?? auth.RegisterPrompt ?? "Don't have an account?";
        const registerLink = auth.registerLink ?? auth.RegisterLink ?? 'Register';

        return `
              <div class="auth-overlay" id="authOverlay" role="dialog" aria-modal="true">
                <div class="auth-card" id="authCard">
                  <button class="close-btn" type="button" aria-label="Close">&times;</button>

                  <div class="auth-section login-section">
                    <input type="text"
                           placeholder="${usernamePlaceholder}"
                           class="auth-input" />
                    <input type="password"
                           placeholder="${passwordPlaceholder}"
                           class="auth-input" />
                    <button class="auth-action-btn" type="button">${signIn}</button>
                    <div class="auth-separator"><span>or</span></div>
                    <button class="auth-btn google" type="button">
                      <img src="/images/google-icon.png" alt="Google" />
                      ${googleSignIn}
                    </button>
                    <button class="auth-btn microsoft" type="button">
                      <img src="/images/microsoft-icon.png" alt="Microsoft" />
                      ${msSignIn}
                    </button>
                    <p class="auth-link">
                      ${registerPrompt}
                      <a href="#" id="showRegister">${registerLink}</a>
                    </p>
                  </div>
                </div>
              </div>`;
    }


    // in DynamicHtmlManager
    static GetLayoutHeaderFromModel(section, lang) {
        if (!section) return '';

        const download = section.download;
        const signIn = section.signIn;
        const langENG = section.langENG;
        const langGeo = section.langGeo;

        const items = (section.mneuItemName ?? section.MneuItemName ?? [])
            .map(x => x.menuItems ?? x.MenuItems ?? '')
            .filter(Boolean);

        const targetsByIndex = [
            '.slider',
            '.discover',
            '.solutions',
            '.ms-plans',
            '.plans-section-container'
        ];

        const menuHtml = items
            .map((item, index) => {
                const target = targetsByIndex[index] || '';
                const dataAttr = target ? ` data-target="${target}"` : '';
                return `<a href="#" class="ms-nav-link"${dataAttr}>${item}</a>`;
            })
            .join('');

        // decide which language is active
        const isKaActive = lang === 'ka';
        const isEnActive = lang === 'en';

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

              <button class="ms-toggle" type="button" aria-label="Toggle menu">☰</button>

              <nav class="ms-nav" id="mainMenu">
                ${menuHtml}
              </nav>

              <div class="ms-buttons">
                <button class="ms-btn" type="button">
                    ${download}
                </button>
                <button class="ms-btn signin-btn" type="button">
                    ${signIn}
                </button>

                <div class="lang-dropdown">
                  <button class="ms-btn lang-toggle" type="button" aria-label="Change language">
                      <svg viewBox="0 0 24 24" aria-hidden="true">
                        <circle cx="12" cy="12" r="9" fill="none" stroke="currentColor" stroke-width="1.8"/>
                        <path d="M3 12h18M12 3a16 16 0 0 1 0 18M12 3a16 16 0 0 0 0 18" 
                              fill="none" stroke="currentColor" stroke-width="1.8"/>
                      </svg>
                  </button>

                  <div class="lang-menu" id="langMenu">
                    <button class="lang-btn${isKaActive ? ' is-active' : ''}" data-lang="ka" type="button">${langGeo}</button>
                    <button class="lang-btn${isEnActive ? ' is-active' : ''}" data-lang="en" type="button">${langENG}</button>
                  </div>
                </div>
              </div>
            </div>
          </header>`;
    }





    static GetLayoutFooterModal(section, lang) {

        const footerFirstText = section.footerFirstText ?? section.FooterFirstText ?? '';

        const items = (section.mneuItemName ?? section.MneuItemName ?? [])
            .map(x => x.menuItems ?? x.MenuItems ?? '')
            .filter(Boolean);

        // Use the same mapping as header: index -> section selector
        const targetsByIndex = [
            '.slider',                 // 0: Products
            '.discover',               // 1: Features
            '.solutions',              // 2: Solutions
            '.ms-plans',               // 3: Plans section
            '.plans-section-container' // 4: Resources
        ];

        const linksHtml = items
            .map((item, index) => {
                const target = targetsByIndex[index] || '';
                const dataAttr = target ? ` data-target="${target}"` : '';
                // IMPORTANT: reuse ms-nav-link so the same click handler works
                return `<a href="#" class="ms-nav-link footer-link"${dataAttr}>${item}</a>`;
            })
            .join('');

        return `
        <footer class="meetdesk-footer">
          <div class="meetdesk-footer-container">
            <div class="footer-links">
              ${linksHtml}
            </div>
            <div class="footer-bottom-text">
              ${footerFirstText}
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


    static GetSolutionsSectionModalFromModel(section) {
        if (!section) return '';

        // helpers: support both PascalCase and camelCase; normalize relative image paths
        const pick = (obj, ...keys) => {
            for (const k of keys) if (obj && obj[k] != null) return obj[k];
            return undefined;
        };
        const normalizeImg = (p) => {
            if (!p) return '';
            if (/^https?:\/\//i.test(p) || p.startsWith('/')) return p; // absolute URL or root path
            return p.startsWith('images/') ? p : `images/${p}`;          // ensure "images/" prefix
        };

        const title = pick(section, 'solutionName', 'SolutionName') ?? 'SOLUTIONS';
        // SolutionDescription may include HTML (e.g., <span>MeetDesk</span>)—we trust your data here
        const subtitle = pick(section, 'solutionDescription', 'SolutionDescription') ?? '';

        const cards = pick(section, 'cards', 'Cards') ?? [];
        const safeCards = Array.isArray(cards) ? cards : [];
        const total = safeCards.length || 0;

        // Accordion items (first one active)
        const accordionHtml = safeCards.map((card, i) => {
            const name = pick(card, 'solutionCardName', 'SolutionCardName') ?? `Item ${i + 1}`;
            const desc = pick(card, 'solutionCardDescription', 'SolutionCardDescription') ?? '';
            const btn = pick(card, 'solutionCardButton', 'SolutionCardButton') ?? '';
            return `
              <div class="accordion-item${i === 0 ? ' active' : ''}">
                <button class="accordion-header" data-slide="${i}">${name}</button>
                <div class="accordion-content">
                  <p>${desc}</p>
                  ${btn ? `<a href="#" class="card-cta"><span class="cta-dot">➜</span>${btn}</a>` : ''}
                </div>
              </div>`;
                }).join('');

                // Slides (match the same order & count)
                const slidesHtml = safeCards.map((card, i) => {
                    const img = normalizeImg(pick(card, 'solutionCardSliderImg', 'solutionCardSliderImg'));
                    const alt = pick(card, 'solutionCardName', 'SolutionCardName') ?? `Slide ${i + 1}`;
                    return `
              <div class="solutions-slide" role="group" aria-label="Slide ${i + 1} of ${total || 1}">
                <img src="${img}" alt="${alt}" loading="lazy">
              </div>`;
        }).join('');

        return `
            <div class="solutions-container">
              <!-- Left side -->
              <div class="solutions-text">
                <h5 class="solutions-subtitle">${title}</h5>
                <h2 class="solutions-title">${subtitle}</h2>
                <div class="accordion">
                  ${accordionHtml}
                </div>
              </div>

              <!-- Right side -->
              <div class="solutions-image">
                <div class="solutions-slider" role="region" aria-roledescription="carousel" aria-label="Solutions screenshots" tabindex="0">
                  <div class="solutions-track">
                    ${slidesHtml}
                  </div>
                  <div class="solutions-dots" aria-label="Slide navigation"></div>
                </div>
              </div>
           </div>`;
    }


    static GetPlansSectionModalFromModel(section) {
        if (!section) return '';

        // If API returns a list, use the first one (en, for example)
        const sec = Array.isArray(section) ? section[0] : section;

        // Support both camelCase and PascalCase
        const plansTitle = sec.plansTitle ?? sec.PlansTitle ?? '';
        const plansDescription = sec.plansDescription ?? sec.PlansDescription ?? '';
        const allPlans = sec.allPlans ?? sec.AllPlans ?? [];

        if (!allPlans.length) return '';

        // Helper: turn "Home" -> "home", "Business" -> "business", "Network" -> "network"
        const slugify = (text) =>
            String(text || '')
                .toLowerCase()
                .trim()
                .replace(/\s+/g, '-')
                .replace(/[^a-z0-9-]/g, '');

        // First hero image as default/fallback
        const firstImg = allPlans[0].img ?? allPlans[0].Img ?? '/images/home.png';

        // Build tabs
        const tabsHtml = allPlans.map((group, index) => {
            const label = group.menuItem ?? group.MenuItem ?? '';
            const slug = slugify(label) || `tab-${index}`;
            const isActive = index === 0;
            return `
            <button
                role="tab"
                aria-selected="${isActive ? 'true' : 'false'}"
                aria-controls="panel-${slug}"
                id="tab-${slug}"
                class="ms-tab${isActive ? ' is-active' : ''}"
                data-tab="${slug}">
                ${label}
            </button>`;
        }).join('');

        // Build panels + cards
        const panelsHtml = allPlans.map((group, index) => {
            const label = group.menuItem ?? group.MenuItem ?? '';
            const slug = slugify(label) || `tab-${index}`;
            const img = group.img ?? group.Img ?? firstImg;
            const heroAlt = `${label} hero image`;

            const cards = group.plans ?? group.Plans ?? [];
            const cardsHtml = cards.map(card => {
                const icon = card.icon ?? card.Icon ?? '';
                const title = card.title ?? card.Title ?? '';
                const description = card.description ?? card.Description ?? '';
                const more = card.more ?? card.More ?? 'Learn more';

                return `
                <article class="ms-card">
                    <div class="ms-card__icon" aria-hidden="true">
                        ${icon}
                    </div>
                    <h3 class="ms-card__title">${title}</h3>
                    <p class="ms-card__text">${description}</p>
                    <a href="#" class="card-cta"><span class="cta-dot">➜</span>${more}</a>
                </article>`;
            }).join('');

            const isVisible = index === 0;

            return `
            <div id="panel-${slug}"
                 class="ms-panel${isVisible ? ' is-visible' : ''}"
                 role="tabpanel"
                 aria-labelledby="tab-${slug}"
                 data-panel="${slug}"
                 data-hero="${img}"
                 data-hero-alt="${heroAlt}"
                 ${isVisible ? '' : 'hidden'}>
                <div class="ms-cards">
                    ${cardsHtml}
                </div>
            </div>`;
        }).join('');

        // Put it all together
        const headerHtml = `
        <div class="ms-plans__container">
            <header class="ms-plans__header">
                <p class="solutions-subtitle">${plansTitle}</p>
                <h1 class="solutions-title">${plansDescription}</h1>
                <div class="ms-plans__tabs" role="tablist" aria-label="Plan categories">
                    ${tabsHtml}
                </div>
            </header>

            <div class="ms-plans__layout">
                <figure class="ms-hero" aria-hidden="true">
                    <img id="ms-hero-img"
                         src="${firstImg}"
                         alt=""
                         data-fallback="${firstImg}">
                </figure>

                <div class="ms-plans__panels">
                    ${panelsHtml}
                </div>
            </div>
        </div>`;

        return headerHtml;
    }


    static RenderResourcesSectionFromModel(section = {}) {
        if (!section) return '';

        const sectionTitle = section.CardName ?? section.cardName ?? '';
        const sectionDesc = section.CardDescription ?? section.cardDescription ?? '';
        const cards = section.Cards ?? section.cards ?? [];//BackgroundImg
        const backgroundImg = section.BackgroundImg ?? section.backgroundImg ?? [];

        const cardsHtml = cards.map(c => {
            const cardTitle = c.CardName ?? c.cardName ?? '';
            const cardDesc = c.CardDescription ?? c.cardDescription ?? '';
            const cardBtn = c.CardButton ?? c.cardButton ?? '';
            const cardSvg = c.cardSvg ?? '';

            return `
         <article class="card">
             <div class="card-top">
                 <div class="icon" aria-hidden="true">
                     ${cardSvg} 
                 </div>
                 <h2>${cardTitle}</h2>
                 <p>${cardDesc}</p>
             </div>
             <div class="card-bottom">
                 <a class="btn" href="#">
                     <span class="cta-dot">➜</span>${cardBtn}
                 </a>
             </div>
         </article>`;
        }).join('');

        return `
     <section class="plans-section-container" style="background-image: url(${backgroundImg}); background-size: cover; background-position: center; background-repeat: no-repeat;">
         <main class="container">
             <p class="kicker"><b>${sectionTitle}</b></p>
             <h1 class="headline">${sectionDesc}</h1>
             <section class="cards">
                 ${cardsHtml}
             </section>
         </main>
     </section>`;
    }
}
