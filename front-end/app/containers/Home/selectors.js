import get from 'lodash/fp/get';
import { createSelector } from 'reselect';

const select = state => state.home;

const selectListProduct = createSelector(
  select,
  state => get('products.data', state),
);

const selectPagination = createSelector(
  select,
  state => get('products.pagination', state),
);

const selectListProductStatus = createSelector(
  select,
  state => get('products.status', state),
);

export { selectListProduct, selectListProductStatus, selectPagination };
