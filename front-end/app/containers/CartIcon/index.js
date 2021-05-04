import React from 'react';
import { Link } from 'react-router-dom';
import { useInjectSaga } from 'utils/injectSaga';
import { useInjectReducer } from 'utils/injectReducer';
import { ACTION_STATUS } from 'utils/constants';
import { sliceKey, reducer } from './slice';
import saga from './saga';
import useHooks from './hooks';
import './styles/style.scss';

export default function CartIcon() {
  useInjectSaga({ key: sliceKey, saga });
  useInjectReducer({ key: sliceKey, reducer });
  const { states } = useHooks();
  const { cart, getCartStatus } = states;

  const total = cart ? cart.map(item => item.quantity).reduce((a, b) => a + b, 0) : 0;

  return (
    <div className="cart-icon widget-header mr-3">
      <Link to="/cart" className="icon icon-sm rounded-circle border">
        <i className="fa fa-shopping-cart" />
      </Link>
      <span className="badge badge-pill badge-danger notify">
        {getCartStatus === ACTION_STATUS.PENDING ? (
          <div className="spinner-border" role="status">
            <span className="sr-only">Loading...</span>
          </div>
        ) : (
          total
        )}
      </span>
    </div>
  );
}
