export interface ICategoryItem {
  id: number,
  name: string,
  urlSlug: string,
  description: string
}

export interface ICategoryCreate {
  name: string,
  urlSlug: string,
  description: string
}

export interface ICategoryEdit {
  id: number
  name: string,
  urlSlug: string,
  description: string
}