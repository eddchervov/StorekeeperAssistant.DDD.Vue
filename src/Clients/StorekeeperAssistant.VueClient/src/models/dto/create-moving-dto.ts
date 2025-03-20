import { AddInventoryItemDto } from "./add-inventory-item-dto"

export interface CreateMovingDto {
  departureWarehouseId: string
  arrivalWarehouseId: string
  inventoryItems: AddInventoryItemDto[]
}
