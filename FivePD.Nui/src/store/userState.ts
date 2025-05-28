import { IListItem, ILocalization } from "@interfaces";

export interface UserState {
  isSidebarCollapsed: boolean,
  isPanicModeActive: boolean,
  availableLocales: IListItem[],
  localization: ILocalization | undefined,
}

export const userState = {
  state: (): UserState => {
    return {
      isSidebarCollapsed: false,
      isPanicModeActive: false,
      availableLocales: [],
      localization: undefined,
    }
  },
  mutations: {
    toggleSidebarCollapse(state: UserState) {
      state.isSidebarCollapsed = !state.isSidebarCollapsed;
    },
    togglePanicMode(state: UserState) {
      state.isPanicModeActive = !state.isPanicModeActive;
    },
    setAvailableLocales(state: UserState, availableLocales: IListItem[]) {
      state.availableLocales = availableLocales;
    },
    setLocalization(state: UserState, localization: ILocalization) {
      state.localization = localization;
    }
  },
  actions: {}
};