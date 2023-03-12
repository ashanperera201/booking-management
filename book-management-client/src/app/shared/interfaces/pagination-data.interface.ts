export interface IPaginationData<T> {
  total: number;
  page: number;
  records: T[];
}
