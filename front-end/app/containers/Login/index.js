import React from 'react';
import { Link } from 'react-router-dom';
import { ACTION_STATUS } from 'utils/constants';
import useHooks from './hooks';
import './styles/styles.css';

export default function Login() {
  const { states, handlers } = useHooks();
  const { payload, loginState, loginError } = states;
  const { setPayload, onSubmit } = handlers;

  return (
    <>
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
    </>
  );
}
