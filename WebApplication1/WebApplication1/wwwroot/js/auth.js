document.addEventListener('DOMContentLoaded', () => {
    const signInButton = document.querySelector('.signin-btn');

    if (signInButton) {
        signInButton.addEventListener('click', () => {
            // თუ უკვე არსებობს authOverlay — წაშალე
            const existingOverlay = document.getElementById('authOverlay');
            if (existingOverlay) {
                existingOverlay.remove();
            }

            // HTML ბლოკი
            const overlayHtml = `
                <div class="auth-overlay" id="authOverlay">
                    <div class="auth-card" id="authCard">
                        <button class="close-btn" type="button" aria-label="Close">&times;</button>

                        <!-- Login Form -->
                        <div class="auth-section login-section">
                            <input type="text" placeholder="Username or Email" class="auth-input" />
                            <input type="password" placeholder="Password" class="auth-input" />
                            <button class="auth-action-btn" type="button">Sign in</button>
                            <div class="auth-separator"><span>or</span></div>
                            <button class="auth-btn google" type="button">
                                <img src="/images/google-icon.png" alt="Google" />
                                Sign in with Google
                            </button>
                            <button class="auth-btn microsoft" type="button">
                                <img src="/images/microsoft-icon.png" alt="Microsoft" />
                                Sign in with Microsoft
                            </button>
                            <p class="auth-link">Don't have an account? <a href="#" id="showRegister">Register</a></p>
                        </div>
                    </div>
                </div>
            `;

            // დინამიური DOM ელემენტის შექმნა
            const div = document.createElement('div');
            div.innerHTML = overlayHtml;
            document.body.appendChild(div); // ვამატებთ მთელ გვერდზე

            // გათიშვის ღილაკი
            const closeBtn = div.querySelector('.close-btn');
            if (closeBtn) {
                closeBtn.addEventListener('click', () => {
                    div.remove();
                });
            }
        });
    }
});
