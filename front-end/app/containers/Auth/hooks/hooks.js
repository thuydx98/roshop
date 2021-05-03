import { useCallback, useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import useActions from 'utils/hooks/useActions';
import { actions as appActions } from 'containers/App/slice';
import { ACTION_STATUS } from 'utils/constants';
import { selectLoginStatus } from '../selectors';
import { actions } from '../slice';

export const useHooks = () => {
  const [selectedProvider, setSelectedProvider] = useState();
  const socialLoginState = useSelector(selectLoginStatus);

  const { login, authenticateSuccess, resetState } = useActions(
    {
      login: actions.login,
      authenticateSuccess: appActions.authenticateSuccess,
      resetState: actions.resetState,
    },
    [actions, appActions],
  );

  useEffect(() => {
    if (socialLoginState === ACTION_STATUS.SUCCESS) {
      authenticateSuccess();
      resetState();
    }
  }, [socialLoginState]);

  const onSocialLogin = useCallback((provider, accessToken) => {
    setSelectedProvider(provider);
    login({
      provider,
      accessToken,
      grantType: 'external',
    });
  }, []);

  return {
    states: { socialLoginState, selectedProvider },
    handlers: { onSocialLogin },
  };
};

export default useHooks;
