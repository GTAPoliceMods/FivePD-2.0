import { createStore } from 'vuex';
import { userState } from './userState';
import { globalState } from './globalState';

export const store = createStore({
  modules: {
    user: userState,
    global: globalState
  }
});