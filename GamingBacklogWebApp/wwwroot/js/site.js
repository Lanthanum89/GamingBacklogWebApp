// Gaming Backlog Web App - Interactive JavaScript Features
// Modern JavaScript enhancements for better user experience

// ====================================
// 🎮 GAMING BACKLOG INTERACTIVE FEATURES
// ====================================

// Wait for DOM to be fully loaded
document.addEventListener('DOMContentLoaded', function() {
    initializeGamingFeatures();
});

function initializeGamingFeatures() {
    setupLiveSearch();
    setupQuickFilters();
    setupConfirmationDialogs();
    setupProgressAnimations();
    setupKeyboardShortcuts();
    setupToastNotifications();
    console.log('🎮 Gaming Backlog features initialized!');
}

// ====================================
// 🔍 LIVE SEARCH FUNCTIONALITY
// ====================================
function setupLiveSearch() {
    const searchInput = document.getElementById('SearchTerm');
    if (!searchInput) return;

    let searchTimeout;
    
    searchInput.addEventListener('input', function() {
        clearTimeout(searchTimeout);
        const searchTerm = this.value.toLowerCase().trim();
        
        // Add loading state
        this.classList.add('searching');
        
        searchTimeout = setTimeout(() => {
            filterGamesRealTime(searchTerm);
            this.classList.remove('searching');
        }, 300); // Debounce for 300ms
    });
}

function filterGamesRealTime(searchTerm) {
    const gameCards = document.querySelectorAll('.game-card');
    let visibleCount = 0;
    
    gameCards.forEach(card => {
        const title = card.querySelector('.card-title')?.textContent.toLowerCase() || '';
        const description = card.querySelector('.card-text')?.textContent.toLowerCase() || '';
        const platform = card.querySelector('.badge')?.textContent.toLowerCase() || '';
        
        const matches = title.includes(searchTerm) || 
                       description.includes(searchTerm) || 
                       platform.includes(searchTerm);
        
        if (matches || searchTerm === '') {
            card.style.display = 'block';
            card.style.animation = 'fadeInUp 0.3s ease forwards';
            visibleCount++;
        } else {
            card.style.animation = 'fadeOut 0.2s ease forwards';
            setTimeout(() => {
                card.style.display = 'none';
            }, 200);
        }
    });
    
    updateResultsCounter(visibleCount, gameCards.length);
}

// ====================================
// ⚡ QUICK FILTERS WITH ANIMATIONS
// ====================================
function setupQuickFilters() {
    const filterSelects = document.querySelectorAll('#StatusFilter, #PlatformFilter');
    
    filterSelects.forEach(select => {
        select.addEventListener('change', function() {
            this.classList.add('filter-active');
            setTimeout(() => {
                this.classList.remove('filter-active');
            }, 300);
        });
    });
    
    // Add quick filter buttons
    addQuickFilterButtons();
}

function addQuickFilterButtons() {
    const filterCard = document.querySelector('.card-body form');
    if (!filterCard) return;
    
    const quickFiltersHtml = `
        <div class="col-12 mt-3">
            <div class="d-flex flex-wrap gap-2">
                <button type="button" class="btn btn-outline-primary btn-sm quick-filter" data-status="all">
                    <i class="fas fa-th"></i> All Games
                </button>
                <button type="button" class="btn btn-outline-info btn-sm quick-filter" data-status="Wishlist">
                    <i class="fas fa-heart"></i> Wishlist
                </button>
                <button type="button" class="btn btn-outline-warning btn-sm quick-filter" data-status="Playing">
                    <i class="fas fa-play"></i> Playing
                </button>
                <button type="button" class="btn btn-outline-success btn-sm quick-filter" data-status="Completed">
                    <i class="fas fa-trophy"></i> Completed
                </button>
            </div>
        </div>
    `;
    
    filterCard.insertAdjacentHTML('beforeend', quickFiltersHtml);
    
    // Add event listeners to quick filter buttons
    document.querySelectorAll('.quick-filter').forEach(button => {
        button.addEventListener('click', function() {
            const status = this.dataset.status;
            const statusSelect = document.getElementById('StatusFilter');
            
            if (statusSelect) {
                statusSelect.value = status === 'all' ? '' : status;
                statusSelect.dispatchEvent(new Event('change'));
                
                // Visual feedback
                document.querySelectorAll('.quick-filter').forEach(btn => btn.classList.remove('active'));
                this.classList.add('active');
                
                // Submit form
                statusSelect.closest('form').submit();
            }
        });
    });
}

