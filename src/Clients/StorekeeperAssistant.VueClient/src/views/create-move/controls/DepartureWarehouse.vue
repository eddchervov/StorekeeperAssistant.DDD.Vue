<template>
  <div class="row mb-2">
    <div class="col-md-4 mb-2">
      <span class="line-h-text-by-input">Склад отправления (Расход)</span>
    </div>
    <div class="col-md-8">
      <VSelect
        :options="departureWarehouses"
        :reduce="(m) => m.id"
        :placeholder="'Выберите склад отправления'"
        label="name"
        v-model="selectDepartureWarehouseId"
      >
        <template v-slot:no-options> Не найдено </template>
      </VSelect>
    </div>
  </div>
</template>

<script lang="ts">
import { WarehouseDto } from "@/models/dto/warehouse-dto";
import actions from "@/store/actions";
import mutations from "@/store/mutations";
import { Component, Vue } from "vue-property-decorator";

@Component({})
export default class DepartureWarehouse extends Vue {
  get selectDepartureWarehouseId(): string | null {
    return this.$store.getters.selectDepartureWarehouseId;
  }
  set selectDepartureWarehouseId(warehouseId: string | null) {
    this.$store.commit(mutations.setDepartureWarehouseId, warehouseId);
    var warehouses: Array<WarehouseDto> = [];
    if (warehouseId) {
      (this.$store.getters.warehouses as Array<WarehouseDto>).forEach(
        (warehouse) => {
          if (warehouse.id != this.$store.getters.selectDepartureWarehouseId)
            warehouses.push(warehouse);
        }
      );

      this.$store.commit(
        mutations.setLoadDepartureWarehouseInventoryItems,
        true
      );
      this.$store.dispatch(actions.GetWarehouseInventoryItems, { warehouseId });
    } else warehouses = this.$store.state.warehouses;

    this.$store.commit(mutations.setArrivalWarehouses, warehouses);
  }

  get departureWarehouses(): Array<WarehouseDto> {
    return this.$store.getters.departureWarehouses;
  }
}
</script>
