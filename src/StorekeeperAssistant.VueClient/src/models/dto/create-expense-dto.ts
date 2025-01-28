import { AddInventoryItemDto } from "./add-inventory-item-dto"

export interface CreateExpenseDto {
  departureWarehouseId: string
  inventoryItems: AddInventoryItemDto[]
}
