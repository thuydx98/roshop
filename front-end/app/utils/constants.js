import CartItem from 'models/CartItem';
import AuthUtils from 'utils/authentication';
import CartUtils from 'utils/cart';

export const RESTART_ON_REMOUNT = '@@saga-injector/restart-on-remount';
export const DAEMON = '@@saga-injector/daemon';
export const ONCE_TILL_UNMOUNT = '@@saga-injector/once-till-unmount';

export const ACTION_STATUS = {
  PENDING: 'PENDING',
  SUCCESS: 'SUCCESS',
  FAILED: 'FAILED',
};

export const onUpdateCart = (cart, setMemoryCart, updateDatabaseCart, productId, quantity) => {
  let newCart = cart ? [...cart] : [];
  const index = newCart.findIndex(c => c.productId === productId);
  if (index === -1) {
    newCart.unshift(new CartItem({ productId, quantity }));
  } else {
    newCart[index] = { ...newCart[index], quantity: newCart[index].quantity + quantity };
  }

  newCart = newCart.filter(item => item.quantity > 0);
  setMemoryCart([...newCart]);
  if (AuthUtils.isAuthenticated()) {
    updateDatabaseCart({ productId, quantity });
  } else {
    CartUtils.updateCart(newCart);
  }
};
