import { useCallback, useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import useActions from 'utils/hooks/useActions';
import { actions as appActions } from 'containers/App/slice';
import { ACTION_STATUS } from 'utils/constants';
import { makeSelectLoginStatus, makeSelectLoginError } from '../selectors';
import { actions } from '../slice';

export const useHooks = () => {
  const [isSubmitted, setIsSubmitted] = useState(false);
  const [payload, setPayload] = useState({
    email: 'thuydx.9598@gmail.com',
    password: '123456',
  });

  const loginState = useSelector(makeSelectLoginStatus);
  const loginError = useSelector(makeSelectLoginError);

  const { login, authenticateSuccess, resetState } = useActions(
    {
      login: actions.login,
      authenticateSuccess: appActions.authenticateSuccess,
      resetState: actions.resetState,
    },
    [actions, appActions],
  );

  useEffect(() => {
    if (loginState === ACTION_STATUS.SUCCESS) {
      authenticateSuccess();
      setIsSubmitted(false);
      return () => resetState();
    }
    return undefined;
  }, [loginState, setIsSubmitted]);

  const onSubmit = useCallback(
    event => {
      event.preventDefault();
      setIsSubmitted(true);
      if (payload.email && payload.password) {
        login(payload);
      }
    },
    [payload, setIsSubmitted],
  );

  return {
    states: { payload, loginState, loginError, isSubmitted },
    handlers: { setPayload, onSubmit },
  };
};

export default useHooks;
