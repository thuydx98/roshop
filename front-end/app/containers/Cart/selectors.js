import get from 'lodash/fp/get';
import { createSelector } from 'reselect';

const select = state => state.cart;

const selectGetListProductStatus = createSelector(
  select,
  state => get('products.status', state),
);
const selectListProduct = createSelector(
  select,
  state => get('products.data', state),
);

export { selectListProduct, selectGetListProductStatus };
