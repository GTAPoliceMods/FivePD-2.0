import { MenuItemType } from "@enums";
import { post } from "@utils";
import { MenuListItem } from "./MenuListItem";

export class MenuItem {

  public title: string;
  public enabled = true;

  private _type: MenuItemType;
  private _description: string;
  private _hashcode: string;

  constructor(title: string, description: string, hashcode: string, type: MenuItemType) {
    this.title = title;
    this._description = description;
    this._hashcode = hashcode;
    this._type = type;
  }

  get type(): MenuItemType {
    return this._type;
  }

  get description(): string {
    return this._description;
  }

  get hashcode(): string {
    return this._hashcode;
  }

  onPress(): void {
    if (this._type === MenuItemType.List) {
      const menuItemList = ((this as unknown)) as MenuListItem;
      if (menuItemList.items.length) {
        post(`fivepd:menu:trigger_item_${this._hashcode}`, menuItemList.currentIndex).subscribe();
      }
    } else {
      post(`fivepd:menu:trigger_item_${this._hashcode}`, -1).subscribe();
    }
  }

}