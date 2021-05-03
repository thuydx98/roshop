import React from 'react';
import { Helmet } from 'react-helmet';
import { Switch, Route, BrowserRouter, Redirect } from 'react-router-dom';
import { useInjectSaga } from 'utils/injectSaga';
import { useInjectReducer } from 'utils/injectReducer';

import Home from 'containers/Home/Loadable';
import Auth from 'containers/Auth/Loadable';
import Cart from 'containers/Cart/Loadable';
import Profile from 'containers/Profile/Loadable';
import NotFound from 'containers/NotFound/Loadable';
import Footer from 'containers/Layouts/Footer/Loadable';
import Header from 'containers/Layouts/Header/Loadable';
import { sliceKey, reducer } from './slice';
import saga from './saga';
import useHooks from './hook';

import 'assets/css/bootstrap.css';
import 'assets/css/ui.css';
import 'assets/css/responsive.css';
import 'assets/css/all.min.css';

export default function App() {
  useInjectSaga({ key: sliceKey, saga });
  useInjectReducer({ key: sliceKey, reducer });
  const { selectors } = useHooks();
  const { isAuthenticated } = selectors;

  return (
    <BrowserRouter>
      <Helmet titleTemplate="%s - RoShop Việt Nam" defaultTitle="RoShop Việt Nam">
        <meta name="description" content="RoShop" />
      </Helmet>

      <Header />
      <Switch>
        <Route exact path="/" component={Home} />
        <Route exact path="/cart" component={Cart} />
        <Route exact path="/sign-in" render={() => (isAuthenticated ? <Redirect to="/" /> : <Auth />)} />
        <Route exact path="/sign-up" render={() => (isAuthenticated ? <Redirect to="/" /> : <Auth />)} />
        <Route exact path="/profile" render={() => (!isAuthenticated ? <Redirect to="/sign-in" /> : <Profile />)} />
        <Route component={NotFound} />
      </Switch>
      <Footer />
    </BrowserRouter>
  );
}
