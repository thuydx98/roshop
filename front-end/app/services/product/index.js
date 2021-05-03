import service, { handleGeneralError } from '../index';

const BASE_URL = `${process.env.API_URI}/api/products`;

export function getListProduct(payload) {
  return service(BASE_URL, {
    url: '',
    method: 'GET',
    params: { ...payload },
  })
    .then(response => response.data)
    .then(data => ({ response: data }))
    .catch(handleGeneralError);
}
