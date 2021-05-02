import { createSlice } from '@reduxjs/toolkit';
import set from 'lodash/fp/set';
import flow from 'lodash/fp/flow';
import authUtils from 'utils/authentication';

export const initialState = {
  isAuthenticated: authUtils.isAuthenticated(),
  userInfo: null,
};

const authenticationSlice = createSlice({
  name: 'authentication',
  initialState,
  reducers: {
    authenticateSuccess(state) {
      return flow(set('isAuthenticated', true))(state);
    },
    logout(state) {
      return flow(
        set('isAuthenticated', false),
        set('userInfo', null),
      )(state);
    },
  },
});

export const { actions, reducer, name: sliceKey } = authenticationSlice;
