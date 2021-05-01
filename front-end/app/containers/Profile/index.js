import React from 'react';
import './styles.scss';

export default function Profile() {
  return (
    <div className="container">
      <div className="profile-page">
        <div className="row gutters-sm">
          <div className="col-md-3 mb-3">
            <div className="card">
              <div className="card-body">
                <div className="d-flex flex-column align-items-center text-center">
                  <img
                    src="https://bootdey.com/img/Content/avatar/avatar7.png"
                    alt="Admin"
                    className="rounded-circle"
                    width="150"
                  />
                  <div className="mt-3">
                    <h4>John Doe</h4>
                    <p className="text-muted font-size-sm">Bay Area, San Francisco, CA</p>

                    <button type="button" className="btn btn-link text-left w-100 px-0">
                      <i className="fa fa-info-circle fa-lg text-primary mr-4" />
                      <span className="text-danger">Information</span>
                    </button>
                    <button type="button" className="btn btn-link text-left w-100 px-0">
                      <i className="fa fa-address-card fa-lg text-warning mr-4" />
                      Addresses
                    </button>
                    <button type="button" className="btn btn-link text-left w-100 px-0">
                      <i className="fas fa-receipt fa-lg text-danger ml-1 mr-4" />
                      Purchase orders
                    </button>
                  </div>
                </div>
              </div>
            </div>
            <div className="card mt-3">
              <ul className="list-group list-group-flush">
                <li className="list-group-item d-flex align-items-center flex-wrap">
                  <h6 className="mb-0">
                    <span className="text-center mr-3">
                      <div className="fab fa-facebook-f text-primary fa-lg" />
                    </span>
                    Facebook
                  </h6>
                  <span className="text-left text-social text-secondary">bootdey</span>
                </li>
                <button type="button" className="btn btn-secondary btn-social-link btn-sm">
                  Link
                </button>
              </ul>
              <ul className="list-group list-group-flush">
                <li className="list-group-item d-flex align-items-center flex-wrap">
                  <h6 className="mb-0">
                    <span className="text-center mr-3">
                      <div className="fab fa-google text-danger fa-lg" />
                    </span>
                    Google
                  </h6>
                  <span className="text-left text-social text-secondary">google</span>
                </li>
                <button type="button" className="btn btn-secondary btn-social-link btn-sm">
                  Link
                </button>
              </ul>
              <ul className="list-group list-group-flush">
                <li className="list-group-item d-flex align-items-center flex-wrap">
                  <h6 className="mb-0">
                    <span className="text-center mr-3">
                      <div className="fab fa-apple text-muted fa-lg" />
                    </span>
                    Apple
                  </h6>
                  <span className="text-left text-social text-secondary">bootdey</span>
                </li>
                <button type="button" className="btn btn-secondary btn-social-link btn-sm">
                  Link
                </button>
              </ul>
            </div>
          </div>
          <div className="col-md-9">
            <div className="card mb-3">
              <div className="card-body">
                <div className="row">
                  <div className="col-sm-3">
                    <h6 className="mb-0">Full Name</h6>
                  </div>
                  <div className="col-sm-9 text-secondary">Kenneth Valdez</div>
                </div>
                <hr />
                <div className="row">
                  <div className="col-sm-3">
                    <h6 className="mb-0">Email</h6>
                  </div>
                  <div className="col-sm-9 text-secondary">fip@jukmuh.al</div>
                </div>
                <hr />
                <div className="row">
                  <div className="col-sm-3">
                    <h6 className="mb-0">Phone</h6>
                  </div>
                  <div className="col-sm-9 text-secondary">(239) 816-9029</div>
                </div>
                <hr />
                <div className="row">
                  <div className="col-sm-3">
                    <h6 className="mb-0">Mobile</h6>
                  </div>
                  <div className="col-sm-9 text-secondary">(320) 380-4539</div>
                </div>
                <hr />
                <div className="row">
                  <div className="col-sm-3">
                    <h6 className="mb-0">Address</h6>
                  </div>
                  <div className="col-sm-9 text-secondary">Bay Area, San Francisco, CA</div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
