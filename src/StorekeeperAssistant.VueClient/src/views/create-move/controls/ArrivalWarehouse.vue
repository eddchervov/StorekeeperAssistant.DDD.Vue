<template>
  <div class="row mb-2">
    <div class="col-md-4 mb-2">
      <span class="line-h-text-by-input">Склад прибытия (Приход)</span>
    </div>
    <div class="col-md-8">
      <VSelect
        :options="arrivalWarehouses"
        :reduce="(m) => m.id"
        placeholder="Выберите склад прибытия"
        label="name"
        v-model="selectArrivalWarehouseId"
      >
        <template v-slot:no-options> Не найдено </template>
      </VSelect>
    </div>
  </div>
</template>

<script lang="ts">
import { WarehouseDto } from "@/models/dto/warehouse-dto";
import mutations from "@/store/mutations";
import { Component, Vue } from "vue-property-decorator";
@Component({})
export default class ArrivalWarehouse extends Vue {
  get selectArrivalWarehouseId(): string | null {
    return this.$store.getters.selectArrivalWarehouseId;
  }
  set selectArrivalWarehouseId(value: string | null) {
    this.$store.commit(mutations.setArrivalWarehouseId, value);

    let warehouses: Array<WarehouseDto> = [];
    if (value)
      (this.$store.getters.warehouses as Array<WarehouseDto>).forEach(
        (warehouse) => {
          if (warehouse.id != this.$store.getters.selectArrivalWarehouseId)
            warehouses.push(warehouse);
        }
      );
    else warehouses = this.$store.getters.warehouses;

    this.$store.commit(mutations.setDepartureWarehouses, warehouses);
  }

  get arrivalWarehouses(): Array<WarehouseDto> {
    return this.$store.getters.arrivalWarehouses;
  }
}
</script>