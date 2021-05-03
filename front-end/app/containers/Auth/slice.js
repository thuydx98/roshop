import { createSlice } from '@reduxjs/toolkit';
import set from 'lodash/fp/set';
import flow from 'lodash/fp/flow';
import { ACTION_STATUS } from 'utils/constants';

export const initialState = {
  state: null,
  error: null,
};

const authenticationSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    login(state) {
      return flow(
        set('state', ACTION_STATUS.PENDING),
        set('error', null),
      )(state);
    },
    loginSuccess(state) {
      return flow(
        set('state', ACTION_STATUS.SUCCESS),
        set('error', null),
      )(state);
    },
    loginFailed(state, action) {
      return flow(
        set('state', ACTION_STATUS.FAILED),
        set('error', action.payload),
      )(state);
    },
    resetState(state) {
      return flow(
        set('state', null),
        set('error', null),
      )(state);
    },
  },
});

export const { actions, reducer, name: sliceKey } = authenticationSlice;
