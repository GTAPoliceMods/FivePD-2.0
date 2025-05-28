<script setup lang="ts">
import { computed } from 'vue';
import type { IRoute } from '@interfaces';
import { useStore } from 'vuex';

const store = useStore();
const isSidebarCollapsed = computed(() => store.state.user.isSidebarCollapsed);
const isPanicModeActive = computed(() => store.state.user.isPanicModeActive);

const routes: IRoute[] = [
  {
    title: 'Dashboard',
    path: '/',
    icon: `
      <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor">
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2H6a2 2 0 01-2-2V6zM14 6a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2h-2a2 2 0 01-2-2V6zM4 16a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2H6a2 2 0 01-2-2v-2zM14 16a2 2 0 012-2h2a2 2 0 012 2v2a2 2 0 01-2 2h-2a2 2 0 01-2-2v-2z" />
      </svg>`
  },
  {
    title: 'Citizen database',
    path: '/citizen-database',
    icon: `
      <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor">
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10 6H5a2 2 0 00-2 2v9a2 2 0 002 2h14a2 2 0 002-2V8a2 2 0 00-2-2h-5m-4 0V5a2 2 0 114 0v1m-4 0a2 2 0 104 0m-5 8a2 2 0 100-4 2 2 0 000 4zm0 0c1.306 0 2.417.835 2.83 2M9 14a3.001 3.001 0 00-2.83 2M15 11h3m-3 4h2" />
      </svg>`
  },
  {
    title: 'Vehicle database',
    path: '/vehicle-database',
    icon: `
      <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
        <path d="M9 17a2 2 0 11-4 0 2 2 0 014 0zM19 17a2 2 0 11-4 0 2 2 0 014 0z" />
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 16V6a1 1 0 00-1-1H4a1 1 0 00-1 1v10a1 1 0 001 1h1m8-1a1 1 0 01-1 1H9m4-1V8a1 1 0 011-1h2.586a1 1 0 01.707.293l3.414 3.414a1 1 0 01.293.707V16a1 1 0 01-1 1h-1m-6-1a1 1 0 001 1h1M5 17a2 2 0 104 0m-4 0a2 2 0 114 0m6 0a2 2 0 104 0m-4 0a2 2 0 114 0" />
      </svg>`
  },
  {
    title: 'Warrants',
    path: '/warrants',
    icon: `
      <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M20.618 5.984A11.955 11.955 0 0112 2.944a11.955 11.955 0 01-8.618 3.04A12.02 12.02 0 003 9c0 5.591 3.824 10.29 9 11.622 5.176-1.332 9-6.03 9-11.622 0-1.042-.133-2.052-.382-3.016zM12 9v2m0 4h.01" />
      </svg>`
  },
  {
    title: 'Investigations',
    path: '/investigations',
    icon: `
      <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
      </svg>`
  },
  {
    title: 'Settings',
    path: '/settings',
    icon: `
      <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10.325 4.317c.426-1.756 2.924-1.756 3.35 0a1.724 1.724 0 002.573 1.066c1.543-.94 3.31.826 2.37 2.37a1.724 1.724 0 001.065 2.572c1.756.426 1.756 2.924 0 3.35a1.724 1.724 0 00-1.066 2.573c.94 1.543-.826 3.31-2.37 2.37a1.724 1.724 0 00-2.572 1.065c-.426 1.756-2.924 1.756-3.35 0a1.724 1.724 0 00-2.573-1.066c-1.543.94-3.31-.826-2.37-2.37a1.724 1.724 0 00-1.065-2.572c-1.756-.426-1.756-2.924 0-3.35a1.724 1.724 0 001.066-2.573c-.94-1.543.826-3.31 2.37-2.37.996.608 2.296.07 2.572-1.065z" />
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
      </svg>`
  },
  {
    title: 'Department',
    path: '/department',
    icon: `
      <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M17 20h5v-2a3 3 0 00-5.356-1.857M17 20H7m10 0v-2c0-.656-.126-1.283-.356-1.857M7 20H2v-2a3 3 0 015.356-1.857M7 20v-2c0-.656.126-1.283.356-1.857m0 0a5.002 5.002 0 019.288 0M15 7a3 3 0 11-6 0 3 3 0 016 0zm6 3a2 2 0 11-4 0 2 2 0 014 0zM7 10a2 2 0 11-4 0 2 2 0 014 0z" />
      </svg>`
  },
];
</script>

