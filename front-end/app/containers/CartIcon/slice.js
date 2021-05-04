import { createSlice } from '@reduxjs/toolkit';
import { commonStoreKey, handleCommonPending, handleCommonSuccess, handleCommonFailed } from 'utils/@reduxjs/toolkit';

export const initialState = {
  cart: { ...commonStoreKey },
  updateCart: { ...commonStoreKey },
};

const authenticationSlice = createSlice({
  name: 'cartIcon',
  initialState,
  reducers: {
    setCart(state, action) {
      return { ...state, cart: { data: action.payload } };
    },
    getCart(state, action) {
      return handleCommonPending(state, { key: 'cart', ...action });
    },
    getCartSuccess(state, action) {
      return handleCommonSuccess(state, { key: 'cart', ...action });
    },
    getCartFailed(state, action) {
      return handleCommonFailed(state, { key: 'cart', ...action });
    },

    updateCart(state, action) {
      return handleCommonPending(state, { key: 'updateCart', ...action });
    },
    updateCartSuccess(state, action) {
      return handleCommonSuccess(state, { key: 'updateCart', ...action });
    },
    updateCartFailed(state, action) {
      return handleCommonFailed(state, { key: 'updateCart', ...action });
    },
  },
});

export const { actions, reducer, name: sliceKey } = authenticationSlice;
