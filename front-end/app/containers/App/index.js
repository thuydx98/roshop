import React from 'react';
import { Helmet } from 'react-helmet';
import { Switch, Route, BrowserRouter } from 'react-router-dom';

import Home from 'containers/Home/Loadable';
import Login from 'containers/Login/Loadable';
import Register from 'containers/Register/Loadable';
import Cart from 'containers/Cart/Loadable';
import Profile from 'containers/Profile/Loadable';
import NotFound from 'containers/NotFound/Loadable';
import Footer from '../Layouts/Footer/Loadable';
import Header from '../Layouts/Header/Loadable';

import 'assets/css/bootstrap.css';
import 'assets/css/ui.css';
import 'assets/css/responsive.css';
import 'assets/css/all.min.css';

export default function App() {
  return (
    <BrowserRouter>
      <Helmet titleTemplate="%s - RoShop Việt Nam" defaultTitle="RoShop Việt Nam">
        <meta name="description" content="RoShop" />
      </Helmet>

      <Header />
      <Switch>
        <Route exact path="/" component={Home} />
        <Route exact path="/sign-in" component={Login} />
        <Route exact path="/sign-up" component={Register} />
        <Route exact path="/cart" component={Cart} />
        <Route exact path="/profile" component={Profile} />
        <Route component={NotFound} />
      </Switch>
      <Footer />
    </BrowserRouter>
  );
}
