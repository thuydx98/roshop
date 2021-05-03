import { useEffect, useCallback } from 'react';
import useActions from 'utils/hooks/useActions';
import { useSelector } from 'react-redux';
import AuthUtils from 'utils/authentication';
import CartUtils from 'utils/cart';
import CartItem from 'models/CartItem';
import { actions } from './slice';
import { selectCart, selectGetCartStatus } from './selectors';

export const useHooks = () => {
  const getCartStatus = useSelector(selectGetCartStatus);
  const cart = useSelector(selectCart);

  const { setCart, updateCart } = useActions(
    {
      setCart: actions.setCart,
      updateCart: actions.updateCart,
    },
    [actions],
  );

  useEffect(() => {
    if (!cart) {
      const isAuthenticated = AuthUtils.isAuthenticated();
      if (!isAuthenticated) {
        const cartCookie = CartUtils.getCart();
        setCart(cartCookie);
      }
    }
  }, [cart]);

  const handleUpdateCart = useCallback(
    (productId, quantity) => {
      let newCart = cart ? [...cart] : [];
      const isAuthenticated = AuthUtils.isAuthenticated();
      const index = newCart.findIndex(c => c.productId === productId);
      if (index === -1) {
        newCart.unshift(new CartItem({ productId, quantity }));
      } else {
        newCart[index] = { ...newCart[index], quantity: newCart[index].quantity + quantity };
      }

      newCart = newCart.filter(item => item.quantity > 0);
      setCart([...newCart]);
      if (isAuthenticated) {
        updateCart({ productId, quantity });
      } else {
        CartUtils.updateCart(newCart);
      }
    },
    [cart],
  );

  return {
    states: { cart, getCartStatus },
    handlers: { handleUpdateCart },
  };
};

export default useHooks;
