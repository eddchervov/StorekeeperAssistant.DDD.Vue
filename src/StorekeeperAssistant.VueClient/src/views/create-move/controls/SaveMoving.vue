<template>
  <div>
    <button
      class="btn btn-secondary"
      @click="click_moving"
      :disabled="isLoadDepartureWarehouseInventoryItems || isCreateMoving"
    >
      {{ text }}
    </button>
  </div>
</template>

<script lang="ts">
import { AddInventoryItemDto } from "@/models/dto/add-inventory-item-dto";
import { AddMovingDto } from "@/models/dto/add-moving-dto";
import { WarehouseInventoryItemVm } from "@/models/view-models/warehouse-inventory-item-vm";
import { validationMoving } from "@/store/moving-create-validation";
import mutations from "@/store/mutations";
import { Component, Prop, Vue } from "vue-property-decorator";

@Component({
  components: {},
})
export default class SaveMoving extends Vue {
  @Prop() readonly text!: string;

  get isCreateMoving(): boolean {
    return this.$store.getters.isCreateMoving;
  }

  get isLoadDepartureWarehouseInventoryItems(): boolean {
    return this.$store.getters.isLoadDepartureWarehouseInventoryItems;
  }

  click_moving(): void {
    const departureWarehouseInventoryItems = this.$store.getters
      .departureWarehouseInventoryItems as Array<WarehouseInventoryItemVm>;

    const result = validationMoving(departureWarehouseInventoryItems);
    if (result.isError == false) {
      const addInventoryItems: Array<AddInventoryItemDto> = [];
      departureWarehouseInventoryItems.forEach((warehouseInventoryItem) => {
        if (warehouseInventoryItem.newCount)
          addInventoryItems.push({
            id: warehouseInventoryItem.inventoryItem.id,
            count: warehouseInventoryItem.newCount,
          } as AddInventoryItemDto);
      });

      if (addInventoryItems.length == 0) return;

      const addMovingDto = {
        departureWarehouseId: this.$store.getters.selectDepartureWarehouseId,
        arrivalWarehouseId: this.$store.getters.selectArrivalWarehouseId,
        inventoryItems: addInventoryItems,
      } as AddMovingDto;

      this.$emit("saved", addMovingDto);
    } else
      result.messages.forEach((x) => {
        this.$store.commit(mutations.setError, { msg: x });
      });
  }
}
</script>
