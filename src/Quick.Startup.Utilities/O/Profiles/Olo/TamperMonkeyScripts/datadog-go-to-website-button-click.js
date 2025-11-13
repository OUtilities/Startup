// ==UserScript==
// @name          Datadog Mobile Login Bypass
// @namespace     http://tampermonkey.net/
// @version       1.0
// @description   Automatically clicks the 'Use the Datadog Website' link to switch from the mobile login view.
// @author        Gemini
// @match         https://app.datadoghq.com/account/login/mobile*
// @grant         none
// @run-at        document-idle
// ==/UserScript==

(function () {
    'use strict';

    // The specific text of the link element we need to click
    const TARGET_LINK_TEXT = 'Use the Datadog Website';

    console.log(`[Datadog Bypass] Script injected. Waiting for link with text: "${TARGET_LINK_TEXT}"`);

    // --- Helper function to wait for elements to load ---
    // Use a MutationObserver to efficiently wait for the link element
    function waitForElement(xpath, callback) {

        // This targets the entire document body for changes
        const observer = new MutationObserver((mutationsList, observer) => {
            // Use XPath to find the element based on the text content
            const element = document.evaluate(xpath, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;

            if (element) {
                observer.disconnect();
                callback(element);
            }
        });

        // Start observing the entire document body for element changes
        observer.observe(document.body, { childList: true, subtree: true });

        // Try once immediately as a fallback
        const initialElement = document.evaluate(xpath, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
        if (initialElement) {
            observer.disconnect();
            callback(initialElement);
        }
    }

    // --- Main execution logic ---

    // Use XPath to reliably find the <a> tag that contains the exact text.
    // normalize-space() helps ignore extra whitespace in the element's text content.
    const LINK_XPATH = `//a[normalize-space(text())='${TARGET_LINK_TEXT}']`;

    waitForElement(LINK_XPATH, (linkElement) => {
        if (linkElement) {
            console.log(`[Datadog Bypass] Found link. Clicking automatically.`);

            // Perform the click action
            linkElement.click();

        } else {
            console.error(`[Datadog Bypass] Link element not found for text: ${TARGET_LINK_TEXT}.`);
        }
    });
})();