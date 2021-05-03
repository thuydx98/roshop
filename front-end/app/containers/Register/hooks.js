import { useCallback, useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import useActions from 'utils/hooks/useActions';
import { actions as appActions } from 'containers/App/slice';
import { ACTION_STATUS } from 'utils/constants';
import { actions as authActions } from 'containers/Auth/slice';
import { selectLoginStatus } from 'containers/Auth/selectors';
import { selectRegisterStatus, selectRegisterError, selectVerifyStatus, selectVerifyError } from './selectors';
import { actions } from './slice';

export const useHooks = () => {
  const [isSubmitted, setIsSubmitted] = useState(false);
  const [payload, setPayload] = useState({
    email: 'thuydx.9598@gmail.com',
    password: '123456',
    confirmPassword: '123456',
  });

  const registerStatus = useSelector(selectRegisterStatus);
  const registerError = useSelector(selectRegisterError);
  const verifyStatus = useSelector(selectVerifyStatus);
  const verifyError = useSelector(selectVerifyError);
  const loginStatus = useSelector(selectLoginStatus);

  const { login, register, verify, resetState } = useActions(
    {
      login: authActions.login,
      register: actions.register,
      verify: actions.verify,
      resetState: actions.resetState,
    },
    [actions, appActions],
  );

  useEffect(() => {
    if (registerStatus === ACTION_STATUS.SUCCESS) {
      setIsSubmitted(false);
    }
  }, [registerStatus]);

  useEffect(() => {
    if (verifyStatus === ACTION_STATUS.SUCCESS) {
      login(payload);
    }
  }, [verifyStatus, payload]);

  useEffect(() => {
    if (loginStatus === ACTION_STATUS.SUCCESS) {
      resetState();
    }
  }, [loginStatus]);

  const onRegister = useCallback(() => {
    setIsSubmitted(true);
    if (payload.email && payload.password && payload.password === payload.confirmPassword) {
      register(payload);
    }
  }, [payload, setIsSubmitted]);

  const onVerify = useCallback(() => {
    setIsSubmitted(true);
    if (payload.email && payload.password && payload.verifyCode) {
      verify(payload);
    }
  }, [payload, setIsSubmitted]);

  return {
    states: { payload, registerStatus, registerError, verifyStatus, verifyError, isSubmitted, loginStatus },
    handlers: { setPayload, onRegister, onVerify },
  };
};

export default useHooks;
