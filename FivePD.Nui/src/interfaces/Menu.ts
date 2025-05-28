export interface IMenuItem {
  Title: string,
  Hashcode: string
}

export interface IMenu {
  Title: string,
  Id: string,
  Items: IMenuItem[]
}