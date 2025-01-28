<template>
  <div>
    <h4>Создание перемещения</h4>
    <hr class="mb-4" />
    <SelectOperation />
    <template v-if="isExpense">
      <ConsumptionForm />
    </template>
    <template v-if="isIncome">
      <ComingForm />
    </template>
    <template v-if="isMoving">
      <MovingForm />
    </template>
    <SaveForm />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator"
import ComingForm from "./controls/ComingForm.vue"
import ConsumptionForm from "./controls/ConsumptionForm.vue"
import MovingForm from "./controls/MovingForm.vue"
import SaveForm from "./controls/SaveForm.vue"
import SelectOperation from "./controls/SelectOperation.vue"
@Component({
  components: {
    SelectOperation,
    ComingForm,
    ConsumptionForm,
    MovingForm,
    SaveForm,
  },
})
export default class CreateMove extends Vue {
  get isIncome(): boolean {
    return (
      this.$store.getters.selectOperation == this.$store.state.operation.INCOME
    );
  }

  get isExpense(): boolean {
    return (
      this.$store.getters.selectOperation == this.$store.state.operation.EXPENSE
    );
  }

  get isMoving(): boolean {
    return (
      this.$store.getters.selectOperation == this.$store.state.operation.MOVING
    );
  }
}
</script>
