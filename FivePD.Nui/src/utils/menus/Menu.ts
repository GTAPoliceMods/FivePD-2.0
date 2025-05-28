import { MenuItem } from './MenuItem';

export class Menu {

  private _id: string;
  private _title: string;
  private _useLocalization: boolean;
  private _items: MenuItem[];

  constructor(id: string, title: string, useLocalization: boolean, items: MenuItem[]) {
    this._id = id;
    this._title = title;
    this._useLocalization = useLocalization;
    this._items = items;
  }

  get id(): string {
    return this._id;
  }

  get title(): string {
    return this._title;
  }

  get useLocalization(): boolean {
    return this._useLocalization;
  }

  get items(): MenuItem[] {
    return this._items;
  }

}