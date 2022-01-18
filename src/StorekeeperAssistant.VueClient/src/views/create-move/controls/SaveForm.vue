<template>
  <div class="row mt-3">
    <div class="col-12 text-end">
      <template v-if="isMovingAndSelectDepartureAndArrival">
        <SaveMoving :text="'Переместить'" @saved="saveMoving" />
      </template>

      <template v-if="isConsumptionAndSelectDeparture">
        <SaveMoving :text="'Убрать со склада'" @saved="saveMoving" />
      </template>

      <template v-if="isComingAndSelectArrival">
        <SaveComing :text="'Добавить на склад'" @saved="saveMoving" />
      </template>
    </div>
  </div>
</template>


<script lang="ts">
import { AddMovingDto } from "@/models/dto/add-moving-dto";
import api from "@/store/api";
import { Component, Vue } from "vue-property-decorator";
import SaveComing from "./SaveComing.vue";
import SaveMoving from "./SaveMoving.vue";

@Component({
  components: {
    SaveMoving,
    SaveComing,
  },
})
export default class SaveForm extends Vue {
  get isMovingAndSelectDepartureAndArrival(): boolean {
    return (
      this.$store.getters.selectOperation ==
        this.$store.state.operation.MOVING &&
      Boolean(this.$store.getters.selectDepartureWarehouseId) &&
      Boolean(this.$store.getters.selectArrivalWarehouseId)
    );
  }

  get isConsumptionAndSelectDeparture(): boolean {
    return (
      this.$store.getters.selectOperation ==
        this.$store.state.operation.CONSUMPTION &&
      Boolean(this.$store.state.selectDepartureWarehouseId)
    );
  }

  get isComingAndSelectArrival(): boolean {
    return (
      this.$store.getters.selectOperation ==
        this.$store.state.operation.COMING &&
      Boolean(this.$store.state.selectArrivalWarehouseId)
    );
  }

  async saveMoving(addMovingDto: AddMovingDto): Promise<void> {
    await this.$store.dispatch(api.CreateMoving, addMovingDto);
  }
}
</script>