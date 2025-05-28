<script setup lang="ts">
import { hideNui, showNui } from "@utils";
import { onMounted, ref, onBeforeUnmount } from "vue";

const showTimerBar = ref<boolean>(false);
const title = ref<string>("");
const interval = ref<NodeJS.Timer>();
const percentage = ref<number>(0);

onMounted(() => {
  window.addEventListener("message", onNuiEvent);
});

onBeforeUnmount(() => {
  window.removeEventListener("message", onNuiEvent);
});

const onNuiEvent = (event: any) => {
  if (!event.data.hasOwnProperty("showTimerBar")) return;

  showTimerBar.value = event.data.showTimerBar;

  if (showTimerBar.value) {
    showNui();
    title.value = event.data.timerBarTitle;
    const addToPercentage = 1000 / event.data.timerBarTimeToHold;
    interval.value = setInterval(() => {
      percentage.value += addToPercentage;
    }, 1);
  } else {
    const intervalId: number = interval.value as unknown as number;
    clearInterval(intervalId);
    percentage.value = 0;
  }
};
</script>

<template>
  <div class="timerbar" v-if="showTimerBar">
    <p class="title">{{ title }}</p>
    <div class="timerbar-inner">
      <div :style="`width: ${percentage}%`"></div>
    </div>
  </div>
</template>

<style scoped lang="sass">
.timerbar
  position: absolute
  bottom: 20px
  right: 20px
  display: flex
  flex-direction: row
  align-items: center
  border-radius: var(--radius-sm)
  background: var(--clr-bg)
  padding: 10px 15px
  gap: 15px

  .title
    font-size: 20px
    color: white

  .timerbar-inner
    overflow: hidden
    height: 20px
    width: 200px
    border-radius: var(--radius-sm)
    border: 1px solid var(--clr-border)

    div
      height: 100%
      background: var(--clr-blue)

</style>
