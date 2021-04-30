import React from 'react';
import { Link } from 'react-router-dom';

export default function Header() {
  return (
    <header className="section-header">
      <section className="header-main border-bottom">
        <div className="container">
          <div className="row align-items-center">
            <div className="col-lg-2 col-4">
              <Link to="/" className="brand-wrap">
                <img className="logo" alt="..." src={require('assets/images/logos/logo.png')} />
              </Link>
            </div>
            <div className="col-lg-6 col-sm-12">
              <form action="#" className="search">
                <div className="input-group w-100">
                  <input type="text" className="form-control" placeholder="Search" />
                  <div className="input-group-append">
                    <button className="btn btn-primary" type="submit">
                      <i className="fa fa-search" />
                    </button>
                  </div>
                </div>
              </form>
            </div>
            <div className="col-lg-4 col-sm-6 col-12">
              <div className="widgets-wrap float-md-right">
                <div className="widget-header  mr-3">
                  <Link to="/cart" className="icon icon-sm rounded-circle border">
                    <i className="fa fa-shopping-cart" />
                  </Link>
                  <span className="badge badge-pill badge-danger notify">0</span>
                </div>
                <div className="widget-header icontext">
                  <Link to="profile" className="icon icon-sm rounded-circle border">
                    <i className="fa fa-user" />
                  </Link>
                  <div className="text">
                    <span className="text-muted">Welcome!</span>
                    <div>
                      <Link to="/sign-in">Sign in</Link> |<Link to="/sign-up"> Sign up</Link>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
    </header>
  );
}
