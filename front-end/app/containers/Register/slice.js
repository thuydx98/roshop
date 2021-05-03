import { createSlice } from '@reduxjs/toolkit';
import { commonStoreKey, handleCommonPending, handleCommonSuccess, handleCommonFailed } from 'utils/@reduxjs/toolkit';

export const initialState = {
  register: { ...commonStoreKey },
  verify: { ...commonStoreKey },
};

const authenticationSlice = createSlice({
  name: 'register',
  initialState,
  reducers: {
    register(state, action) {
      return handleCommonPending(state, { key: 'register', ...action });
    },
    registerSuccess(state, action) {
      return handleCommonSuccess(state, { key: 'register', ...action });
    },
    registerFailed(state, action) {
      return handleCommonFailed(state, { key: 'register', ...action });
    },

    verify(state, action) {
      return handleCommonPending(state, { key: 'verify', ...action });
    },
    verifySuccess(state, action) {
      return handleCommonSuccess(state, { key: 'verify', ...action });
    },
    verifyFailed(state, action) {
      return handleCommonFailed(state, { key: 'verify', ...action });
    },

    resetState() {
      return null;
    },
  },
});

export const { actions, reducer, name: sliceKey } = authenticationSlice;
