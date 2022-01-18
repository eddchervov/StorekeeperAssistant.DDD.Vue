<template>
  <div>
    <DepartureWarehouse />
    <template v-if="isDepartureWarehouseInventoryItems">
      <DepartureWarehouseInventoryItems />
    </template>

    <ArrivalWarehouse />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import ArrivalWarehouse from "./ArrivalWarehouse.vue";
import DepartureWarehouse from "./DepartureWarehouse.vue";
import DepartureWarehouseInventoryItems from "./DepartureWarehouseInventoryItems.vue";

@Component({
  components: {
    DepartureWarehouse,
    DepartureWarehouseInventoryItems,
    ArrivalWarehouse,
  },
})
export default class MovingForm extends Vue {
  get isDepartureWarehouseInventoryItems(): boolean {
    return (
      Boolean(this.$store.getters.selectDepartureWarehouseId) &&
      this.$store.getters.departureWarehouses.length > 0
    );
  }
}
</script>
