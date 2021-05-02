import { createSlice as createSliceOriginal } from '@reduxjs/toolkit';
import flow from 'lodash/fp/flow';
import set from 'lodash/fp/set';
import isNil from 'lodash/fp/isNil';
import { ACTION_STATUS } from 'utils/constants';

/* Wrap createSlice with stricter Name options */

/* istanbul ignore next */
export const createSlice = options => {
  return createSliceOriginal(options);
};

export const commonStoreKey = {
  status: '',
  data: null,
  error: null,
};

/**
 * Hanle common failed case
 * @param {*} state
 * @param {*} action
 */
export const handleCommonFailed = (state, action) => {
  const { key, payload } = action;
  return flow(
    set(`${key}.status`, ACTION_STATUS.FAILED),
    set(`${key}.data`, null),
    set(`${key}.error`, payload),
  )(state);
};

/**
 * Hanle common pending case
 * @param {*} state
 * @param {*} action
 */
export const handleCommonPending = (state, action) => {
  const { key } = action;
  return flow(set(`${key}.status`, ACTION_STATUS.PENDING))(state);
};

/**
 * Hanle common success case
 * @param {*} state
 * @param {*} action
 */
export const handleCommonSuccess = (state, action) => {
  const { key, payload } = action;
  return flow(
    set(`${key}.status`, ACTION_STATUS.SUCCESS),
    set(`${key}.data`, Array.isArray(payload) ? [...payload] : { ...payload }),
  )(state);
};

/**
 * Hanle common pagination success case
 * @param {*} state
 * @param {*} action
 */
export const handleCommonPaginationSuccess = (state, action) => {
  const { key, payload } = action;
  let dataItems;
  let remainingData;
  if (!isNil(payload)) {
    const { items, ...remainingProps } = payload;
    dataItems = items;
    remainingData = remainingProps;
  }

  return flow(
    set(`${key}.status`, ACTION_STATUS.SUCCESS),
    set(`${key}.data`, dataItems),
    set(`${key}.pagination`, remainingData),
  )(state);
};
