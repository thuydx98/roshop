import { useCallback, useEffect } from 'react';
import useActions from 'utils/hooks/useActions';
import { onUpdateCart, ACTION_STATUS } from 'utils/constants';
import { useSelector } from 'react-redux';
import { selectCart } from 'containers/CartIcon/selectors';
import { actions as cartIconActions } from 'containers/CartIcon/slice';
import { actions } from './slice';
import { selectListProduct, selectGetListProductStatus } from './selectors';

export const useHooks = () => {
  const getListProductStatus = useSelector(selectGetListProductStatus);
  const products = useSelector(selectListProduct);
  const cartItems = useSelector(selectCart);

  const { getListProduct, setCart, updateCart } = useActions(
    {
      getListProduct: actions.getListProduct,
      setCart: cartIconActions.setCart,
      updateCart: cartIconActions.updateCart,
    },
    [actions, cartIconActions],
  );

  useEffect(() => {
    const productIds = cartItems && cartItems.map(i => i.productId).join(',');
    if (productIds) {
      getListProduct({ productIds });
    }
  }, []);

  useEffect(() => {
    if (!getListProductStatus) {
      const productIds = cartItems && cartItems.map(i => i.productId).join(',');
      if (productIds) {
        getListProduct({ productIds });
      }
    }
  }, [getListProductStatus, cartItems]);

  const handleUpdateCart = useCallback(
    (productId, quantity) => onUpdateCart(cartItems, setCart, updateCart, productId, quantity),
    [cartItems],
  );

  return {
    states: { cartItems, products, getListProductStatus },
    handlers: { handleUpdateCart },
  };
};

export default useHooks;
