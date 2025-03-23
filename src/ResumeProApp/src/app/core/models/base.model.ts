export interface Result {
  succeeded: boolean;
  errors: string[];
}

export interface PagedList<T> {
  items: T[];
  pageIndex: number;
  totalPages: number;
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

export interface PagingQuery {
  pageIndex?: number;
  pageSize?: number;
}
