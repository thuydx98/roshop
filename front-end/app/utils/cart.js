import isEmpty from 'lodash/fp/isEmpty';
import isNil from 'lodash/fp/isNil';

import CookieStorage from 'utils/cookieStorage';
import CartItem from 'models/CartItem';

const CART_INFO = 'Cart';

class CartUtils {
  updateCart(data) {
    const cart = !isEmpty(data) ? data.map(item => new CartItem(item)) : null;
    CookieStorage.removeItem(CART_INFO);
    CookieStorage.setItem(CART_INFO, JSON.stringify(cart));
  }

  clearCart() {
    CookieStorage.removeItem(CART_INFO);
  }

  getCart() {
    try {
      const jsonData = JSON.parse(CookieStorage.getItem(CART_INFO));
      const cart = !isEmpty(jsonData) ? jsonData.map(item => new CartItem(item)) : null;
      return isNil(cart) ? [] : [...cart];
    } catch (error) {
      return [];
    }
  }
}

const singleton = new CartUtils();
export default singleton;
