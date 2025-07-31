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

    static GetLayoutHeaderModal() {
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
                    <a href="#">Products</a>
                    <a href="#">Features</a>
                    <a href="#">Pricing</a>
                    <a href="#">Solutions</a>
                    <a href="#">Resources</a>
                    <a href="#">Support</a>
                </nav>

                <!-- Right: Buttons -->
                <div class="ms-buttons">
                    <button class="ms-btn" type="button">Download MetDesk</button>
                    <button class="ms-btn signin-btn" type="button">Sign in</button>
                </div>
            </div>
            </header>`;
    }

    static GetLayoutFooterModal() {
        return ` <footer class="meetdesk-footer">
                    <div class="meetdesk-footer-content">
                        &copy; 2025 - MeetDesk. All rights reserved. <br />
                        <a href="/Home/Privacy">Privacy Policy</a>
                    </div>
                 </footer>`;
    }

    static GetSliderModal() {
        return `<div class="slides-wrapper" id="slidesWrapper">

                    <div class="slide">
                        <div class="hero-content">
                            <h1>
                                Make friends with<br>
                                <span style="font-weight:700;font-size:42px;color:#1a1a1a;">
                                    <span style="color:#0078d4;">Meet</span>Desk
                                </span>
                            </h1>
                            <p>Start meetings, share moments and work together easily!</p>
                            <a href="#" class="download-btn">Download now</a>
                        </div>
                        <div class="hero-image">
                            <img src="/images/teams-hero.png" style="margin-left: -200px;" alt="Slide 1">
                        </div>
                    </div>

                    <div class="slide">
                        <div class="hero-content">
                            <h1>
                                Find a friend if they are free. Via:
                                <span style="font-weight:700;font-size:42px;color:#1a1a1a;">
                                    <span style="color:#0078d4;">Meet</span>Desk
                                </span>
                            </h1>
                            <p>Start Searching, share moments and work together easily!.</p>
                            <a href="#" class="download-btn">Start now</a>
                        </div>
                        <div class="hero-image">
                            <img src="/images/teams-hero2.png" alt="Slide 2">
                        </div>
                    </div>

                    <div class="slide">
                        <div class="hero-content">
                            <h1>
                                Organize your Daylly meetings via
                                <span style="font-weight:700;font-size:42px;color:#1a1a1a;">
                                    <span style="color:#0078d4;">Meet</span>Desk
                                </span>
                            </h1>
                            <p>Start Searching, share moments and work together easily!.</p>
                            <a href="#" class="download-btn">Try it free</a>
                        </div>
                        <div class="hero-image">
                            <img src="/images/teams-hero3.png" alt="Slide 3">
                        </div>
                    </div>

                    <div class="slide">
                        <div class="hero-content">
                            <h1>
                                Organize your Daylly meetings via
                                <span style="font-weight:700;font-size:42px;color:#1a1a1a;">
                                    <span style="color:#0078d4;">Meet</span>Desk
                                </span>
                            </h1>
                            <p>Start Searching, share moments and work together easily!.</p>
                            <a href="#" class="download-btn">Try it free</a>
                        </div>
                        <div class="hero-image">
                            <img src="/images/teams-hero4.png" alt="Slide 3">
                        </div>
                    </div>

                </div>

                <div class="slider-dots">
                    <span class="dot active" data-index="0"></span>
                    <span class="dot" data-index="1"></span>
                    <span class="dot" data-index="2"></span>
                    <span class="dot" data-index="3"></span>
                </div>`;
    }
}
