/*
 * Home Messages
 *
 * This contains all the text for the Home container.
 */
import { defineMessages } from 'react-intl';

export const scope = 'app.containers.Home';

export default defineMessages({
  header: {
    id: `${scope}.header`,
    defaultMessage: 'This is the Home container!',
  },
});