<template>
  <aside :class="isSidebarCollapsed ? 'collapsed' : ''">

    <div class="links">
      <router-link v-for="item in routes" :to="item.path" :key="item.path" class="link" active-class="active">
        <div class="icon" v-html="item.icon"></div>
        <p>{{ item.title }}</p>
      </router-link>
    </div>

    <div class="buttons">
      <button
        :class="`panic-btn ${isPanicModeActive ? 'panic-btn--active' : ''}`"
        @click="store.commit('togglePanicMode')"
      >
        <span v-if="!isSidebarCollapsed">{{ isPanicModeActive ? 'cancel' : 'panic' }}</span>
        <svg v-else-if="isSidebarCollapsed && !isPanicModeActive" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z" />
        </svg>
        <svg v-else xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
        </svg>
      </button>

      <button @click="store.commit('toggleSidebarCollapse')" :class="`collapse-btn ${isSidebarCollapsed ? 'collapsed' : ''}`">
        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 16l-4-4m0 0l4-4m-4 4h18" />
        </svg>
        <span>Collapse</span>
      </button>
    </div>

  </aside>
</template>

<style scoped lang="sass">
aside
  width: 100%
  height: 100%
  border-right: 1px solid var(--clr-border)
  display: flex
  flex-direction: column
  align-items: center
  justify-content: space-between
  padding: 40px 20px 20px 20px

  .links
    width: 100%

    .link
      position: relative
      display: flex
      flex-direction: row
      align-items: center
      width: 100%
      height: 50px
      margin-bottom: 20px
      text-decoration: none
      &::before
        content: ''
        position: absolute
        top: 0
        left: 0
        width: 100%
        height: 100%
        border-radius: var(--radius)
        background: var(--clr-gray)
        opacity: 0
      &:hover::before
        opacity: .1
      &.active
        opacity: 1
        &::before
          opacity: .2
        p, .icon:deep(svg)
          color: var(--clr-text)

      .icon
        width: 50px
        height: 100%
        display: flex
        align-items: center
        justify-content: center
        &:deep(svg)
          height: 25px
          color: var(--clr-gray)

      p
        font-size: 16px
        font-weight: 500
        color: var(--clr-gray)
        white-space: nowrap

  .buttons
    width: 100%

    button
      position: relative
      width: 100%
      height: 50px
      background: transparent
      border: none
      cursor: pointer
      display: flex
      flex-direction: row
      align-items: center
      justify-content: center
      margin-bottom: 10px
      &::before
        content: ''
        position: absolute
        top: 0
        left: 0
        width: 100%
        height: 100%
        border-radius: var(--radius)
        opacity: .1
      &:hover
        &::before
          opacity: .2
      &.panic-btn--active:not(:hover)
        &::before
          transition: none
          animation: panic .4s ease infinite

      span, svg
        font-size: 18px
        font-weight: 500
        letter-spacing: 5px
        text-transform: uppercase

      svg
        height: 25px

      &.panic-btn
        &::before
          background: var(--clr-red)
        span, svg
          color: var(--clr-red)
      &.collapse-btn
        &::before
          background: var(--clr-gray)
        span, svg
          // font-size: 16px
          letter-spacing: 0.2px
          text-transform: none
          color: var(--clr-gray)
        svg
          margin-right: 10px
        &.collapsed
          span
            display: none
          svg
            margin: 0
            transform: rotate(180deg)

  &.collapsed
    .links
      .link
        p
          display: none

@keyframes panic
  0%
    opacity: 0
  50%
    opacity: .2
  100%
    opacity: 0

</style>
