/* istanbul ignore file */
import { useState, useEffect, useCallback } from 'react';

const initAuth = () =>
  // eslint-disable-next-line no-unused-vars
  new Promise((resolve, _reject) => {
    // eslint-disable-next-line no-undef
    gapi.load('auth2', () => {
      // eslint-disable-next-line no-undef
      const auth = gapi.auth2.init({
        client_id: '724743549600-mt82homvcl2vutgt330fblndapmams99.apps.googleusercontent.com',
      });
      resolve(auth);
    });
  });

const initializeGoogle = () =>
  // eslint-disable-next-line no-unused-vars
  new Promise((resolve, _reject) => {
    if (typeof gapi !== 'undefined') {
      window.googleAsyncInit().then(auth => {
        resolve(auth);
      });
    } else {
      // eslint-disable-next-line func-names
      window.googleAsyncInit = async function() {
        const auth = await initAuth();
        resolve(auth);
        return auth;
      };

      // eslint-disable-next-line func-names
      (function(d, s, id) {
        let js;
        const gjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) {
          return;
        }
        // eslint-disable-next-line prefer-const
        js = d.createElement(s);
        js.id = id;
        js.src = 'https://apis.google.com/js/platform.js';
        js.onload = window.googleAsyncInit;
        gjs.parentNode.insertBefore(js, gjs);
      })(document, 'script', 'google_api');
    }
  });

const useGoogle = () => {
  const [google, setGoogle] = useState([]);
  const [isReady, setReady] = useState(false);

  const initGoogle = useCallback(async () => {
    const auth = await initializeGoogle();
    if (auth !== 'undefined') {
      setGoogle(auth);
      setReady(true);
    }
  });

  useEffect(() => {
    initGoogle();
  }, [initGoogle]);

  return [google, isReady];
};

export default useGoogle;