import get from 'lodash/fp/get';
import { createSelector } from 'reselect';

const select = state => state.home;

const selectListProduct = createSelector(
  select,
  state => get('products.data', state),
);

const selectListProductStatus = createSelector(
  select,
  state => get('products.state', state),
);

export { selectListProduct, selectListProductStatus };
