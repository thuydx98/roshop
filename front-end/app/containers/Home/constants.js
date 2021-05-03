export const Sort = {
  By: 'sortBy',
  Type: 'sortType',
};

export const SortType = {
  Asc: 'asc',
  Desc: 'desc',
};

export const SortBy = {
  Price: {
    [Sort.By]: 'price',
    [Sort.Type]: SortType.Acs,
  },
  Sales: {
    [Sort.By]: 'sales',
    [Sort.Type]: SortType.Desc,
  },
  Trending: {
    [Sort.By]: 'trending',
    [Sort.Type]: SortType.Desc,
  },
  Latest: {
    [Sort.By]: 'latest',
    [Sort.Type]: SortType.Desc,
  },
};
