<script setup lang="ts">
import { type Ref, ref, onMounted, onBeforeMount, onBeforeUnmount } from 'vue';
import { useT } from '@hooks';
import { type Menu, MenuItem, MenuListItem, MenuCheckboxItem, onNuiEvent } from '@utils';
import { MenuItemType, NuiEventType } from '@enums';
import { BaseNuiEvent } from '@interfaces';
import { Subscription } from 'rxjs';

const props = defineProps<{
  menu: Menu
}>();

const t = useT();
const menuEvents$ = ref<Subscription>();

const currentItemIndex: Ref<number> = ref(0);
const itemNodes: Ref<HTMLElement[] | null> = ref(null);
const selectorNode: Ref<HTMLElement | null> = ref(null);
const menuItems: Ref<MenuItem[]> = ref([]);

onBeforeMount(() => {
  menuItems.value = props.menu.items;
});

onMounted(() => {
  itemNodes.value = Array.from(document.querySelectorAll('.menu-item'));
  selectorNode.value = document.querySelector('.selector');

  menuEvents$.value = onNuiEvent<BaseNuiEvent>(NuiEventType.MenuUp, NuiEventType.MenuDown, NuiEventType.MenuLeft, NuiEventType.MenuRight, NuiEventType.MenuSelect).subscribe(event => {
    switch (event.type) {
      case NuiEventType.MenuUp: {
        onMenuUp();
        break;
      }
      case NuiEventType.MenuDown: {
        onMenuDown();
        break;
      }
      case NuiEventType.MenuLeft: {
        onMenuLeft();
        break;
      }
      case NuiEventType.MenuRight: {
        onMenuRight();
        break;
      }
      case NuiEventType.MenuSelect: {
        onMenuSelect();
        break;
      }
    }
  });
});

onBeforeUnmount(() => {
  menuEvents$.value?.unsubscribe();
});

const onMenuUp = () => {
  if (!selectorNode.value || !itemNodes.value || !menuItems.value) return;

  if (currentItemIndex.value > 0) currentItemIndex.value = currentItemIndex.value - 1;
  else currentItemIndex.value = menuItems.value.length - 1;

  const currentItem = menuItems.value[currentItemIndex.value];
  if (!currentItem.enabled) {
    onMenuUp();
    return;
  }

  const currentItemNode = itemNodes.value[currentItemIndex.value];
  selectorNode.value.style.top = `${currentItemNode.offsetTop}px`;
}

const onMenuDown = () => {
  if (!selectorNode.value || !itemNodes.value || !menuItems.value) return;

  if (currentItemIndex.value < menuItems.value.length - 1) currentItemIndex.value = currentItemIndex.value + 1;
  else currentItemIndex.value = 0;

  const currentItem = menuItems.value[currentItemIndex.value];
  if (!currentItem.enabled) {
    onMenuDown();
    return;
  }

  const currentItemNode = itemNodes.value[currentItemIndex.value];
  selectorNode.value.style.top = `${currentItemNode.offsetTop}px`;
}

const onMenuLeft = () => {
  onListSlide(NuiEventType.MenuLeft);
}

const onMenuRight = () => {
  onListSlide(NuiEventType.MenuRight);
}

const onMenuSelect = () => {
  if (!selectorNode.value || !itemNodes.value || !menuItems.value) return;

  const currentItem = menuItems.value[currentItemIndex.value];
  if (!currentItem.enabled) return;

  // Change DOM state
  selectorNode.value.classList.add('selector--on-select');
  setTimeout(() => {
    selectorNode.value!.classList.remove('selector--on-select');
  }, 100);

  // Change actual item state
  if (currentItem.type === MenuItemType.Toggle) (currentItem as MenuCheckboxItem).toggle();
  currentItem.onPress();
}

const getMarqueeClass = (item: MenuListItem) => {

  if (item.items.length <= item.currentIndex) {
    return '';
  }

  return item.items[item.currentIndex].length > 10 ? 'marquee' : '';
}

const onListSlide = (dir: string) => {
  if (!selectorNode.value || !itemNodes.value || !menuItems.value) return;

  const currentItem: MenuListItem = menuItems.value[currentItemIndex.value] as MenuListItem;
  if (currentItem.type !== MenuItemType.List) return;

  const currentItemNode = itemNodes.value[currentItemIndex.value];
  const leftArrow = currentItemNode.querySelector('.left-arrow');
  const rightArrow = currentItemNode.querySelector('.right-arrow');

  const arrow = dir === NuiEventType.MenuLeft ? leftArrow : rightArrow;
  arrow!.classList.add('on-press');
  setTimeout(() => {
    arrow!.classList.remove('on-press');
  }, 100);

  if (dir === NuiEventType.MenuLeft) {
    currentItem.prev();
  } else {
    currentItem.next();
  }

}
</script>

