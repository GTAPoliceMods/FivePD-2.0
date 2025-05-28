<script setup lang="ts">
import { type Ref, ref } from 'vue';
import { hideNui } from '@utils';

const username: string = 'Cam';
const currentStatus: Ref<string> = ref('10-0');
const isStatusSelectOpened: Ref<boolean> = ref(false);

const statusList = ['10-1', '10-2', '10-3', '10-4'];

const toggleStatusSelect = () => {
  isStatusSelectOpened.value = !isStatusSelectOpened.value;
}

const closeStatusSelect = () => {
  isStatusSelectOpened.value = false;
}

const changeStatus = (status: string) => {
  toggleStatusSelect();
  currentStatus.value = status;
}
</script>

<template>
  <header>
    <div class="department">
      <img src="" alt="">
      <p>LSPD</p>
    </div>
    <div class="side-row">
      <div :class="`status-select ${isStatusSelectOpened ? 'status-select--open' : ''}`" v-click-outside="closeStatusSelect">
        <button class="status-select-btn" @click="toggleStatusSelect()">
          <p>Status: {{ currentStatus }}</p>
          <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7" />
          </svg>
        </button>
        <div class="status-select-list" v-if="isStatusSelectOpened">
          <button v-for="status in statusList" :key="status" @click="changeStatus(status)">{{ status }}</button>
        </div>
      </div>
      <p class="user">Logged in as <b>{{ username }}</b></p>
      <button class="close-btn" @click="hideNui()">
        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M17 16l4-4m0 0l-4-4m4 4H7m6 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h4a3 3 0 013 3v1" />
        </svg>
      </button>
    </div>
  </header>
</template>

<style scoped lang="sass">
header
  position: relative
  z-index: var(--layer-100)
  width: 100%
  height: 80px
  border-bottom: 1px solid var(--clr-border)
  display: flex
  flex-direction: row
  align-items: center
  justify-content: space-between

  .department
    display: flex
    flex-direction: row
    align-items: center
    margin-left: 20px

    p
      color: var(--clr-text)
      font-size: 18px
      font-weight: 700

  .side-row
    display: flex
    flex-direction: row
    align-items: center
    height: 100%

    .status-select
      position: relative

      .status-select-btn
        position: relative
        display: flex
        flex-direction: row
        align-items: center
        justify-content: center
        height: 40px
        padding: 0 20px
        background: transparent
        border: none
        cursor: pointer
        &::before
          content: ''
          position: absolute
          top: 0
          left: 0
          width: 100%
          height: 100%
          border-radius: var(--radius)
          background: var(--clr-blue)
          opacity: .1
        &:hover::before
          opacity: .2

        p
          white-space: nowrap
          font-size: 16px
          font-weight: 700
          color: var(--clr-blue)

        svg
          height: 20px
          margin-left: 10px
          color: var(--clr-blue)

      .status-select-list
        position: absolute
        top: 100%
        left: 0
        width: 100%
        display: flex
        flex-direction: column
        align-items: center

        button
          position: relative
          width: 100%
          height: 40px
          border: none
          font-size: 16px
          color: var(--clr-blue)
          background: transparent
          cursor: pointer
          background: var(--clr-bg)
          border-bottom-left-radius: var(--radius)
          border-bottom-right-radius: var(--radius)
          &::before
            content: ''
            position: absolute
            top: 0
            left: 0
            width: 100%
            height: 100%
            background: var(--clr-blue)
            opacity: .1
          &:hover::before
            opacity: .2
          &:last-of-type::before
            border-bottom-left-radius: var(--radius)
            border-bottom-right-radius: var(--radius)

      &.status-select--open
        .status-select-btn
          &::before
            border-bottom-left-radius: 0
            border-bottom-right-radius: 0
          svg
            transform: rotate(180deg)

    .user
      font-size: 16px
      font-weight: 300
      color: var(--clr-text)
      padding: 0 40px

    .close-btn
      width: 80px
      height: 100%
      display: flex
      align-items: center
      justify-content: center
      border: none
      border-left: 1px solid var(--clr-border)
      background: transparent
      cursor: pointer
      &:hover svg
        color: var(--clr-text)

      svg
        height: 30px
        color: var(--clr-gray)

</style>
