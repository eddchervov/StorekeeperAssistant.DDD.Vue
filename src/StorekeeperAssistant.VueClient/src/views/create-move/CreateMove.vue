<template>
  <div>
    <h4>Создание перемещения</h4>
    <hr class="mb-4" />

    <SelectOperation />
    <template v-if="isComing">
      <ComingForm />
    </template>
    <template v-if="isConsumption">
      <ConsumptionForm />
    </template>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import ComingForm from "./controls/ComingForm.vue";
import ConsumptionForm from "./controls/ConsumptionForm.vue";
import SelectOperation from "./controls/SelectOperation.vue";
@Component({
  components: {
    SelectOperation,
    ComingForm,
    ConsumptionForm,
  },
})
export default class CreateMove extends Vue {
  get isComing(): boolean {
    return (
      this.$store.getters.selectOperation == this.$store.state.operation.COMING
    );
  }

  get isConsumption(): boolean {
    return (
      this.$store.getters.selectOperation ==
      this.$store.state.operation.CONSUMPTION
    );
  }
}
</script>