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
import { CreateExpenseDto } from "@/models/dto/create-expense-dto";
import { CreateMovingDto } from "@/models/dto/create-moving-dto";
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

      if (
        this.$store.getters.selectOperation == this.$store.state.operation.MOVING
      ) {

        const addMovingDto = {
          departureWarehouseId: this.$store.getters.selectDepartureWarehouseId,
          arrivalWarehouseId: this.$store.getters.selectArrivalWarehouseId,
          inventoryItems: addInventoryItems,
        } as CreateMovingDto;

        this.$emit("createMoving", addMovingDto);
      }
      if (
        this.$store.getters.selectOperation == this.$store.state.operation.EXPENSE
      ) {

        const createExpenseDto = {
          departureWarehouseId: this.$store.getters.selectDepartureWarehouseId,
          inventoryItems: addInventoryItems,
        } as CreateExpenseDto;

        this.$emit("createExpense", createExpenseDto);
      }
    } else {
      result.messages.forEach((x) => {
        this.$store.commit(mutations.setError, { msg: x });
      });
    }

  }
}
</script>
