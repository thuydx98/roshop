import { call, put, all, fork, takeLatest } from 'redux-saga/effects';
import { register, verify } from 'services/user/authentication';
import { actions } from './slice';

export function* registerWatcher() {
  yield takeLatest(actions.register, registerTask);
}

export function* registerTask(action) {
  const { response, error } = yield call(register, action.payload);
  if (response) {
    yield put(actions.registerSuccess());
  } else {
    yield put(actions.registerFailed(error.data));
  }
}

export function* verifyWatcher() {
  yield takeLatest(actions.verify, verifyTask);
}

export function* verifyTask(action) {
  const { response, error } = yield call(verify, action.payload);
  if (response) {
    yield put(actions.verifySuccess());
  } else {
    yield put(actions.verifyFailed(error.data));
  }
}

export default function* defaultSaga() {
  yield all([fork(registerWatcher), fork(verifyWatcher)]);
}
