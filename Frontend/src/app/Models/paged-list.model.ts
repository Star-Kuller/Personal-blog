export class PagedList<T> {
  public page : number = 0;
  public size : number = 0;
  public totalPages : number = 0;
  public rowsCount : number = 0;
  public Items : T[] = [];
}
