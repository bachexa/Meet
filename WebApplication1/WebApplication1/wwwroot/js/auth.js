document.addEventListener('DOMContentLoaded', () => {
    const signInButton = document.querySelector('.signin-btn');
    const container = document.getElementById('target');

    if (signInButton && container) {
        signInButton.addEventListener('click', () => {
            // თუ უკვე არსებობს authCard — წაშალე
            const existingCard = document.getElementById('authCard');
            if (existingCard) {
                existingCard.remove();
            }

            // HTML ბლოკი
            const row = `
                <div class="auth-card" id="authCard">
                    <button class="close-btn" type="button" aria-label="Close">&times;</button>

                    <!-- Login Form -->
                    <div class="auth-section login-section">
                        <input type="text" placeholder="Username or Email" class="auth-input" />
                        <input type="password" placeholder="Password" class="auth-input" />
                        <button class="auth-action-btn" type="button">Sign in</button>
        
                        <div class="auth-separator">or</div>

                        <button class="auth-btn google" type="button">
                            <img src="/images/google-icon.png" alt="Google" width="20" height="20" />
                            Sign in with Google
                        </button>

                        <button class="auth-btn microsoft" type="button">
                            <img src="/images/microsoft-icon.png" alt="Microsoft"  width="20" height="20" />
                            Sign in with Microsoft
                        </button>

                        <p class="auth-link">Don't have an account? <a href="#" id="showRegister">Register</a></p>
                    </div>

                    <!-- Register Form -->
                    <div class="auth-section register-section" style="display: none;">
                        <input type="text" placeholder="Username" class="auth-input" />
                        <input type="email" placeholder="Email" class="auth-input" />
                        <input type="password" placeholder="Password" class="auth-input" />
                        <input type="password" placeholder="Confirm Password" class="auth-input" />
                        <button class="auth-action-btn" type="button">Register</button>

                        <p class="auth-link">Already have an account? <a href="#" id="showLogin">Back to Login</a></p>
                    </div>
                </div>
                `;


            const div = document.createElement('div');
            div.innerHTML = row;
            container.append(div);

            const closeBtn = div.querySelector('.close-btn');
            const authCard = div.querySelector('.auth-card');

            if (closeBtn && authCard) {
                closeBtn.addEventListener('click', () => {
                    authCard.remove();
                });
            }
        });
    }
});
