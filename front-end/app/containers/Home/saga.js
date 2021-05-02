import { call, put, all, fork, takeLatest } from 'redux-saga/effects';
import { getListProduct } from 'services/product';
import { actions } from './slice';

export function* getListProductWatcher() {
  yield takeLatest(actions.getListProduct, getListProductTask);
}

export function* getListProductTask() {
  const { response, error } = yield call(getListProduct);
  if (response) {
    yield put(actions.getListProductSuccess(response.result));
  } else {
    yield put(actions.getListProductFailed(error));
  }
}

export default function* defaultSaga() {
  yield all([fork(getListProductWatcher)]);
}