import React from 'react';
import get from 'lodash/fp/get';
import { Link } from 'react-router-dom';
import { useInjectSaga } from 'utils/injectSaga';
import { useInjectReducer } from 'utils/injectReducer';
import { ACTION_STATUS } from 'utils/constants';
import { sliceKey, reducer } from './slice';
import saga from './saga';
import useHooks from './hooks';
import './styles.css';

export default function Register() {
  useInjectSaga({ key: sliceKey, saga });
  useInjectReducer({ key: sliceKey, reducer });
  const { states, handlers } = useHooks();
  const { payload, registerStatus, registerError, verifyStatus, verifyError, isSubmitted, loginStatus } = states;
  const { setPayload, onRegister, onVerify } = handlers;
  return (
    <>
      <div className="row px-3">
        <label className="mb-1">
          <h6 className="mb-0 text-sm">Email Address</h6>
        </label>
        <input
          className="mb-4"
          type="text"
          name="email"
          placeholder="Enter a valid email address"
          value={payload.email}
          disabled={registerStatus === ACTION_STATUS.SUCCESS}
          onChange={e => setPayload({ ...payload, email: e.target.value })}
        />
      </div>

      {registerStatus !== ACTION_STATUS.SUCCESS && (
        <>
          <div className="row px-3">
            <label className="mb-1">
              <h6 className="mb-0 text-sm">Password</h6>
            </label>
            <input
              className="mb-4"
              type="password"
              name="password"
              placeholder="Enter password"
              value={payload.password}
              onChange={e => setPayload({ ...payload, password: e.target.value })}
            />
          </div>
          <div className="row px-3">
            <label className="mb-1">
              <h6 className="mb-0 text-sm">Confirm password</h6>
            </label>
            <input
              type="password"
              name="confirm-password"
              placeholder="Enter password"
              value={payload.confirmPassword}
              onChange={e => setPayload({ ...payload, confirmPassword: e.target.value })}
            />
            <label className="text-danger text-sm">
              {registerStatus === ACTION_STATUS.FAILED &&
                get('errorCode', registerError) === 1000 &&
                'Email is already exist'}
            </label>
          </div>
        </>
      )}

      {registerStatus === ACTION_STATUS.SUCCESS && (
        <div className="row px-3">
          <label className="mb-1">
            <h6 className="mb-0 text-sm">Verify code</h6>
          </label>
          <input
            type="number"
            name="code"
            placeholder="Enter verify code"
            onChange={e => setPayload({ ...payload, verifyCode: e.target.value })}
          />
          <label className="text-danger text-sm">
            {verifyStatus === ACTION_STATUS.FAILED &&
              get('errorCode', verifyError) === 1001 &&
              'Verify code is invalid'}
          </label>
        </div>
      )}

      <div className="row my-3 px-3">
        <button
          type="submit"
          className="btn btn-blue text-center"
          disabled={
            isSubmitted &&
            (registerStatus === ACTION_STATUS.PENDING ||
              verifyStatus === ACTION_STATUS.PENDING ||
              loginStatus === ACTION_STATUS.PENDING)
          }
          onClick={registerStatus === ACTION_STATUS.SUCCESS ? onVerify : onRegister}
        >
          {registerStatus === ACTION_STATUS.SUCCESS ? 'Verify' : 'Sign up'}
        </button>
      </div>
      <div className="row mb-4 px-3">
        <small className="w-100 text-center text-sm text-muted">
          Have an account?
          <Link to="/sign-in" className="text-danger font-weight-bold ml-2">
            Sign in
          </Link>
        </small>
      </div>
    </>
  );
}
