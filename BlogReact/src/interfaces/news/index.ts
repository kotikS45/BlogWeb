import { ICategoryItem } from "../category";
import { ITagItem } from "../tag";

export interface IPostItem {
  id: number,
  title: string,
  shortDescription: string,
  description: string,
  meta: string,
  urlSlug: string,
  published: boolean,
  postedOn: Date,
  modified: Date,
  category: ICategoryItem,
  tags: ITagItem[]
}