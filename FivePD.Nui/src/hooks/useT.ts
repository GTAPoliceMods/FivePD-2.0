import { useStore } from "vuex";

export function useT() {

  const t = (key: string) => {
    const store = useStore();
    const localization = store.state.user.localization;

    let currentItem = localization;
    for (const k of key.split('.')) {
      if (!currentItem.hasOwnProperty(k)) return '';
      currentItem = currentItem[k];
    }
    return currentItem;
  }

  return t;
}