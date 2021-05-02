import get from 'lodash/fp/get';
import isEmpty from 'lodash/fp/isEmpty';
import isNil from 'lodash/fp/isNil';

import CookieStorage from 'utils/cookieStorage';
import AuthInfo from 'models/AuthInfo';

const AUTH_INFO = 'AuthenticationInfo';

class AuthInfoUtils {
  storeAuthInfo(authInfo) {
    CookieStorage.removeItem(AUTH_INFO);
    if (!isNil(authInfo)) {
      CookieStorage.setItem(AUTH_INFO, JSON.stringify(authInfo));
    }
  }

  clearAuthInfo() {
    CookieStorage.removeItem(AUTH_INFO);
  }

  getAuthInfo() {
    try {
      const jsonData = JSON.parse(CookieStorage.getItem(AUTH_INFO));
      const authInfo = !isEmpty(jsonData) ? new AuthInfo(jsonData) : null;
      if (!isNil(authInfo)) {
        return { ...authInfo };
      }
      return null;
    } catch (error) {
      return null;
    }
  }

  getRefreshToken() {
    const authInfo = this.getAuthInfo();
    return get('refreshToken')(authInfo);
  }

  getAccessToken() {
    const authInfo = this.getAuthInfo();
    return get('accessToken')(authInfo);
  }

  isAuthenticated() {
    const authInfo = this.getAuthInfo();
    return !isNil(authInfo) ? authInfo.isValid() : false;
  }
}

const singleton = new AuthInfoUtils();
export default singleton;
