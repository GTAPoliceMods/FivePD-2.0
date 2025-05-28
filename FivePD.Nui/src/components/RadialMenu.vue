<script setup lang="ts">
import { NuiEventType } from '@enums';
import type { IMenu, MenuShowEvent, RadialMenuControlEvent } from '@interfaces';
import { onNuiEvent, post } from '@utils';
import { Subscription } from 'rxjs';
import { onMounted, ref, onBeforeUnmount } from 'vue';
import { useStore } from 'vuex';

interface MousePosition {
  x: number,
  y: number
}

const props = defineProps<{
  menuId: string
}>();

const menuShowEvent$ = ref<Subscription>();
const menuControlEvent$ = ref<Subscription>();

const store = useStore();
const menus: IMenu[] = store.state.global.radialMenus;

const canvas = ref<HTMLCanvasElement | null>(null);
const selectedMenu = ref<IMenu | null>(null);
const selectedItemIndex = ref<number>(-1);
const currentMousePosition = ref<MousePosition>({ x: 0, y: 0 });
const lastMousePosition = ref<MousePosition>({ x: 0, y: 0 });
const lastSelectAngle = ref<number>(-1);

onMounted(() => {
  findSelectedMenu(props.menuId);
  drawCanvas();

  menuShowEvent$.value = onNuiEvent<MenuShowEvent>(NuiEventType.RadialMenu).subscribe(event => {
    resetValues();
    findSelectedMenu(event.menuId);
    drawCanvas();
  });

  menuControlEvent$.value = onNuiEvent<RadialMenuControlEvent>(NuiEventType.RadialMenuControl).subscribe(event => {
    if (event.control === 'up' || event.control === 'down') {
      if (event.x && event.y) {
        currentMousePosition.value = {
          x: event.x,
          y: event.y,
        };
        onMouseMove();
      }
    } else if (event.control === 'select') {
      if (selectedItemIndex.value !== -1) {
        post(`fivepd:radialMenu:trigger_item_${selectedMenu.value?.Items[selectedItemIndex.value].Hashcode}`).subscribe();
        resetValues();
      }
    }
  });

});

onBeforeUnmount(() => {
  menuShowEvent$.value?.unsubscribe();
  menuControlEvent$.value?.unsubscribe();
});

const resetValues = () => {
  lastSelectAngle.value = -1;
  selectedItemIndex.value = -1;
  currentMousePosition.value = { x: 0, y: 0 };
  lastMousePosition.value = { x: 0, y: 0 };
}

const findSelectedMenu = (id: string) => {
  selectedMenu.value = menus.find((x: IMenu) => x.Id === id)!;
}

const onMouseMove = () => {

  // Compute the change from last time around
  const deltaX = currentMousePosition.value.x - lastMousePosition.value.x;
  const deltaY = currentMousePosition.value.y - lastMousePosition.value.y;

  // Take the inverse tangent of the movement
  let angle = Math.atan2(deltaY, deltaX);

  // atan2 returns on -pi through pi, but we want 0 through 2pi. Convert!
  if (angle < 0) {
    angle += (2 * Math.PI);
  }

  // Check if this was a deliberate movement. Small changes can be ignored for now
  if (deltaX > 20 || deltaX < -20 || deltaY > 20 || deltaY < -20) {
    // Update the angles and redraw!
    lastMousePosition.value = currentMousePosition.value;
    lastSelectAngle.value = angle;
    drawCanvas();
  }
}

const drawCanvas = () => {
  if (!selectedMenu.value || !canvas.value) return;

  const menu: IMenu = selectedMenu.value;

  // All of the configuration stuff for canvas (2d context)
  const ctx: CanvasRenderingContext2D  = canvas.value.getContext('2d')!;
  ctx.resetTransform();
  ctx.clearRect(0, 0, canvas.value.width, canvas.value.height);
  ctx.textAlign = 'center';
  ctx.font = '16px Arial';

  // Configuration
  const TOTAL_RADIUS = canvas.value.width / 2;
  const SECTION_DEPTH = 100; // How deep should each section appear?

  // Normally the origin is in the top left. Move it to the middle.
  ctx.translate(canvas.value.width / 2, canvas.value.height / 2);

  // Calculate the arc length of each section
  const arcLength = 2 * Math.PI / menu.Items.length;

  // Calculate the average radius of each section
  const midRadius = TOTAL_RADIUS - (SECTION_DEPTH / 2);

  // Increment through each section
  for (let i = 0; i < menu.Items.length; i++) {

    // Calculate angle for start and stop
    const start = arcLength * i;
    const stop = arcLength * (i + 1);

    // Uncomment for aim assist!
    ctx.beginPath();
    ctx.moveTo(0, 0);
    ctx.lineTo(50 * Math.cos(lastSelectAngle.value), 50 * Math.sin(lastSelectAngle.value));
    ctx.stroke();

    // Draw arcs
    ctx.beginPath();
    ctx.arc(0, 0, TOTAL_RADIUS, start, stop);
    ctx.arc(0, 0, TOTAL_RADIUS - SECTION_DEPTH, stop, start, true);
    ctx.stroke();

    // Calculate angle of section middle
    const middle = arcLength * (i + 0.5);

    // Show the selected item as being special
    if (lastSelectAngle.value >= start && lastSelectAngle.value < stop) {
      ctx.fillStyle = 'red';
      selectedItemIndex.value = i;
    } else {
      ctx.fillStyle = 'black';
    }

    // Draw demo text in middle(ish) of each box
    ctx.fillText(menu.Items[i].Title, midRadius * Math.cos(middle), midRadius * Math.sin(middle));
  }
}
</script>

<template>
  <div>
    <canvas width="600" height="600" ref="canvas"></canvas>
  </div>
</template>

<style lang="sass" scoped>
div
  position: absolute
  top: 0
  left: 0
  height: 100%
  width: 100%
  background: rgba(50, 50, 50, 0.3)

canvas
  position: absolute
  top: 50%
  left: 50%
  transform: translate(-50%,-50%)
</style>
