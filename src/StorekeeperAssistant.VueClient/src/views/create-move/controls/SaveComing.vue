<template>
  <div>
    <button
      class="btn btn-secondary"
      @click="click_moving"
      :disabled="isCreateMoving"
    >
      {{ text }}
    </button>
  </div>
</template>


<script lang="ts">
import { AddInventoryItemDto } from "@/models/dto/add-inventory-item-dto";
import { AddMovingDto } from "@/models/dto/add-moving-dto";
import { InventoryItemVm } from "@/models/view-models/inventory-item-vm";
import { validationComing } from "@/store/moving-create-validation";
import mutations from "@/store/mutations";
import { Component, Prop, Vue } from "vue-property-decorator";

@Component({
  components: {},
})
export default class SaveComing extends Vue {
  @Prop() readonly text!: string;

  get isCreateMoving(): boolean {
    return this.$store.getters.isCreateMoving;
  }

  getNewInventoryItems(): Array<InventoryItemVm> {
    const inventoryItems = this.$store.getters
      .inventoryItems as Array<InventoryItemVm>;
    const newInventoryItems: Array<InventoryItemVm> = [];
    inventoryItems.forEach((inventoryItem) => {
      if (inventoryItem.newCount) newInventoryItems.push(inventoryItem);
    });
    return newInventoryItems;
  }

  mapToDto(inventoryItems: Array<InventoryItemVm>): Array<AddInventoryItemDto> {
    const addInventoryItemDtos: Array<AddInventoryItemDto> = [];
    inventoryItems.forEach((inventoryItem) => {
      if (inventoryItem.newCount)
        addInventoryItemDtos.push({
          id: inventoryItem.id,
          count: inventoryItem.newCount,
        } as AddInventoryItemDto);
    });
    return addInventoryItemDtos;
  }

  click_moving(): void {
    const inventoryItems = this.getNewInventoryItems();
    var result = validationComing(
      inventoryItems,
      this.$store.state.maxValueInventoryItem
    );
    if (result.isError == false) {
      var addInventoryItems = this.mapToDto(inventoryItems);
      const addMovingDto = {
        departureWarehouseId: null,
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