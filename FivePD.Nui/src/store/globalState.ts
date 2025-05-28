import { IMenu } from "@interfaces";
import { Menu } from "@utils";

export interface GlobalState {
  radialMenus: IMenu[],
  menus: Menu[],
}

export const globalState = {
  state: (): GlobalState => {
    return {
      radialMenus: [],
      menus: [],
    }
  },
  mutations: {
    setRadialMenus(state: GlobalState, radialMenus: IMenu[]) {
      state.radialMenus = radialMenus;
    },
    setMenus(state: GlobalState, menus: Menu[]) {
      state.menus = menus;
    },
  },
  actions: {}
};