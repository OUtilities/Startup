// ==UserScript==
// @name          Octopus Google Sign-in Auto-Clicker
// @namespace     http://tampermonkey.net/
// @version       1.0
// @description   Automatically clicks the "Sign in with Google" span button on the Octopus sign-in page.
// @author        Gemini
// @match         https://octopus.olobuild.net/*
// @grant         none
// @run-at        document-idle
// ==/UserScript==

(function () {
    'use strict';

    // 1. CONFIGURATION
    const TARGET_LINK_TEXT = 'Sign in with Google';
    // We check the window hash for this path, as it's a Single Page Application (SPA)
    const TARGET_URL_HASH = '#/users/sign-in';

    console.log(`[Octopus Auto-Clicker] Script injected. Waiting for target URL and link.`);

    // --- Helper function to wait for elements to load ---
    // Use a MutationObserver to efficiently wait for the element based on XPath
    function waitForElement(xpath, callback) {

        const observer = new MutationObserver((mutationsList, observer) => {
            // 1. Check if the current URL hash matches the target path before searching the DOM
            if (!window.location.hash.includes(TARGET_URL_HASH)) {
                return;
            }

            // 2. Use XPath to find the element based on the text content
            const element = document.evaluate(xpath, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;

            if (element) {
                observer.disconnect();
                callback(element);
            }
        });

        // Start observing the entire document body for element changes
        observer.observe(document.body, { childList: true, subtree: true });

        // Try once immediately as a fallback, also checking the hash
        if (window.location.hash.includes(TARGET_URL_HASH)) {
            const initialElement = document.evaluate(xpath, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
            if (initialElement) {
                observer.disconnect();
                callback(initialElement);
            }
        }
    }

    // --- Main execution logic ---

    // Use XPath to reliably find the <span> tag that contains the exact text.
    const LINK_XPATH = `//span[normalize-space(text())='${TARGET_LINK_TEXT}']`;

    waitForElement(LINK_XPATH, (linkElement) => {
        if (linkElement) {
            console.log(`[Octopus Auto-Clicker] Found button. Clicking automatically.`);

            // Perform the click action
            linkElement.click();

        } else {
            console.error(`[Octopus Auto-Clicker] Target element not found for text: ${TARGET_LINK_TEXT}.`);
        }
    });
})();