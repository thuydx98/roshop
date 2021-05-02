import { useEffect } from 'react';
import useActions from 'utils/hooks/useActions';
import { useSelector } from 'react-redux';
import { actions } from './slice';
import { selectListProduct, selectListProductStatus } from './selectors';

export const useHooks = () => {
  const products = useSelector(selectListProduct);
  const getListProductStatus = useSelector(selectListProductStatus);

  const { getListProduct } = useActions(
    {
      getListProduct: actions.getListProduct,
    },
    [actions],
  );

  useEffect(() => {
    getListProduct();
  }, [getListProduct]);

  return {
    states: {},
    selectors: {
      products,
      getListProductStatus,
    },
    handlers: {},
  };
};

export default useHooks;
