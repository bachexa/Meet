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
}

// opens overlay with desired form (login | register | forgot)
async function openAuthOverlay(form = 'login', lang = getState().lang) {
    closeAuthOverlay();

    // try to fetch localized texts (MainMenuSection)
    let section = {};
    try {
        section = await fetchJSON('/api/MainMenuSection', { lang });
    } catch (e) {
        // fallback to empty section if API unavailable
        console.warn('Could not fetch MainMenuSection', e);
        section = {};
    }

    let html = '';
    if (form === 'login') html = DynamicHtmlManager.GetAuthHtmlFromModel(section);
    else if (form === 'register') html = DynamicHtmlManager.GetRegisterHtml(lang);
    else if (form === 'forgot') html = DynamicHtmlManager.GetForgotPasswordHtml(lang);

    const wrap = document.createElement('div');
    wrap.innerHTML = html;
    const node = wrap.firstElementChild || wrap;
    node.dataset.form = form;

    document.body.appendChild(node);
    document.body.dataset.prevOverflow = document.body.style.overflow || '';
    document.body.style.overflow = 'hidden';

    // close on X
    node.querySelector('.close-btn')?.addEventListener('click', closeAuthOverlay);

    // clicking outside card closes
    node.addEventListener('click', (e) => {
        if (e.target === node) closeAuthOverlay();
    });

    // back link inside register/forgot
    node.querySelector('#goBack')?.addEventListener('click', (e) => {
        e.preventDefault();
        openAuthOverlay('login', lang);
    });

    // showRegister link (from login -> open register)
    node.querySelector('#showRegister')?.addEventListener('click', (e) => {
        e.preventDefault();
        openAuthOverlay('register', lang);
    });

    // forgot link (from login)
    node.querySelector('.forgot-link')?.addEventListener('click', (e) => {
        e.preventDefault();
        openAuthOverlay('forgot', lang);
    });

    // ENTER / ESC handling
    const onKey = (e) => {
        if (e.key === 'Escape') {
            closeAuthOverlay();
            document.removeEventListener('keydown', onKey);
        }
    };
    document.addEventListener('keydown', onKey);
}

// global: open when header sign-in clicked
document.addEventListener('click', (e) => {
    if (e.target.closest('.signin-btn')) {
        openAuthOverlay('login', getState().lang);
    }
});

// register submit
document.addEventListener('click', async (e) => {
    if (!e.target.closest('#sendRegister')) return;
    const card = e.target.closest('.auth-card');
    if (!card) return;
    const data = {
        username: card.querySelector('[data-field="username"]').value || '',
        lastName: card.querySelector('[data-field="LastName"]').value || '',
        phoneNumber: card.querySelector('[data-field="phone"]').value || '',
        password: card.querySelector('[data-field="password"]').value || '',
        confirmPassword: card.querySelector('[data-field="password2"]').value || ''
    };
    try {
        await fetchJSON('/api/auth/register', data, 'POST');
        // optionally show success/toast
        closeAuthOverlay();
    } catch (err) {
        console.error('Register failed', err);
        // show error UI if you want
    }
});

// forgot password submit
document.addEventListener('click', async (e) => {
    if (!e.target.closest('#sendForgot')) return;
    const card = e.target.closest('.auth-card');
    if (!card) return;
    const phone = card.querySelector('[data-field="phone"]').value || '';
    try {
        await fetchJSON('/api/auth/forgot-password', { phoneNumber: phone }, 'POST');
        closeAuthOverlay();
    } catch (err) {
        console.error('Forgot password failed', err);
    }
});

// react to language change: if overlay open, re-open it in same form with new lang
on('languageChanged', ({ lang }) => {
    const overlay = document.getElementById('authOverlay');
    if (!overlay) return;
    const form = overlay.dataset.form || 'login';
    openAuthOverlay(form, lang);
});

// export if other modules want to open/close directly
export { openAuthOverlay, closeAuthOverlay };