import qs from 'qs';
import service, { handleGeneralError } from '../index';

const BASE_URL = process.env.API_URI;
const identity = {
  client_id: 'website',
  client_secret: 'xBmFnXeSz3=',
  grant_type: 'password',
  scope: 'openid offline_access profile api',
};
export function login(username, password) {
  return service(BASE_URL, {
    url: '/connect/token',
    method: 'POST',
    data: qs.stringify({ ...identity, username, password }),
    headers: { 'content-type': 'application/x-www-form-urlencoded' },
  })
    .then(response => response.data)
    .then(data => ({ response: data }))
    .catch(handleGeneralError);
}
