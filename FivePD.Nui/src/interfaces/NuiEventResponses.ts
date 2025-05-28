import { ILocalization } from "./ILocalization";

export interface BaseNuiEvent {
  type: string;
}

export interface MenuObjectsEvent extends BaseNuiEvent {
  data: {
    Title: string;
    UseLocalization: boolean;
    Id: string;
    Items: {
      Title: string;
      Description: string;
      Hashcode: string;
      Type: number;
      Items?: string[];
    }[];
  }[];
}

export interface RadialMenuObjectsEvent extends BaseNuiEvent {
  data: {
    Title: string;
    Id: string;
    Items: {
      Title: string;
      Hashcode: string;
    }[];
  }[];
}

export interface LocalizationChangeEvent extends BaseNuiEvent {
  data: ILocalization;
}

export interface MenuShowEvent extends BaseNuiEvent {
  menuId: string;
}

export interface RadialMenuControlEvent extends BaseNuiEvent {
  control: 'select' | 'up' | 'down';
  x?: number;
  y: number;
}

export interface UpdateMenuItemEvent extends BaseNuiEvent {
  hashcode: string;
  fieldname: string;
  value: any;
}

export interface SoundEvent extends BaseNuiEvent {
  sound: string;
}

export interface SoundSpeakEvent extends BaseNuiEvent {
  textToPlay: string;
}