import { useState, useEffect, useCallback } from 'react';

const initialize = () =>
  new Promise(resolve => {
    if (typeof AppleID !== 'undefined') {
      resolve();
    } else {
      // eslint-disable-next-line func-names
      window.appleAsyncInit = function() {
        // eslint-disable-next-line no-undef
        AppleID.auth.init({
          clientId: 'vn.com.netlove.app',
          scope: 'name email',
          redirectURI: 'localhost',
          state: '[STATE]',
          nonce: '[NONCE]',
          usePopup: true,
        });
        resolve();
      };

      // eslint-disable-next-line func-names
      (function(d, s, id) {
        let js;
        const fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) {
          return;
        }
        // eslint-disable-next-line prefer-const
        js = d.createElement(s);
        js.id = id;
        js.src = 'https://appleid.cdn-apple.com/appleauth/static/jsapi/appleid/1/en_US/appleid.auth.js';
        js.onload = window.appleAsyncInit;
        fjs.parentNode.insertBefore(js, fjs);
      })(document, 'script', 'appleid-signin');
    }
  });

const useApple = () => {
  const [apple, setApple] = useState([]);
  const [isReady, setReady] = useState(false);

  const initApple = useCallback(async () => {
    await initialize();
    // eslint-disable-next-line no-undef
    if (typeof AppleID !== 'undefined') {
      // eslint-disable-next-line no-undef
      setApple(AppleID);
      setReady(true);
    }
  });

  useEffect(() => {
    initApple();
  }, [initApple]);

  return [apple, isReady];
};

export default useApple;
