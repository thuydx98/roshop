import Cookies from 'js-cookie';

const createCookie = (name, value, days = 600) =>
  Cookies.set(name, value, {
    expires: days,
    secure: false, // process.env.NODE_ENV === 'production',
  });

const readCookie = name => Cookies.get(name);

const eraseCookie = name => Cookies.remove(name);

export default {
  setItem: createCookie,
  getItem: readCookie,
  removeItem: eraseCookie,
};
