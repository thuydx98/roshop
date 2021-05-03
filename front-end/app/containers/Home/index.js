import React from 'react';
import { useInjectSaga } from 'utils/injectSaga';
import { useInjectReducer } from 'utils/injectReducer';
import Product from 'components/Product';
import Pagination from 'components/Pagination';
import { ACTION_STATUS } from 'utils/constants';
import saga from './saga';
import { sliceKey, reducer } from './slice';
import useHooks from './hook';
import { SortBy } from './constants';

export default function Home() {
  useInjectSaga({ key: sliceKey, saga });
  useInjectReducer({ key: sliceKey, reducer });

  const { states, handlers } = useHooks();
  const { products, pagination, getStatus, params } = states;
  const { onChange } = handlers;

  return (
    <div className="App">
      <section className="section-content padding-y">
        <div className="container">
          <div className="row">
            <aside className="col-md-3">
              <div className="card">
                <article className="filter-group">
                  <header className="card-header">
                    <a href="#" data-toggle="collapse" data-target="#collapse_1" aria-expanded="true">
                      <i className="icon-control fa fa-chevron-down" />
                      <h6 className="title">Product type</h6>
                    </a>
                  </header>
                  <div className="filter-content collapse show" id="collapse_1">
                    <div className="card-body">
                      <ul className="list-menu">
                        <li>
                          <a onClick={() => onChange({ categoryId: 1 })}>Clothes </a>
                        </li>
                        <li>
                          <a href="#">Shoes </a>
                        </li>
                        <li>
                          <a href="#">Watches </a>
                        </li>
                        <li>
                          <a href="#">Coats </a>
                        </li>
                        <li>
                          <a href="#">Glasses </a>
                        </li>
                        <li>
                          <a href="#">Accessories</a>
                        </li>
                      </ul>
                    </div>
                  </div>
                </article>
                <article className="filter-group">
                  <header className="card-header">
                    <a href="#" data-toggle="collapse" data-target="#collapse_2" aria-expanded="true" className="">
                      <i className="icon-control fa fa-chevron-down" />
                      <h6 className="title">Brands </h6>
                    </a>
                  </header>
                  <div className="filter-content collapse show" id="collapse_2">
                    <div className="card-body">
                      <label className="custom-control custom-checkbox">
                        <input type="checkbox" className="custom-control-input" />
                        <div className="custom-control-label">
                          Adidas
                          <b className="badge badge-pill badge-light float-right">120</b>
                        </div>
                      </label>
                      <label className="custom-control custom-checkbox">
                        <input type="checkbox" className="custom-control-input" />
                        <div className="custom-control-label">
                          Gucci
                          <b className="badge badge-pill badge-light float-right">15</b>
                        </div>
                      </label>
                      <label className="custom-control custom-checkbox">
                        <input type="checkbox" className="custom-control-input" />
                        <div className="custom-control-label">
                          Mitsubishi
                          <b className="badge badge-pill badge-light float-right">35</b>
                        </div>
                      </label>
                      <label className="custom-control custom-checkbox">
                        <input type="checkbox" className="custom-control-input" />
                        <div className="custom-control-label">
                          Nissan
                          <b className="badge badge-pill badge-light float-right">89</b>
                        </div>
                      </label>
                      <label className="custom-control custom-checkbox">
                        <input type="checkbox" className="custom-control-input" />
                        <div className="custom-control-label">
                          Honda
                          <b className="badge badge-pill badge-light float-right">30</b>
                        </div>
                      </label>
                    </div>
                  </div>
                </article>
                <article className="filter-group">
                  <header className="card-header">
                    <a href="#" data-toggle="collapse" data-target="#collapse_3" aria-expanded="true" className="">
                      <i className="icon-control fa fa-chevron-down" />
                      <h6 className="title">Price range </h6>
                    </a>
                  </header>
                  <div className="filter-content collapse show" id="collapse_3">
                    <div className="card-body">
                      <div className="form-row">
                        <div className="form-group col-md-6">
                          <label>Min</label>
                          <input className="form-control" placeholder="$0" type="number" />
                        </div>
                        <div className="form-group text-right col-md-6">
                          <label>Max</label>
                          <input className="form-control" placeholder="$1,0000" type="number" />
                        </div>
                      </div>
                      <button className="btn btn-block btn-primary">Apply</button>
                    </div>
                  </div>
                </article>
              </div>
            </aside>
            <main className="col-md-9">
              <header className="border-bottom mb-4 pb-3">
                <div className="form-inline">
                  <span className="mr-md-auto mt-3">{pagination && `${pagination.total} Items found`} </span>
                  <select className="mr-2 form-control" onChange={e => onChange(JSON.parse(e.target.value))}>
                    <option value={JSON.stringify(SortBy.Latest)}>Latest items</option>
                    <option value={JSON.stringify(SortBy.Trending)}>Trending</option>
                    <option value={JSON.stringify(SortBy.Sales)}>Most Popular</option>
                    <option value={JSON.stringify(SortBy.Price)}>Cheapest</option>
                  </select>
                  <div className="btn-group">
                    <button type="button" className="btn btn-outline-secondary" title="List view">
                      <i className="fa fa-bars" />
                    </button>
                    <button type="button" className="btn btn-outline-secondary active" title="Grid view">
                      <i className="fa fa-th" />
                    </button>
                  </div>
                </div>
              </header>

              <div className="row">
                {getStatus === ACTION_STATUS.PENDING && (
                  <div className="w-100 text-center">
                    <div className="spinner-border text-primary mt-3" role="status">
                      <span className="sr-only">Loading...</span>
                    </div>
                  </div>
                )}

                {getStatus === ACTION_STATUS.SUCCESS && products.map(item => <Product key={item.id} product={item} />)}
              </div>

              {getStatus === ACTION_STATUS.SUCCESS && (
                <Pagination pagination={pagination} onChange={page => onChange({ page })} />
              )}
            </main>
          </div>
        </div>
      </section>
    </div>
  );
}
