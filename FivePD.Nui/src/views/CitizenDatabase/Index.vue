<script setup lang="ts">
import { ClientEvents } from '@enums';
import { ISearchSubmitField, PedSearchResponse } from '@interfaces';
import { post } from '@utils';
import { ref } from 'vue';

const onSearchSubmit = async (fields: ISearchSubmitField[]) => {
  post<PedSearchResponse>(ClientEvents.PedSearch, {...fields}).subscribe(res => {
    citizens.value = res;
  });
}

const fields= [
  {
    type: 'text',
    name: 'Firstname',
    label: 'Firstname',
    placeholder: 'John'
  },
  {
    type: 'text',
    name: 'Lastname',
    label: 'Lastname',
    placeholder: 'Doe'
  },
  {
    type: 'date',
    name: 'Birthdate',
    label: 'Birthdate'
  }
];
const citizens = ref<PedSearchResponse>([]);
</script>

<template>
  <PageHeader title="Citizen database" />
  <div class="content">

    <SearchPanel
      :fields="fields"
      title="Search"
      subtitle="All fields are optional. If empty, every citizen will be displayed."
      buttonText="Search"
      @onSubmit="onSearchSubmit"
    />

    <div class="list" v-if="citizens?.length">
      <div class="list-item list-header">
        <p>Firstname</p>
        <p>Lastname</p>
        <p>Birthdate</p>
        <p>Wanted</p>
      </div>
      <router-link v-for="citizen in citizens" :key="citizen.NetworkId" :to="`/citizen-database/${citizen.NetworkId}`" class="list-item">
        <p>{{ citizen.Firstname }}</p>
        <p>{{ citizen.Lastname }}</p>
        <p>{{ citizen.Birthdate.Month }} / {{ citizen.Birthdate.Day }} / {{ citizen.Birthdate.Year }}</p>
        <p :class="citizen.wanted ? 'text-danger' : ''">{{ citizen.wanted ? 'WANTED' : 'Not wanted' }}</p>
      </router-link>
    </div>
    <p v-else class="list--empty">There are no citizens in the system.</p>

  </div>
</template>

<style scoped lang="sass">
.content
  display: grid
  grid: 1fr / 300px 1fr
  gap: 80px
  z-index: var(--layer-0)

  .list
    display: flex
    flex-direction: column
    gap: 5px
    width: 100%

    .list-item
      display: grid
      grid: 1fr / repeat(4, 1fr)
      height: 40px
      border-radius: var(--radius)
      align-items: center
      padding: 0 20px
      transition: none
      border: 1px solid transparent
      cursor: pointer
      text-decoration: none
      &:nth-child(even)
        background: var(--clr-bg-5)
      &:hover
        background: var(--clr-bg-40)
      &.list-header
        position: sticky
        top: var(--page-header-height)
        background: var(--clr-bg)
        p
          font-weight: 700
          color: var(--clr-text-primary)

      p
        font-size: 16px
        color: var(--clr-text-secondary)
        &.text-danger
          color: var(--clr-red)
          font-weight: 700

  .list--empty
    font-size: 18px
    color: var(--clr-text-primary)

</style>