<template>
  <div class="menu">
    <div class="menu-header">
      <p>{{ menu.useLocalization ? t(menu.title) : menu.title }}</p>
    </div>
    <div class="menu-items">

      <div class="selector"></div>

      <div v-for="(item, index) in menuItems || []" :key="index" :class="`menu-item ${item.enabled ? '' : 'menu-item--disabled'}`">

        <p class="title">{{ menu.useLocalization ? t(item.title) : item.title }}</p>

        <div v-if="item.type === MenuItemType.Toggle" :class="`menu-item-toggle ${(item as MenuCheckboxItem).checked ? 'checked' : ''}`">
          <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
            <path stroke-linecap="round" stroke-linejoin="round" d="M5 13l4 4L19 7" />
          </svg>
        </div>

        <div v-if="item.type === MenuItemType.List" class="menu-item-list">
          <svg xmlns="http://www.w3.org/2000/svg" :class="`left-arrow ${(item as MenuListItem).currentIndex === 0 ? 'arrow-disabled' : ''}`" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
            <path stroke-linecap="round" stroke-linejoin="round" d="M15 19l-7-7 7-7" />
          </svg>
          <div class="list-wrapper">
            <div :class="`list-wrapper-item-inner ${getMarqueeClass(item as MenuListItem)}`">
              <p>{{ (item as MenuListItem).items[(item as MenuListItem).currentIndex] }}</p>
            </div>
          </div>
          <svg xmlns="http://www.w3.org/2000/svg" :class="`right-arrow ${(item as MenuListItem).currentIndex === (item as MenuListItem).items.length-1 ? 'arrow-disabled' : ''}`" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2">
            <path stroke-linecap="round" stroke-linejoin="round" d="M9 5l7 7-7 7" />
          </svg>
        </div>

      </div>

    </div>
  </div>
</template>

<style lang="sass">
.menu
  position: absolute
  top: 50px
  background: var(--clr-bg)
  min-width: 300px
  border-radius: var(--radius)
  padding-bottom: 20px

  .menu-header
    display: flex
    flex-direction: row
    align-items: center
    justify-content: flex-start
    padding: 20px 15px

    p
      font-weight: 600
      color: var(--clr-text)

  .menu-items
    position: relative
    width: 100%
    display: flex
    flex-direction: column
    align-items: flex-start
    padding: 0 5px

    .selector
      position: absolute
      z-index: 1
      top: 0
      left: 0
      width: 100%
      height: 40px
      border-left: 3px solid var(--clr-gray)
      border-right: 3px solid var(--clr-gray)
      transition: var(--transition), border-color 0.1s cubic-bezier(0.33, 1, 0.68, 1)
      &::before
        content: ''
        position: absolute
        z-index: 1
        top: 0
        left: 0
        width: 100%
        height: 100%
        background: white
        opacity: 0.05
        transition: var(--transition)
      &.selector--on-select
        border-color: var(--clr-blue)
        &::before
          background: var(--clr-blue)

    .menu-item
      position: relative
      z-index: 2
      width: 100%
      height: 40px
      display: flex
      flex-direction: row
      align-items: center
      justify-content: space-between
      gap: 10px
      transition: var(--transition)
      margin-bottom: 5px
      padding: 0 10px
      &.menu-item--disabled
        opacity: 0.5

      .title
        color: var(--clr-text-secondary)

    .menu-item-toggle
      width: 20px
      height: 20px
      border: 2px solid var(--clr-bg-80)
      border-radius: var(--radius-sm)
      display: flex
      align-items: center
      justify-content: center
      transition: none
      &.checked
        border-color: var(--clr-text-secondary)

        svg
          opacity: 1

      svg
        height: 18px
        opacity: 0
        color: white
        transition: none

    .menu-item-list
      margin-right: -10px
      position: relative
      height: 100%
      width: calc(30px + 100px + 30px)
      display: grid
      grid: 1fr / 30px 1fr 30px
      align-items: center
      justify-items: center

      svg
        height: 22px
        color: var(--clr-text-secondary)
        transition: none
        &.arrow-disabled
          color: var(--clr-bg-80)
        &:not(.arrow-disabled).on-press
          color: var(--clr-blue)

      .list-wrapper
        position: relative
        width: 100%
        height: 100%
        overflow: hidden

        .list-wrapper-item-inner
          position: absolute
          z-index: 1
          top: 50%
          left: 50%
          transform: translate(-50%, -50%)
          display: flex
          flex-direction: row
          align-items: center
          &.marquee
            left: 50%
            transform: translateY(-50%)
            animation: marquee 4s linear infinite
            animation-delay: 1s

          p
            color: var(--clr-text-secondary)
            white-space: nowrap

@keyframes marquee
  0%
    transform: translate(0, -50%)
  100%
    transform: translate(-100%, -50%)

</style>
