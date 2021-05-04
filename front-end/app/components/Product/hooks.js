import { useCallback } from 'react';
import useActions from 'utils/hooks/useActions';
import { useSelector } from 'react-redux';
import { selectCart } from 'containers/CartIcon/selectors';
import { actions } from 'containers/CartIcon/slice';
import { onUpdateCart } from 'utils/constants';

export const useHooks = () => {
  const cart = useSelector(selectCart);

  const { setCart, updateCart } = useActions(
    {
      setCart: actions.setCart,
      updateCart: actions.updateCart,
    },
    [actions],
  );

  const handleUpdateCart = useCallback(
    (productId, quantity) => onUpdateCart(cart, setCart, updateCart, productId, quantity),
    [cart],
  );

  return {
    handlers: { handleUpdateCart },
  };
};

export default useHooks;
