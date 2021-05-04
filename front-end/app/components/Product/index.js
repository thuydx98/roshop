import React from 'react';
import moment from 'moment';
import useHooks from './hooks';

const Product = props => {
  const now = moment();
  const { product } = props;
  const { handlers } = useHooks();
  const { handleUpdateCart } = handlers;

  return (
    <div className="col-md-4">
      <figure className="card card-product-grid">
        <div className="img-wrap">
          <span className="badge badge-danger">{now.add(-15, 'd') < moment(product.createdAt) && ' NEW '}</span>
          <img alt="..." src={product.avatarUrl} />
          <button className="border-0 btn-overlay" type="button">
            <i className="fa fa-search-plus" /> Quick view
          </button>
        </div>
        <figcaption className="info-wrap">
          <div className="fix-height">
            <button type="button" className="title btn btn-link p-0 text-left">
              {product.name}
            </button>
            <div className="price-wrap mt-2">
              <span className="price">${product.salePrice || product.price}</span>
              <del className="price-old">{product.salePrice && `${product.salePrice}`}</del>
            </div>
          </div>
          <button type="button" className="btn btn-block btn-primary" onClick={() => handleUpdateCart(product.id, 1)}>
            Add to cart
          </button>
        </figcaption>
      </figure>
    </div>
  );
};

export default Product;
