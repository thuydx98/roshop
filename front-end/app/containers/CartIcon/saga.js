import { call, put, all, fork, takeLatest } from 'redux-saga/effects';
import { getCart, syncCart, updateCart } from 'services/cart';
import CartUtils from 'utils/cart';
import get from 'lodash/fp/get';
import { actions } from './slice';

export function* getCartWatcher() {
  yield takeLatest(actions.getCart, getCartTask);
}

export function* getCartTask(action) {
  const cartCookieAmount = get('payload.length', action) || 0;
  const service = cartCookieAmount > 0 ? syncCart : getCart;
  const { response, error } = yield call(service, action.payload);
  if (response) {
    CartUtils.clearCart();
    yield put(actions.getCartSuccess(response.result));
  } else {
    yield put(actions.getCartFailed(error.data));
  }
}

export function* updateCartWatcher() {
  yield takeLatest(actions.updateCart, updateCartTask);
}

export function* updateCartTask(action) {
  const { response, error } = yield call(updateCart, action.payload);
  if (response) {
    yield put(actions.updateCartSuccess(response.result));
  } else {
    yield put(actions.updateCartFailed(error.data));
  }
}

export default function* defaultSaga() {
  yield all([fork(getCartWatcher), fork(updateCartWatcher)]);
}
