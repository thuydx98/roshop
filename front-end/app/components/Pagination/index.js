/* eslint-disable jsx-a11y/no-static-element-interactions */
/* eslint-disable jsx-a11y/click-events-have-key-events */
/* eslint-disable jsx-a11y/anchor-is-valid */
import React from 'react';
import './styles.scss';

const Pagination = props => {
  const { pagination, onChange } = props;
  const { total, page, size } = pagination || {};

  if (!pagination || !total || total === 0) return null;

  const totalPage = total % size === 0 ? Math.floor(total / size) : Math.floor(total / size + 1);

  const isShowDots = index => {
    if (totalPage < 7) {
      return false;
    }
    if ((index === 1 && page > 3) || (index === totalPage - 2 && page < totalPage - 3)) {
      return true;
    }
    return false;
  };

  const isShowNumber = index => {
    if (totalPage < 7 || index === 0 || index === totalPage - 1) {
      return true;
    }

    if (index >= page - 2 && index <= page && page > 2 && page < total - 2) {
      return true;
    }

    if (page <= 3 && index <= 3) {
      return true;
    }

    if (page >= totalPage - 3 && index >= totalPage - 4) {
      return true;
    }

    return false;
  };

  const renderItems = () => {
    const items = [];

    for (let index = 0; index < totalPage; index += 1) {
      if (isShowDots(index)) {
        items.push(
          <li key={index} className="page-item">
            <a className="page-link dots">...</a>
          </li>,
        );
      } else if (isShowNumber(index)) {
        items.push(
          <li key={index} className={`page-item ${index + 1 === page && 'active'}`}>
            <a className="page-link" onClick={() => onChange(index + 1)}>
              {index + 1}
            </a>
          </li>,
        );
      }
    }

    return items;
  };

  return (
    <nav className="mt-4">
      <ul className="pagination">
        <li className={`page-item ${page <= 1 && 'disabled'}`}>
          <a className="page-link">{'<'}</a>
        </li>

        {renderItems()}

        <li className={`page-item ${page === totalPage && 'disabled'}`}>
          <a className="page-link" onClick={() => onChange(page + 1)}>
            {'>'}
          </a>
        </li>
      </ul>
    </nav>
  );
};

export default Pagination;
