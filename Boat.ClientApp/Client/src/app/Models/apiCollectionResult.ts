export interface apiCollectionResult<T> {
  pageIndex: number
  pageSize: number
  totalPageCount: number
  totalCount: number
  data: T[]
}