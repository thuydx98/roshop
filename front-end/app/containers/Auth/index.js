import React from 'react';
import { useLocation, Route, Switch } from 'react-router-dom';
import { useInjectSaga } from 'utils/injectSaga';
import { useInjectReducer } from 'utils/injectReducer';
import { ACTION_STATUS } from 'utils/constants';
import Login from 'containers/Login/Loadable';
import Register from 'containers/Register/Loadable';
import get from 'lodash/fp/get';
import { ProviderType } from './constants';
import { sliceKey, reducer } from './slice';
import saga from './saga';
import useHooks from './hooks/hooks';
import useFacebook from './hooks/facebook';
import useGoogle from './hooks/google';
import useApple from './hooks/apple';
import './styles/styles.css';

export default function Auth() {
  useInjectSaga({ key: sliceKey, saga });
  useInjectReducer({ key: sliceKey, reducer });

  const location = useLocation();
  const [FBInstance, isFacebookReady] = useFacebook();
  const [GoogleInstance, isGoogleReady] = useGoogle();
  const [AppleInstance, isAppleReady] = useApple();
  const { states, handlers } = useHooks();
  const { socialLoginState, selectedProvider } = states;
  const { onSocialLogin } = handlers;

  const loginWithFacebook = response => {
    if (response.status === 'connected') {
      onSocialLogin(ProviderType.FACEBOOK, response.authResponse.accessToken);
    } else {
      FBInstance.login(res => {
        if (res.status === 'connected') {
          onSocialLogin(ProviderType.FACEBOOK, res.authResponse.accessToken);
        }
      });
    }
  };

  const loginWithGoogle = response => {
    const authResponse = response.getAuthResponse(true);
    const token = get('access_token', authResponse);
    if (token) {
      onSocialLogin(ProviderType.GOOGLE, token);
    }
  };

  const loginWithApple = response => {
    const token = get('authorization.code', response);
    if (token) {
      onSocialLogin(ProviderType.APPLE, token);
    }
  };

  return (
    <div className="container-fluid mx-auto">
      <div className="card card0 border-0 px-1 px-md-5 px-lg-1 px-xl-5 py-4">
        <div className="row d-flex">
          <div className="col-lg-6">
            <div className="card1 pt-5">
              <div className="row px-3 justify-content-center mt-4 mb-5 border-line">
                <img src="https://i.imgur.com/uNGdWHi.png" className="image" />
              </div>
            </div>
          </div>
          <div className="col-lg-6">
            <div className="card2 card border-0 px-4 py-5">
              <div className="row mb-4 px-3">
                <h6 className="mb-0 mr-4 mt-2">{location.pathname === '/sign-in' ? 'Sign in' : 'Sign up'} with</h6>
                {isFacebookReady && (
                  <button
                    type="button"
                    className="facebook text-center mr-3"
                    onClick={() => FBInstance.getLoginStatus(res => loginWithFacebook(res))}
                  >
                    {socialLoginState === ACTION_STATUS.PENDING && selectedProvider === ProviderType.FACEBOOK ? (
                      <div className="spinner-border spinner-border-sm text-light mb-1" role="status">
                        <span className="sr-only">Loading...</span>
                      </div>
                    ) : (
                      <div className="fab fa-facebook-f" />
                    )}
                  </button>
                )}
                {isGoogleReady && (
                  <button
                    type="button"
                    className="google text-center mr-3"
                    onClick={() => GoogleInstance.signIn().then(res => loginWithGoogle(res))}
                  >
                    {socialLoginState === ACTION_STATUS.PENDING && selectedProvider === ProviderType.GOOGLE ? (
                      <div className="spinner-border spinner-border-sm text-light mb-1" role="status">
                        <span className="sr-only">Loading...</span>
                      </div>
                    ) : (
                      <div className="fab fa-google" />
                    )}
                  </button>
                )}
                {isAppleReady && (
                  <button
                    type="button"
                    className="apple text-center mr-3"
                    onClick={() => AppleInstance.auth.signIn().then(res => loginWithApple(res))}
                  >
                    {socialLoginState === ACTION_STATUS.PENDING && selectedProvider === ProviderType.APPLE ? (
                      <div className="spinner-border spinner-border-sm text-light mb-1" role="status">
                        <span className="sr-only">Loading...</span>
                      </div>
                    ) : (
                      <div className="fab fa-apple" />
                    )}
                  </button>
                )}
              </div>
              <div className="row px-3 mb-4">
                <div className="line" />
                <small className="or text-center">Or</small>
                <div className="line" />
              </div>

              <Switch>
                <Route exact path="/sign-in" component={Login} />
                <Route exact path="/sign-up" component={Register} />
              </Switch>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
