<script setup lang="ts">
const props = defineProps<{
  title: string
}>();
</script>

<template>
  <div class="page-header">
    <h1>{{ title }}</h1>
  </div>
</template>

<style scoped lang="sass">
.page-header
  position: sticky
  top: 0
  height: var(--page-header-height)
  display: flex
  flex-direction: row
  align-items: center
  justify-content: space-between
  background: var(--clr-bg)
  z-index: var(--layer-10)

  h1
    font-size: 24px
    font-weight: 700
    color: var(--clr-text-primary)

</style>
