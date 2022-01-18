<template>
  <div>
    <div
      class="row"
      v-for="(inventoryItem, index) in inventoryItems"
      :key="inventoryItem.name + '-' + index"
    >
      <div class="col-md-6 mb-2 text-right">
        <span class="line-h-text-by-input">{{ inventoryItem.name }}</span>
      </div>
      <div class="col-md-6 mb-2">
        <input
          type="number"
          class="form-control"
          v-model.number="inventoryItem.newCount"
        />
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { InventoryItemVm } from "@/models/view-models/inventory-item-vm";
import { Vue } from "vue-property-decorator";
export default class Coming extends Vue {
  get inventoryItems(): Array<InventoryItemVm> {
    return this.$store.getters.inventoryItems;
  }
}
</script>
