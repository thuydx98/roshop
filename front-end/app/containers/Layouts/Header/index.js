import React from 'react';
import { useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { selectIsAuthenticated } from 'containers/App/selectors';
import { actions } from 'containers/App/slice';
import useActions from 'utils/hooks/useActions';
import CartIcon from 'containers/CartIcon/Loadable';
import SearchBar from './SearchBar';

export default function Header() {
  const isAuthenticated = useSelector(selectIsAuthenticated);
  const { logout } = useActions(
    {
      logout: actions.logout,
    },
    [actions],
  );

  return (
    <header className="section-header">
      <section className="header-main border-bottom">
        <div className="container">
          <div className="row align-items-center">
            <div className="col-lg-2 col-4">
              <Link to="/" className="brand-wrap">
                <img
                  className="logo"
                  alt="logo"
                  src="https://res.cloudinary.com/roshop/image/upload/v1620038990/logos/logo_czqhlu.png"
                />
              </Link>
            </div>
            <SearchBar />
            <div className="col-lg-4 col-sm-6 col-12">
              <div className="widgets-wrap float-md-right">
                <CartIcon />
                <div className="widget-header icontext">
                  <Link to="profile" className="icon icon-sm rounded-circle border">
                    <i className="fa fa-user" />
                  </Link>
                  <div className="text">
                    <span className="text-muted">Welcome!</span>
                    <div>
                      {!isAuthenticated ? (
                        <>
                          <Link to="/sign-in">Sign in</Link> | <Link to="/sign-up"> Sign up</Link>
                        </>
                      ) : (
                        <button type="button" className="btn btn-link p-0 pl-1" onClick={logout}>
                          Sign out
                        </button>
                      )}
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
