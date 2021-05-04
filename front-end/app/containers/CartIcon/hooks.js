import { useEffect } from 'react';
import useActions from 'utils/hooks/useActions';
import { useSelector } from 'react-redux';
import CartUtils from 'utils/cart';
import { selectIsAuthenticated } from 'containers/App/selectors';
import { actions } from './slice';
import { selectCart, selectGetCartStatus } from './selectors';

export const useHooks = () => {
  const getCartStatus = useSelector(selectGetCartStatus);
  const cart = useSelector(selectCart);
  const isAuthenticated = useSelector(selectIsAuthenticated);

  const { getCart, setCart } = useActions(
    {
      getCart: actions.getCart,
      setCart: actions.setCart,
      updateCart: actions.updateCart,
    },
    [actions],
  );

  useEffect(() => {
    const cartCookie = CartUtils.getCart();
    console.log('Cart: ', isAuthenticated);
    if (isAuthenticated) {
      getCart(cartCookie);
    }
    if (!isAuthenticated) {
      setCart(cartCookie);
    }
  }, [isAuthenticated]);

  return {
    states: { cart, getCartStatus },
  };
};

export default useHooks;
