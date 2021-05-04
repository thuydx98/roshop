import { createSlice } from '@reduxjs/toolkit';
import { commonStoreKey, handleCommonPending, handleCommonSuccess, handleCommonFailed } from 'utils/@reduxjs/toolkit';

export const initialState = {
  products: { ...commonStoreKey },
};

const authenticationSlice = createSlice({
  name: 'cart',
  initialState,
  reducers: {
    setListProduct(state, action) {
      return { ...state, cart: { data: action.payload } };
    },
    getListProduct(state, action) {
      return handleCommonPending(state, { key: 'products', ...action });
    },
    getListProductSuccess(state, action) {
      return handleCommonSuccess(state, { key: 'products', ...action });
    },
    getListProductFailed(state, action) {
      return handleCommonFailed(state, { key: 'products', ...action });
    },
  },
});

export const { actions, reducer, name: sliceKey } = authenticationSlice;
