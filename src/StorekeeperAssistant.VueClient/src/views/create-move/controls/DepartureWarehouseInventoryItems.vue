<template>
  <div class="row mb-2">
    <div class="col-md-12 mb-3">
      <span class="line-h-text-by-input"><b>Доступные лимиты склада</b></span>
    </div>

    <div
      class="col-md-12 text-primary"
      v-if="isLoadDepartureWarehouseInventoryItems"
    >
      Загрузка...
    </div>

    <template v-if="isExistDepartureWarehouseInventoryItems">
      <template v-for="(ii, index) in departureWarehouseInventoryItems">
        <div class="col-md-6 col-xl-4 mb-2" :key="ii.id + '_1_' + index">
          <span class="line-h-text-by-input"
            >{{ ii.inventoryItem.name }} ({{ ii.count }})</span
          >
        </div>
        <div class="col-md-6 col-xl-8 mb-2" :key="ii.id + '_2_' + index">
          <input
            type="number"
            class="form-control"
            v-model.number="ii.newCount"
          />
        </div>
      </template>
    </template>

    <template v-if="isNotExistDepartureWarehouseInventoryItems">
      <div class="col-8">У склада нет ТМЦ</div>
    </template>
  </div>
</template>

<script lang="ts">
import { WarehouseInventoryItemVm } from "@/models/view-models/warehouse-inventory-item-vm";
import { Component, Vue } from "vue-property-decorator";

@Component({})
export default class DepartureWarehouseInventoryItems extends Vue {
  get isExistDepartureWarehouseInventoryItems(): boolean {
    return (
      this.isMovingOrConsumption &&
      this.isLoadDepartureWarehouseInventoryItems == false &&
      this.departureWarehouseInventoryItems.length > 0
    );
  }
  get isNotExistDepartureWarehouseInventoryItems(): boolean {
    return (
      this.isLoadDepartureWarehouseInventoryItems == false &&
      this.departureWarehouseInventoryItems.length == 0
    );
  }
  get isLoadDepartureWarehouseInventoryItems(): boolean {
    return this.$store.getters.isLoadDepartureWarehouseInventoryItems;
  }
  get isMovingOrConsumption(): boolean {
    return (
      this.$store.getters.selectOperation ==
        this.$store.state.operation.MOVING ||
      this.$store.getters.selectOperation ==
        this.$store.state.operation.CONSUMPTION
    );
  }
  get departureWarehouseInventoryItems(): Array<WarehouseInventoryItemVm> {
    return this.$store.getters.departureWarehouseInventoryItems;
  }
}
</script>