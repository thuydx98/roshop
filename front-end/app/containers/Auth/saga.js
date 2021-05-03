import { call, put, all, fork, takeLatest } from 'redux-saga/effects';
import { login } from 'services/user/authentication';
import AuthInfo from 'models/AuthInfo';
import AuthUtils from 'utils/authentication';
import { actions } from './slice';

export function* loginWatcher() {
  yield takeLatest(actions.login, loginTask);
}

export function* loginTask(action) {
  const { response, error } = yield call(login, action.payload);
  if (response) {
    const authInfo = new AuthInfo(response);
    AuthUtils.storeAuthInfo(authInfo);
    yield put(actions.loginSuccess());
  } else {
    yield put(actions.loginFailed(error.data));
  }
}

export default function* defaultSaga() {
  yield all([fork(loginWatcher)]);
}