// ====================================
// ✨ CONFIRMATION DIALOGS
// ====================================
function setupConfirmationDialogs() {
    document.addEventListener('click', function(e) {
        if (e.target.matches('a[href*="Delete"], button[formaction*="Delete"]')) {
            e.preventDefault();
            showDeleteConfirmation(e.target);
        }
    });
}

function showDeleteConfirmation(element) {
    const gameTitle = element.closest('.card')?.querySelector('.card-title')?.textContent || 'this game';
    
    const modal = createModal({
        title: '🗑️ Delete Game',
        message: `Are you sure you want to delete "${gameTitle}" from your backlog?`,
        confirmText: 'Delete Game',
        confirmClass: 'btn-danger',
        onConfirm: () => {
            // Proceed with deletion
            if (element.tagName === 'A') {
                window.location.href = element.href;
            } else {
                element.closest('form').submit();
            }
        }
    });
    
    modal.show();
}

// ====================================
// 🎯 PROGRESS ANIMATIONS
// ====================================
function setupProgressAnimations() {
    // Animate counters on the dashboard
    animateCounters();
    
    // Animate progress bars
    animateProgressBars();
}

function animateCounters() {
    const counters = document.querySelectorAll('.stat-number, .display-4');
    
    counters.forEach(counter => {
        const target = parseInt(counter.textContent) || 0;
        if (target === 0) return;
        
        const increment = target / 30; // 30 frames
        let current = 0;
        
        const updateCounter = () => {
            current += increment;
            if (current < target) {
                counter.textContent = Math.floor(current);
                requestAnimationFrame(updateCounter);
            } else {
                counter.textContent = target;
            }
        };
        
        // Start animation when element is visible
        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    updateCounter();
                    observer.unobserve(entry.target);
                }
            });
        });
        
        observer.observe(counter);
    });
}

function animateProgressBars() {
    const progressBars = document.querySelectorAll('.progress-bar');
    
    progressBars.forEach(bar => {
        const width = bar.style.width || bar.getAttribute('aria-valuenow') + '%';
        bar.style.width = '0%';
        
        setTimeout(() => {
            bar.style.transition = 'width 1s ease-in-out';
            bar.style.width = width;
        }, 100);
    });
}

// ====================================
// ⌨️ KEYBOARD SHORTCUTS
// ====================================
function setupKeyboardShortcuts() {
    document.addEventListener('keydown', function(e) {
        // Alt + A: Add new game
        if (e.altKey && e.key === 'a') {
            e.preventDefault();
            const addButton = document.querySelector('a[href*="AddGame"]');
            if (addButton) addButton.click();
        }
        
        // Alt + S: Focus search
        if (e.altKey && e.key === 's') {
            e.preventDefault();
            const searchInput = document.getElementById('SearchTerm');
            if (searchInput) {
                searchInput.focus();
                searchInput.select();
            }
        }
        
        // Escape: Clear search
        if (e.key === 'Escape') {
            const searchInput = document.getElementById('SearchTerm');
            if (searchInput && searchInput === document.activeElement) {
                searchInput.value = '';
                searchInput.dispatchEvent(new Event('input'));
            }
        }
    });
    
    // Add keyboard shortcut hints
    addKeyboardHints();
}

function addKeyboardHints() {
    const hints = document.createElement('div');
    hints.className = 'keyboard-hints';
    hints.innerHTML = `
        <small class="text-muted">
            <i class="fas fa-keyboard"></i> 
            <strong>Alt+A</strong> Add Game | 
            <strong>Alt+S</strong> Search | 
            <strong>Esc</strong> Clear
        </small>
    `;
    
    const container = document.querySelector('.container-fluid');
    if (container) {
        container.appendChild(hints);
    }
}

// ====================================
// 🍞 TOAST NOTIFICATIONS
// ====================================
function setupToastNotifications() {
    // Create toast container
    const toastContainer = document.createElement('div');
    toastContainer.className = 'toast-container position-fixed top-0 end-0 p-3';
    toastContainer.style.zIndex = '9999';
    document.body.appendChild(toastContainer);
    
    // Show welcome toast
    setTimeout(() => {
        showToast('🎮 Welcome to your Gaming Backlog!', 'success');
    }, 1000);
}

