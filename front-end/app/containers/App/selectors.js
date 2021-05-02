import { createSelector } from 'reselect';
import get from 'lodash/fp/get';

const selectRouter = state => state.router;
const selectAuthentication = state => state.authentication;

const selectIsAuthenticated = createSelector(
  selectAuthentication,
  state => get('isAuthenticated', state),
);

const makeSelectLocation = () =>
  createSelector(
    selectRouter,
    routerState => routerState.location,
  );

export { selectIsAuthenticated, makeSelectLocation };
