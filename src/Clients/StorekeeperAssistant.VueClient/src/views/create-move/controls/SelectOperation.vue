<template>
  <div class="row mb-2">
    <div class="col-md-3 mb-2">
      <span class="line-h-text-by-input">Тип операции</span>
    </div>
    <div class="col-md-9">
      <VSelect
        :options="$store.getters.typeOperations"
        :reduce="(m) => m.id"
        :placeholder="'Выберите тип операции'"
        label="name"
        v-model="selectOperation"
      >
        <template v-slot:no-options> Не найдено </template>
      </VSelect>
    </div>
  </div>
</template>

<script lang="ts">
import mutations from "@/store/mutations"
import { Component, Vue } from "vue-property-decorator"

@Component({})
export default class SelectOperation extends Vue {
  get selectOperation(): number | null {
    return this.$store.getters.selectOperation
  }
  set selectOperation(value: number | null) {
    this.$store.commit(mutations.changeSelectOperation, value)
  }
}
</script>
