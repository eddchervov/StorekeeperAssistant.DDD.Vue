import { InventoryItemDto } from "./inventory-item-dto";

export interface MovingDetailDto {
  id: string;
  inventoryItem: InventoryItemDto;
  count: number;
}
