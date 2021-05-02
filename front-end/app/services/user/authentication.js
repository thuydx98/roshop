import qs from 'qs';
import service, { handleGeneralError } from '../index';

const BASE_URL = process.env.API_URI;
const identity = {
  client_id: 'website',
  client_secret: 'xBmFnXeSz3=',
  scope: 'openid offline_access profile api',
};

export function login(payload) {
  const { email, password, grantType, accessToken, provider } = payload;
  return service(BASE_URL, {
    url: '/connect/token',
    method: 'POST',
    data: qs.stringify({
      ...identity,
      username: email,
      password,
      grant_type: grantType || 'password',
      provider,
      external_token: accessToken,
    }),
    headers: { 'content-type': 'application/x-www-form-urlencoded' },
  })
    .then(response => response.data)
    .then(data => ({ response: data }))
    .catch(handleGeneralError);
}
