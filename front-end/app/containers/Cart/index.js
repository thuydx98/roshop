import React from 'react';
import get from 'lodash/fp/get';
import { Link } from 'react-router-dom';
import { useInjectSaga } from 'utils/injectSaga';
import { useInjectReducer } from 'utils/injectReducer';
import { ACTION_STATUS } from 'utils/constants';
import { sliceKey, reducer } from './slice';
import saga from './saga';
import useHooks from './hooks';
import './styles.scss';

export default function Cart() {
  useInjectSaga({ key: sliceKey, saga });
  useInjectReducer({ key: sliceKey, reducer });
  const { states, handlers } = useHooks();
  const { cartItems, products, getListProductStatus } = states;
  const { handleUpdateCart } = handlers;

  const total = cartItems ? cartItems.map(item => item.quantity).reduce((a, b) => a + b, 0) : 0;
  let totalPrice = 0;

  return (
    <div className="cart-card">
      <div className="row">
        <div className="col-md-8 cart">
          <div className="title">
            <div className="row">
              <div className="col">
                <h4>
                  <b>Shopping Cart</b>
                </h4>
              </div>
              <div className="col align-self-center text-right text-muted">{total} items</div>
            </div>
          </div>

          {getListProductStatus === ACTION_STATUS.PENDING && (
            <div className="w-100 text-center">
              <div className="spinner-border text-primary mt-3" role="status">
                <span className="sr-only">Loading...</span>
              </div>
            </div>
          )}

          {getListProductStatus === ACTION_STATUS.SUCCESS &&
            cartItems.map(item => {
              const product = Array.isArray(products) ? products.find(p => p.id === item.productId) : {};
              totalPrice += product ? product.price * item.quantity : 0;
              return (
                <div key={item.productId} className="row border-top">
                  <div className="row main align-items-center">
                    <div className="col-2">
                      <img className="img-fluid" src={get('avatarUrl', product)} />
                    </div>
                    <div className="col-5">
                      <div className="row">{get('name', product)}</div>
                    </div>
                    <div className="col-3">
                      <a href="#" onClick={() => handleUpdateCart(item.productId, -1)}>
                        -
                      </a>
                      <a href="#" className="border">
                        {item.quantity}
                      </a>
                      <a href="#" onClick={() => handleUpdateCart(item.productId, 1)}>
                        +
                      </a>
                    </div>
                    <div className="col-2 text-primary">
                      ${get('price', product)}
                      <button className="close" onClick={() => handleUpdateCart(item.productId, -item.quantity)}>
                        &#10005;
                      </button>
                    </div>
                  </div>
                </div>
              );
            })}

          <div className="back-to-shop">
            <Link to="/">&#8592;</Link>
            <span className="text-muted ml-2">Back to shop</span>
          </div>
        </div>
        <div className="col-md-4 summary">
          <div>
            <h5>
              <b>Summary</b>
            </h5>
          </div>
          <hr />
          <div className="row">
            <div className="col pl-0">ITEMS {total}</div>
            <div className="col text-right">${totalPrice}</div>
          </div>
          <form>
            <p>SHIPPING</p>
            <select>
              <option className="text-muted">Standard-Delivery- $5.00</option>
            </select>
            <p>GIVE CODE</p>
            <input id="code" placeholder="Enter your code" className="mb-0" />
          </form>
          <hr />
          <div className="row">
            <div className="col">TOTAL PRICE</div>
            <div className="col text-right">${totalPrice + 5}</div>
          </div>
          <button type="button" className="btn btn-primary">
            CHECKOUT
          </button>
        </div>
      </div>
    </div>
  );
}
