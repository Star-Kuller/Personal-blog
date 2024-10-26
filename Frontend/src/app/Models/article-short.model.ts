export class ArticleShort{
  public id: number = 0;
  public title : string = '';
  public text : string = '';
  public isPublished : boolean = false;
  public previewUrl: string | null = null;
  public liked : boolean = false;
  public likesCount : number = 0;
  public commentsCount : number = 0;
  public authorId : number = 0;
}
