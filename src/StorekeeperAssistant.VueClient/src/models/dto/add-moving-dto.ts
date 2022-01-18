import { AddInventoryItemDto } from "./add-inventory-item-dto";

export interface AddMovingDto {
  departureWarehouseId: string | null;
  arrivalWarehouseId: string | null;
  inventoryItems: AddInventoryItemDto[];
}
