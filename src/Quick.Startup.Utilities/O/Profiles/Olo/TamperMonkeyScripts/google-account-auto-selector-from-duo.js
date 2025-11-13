// ==UserScript==
// @name          Google Account Auto-Selector (Duo bypass)
// @namespace     http://tampermonkey.net/
// @version       1.0
// @description   Automatically clicks the specified Google account when redirected from the Duo login page.
// @author        Gemini
// @match         *://accounts.google.com/*
// @grant         none
// @run-at        document-idle
// ==/UserScript==

(function () {
    'use strict';
    // 1. CONFIGURATION
    const TARGET_ACCOUNT = 'oleg.lazarovych@olo.com';
    const DUO_REFERRER_URL = 'sso.duosecurity.com';

    // *** NEW: The selector now targets the data-identifier attribute ***
    const TARGET_SELECTOR = `div[data-identifier="${TARGET_ACCOUNT}"]`;
    // ***************************************************************

    // --- Helper function to wait for elements to load ---
    function waitForElement(selector, callback) {
        // Use a MutationObserver for efficient waiting on dynamically loaded content
        const observer = new MutationObserver((mutationsList, observer) => {
            const element = document.querySelector(selector);
            if (element) {
                observer.disconnect();
                callback(element);
            }
        });

        // Start observing the entire document body for configuration changes
        observer.observe(document.body, { childList: true, subtree: true });

        // As a fallback, try once immediately
        const initialElement = document.querySelector(selector);
        if (initialElement) {
            observer.disconnect();
            callback(initialElement);
        }
    }

    // --- Main execution logic ---

    // ⛔️ REFERRER CHECK BYPASSED FOR TESTING THE SCRIPT INJECTION ⛔️
    console.log(`[Google Auto-Selector] Referrer check BYPASSED. Proceeding to find account.`);
    // -------------------------------------------------------------

    // Check for the account element using the new data-identifier selector
    waitForElement(TARGET_SELECTOR, (accountElement) => {

        if (accountElement) {
            console.log(`[Google Auto-Selector] Found target account: ${TARGET_ACCOUNT}. Clicking now.`);

            // Attempt to click the element.
            accountElement.click();

        } else {
            console.error(`[Google Auto-Selector] Target account element not found using selector: ${TARGET_SELECTOR}`);
        }
    });
})();