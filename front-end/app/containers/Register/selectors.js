import get from 'lodash/fp/get';
import { createSelector } from 'reselect';

const select = state => state.register;

const selectRegisterStatus = createSelector(
  select,
  state => get('register.status', state),
);
const selectRegisterError = createSelector(
  select,
  state => get('register.error', state),
);

const selectVerifyStatus = createSelector(
  select,
  state => get('verify.status', state),
);
const selectVerifyError = createSelector(
  select,
  state => get('verify.error', state),
);

export { selectRegisterStatus, selectRegisterError, selectVerifyStatus, selectVerifyError };
