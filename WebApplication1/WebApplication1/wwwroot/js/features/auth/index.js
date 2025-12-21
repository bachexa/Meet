import { on } from '../../core/bus.js';
import { getState } from '../../core/store.js';
import { fetchJSON } from '../../core/api.js';
import { DynamicHtmlManager } from '../shared/dynamicHtml.js';

function closeAuthOverlay() {
    const overlay = document.getElementById('authOverlay');
    if (overlay) overlay.remove();

    document.body.style.overflow = '';
    const onKey = document._auth_onKey;
    if (onKey) {
        document.removeEventListener('keydown', onKey);
        delete document._auth_onKey;
    }
}

async function openAuthOverlay(form = 'login', lang = getState().lang) {
    closeAuthOverlay();

    let section = {};
    let registerText = {};

    try {
        section = await fetchJSON('/api/MainMenuSection', { lang });
        registerText = await fetchJSON('/api/RegisterText', { lang });
    } catch (e) {
        console.warn('Auth texts fetch failed', e);
    }

    let html = '';
    if (form === 'login') {
        html = DynamicHtmlManager.GetAuthHtmlFromModel(section);
    }
    else if (form === 'register') {
        html = DynamicHtmlManager.GetRegisterHtmlFromModel(registerText);
    }
    else if (form === 'forgot') {
        html = DynamicHtmlManager.GetForgotPasswordHtml(lang);
    }

    const wrap = document.createElement('div');
    wrap.innerHTML = html;
    const node = wrap.firstElementChild;
    node.dataset.form = form;

    document.body.appendChild(node);
    document.body.style.overflow = 'hidden';

    node.querySelector('.close-btn')?.addEventListener('click', closeAuthOverlay);

    node.addEventListener('click', async (e) => {

        if (e.target.closest('#showRegister')) {
            e.preventDefault();
            openAuthOverlay('register', lang);
            return;
        }

        if (e.target.closest('#showForgot')) {
            e.preventDefault();
            openAuthOverlay('forgot', lang);
            return;
        }

        if (e.target.closest('#goBack')) {
            e.preventDefault();
            openAuthOverlay('login', lang);
            return;
        }

        // REGISTER
        if (e.target.closest('#sendRegister')) {
            e.preventDefault();
            const card = node.querySelector('.auth-card');

            const firstName = card.querySelector('[data-field="firstName"]')?.value.trim();
            const lastName = card.querySelector('[data-field="lastName"]')?.value.trim();
            const phone = card.querySelector('[data-field="phone"]')?.value.trim();
            const password = card.querySelector('[data-field="password"]')?.value;
            const confirmPassword = card.querySelector('[data-field="confirmPassword"]')?.value;

            if (!firstName || !phone || !password) {
                alert('Fill required fields');
                return;
            }

            await fetchJSON('/api/auth/register', {
                firstName,
                lastName,
                phoneNumber: phone,
                password,
                confirmPassword
            }, 'POST');

            closeAuthOverlay();
        }

        // LOGIN
        if (e.target.closest('#doSignIn')) {
            e.preventDefault();
            const card = node.querySelector('.auth-card');
            const username = card.querySelector('[data-field="username"]')?.value.trim();
            const password = card.querySelector('[data-field="password"]')?.value;

            await fetchJSON('/api/auth/login', { username, password }, 'POST');
            closeAuthOverlay();
        }

        // FORGOT
        if (e.target.closest('#sendForgot')) {
            e.preventDefault();
            const phone = node.querySelector('[data-field="phone"]')?.value.trim();
            await fetchJSON('/api/auth/forgot-password', { phoneNumber: phone }, 'POST');
            closeAuthOverlay();
        }
    });

    const onKey = (e) => e.key === 'Escape' && closeAuthOverlay();
    document._auth_onKey = onKey;
    document.addEventListener('keydown', onKey);
}

document.addEventListener('click', (e) => {
    if (e.target.closest('.signin-btn')) {
        openAuthOverlay('login', getState().lang);
    }
});

on('languageChanged', ({ lang }) => {
    const overlay = document.getElementById('authOverlay');
    if (!overlay) return;
    openAuthOverlay(overlay.dataset.form, lang);
});

export { openAuthOverlay, closeAuthOverlay };