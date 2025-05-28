<script setup lang="ts">
import { IDateInput, ISearchSubmitField } from '@interfaces';
import { createBirthdateInputList } from '@utils';
import { DateFormat } from '@enums';

const emit = defineEmits(['onSubmit']);

const props = defineProps<{
  fields: any,
  title: string,
  subtitle?: string,
  buttonText: string
}>();

const onSearchSubmit = (event: any) => {

  const fields: any = {};

  for (const e of event.target.elements) {
    if (!e.name || !e.value) continue;
    if (['Day', 'Month', 'Year'].includes(e.name)) {
      if (!fields.hasOwnProperty('Birthdate')) {
        fields['Birthdate'] = {};
      }
      fields['Birthdate'][e.name] = e.value;
    } else {
      fields[e.name] = e.value;
    }
  }

  emit('onSubmit', fields);
}

const birthdateInputList: IDateInput[] = createBirthdateInputList(DateFormat.MDY);
</script>

<template>
  <form @submit.prevent="onSearchSubmit">

    <p class="form-title">{{ title }}</p>
    <p class="form-subtitle">{{ subtitle }}</p>

    <div class="input-box" v-for="field in fields">
      <label :for="field.name">{{ field.label }}</label>
      <input
        type="text"
        :name="field.name"
        :id="field.name"
        :placeholder="field.placeholder"
        v-if="field.type !== 'date'"
      >
      <div class="inputs" v-else>
        <input type="number" min="0" :name="input.name" id="birthdate" :placeholder="input.placeholder" v-for="input in birthdateInputList">
      </div>
    </div>

    <button><span>{{ buttonText }}</span></button>
  </form>
</template>

<style scoped lang="sass">
form
  position: relative
  padding: 20px
  background: var(--clr-bg-secondary)
  border-radius: var(--radius)
  display: flex
  flex-direction: column
  align-items: flex-start
  max-width: 300px
  height: max-content
  position: sticky
  top: var(--page-header-height)
  border: 1px solid var(--clr-border-secondary)

  .form-title
    font-size: 22px
    font-weight: 700
    color: var(--clr-text-primary)
    margin-bottom: 5px

  .form-subtitle
    font-size: 16px
    color: var(--clr-text-secondary)
    margin-bottom: 25px

  .input-box
    width: 100%
    margin-bottom: 20px

    label
      display: block
      font-size: 16px
      color: var(--clr-text-secondary)
      margin-bottom: 5px

    input
      height: 40px
      width: 100%
      border: 1px solid var(--clr-bg-40)
      background: transparent
      border-radius: var(--radius-sm)
      padding: 0 15px
      font-size: 14px
      color: var(--clr-text-secondary)
      &::placeholder
        color: var(--clr-text-secondary)
        opacity: .4
      &:focus
        border-color: var(--clr-gray)

    .inputs
      display: grid
      grid: 1fr / repeat(3, 1fr)
      gap: 5px

      input
        &::-webkit-outer-spin-button, &::-webkit-inner-spin-button
          appearance: none
          -webkit-appearance: none

  button
    width: 100%
    height: 40px
    border: none
    border-radius: var(--radius-sm)
    font-size: 16px
    color: white
    cursor: pointer
    position: relative
    background: transparent
    &::before
      content: ''
      position: absolute
      top: 0
      left: 0
      width: 100%
      height: 100%
      border-radius: var(--radius)
      opacity: 1
      background: var(--clr-blue)
      z-index: var(--layer-0)
    &:hover
      &::before
        opacity: .8
    &:hover
      background: var(--clr-blue-light)

    span
      position: relative
      z-index: var(--layer-10)
</style>
