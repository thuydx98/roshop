import { all, fork, takeLatest } from 'redux-saga/effects';
import AuthUtils from 'utils/authentication';
import CartUtils from 'utils/cart';
import { actions } from './slice';

export function* logoutWatcher() {
  yield takeLatest(actions.logout, logoutTask);
}

export function* logoutTask() {
  AuthUtils.clearAuthInfo();
  CartUtils.clearCart();
}

export default function* defaultSaga() {
  yield all([fork(logoutWatcher)]);
}
