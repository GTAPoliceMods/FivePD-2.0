import { MenuItemType } from '@enums';
import { post } from '@utils';
import { MenuItem } from './MenuItem';

export class MenuListItem extends MenuItem {

  private _currentIndex: number = 0;

  public items: string[] = [];

  constructor(title: string, description: string, hashcode: string) {
    super(title, description, hashcode, MenuItemType.List);
  }

  get currentIndex(): number {
    return this._currentIndex;
  }

  next(): void {
    if (this._currentIndex === this.items.length-1) return;
    this._currentIndex++;
    post(`fivepd:menu:list_onchange_${this.hashcode}`, this.currentIndex).subscribe();
  }

  prev(): void {
    if (this._currentIndex === 0) return;
    this._currentIndex--;
    post(`fivepd:menu:list_onchange_${this.hashcode}`, this.currentIndex).subscribe();
  }

}