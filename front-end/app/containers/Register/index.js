import React from 'react';
import { Link } from 'react-router-dom';
import './styles.css';

export default function Register() {
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
                <h6 className="mb-0 mr-4 mt-2">Sign up with</h6>
                <div className="facebook text-center mr-3">
                  <div className="fab fa-facebook-f" />
                </div>
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
                <input className="mb-4" type="text" name="email" placeholder="Enter a valid email address" />
              </div>
              <div className="row px-3">
                <label className="mb-1">
                  <h6 className="mb-0 text-sm">Password</h6>
                </label>
                <input className="mb-4" type="password" name="password" placeholder="Enter password" />
              </div>
              <div className="row px-3">
                <label className="mb-1">
                  <h6 className="mb-0 text-sm">Confirm password</h6>
                </label>
                <input type="password" name="confirm-password" placeholder="Enter password" />
              </div>
              <div className="row my-3 px-3">
                <button type="submit" className="btn btn-blue text-center">
                  Sign up
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
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
