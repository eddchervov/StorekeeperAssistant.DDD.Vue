import { CreateMovingDto } from "@/models/dto/create-moving-dto"
import axios from "axios"
import api from "./api"
import { CreateExpenseDto } from "@/models/dto/create-expense-dto"
import { CreateIncomeDto } from "@/models/dto/create-income-dto"

export default {
  getWarehouses: () => axios.get(api.GetWarehouses),
  getInventoryItems: () => axios.get(api.GetInventoryItems),
  getMovings: (skipCount: number, takeCount: number) =>
    axios.get(api.GetMovings + "/" + skipCount + "/" + takeCount),
  getWarehouseBalanceReport: (warehouseId: string, date: string | null) =>
    axios.get(
      api.GetWarehouseBalanceReport + "/" + warehouseId + "/" + (date ?? "")
    ),
  createMoving: (createMovingDto: CreateMovingDto) =>
    axios.post(api.CreateMoving, createMovingDto),

  createExpense: (createExpenseDto: CreateExpenseDto) =>
    axios.post(api.CreateExpense, createExpenseDto),

  createIncome: (createIncomeDto: CreateIncomeDto) =>
    axios.post(api.CreateIncome, createIncomeDto),
}