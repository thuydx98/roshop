import get from 'lodash/fp/get';
import { createSelector } from 'reselect';

const select = state => state.auth;

const selectLoginStatus = createSelector(
  select,
  state => get('state', state),
);
const selectLoginError = createSelector(
  select,
  state => get('error', state),
);

export { selectLoginStatus, selectLoginError };
