import { useState, useCallback, useEffect } from 'react';
import qs from 'qs';
import { useHistory, useLocation } from 'react-router';

export const useHooks = () => {
  const history = useHistory();
  const location = useLocation();
  const [search, setSearch] = useState();
  const params = qs.parse(location.search, { ignoreQueryPrefix: true });

  useEffect(() => setSearch(params.search || ''), [params.search]);

  const onSubmit = useCallback(() => history.push(`?search=${search}`), [search]);

  return {
    states: { search },
    handlers: {
      setSearch,
      onSubmit,
    },
  };
};

export default useHooks;
