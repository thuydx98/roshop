import get from 'lodash/fp/get';
import { createSelector } from 'reselect';

const select = state => state.cartIcon;

const selectGetCartStatus = createSelector(
  select,
  state => get('cart.status', state),
);
const selectCart = createSelector(
  select,
  state => get('cart.data', state),
);

export { selectCart, selectGetCartStatus };
