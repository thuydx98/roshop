import { createSlice } from '@reduxjs/toolkit';
import {
  commonStoreKey,
  handleCommonPending,
  handleCommonPaginationSuccess,
  handleCommonFailed,
} from 'utils/@reduxjs/toolkit';

export const initialState = {
  products: { ...commonStoreKey },
};

const slice = createSlice({
  name: 'home',
  initialState,
  reducers: {
    getListProduct(state, action) {
      return handleCommonPending(state, { key: 'products', ...action });
    },
    getListProductSuccess(state, action) {
      return handleCommonPaginationSuccess(state, { key: 'products', ...action });
    },
    getListProductFailed(state, action) {
      return handleCommonFailed(state, { key: 'products', ...action });
    },
  },
});

export const { actions, reducer, name: sliceKey } = slice;
