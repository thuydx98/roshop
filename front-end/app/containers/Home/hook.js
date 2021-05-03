import { useEffect, useCallback } from 'react';
import qs from 'qs';
import useActions from 'utils/hooks/useActions';
import { useSelector } from 'react-redux';
import { useHistory, useLocation } from 'react-router';
import { actions } from './slice';
import { selectListProduct, selectPagination, selectListProductStatus } from './selectors';

export const useHooks = () => {
  const history = useHistory();
  const location = useLocation();

  const products = useSelector(selectListProduct);
  const pagination = useSelector(selectPagination);
  const getStatus = useSelector(selectListProductStatus);

  const { getListProduct } = useActions(
    {
      getListProduct: actions.getListProduct,
    },
    [actions],
  );

  useEffect(() => {
    const params = qs.parse(location.search, { ignoreQueryPrefix: true });
    getListProduct(params);
  }, [location.search]);

  const onChange = useCallback(
    data => {
      const params = qs.parse(location.search, { ignoreQueryPrefix: true });
      const searchParams = new URLSearchParams({ ...params, ...data });
      history.push(`?${searchParams.toString()}`);
    },
    [location.search, history],
  );

  return {
    states: {
      products,
      pagination,
      getStatus,
      params: qs.parse(location.search, { ignoreQueryPrefix: true }),
    },
    handlers: { onChange },
  };
};

export default useHooks;
