import { MenuItemType } from '@enums';
import { MenuItem } from './MenuItem';

export class MenuCheckboxItem extends MenuItem {

  public checked = false;

  constructor(title: string, description: string, hashcode: string) {
    super(title, description, hashcode, MenuItemType.Toggle);
  }

  toggle(): void {
    this.checked = !this.checked;
  }

}