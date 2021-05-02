import get from 'lodash/fp/get';
import { createSelector } from 'reselect';

const select = state => state.loginPage;

const makeSelectLoginStatus = createSelector(
  select,
  state => get('state', state),
);
const makeSelectLoginError = createSelector(
  select,
  state => get('error', state),
);

export { makeSelectLoginStatus, makeSelectLoginError };
