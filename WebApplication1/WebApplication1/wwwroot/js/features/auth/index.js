
import { on } from '../../core/bus.js';
import { getState } from '../../core/store.js';
import { fetchJSON } from '../../core/api.js';
import { DynamicHtmlManager } from '../shared/dynamicHtml.js';

// removes overlay and restores scrolling
function closeAuthOverlay() {
    const overlay = document.getElementById('authOverlay');
    if (overlay) overlay.remove();

    if ('prevOverflow' in document.body.dataset) {
        document.body.style.overflow = document.body.dataset.prevOverflow;
        delete document.body.dataset.prevOverflow;
    } else {
        document.body.style.overflow = '';
    }

    // remove global key listener if any
    const onKey = document._auth_onKey;
    if (onKey) {
        document.removeEventListener('keydown', onKey);
        delete document._auth_onKey;
    }
}

// main: open overlay with desired form (login | register | forgot)
async function openAuthOverlay(form = 'login', lang = getState().lang) {
    // close existing first
    closeAuthOverlay();

    // try to fetch localized texts (MainMenuSection)
    let section = {};
    try {
        section = await fetchJSON('/api/MainMenuSection', { lang });
    } catch (e) {
        // fallback: warn and continue
        console.warn('Could not fetch MainMenuSection', e);
        section = {};
    }

    let html = '';
    if (form === 'login') html = DynamicHtmlManager.GetAuthHtmlFromModel(section, lang);
    else if (form === 'register') html = DynamicHtmlManager.GetRegisterHtml(lang);
    else if (form === 'forgot') html = DynamicHtmlManager.GetForgotPasswordHtml(lang);
    else html = DynamicHtmlManager.GetAuthHtmlFromModel(section, lang);

    const wrap = document.createElement('div');
    wrap.innerHTML = html;
    const node = wrap.firstElementChild || wrap;
    node.id = node.id || 'authOverlay';
    node.dataset.form = form;

    document.body.appendChild(node);
    document.body.dataset.prevOverflow = document.body.style.overflow || '';
    document.body.style.overflow = 'hidden';

    // close on X button
    node.querySelector('.close-btn')?.addEventListener('click', (e) => {
        e.preventDefault();
        closeAuthOverlay();
    });

    // clicking outside card closes
    node.addEventListener('click', (e) => {
        if (e.target === node) closeAuthOverlay();
    });

    // centralized node-level click handler for buttons/links inside overlay
    node.addEventListener('click', async (e) => {
        // show register (from login)
        if (e.target.closest('#showRegister')) {
            e.preventDefault();
            openAuthOverlay('register', lang);
            return;
        }

        // show forgot (from login)
        if (e.target.closest('#showForgot') || e.target.closest('.forgot-link') || e.target.closest('.forgot-password a')) {
            e.preventDefault();
            openAuthOverlay('forgot', lang);
            return;
        }

        // goBack (from register or forgot) -> back to login overlay
        if (e.target.closest('#goBack')) {
            e.preventDefault();
            openAuthOverlay('login', lang);
            return;
        }

        // submit register
        if (e.target.closest('#sendRegister')) {
            e.preventDefault();
            const card = node.querySelector('.auth-card') ?? node.querySelector('.auth-card') ?? node;
            const username = (card.querySelector('[data-field="username"]')?.value || '').trim();
            const lastName = (card.querySelector('[data-field="LastName"]')?.value || '').trim();
            const phone = (card.querySelector('[data-field="phone"]')?.value || '').trim();
            const password = (card.querySelector('[data-field="password"]')?.value || '');
            const confirm = (card.querySelector('[data-field="password2"]')?.value || '');

            // basic validation (you can enhance)
            if (!username || !phone || !password) {
                console.warn('Please fill required fields');
                return;
            }

            try {
                await fetchJSON('/api/auth/register', {
                    username,
                    lastName,
                    phoneNumber: phone,
                    password,
                    confirmPassword: confirm
                }, 'POST');
                // success: close overlay (or show success message)
                closeAuthOverlay();
            } catch (err) {
                console.error('Register failed', err);
                // TODO: show UI error
            }
            return;
        }

        // submit forgot
        if (e.target.closest('#sendForgot')) {
            e.preventDefault();
            const card = node.querySelector('.auth-card') ?? node;
            const phone = (card.querySelector('[data-field="phone"]')?.value || '').trim();
            if (!phone) return;
            try {
                await fetchJSON('/api/auth/forgot-password', { phoneNumber: phone }, 'POST');
                // optionally show a UI success state
                closeAuthOverlay();
            } catch (err) {
                console.error('Forgot password failed', err);
                // TODO: show UI error
            }
            return;
        }

        // optionally: sign-in button inside login (id=doSignIn)
        if (e.target.closest('#doSignIn')) {
            e.preventDefault();
            const card = node.querySelector('.auth-card') ?? node;
            const username = (card.querySelector('[data-field="username"]')?.value || '').trim();
            const password = (card.querySelector('[data-field="password"]')?.value || '');
            try {
                await fetchJSON('/api/auth/login', { username, password }, 'POST');
                closeAuthOverlay();
            } catch (err) {
                console.error('Login failed', err);
                // TODO: show UI error
            }
            return;
        }
    });

    // ESC key closes overlay
    const onKey = (e) => {
        if (e.key === 'Escape') {
            closeAuthOverlay();
            document.removeEventListener('keydown', onKey);
            delete document._auth_onKey;
        }
    };
    document._auth_onKey = onKey;
    document.addEventListener('keydown', onKey);
}

// open overlay when header sign-in clicked
document.addEventListener('click', (e) => {
    if (e.target.closest('.signin-btn')) {
        openAuthOverlay('login', getState().lang);
    }
});

// react to language change: if overlay open, re-open it in same form with new lang
on('languageChanged', ({ lang }) => {
    const overlay = document.getElementById('authOverlay');
    if (!overlay) return;
    const form = overlay.dataset.form || 'login';
    openAuthOverlay(form, lang);
});

// export so other modules can open/close
export { openAuthOverlay, closeAuthOverlay };