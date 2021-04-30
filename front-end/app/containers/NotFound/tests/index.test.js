import React from 'react';
import { render } from 'react-testing-library';
import { IntlProvider } from 'react-intl';

import NotFound from '../index';

describe('<NotFound />', () => {
  it('should render and match the snapshot', () => {
    const {
      container: { firstChild },
    } = render(
      <IntlProvider locale="en">
        <NotFound />
      </IntlProvider>,
    );
    expect(firstChild).toMatchSnapshot();
  });
});
