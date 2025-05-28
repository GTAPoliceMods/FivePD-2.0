import { MenuItemType } from '@enums';
import { MenuItem } from './MenuItem';

export class MenuButtonItem extends MenuItem {

  constructor(title: string, description: string, hashcode: string) {
    super(title, description, hashcode, MenuItemType.Button);
  }

}