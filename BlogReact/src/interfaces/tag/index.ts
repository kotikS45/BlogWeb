export interface ITagItem {
  id: number;
  name: string;
  urlSlug: string;
  description: string;
}

export interface ITagCreate {
  name: string,
  urlSlug: string,
  description: string
}

export interface ITagEdit {
  id: number
  name: string,
  urlSlug: string,
  description: string
}