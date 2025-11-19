// ==UserScript==
// @name          Duo Security Generic Auto-Select
// @namespace     http://tampermonkey.net/
// @version       1.4
// @description   Generic function to automatically click any identity provider button (<h3> tag) on the Duo Central login screen by its text content, only once per session.
// @author        Gemini
// @match         https://olo.login.duosecurity.com/central/*
// @grant         none
// @run-at        document-idle
// ==/UserScript==

(function () {
    'use strict';

    // Key used to store a flag in sessionStorage to prevent clicking on subsequent loads in the same tab/session.
    const SKIP_FLAG_KEY = 'duo_skip_auto_click_session';

    // 1. CONFIGURATION: Add all the application names you want to auto-select here.
    const TARGET_APPLICATIONS = [
        'GitHub',
        'TeamCity (VPN Only)',
        'Sumo Logic - Olo',
        'Raygun',
        'Datadog',
        'Octopus'
    ];

    /**
 * Sets an item in localStorage with an expiration time.
 * * @param {string} key The key to store the data under.
 * @param {any} value The data value to store.
 * @param {number} seconds The number of seconds until the data expires.
 */
    function setLocalStorageItem(key, value, seconds) {
        const now = new Date();
        // Calculate the expiry timestamp by adding milliseconds (seconds * 1000)
        const expiryTimestamp = now.getTime() + (seconds * 1000);

        const item = {
            value: value,
            expiry: expiryTimestamp,
        };

        // Store the object (value + expiry) as a JSON string
        try {
            localStorage.setItem(key, JSON.stringify(item));
            console.log(`Data set. Expires at: ${new Date(expiryTimestamp).toLocaleTimeString()}`);
        } catch (error) {
            console.error(`Error setting data: ${error.message}`);
        }
    }

    /**
     * Retrieves an item from localStorage and automatically checks for expiration.
     * If expired, it removes the item and returns null.
     * * @param {string} key The key of the item to retrieve.
     * @returns {any|null} The stored value if valid, otherwise null.
     */
    function getLocalStorageItem(key) {
        const itemString = localStorage.getItem(key);

        // If the item doesn't exist, return null
        if (!itemString) {
            return null;
        }

        let item;
        try {
            // Parse the JSON string back into an object
            item = JSON.parse(itemString);
        } catch (error) {
            // Handle corrupted data
            console.error('Error parsing stored data. Removing item.');
            localStorage.removeItem(key);
            return null;
        }

        const now = new Date().getTime();

        // Check if the current time is past the stored expiry timestamp
        if (now > item.expiry) {
            // Item has expired, remove it
            localStorage.removeItem(key);
            console.log('Item expired and removed.');
            return null;
        }

        // Item is valid, return the actual stored value
        return item.value;
    }

    // --- Helper function to wait for elements to load (using MutationObserver) ---
    /**
     * Watches the DOM for the element defined by the XPath until it appears.
     * @param {string} xpath - The XPath expression to find the element.
     * @param {function} callback - Function to execute when the element is found.
     */
    function waitForElement(xpath, callback) {
        const observer = new MutationObserver((mutationsList, observer) => {
            const element = document.evaluate(xpath, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;

            if (element) {
                // Disconnect this specific observer
                observer.disconnect();

                // Execute the success callback
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

    // --- Generic Auto-Clicker Function ---
    /**
     * Creates a case-insensitive XPath and attempts to find and click the target <h3>.
     * @param {string} targetText - The inner text of the <h3> to click (e.g., "Datadog").
     * @returns {void}
     */
    function autoClickH3ByText(targetText) {
        const normalizedText = targetText.toLowerCase();

        // XPath with translate() for case-insensitive matching.
        const LINK_XPATH = `//h3[normalize-space(translate(., 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'))='${normalizedText}']`;

        // The MutationObserver logic handles the wait and click.
        waitForElement(LINK_XPATH, (linkElement) => {
            // Check the flag again before clicking, in case a previous watcher has already clicked and redirected.
            if (linkElement) {
                console.log(`[Duo Selector] Found target: ${targetText}. Clicking automatically.`);

                // Directly click the h3 element.
                linkElement.click();

                // Set the flag AFTER the click, ensuring we only set it on success.
                setLocalStorageItem(SKIP_FLAG_KEY, 'true', 8 * 60 * 60);
                console.log(`[Duo Selector] Session flag set.`);
            }
        });
    }


    // --- Main Execution Loop ---

    // Primary check: Skip the entire loop if the script has already executed successfully in this session.
    if (getLocalStorageItem(SKIP_FLAG_KEY)) {
        console.log(`[Duo Selector] Main execution loop skipped. Auto-click already performed in this session.`);
        return; // Exit script if flag is set
    }

    console.log(`[Duo Selector] Main execution started. Checking ${TARGET_APPLICATIONS.length} targets.`);

    // We iterate through the list, firing off asynchronous watchers for each target.
    // The first one found will click and redirect the page.
    for (let i = 0; i < TARGET_APPLICATIONS.length; i++) {
        setTimeout(() => {
            autoClickH3ByText(TARGET_APPLICATIONS[i]);
        }, i * 15 * 1000);
    }

})();
