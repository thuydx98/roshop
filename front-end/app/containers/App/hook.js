import { useEffect } from 'react';
import { useSelector } from 'react-redux';
import useActions from 'utils/hooks/useActions';
import CartUtils from 'utils/cart';
import { actions as cartActions } from 'containers/CartIcon/slice';
import { selectIsAuthenticated } from './selectors';

export const useHooks = () => {
  const isAuthenticated = useSelector(selectIsAuthenticated);
  const { getCart } = useActions(
    {
      getCart: cartActions.getCart,
    },
    [cartActions],
  );

  useEffect(() => {
    if (isAuthenticated) {
      console.log('call sync from App');
      const cartCookie = CartUtils.getCart();
      getCart(cartCookie);
    }
  }, [isAuthenticated]);

  return {
    selectors: { isAuthenticated },
  };
};

export default useHooks;
