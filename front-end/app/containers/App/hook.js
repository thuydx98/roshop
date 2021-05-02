import { useSelector } from 'react-redux';
import { selectIsAuthenticated } from './selectors';

export const useHooks = () => {
  const isAuthenticated = useSelector(selectIsAuthenticated);
  return {
    selectors: { isAuthenticated },
  };
};

export default useHooks;
