<script setup lang="ts">
import { ISearchSubmitField } from '@interfaces';

const onSearchSubmit = (fields: ISearchSubmitField[]) => {
  console.log(fields)
}

const fields= [
  {
    type: 'text',
    name: 'license_plate',
    label: 'License plate',
    placeholder: '12ABC34'
  },
  {
    type: 'text',
    name: 'owner_lastname',
    label: 'Owner\'s lastname',
    placeholder: 'John'
  },
  {
    type: 'text',
    name: 'owner_firstname',
    label: 'Owner\'s firstname',
    placeholder: 'Doe'
  }
];
</script>

<template>
  <PageHeader title="Vehicle database" />
  <div class="content">

    <SearchPanel
      :fields="fields"
      title="Search"
      subtitle="All fields are optional. If empty, every vehicle will be displayed."
      buttonText="Search"
      @onSubmit="onSearchSubmit"
    />

  </div>
</template>

<style scoped lang="sass">
.content
  display: flex
  flex-direction: row
  flex-wrap: wrap

</style>
