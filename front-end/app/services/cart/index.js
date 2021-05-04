import service, { handleGeneralError } from '../index';

const BASE_URL = `${process.env.API_URI}/api/carts`;

export function getCart() {
  return service(BASE_URL, {
    url: '/my-cart',
    method: 'GET',
  })
    .then(response => response.data)
    .then(data => ({ response: data }))
    .catch(handleGeneralError);
}

export function syncCart(payload) {
  return service(BASE_URL, {
    url: '/my-cart/sync',
    method: 'POST',
    data: [...payload],
  })
    .then(response => response.data)
    .then(data => ({ response: data }))
    .catch(handleGeneralError);
}

export function updateCart(payload) {
  return service(BASE_URL, {
    url: '/my-cart',
    method: 'PUT',
    data: { ...payload },
  })
    .then(response => response.data)
    .then(data => ({ response: data }))
    .catch(handleGeneralError);
}
