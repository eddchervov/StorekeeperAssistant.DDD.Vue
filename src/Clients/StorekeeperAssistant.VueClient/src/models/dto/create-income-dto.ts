import { AddInventoryItemDto } from "./add-inventory-item-dto"

export interface CreateIncomeDto {
  arrivalWarehouseId: string
  inventoryItems: AddInventoryItemDto[]
}
