import React from 'react';
import { Link } from 'react-router-dom';
import { useInjectSaga } from 'utils/injectSaga';
import { useInjectReducer } from 'utils/injectReducer';
import { ACTION_STATUS } from 'utils/constants';
import saga from './saga';
import { sliceKey, reducer } from './slice';
import useHooks from './hooks';
import useFacebook from './hooks/facebook';
import './styles.css';

export default function Login() {
  useInjectSaga({ key: sliceKey, saga });
  useInjectReducer({ key: sliceKey, reducer });

  const [FBInstance, isReady] = useFacebook();
  const { states, handlers } = useHooks();
  const { payload, loginState, loginError } = states;
  const { setPayload, onSubmit } = handlers;

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
                <h6 className="mb-0 mr-4 mt-2">Sign in with</h6>
                <button
                  type="button"
                  className="facebook text-center mr-3"
                  onClick={() => FBInstance.getLoginStatus(res => console.log(res))}
                >
                  <div className="fab fa-facebook-f" />
                </button>
                <div className="twitter text-center mr-3">
                  <div className="fab fa-google" />
                </div>
                <div className="apple text-center mr-3">
                  <div className="fab fa-apple" />
                </div>
              </div>
              <div className="row px-3 mb-4">
                <div className="line" />
                <small className="or text-center">Or</small>
                <div className="line" />
              </div>
              <div className="row px-3">
                <label className="mb-1">
                  <h6 className="mb-0 text-sm">Email Address</h6>
                </label>
                <input
                  className="mb-4"
                  type="email"
                  name="email"
                  placeholder="Enter a valid email address"
                  value={payload.email}
                  onChange={e => setPayload({ ...payload, email: e.target.value })}
                />
              </div>
              <div className="row px-3">
                <label className="mb-1">
                  <h6 className="mb-0 text-sm">Password</h6>
                </label>
                <input
                  type="password"
                  name="password"
                  placeholder="Enter password"
                  value={payload.password}
                  onChange={e => setPayload({ ...payload, password: e.target.value })}
                />
              </div>
              <div className="row px-3 mb-4">
                <label className="text-danger text-sm">{loginError && 'Username or password is invalid'}</label>
                <Link to="/" className="ml-auto mb-0 text-sm">
                  Forgot Password?
                </Link>
              </div>
              <div className="row mb-4 px-3">
                <button
                  type="submit"
                  className="btn btn-blue text-center"
                  disabled={loginState === ACTION_STATUS.PENDING}
                  onClick={onSubmit}
                >
                  Sign in
                </button>
              </div>
              <div className="row mb-4 px-3">
                <small className="w-100 text-center text-sm text-muted">
                  Don't have an account?
                  <Link to="/sign-up" className="text-danger font-weight-bold ml-2">
                    Sign up
                  </Link>
                </small>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