function showToast(message, type = 'info', duration = 3000) {
    const toastContainer = document.querySelector('.toast-container');
    if (!toastContainer) return;
    
    const toastId = 'toast-' + Date.now();
    const iconMap = {
        success: 'fas fa-check-circle',
        error: 'fas fa-exclamation-circle',
        warning: 'fas fa-exclamation-triangle',
        info: 'fas fa-info-circle'
    };
    
    const toast = document.createElement('div');
    toast.id = toastId;
    toast.className = `toast align-items-center text-white bg-${type === 'error' ? 'danger' : type} border-0`;
    toast.setAttribute('role', 'alert');
    
    toast.innerHTML = `
        <div class="d-flex">
            <div class="toast-body">
                <i class="${iconMap[type] || iconMap.info}"></i>
                ${message}
            </div>
            <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
        </div>
    `;
    
    toastContainer.appendChild(toast);
    
    // Initialize and show toast
    const bsToast = new bootstrap.Toast(toast, { delay: duration });
    bsToast.show();
    
    // Remove from DOM after hiding
    toast.addEventListener('hidden.bs.toast', () => {
        toast.remove();
    });
}

// ====================================
// 🎨 UTILITY FUNCTIONS
// ====================================
function updateResultsCounter(visible, total) {
    let counter = document.querySelector('.results-counter');
    
    if (!counter) {
        counter = document.createElement('div');
        counter.className = 'results-counter alert alert-info mt-3';
        const gamesList = document.querySelector('.row .col-12');
        if (gamesList) {
            gamesList.appendChild(counter);
        }
    }
    
    counter.innerHTML = `
        <i class="fas fa-search"></i>
        Showing <strong>${visible}</strong> of <strong>${total}</strong> games
    `;
    
    counter.style.display = visible === total ? 'none' : 'block';
}

function createModal({ title, message, confirmText, confirmClass, onConfirm }) {
    const modalId = 'modal-' + Date.now();
    
    const modalHtml = `
        <div class="modal fade" id="${modalId}" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">${title}</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body">
                        ${message}
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        <button type="button" class="btn ${confirmClass}" id="${modalId}-confirm">${confirmText}</button>
                    </div>
                </div>
            </div>
        </div>
    `;
    
    document.body.insertAdjacentHTML('beforeend', modalHtml);
    
    const modalElement = document.getElementById(modalId);
    const modal = new bootstrap.Modal(modalElement);
    
    // Add confirm button event
    document.getElementById(`${modalId}-confirm`).addEventListener('click', () => {
        modal.hide();
        onConfirm();
    });
    
    // Clean up after modal is hidden
    modalElement.addEventListener('hidden.bs.modal', () => {
        modalElement.remove();
    });
    
    return modal;
}

// ====================================
// 🎨 CSS ANIMATIONS (Add to site.css)
// ====================================
const additionalStyles = `
    .searching {
        background-image: linear-gradient(45deg, transparent 40%, rgba(255,255,255,0.3) 50%, transparent 60%);
        background-size: 200% 100%;
        animation: shimmer 1s infinite;
    }
    
    .filter-active {
        transform: scale(1.05);
        transition: transform 0.2s ease;
    }
    
    .quick-filter.active {
        background-color: var(--cozy-pink) !important;
        color: white !important;
        border-color: var(--cozy-pink) !important;
    }
    
    .keyboard-hints {
        position: fixed;
        bottom: 20px;
        right: 20px;
        background: rgba(255, 255, 255, 0.9);
        padding: 10px 15px;
        border-radius: 10px;
        backdrop-filter: blur(10px);
        box-shadow: 0 4px 20px rgba(0,0,0,0.1);
    }
    
    @keyframes shimmer {
        0% { background-position: 200% 0; }
        100% { background-position: -200% 0; }
    }
    
    @keyframes fadeInUp {
        from {
            opacity: 0;
            transform: translateY(20px);
        }
        to {
            opacity: 1;
            transform: translateY(0);
        }
    }
    
    @keyframes fadeOut {
        from {
            opacity: 1;
            transform: scale(1);
        }
        to {
            opacity: 0;
            transform: scale(0.9);
        }
    }
`;

// Add styles to page
const styleSheet = document.createElement('style');
styleSheet.textContent = additionalStyles;
document.head.appendChild(styleSheet);
