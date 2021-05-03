import React from 'react';
import useHooks from './hooks';

export default function SearchBar() {
  const { states, handlers } = useHooks();
  const { search } = states;
  const { setSearch, onSubmit } = handlers;

  return (
    <div className="col-lg-6 col-sm-12">
      <div className="input-group w-100">
        <input
          type="text"
          className="form-control"
          value={search}
          placeholder="Search"
          onChange={e => setSearch(e.target.value)}
          onKeyDown={e => e.key === 'Enter' && onSubmit()}
        />
        <div className="input-group-append">
          <button type="button" onClick={onSubmit} className="btn btn-primary">
            <i className="fa fa-search" />
          </button>
        </div>
      </div>
    </div>
  );
}
