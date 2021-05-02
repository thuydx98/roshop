import axios from 'axios';
import isNil from 'lodash/fp/isNil';
// import { notifyError } from 'utils/notify';
import AuthUtils from 'utils/authentication';

/**
 * Create an Axios Client with defaults
 */
const createClient = baseURL =>
  axios.create({
    baseURL,
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${AuthUtils.getAccessToken()}`,
    },
  });

/**
 * Request Wrapper with default success/error actions
 */
const request = (baseURL, options) => {
  const onSuccess = response => response;
  const onError = error => Promise.reject(error.response || error.message);
  const client = createClient(baseURL);

  return client(options)
    .then(onSuccess)
    .catch(onError);
};

export const handleGeneralError = (error, isShowError = true) => {
  if (isShowError) handleShowError(error);
  if (!isNil(error.response)) {
    return {
      error: error.response
        .clone()
        .json()
        .catch(() => error.response.text())
        .then(objData => ({
          error: { ...objData, status: error.response.status },
        })),
    };
  }
  return { error };
};

const handleShowError = error => {
  if (error.status === 401) {
    // store.dispatch(actions.logoutRequest());
    return;
  }

  if (error.status === 500) {
    // notifyError('Internal server error.');
  } else if (error === 'Network Error') {
    // notifyError('Network error. Please check your internet.');
  }
};

export default request;
