<template>
  <div>
    <DepartureWarehouse />

    <template v-if="isDepartureWarehouseInventoryItems">
      <DepartureWarehouseInventoryItems />
    </template>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import DepartureWarehouse from "./DepartureWarehouse.vue";
import DepartureWarehouseInventoryItems from "./DepartureWarehouseInventoryItems.vue";

@Component({
  components: {
    DepartureWarehouse,
    DepartureWarehouseInventoryItems,
  },
})
export default class ConsumptionForm extends Vue {
  get isDepartureWarehouseInventoryItems(): boolean {
    return (
      Boolean(this.$store.getters.selectDepartureWarehouseId) &&
      this.$store.getters.departureWarehouses.length > 0
    );
  }
}
</script>
