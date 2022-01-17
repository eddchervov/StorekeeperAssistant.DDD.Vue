import { WarehouseInventoryItemDto } from "../dto/warehouse-inventory-item-dto";

export interface WarehouseInventoryItemVm extends WarehouseInventoryItemDto {
  newCount: number | null;
}
