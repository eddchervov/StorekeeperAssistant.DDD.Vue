import { MovementType } from "../enums/movement-type";
import { MovingDetailDto } from "./moving-detail-dto";
import { WarehouseDto } from "./warehouse-dto";

export interface MovingDto {
  id: string;
  movingDetails: MovingDetailDto[];
  departureWarehouse: WarehouseDto | null;
  arrivalWarehouse: WarehouseDto | null;
  transferDate: Date;
  movementType: MovementType
  movementTypeText: string
}
