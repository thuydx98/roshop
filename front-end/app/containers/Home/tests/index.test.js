import React from 'react';
import { render } from 'react-testing-library';
import { IntlProvider } from 'react-intl';

import Home from '../index';

describe('<Home />', () => {
  it('should render and match the snapshot', () => {
    const {
      container: { firstChild },
    } = render(
      <IntlProvider locale="en">
        <Home />
      </IntlProvider>,
    );
    expect(firstChild).toMatchSnapshot();
  });
});
