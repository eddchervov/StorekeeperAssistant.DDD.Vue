<template>
  <div class="row mt-3">
    <div class="col-12 text-end">
      <template v-if="isMovingAndSelectDepartureAndArrival">
        <SaveMoving :text="'Переместить'" @createMoving="createMoving" />
      </template>

      <template v-if="isConsumptionAndSelectDeparture">
        <SaveMoving :text="'Убрать со склада'" @createExpense="createExpense" />
      </template>

      <template v-if="isComingAndSelectArrival">
        <SaveComing :text="'Добавить на склад'" @createIncome="createIncome" />
      </template>
    </div>
  </div>
</template>

<script lang="ts">
import { CreateMovingDto } from "@/models/dto/create-moving-dto";
import actions from "@/store/actions";
import { Component, Vue } from "vue-property-decorator";
import SaveComing from "./SaveComing.vue";
import SaveMoving from "./SaveMoving.vue";
import { CreateExpenseDto } from "@/models/dto/create-expense-dto";
import { CreateIncomeDto } from "@/models/dto/create-income-dto";

@Component({
  components: {
    SaveMoving,
    SaveComing
  },
})
export default class SaveForm extends Vue {
  get isMovingAndSelectDepartureAndArrival(): boolean {
    return (
      this.$store.getters.selectOperation ==
        this.$store.state.operation.MOVING &&
      Boolean(this.$store.getters.selectDepartureWarehouseId) &&
      Boolean(this.$store.getters.selectArrivalWarehouseId)
    );
  }

  get isConsumptionAndSelectDeparture(): boolean {
    return (
      this.$store.getters.selectOperation ==
        this.$store.state.operation.EXPENSE &&
      Boolean(this.$store.state.selectDepartureWarehouseId)
    );
  }

  get isComingAndSelectArrival(): boolean {
    return (
      this.$store.getters.selectOperation ==
        this.$store.state.operation.INCOME &&
      Boolean(this.$store.state.selectArrivalWarehouseId)
    );
  }

  async createMoving(createMovingDto: CreateMovingDto): Promise<void> {
    await this.$store.dispatch(actions.CreateMoving, createMovingDto);
  }

  
  async createIncome(createIncomeDto: CreateIncomeDto): Promise<void> {
    await this.$store.dispatch(actions.CreateIncome, createIncomeDto);
  }

  
  async createExpense(createExpenseDto: CreateExpenseDto): Promise<void> {
    await this.$store.dispatch(actions.CreateExpense, createExpenseDto);
  }
}
</script>
