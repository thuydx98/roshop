/* eslint-disable camelcase */
import { isNil } from 'lodash/fp';
import moment from 'moment';

class AuthInfo {
  static propTypes = {
    accessToken: String,
    refreshToken: String,
    expiresIn: Number,
    loggedInAt: String,
  };

  constructor(authInfo) {
    if (!isNil(authInfo)) {
      const { access_token, accessToken, expires_in, expiresIn, loggedInAt, refresh_token, refreshToken } = authInfo;

      this.accessToken = access_token || accessToken;
      this.refreshToken = refresh_token || refreshToken;
      this.expiresIn = expires_in || expiresIn;
      this.loggedInAt = loggedInAt || moment().format();
    }
  }

  isValid = () => moment() < moment(this.loggedInAt).add(this.expiresIn, 'seconds');
}

export default AuthInfo;
