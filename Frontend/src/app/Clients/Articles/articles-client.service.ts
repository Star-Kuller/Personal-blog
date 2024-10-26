import { Injectable } from '@angular/core';
import {BaseClientService} from "../base-client.service";
import {ISystemMessageService} from "../../Interfaces/i-system-message-service";
import {ArticlesClient} from "../../../generated/articles_pb_service";
import {PagedList} from "../../Models/paged-list.model";
import {ArticleShort} from "../../Models/article-short.model";
import {ServiceError} from "../../../generated/greet_pb_service";
import {RpcError} from "../../Errors/RpcError";
import {PagedListQuery, PagedListResult} from "../../../generated/articles_pb";

@Injectable({
  providedIn: 'root'
})
export class ArticlesClientService extends BaseClientService {
  private _client : ArticlesClient;
  constructor(systemMassages : ISystemMessageService) {
    super(systemMassages);
    this._client = new ArticlesClient(this.host);
  }

  public async getPagedList(page: number, size: number): Promise<PagedList<ArticleShort>> {
    const req = new PagedListQuery();
    req.setPage(page);
    req.setSize(size);

    const response = await new Promise<PagedListResult>((resolve, reject) => {
      this._client.getPagedList(req, this.defaultMetadata, (err: ServiceError | null, response?: PagedListResult | null) => {
        if (err) reject(new RpcError(err.message, err.code, err.metadata));
        else if (response) resolve(response);
        else reject(new Error('No response received'));
      });
    });
    console.log(response)
    return {
      size: response.getSize(),
      page: response.getPage(),
      rowsCount: response.getRowscount(),
      totalPages: response.getTotalpages(),
      Items: response.getItemsList().map((article) => {
        let model = new ArticleShort();
        model.id = article.getId();
        model.title = article.getTitle();
        model.text = article.getText();
        model.isPublished = article.getIspublished();
        model.authorId = article.getAuthorid();
        model.commentsCount = article.getCommentscount();
        model.likesCount = article.getLikescount();
        model.liked = article.getLiked();
        return model;
      })
    };
  }
}
