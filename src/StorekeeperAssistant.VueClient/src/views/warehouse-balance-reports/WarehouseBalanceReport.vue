<template>
  <div>
    <h4 class="text-start">Отчет по остаткам</h4>
    <hr class="mb-4" />

    <div class="row mb-2">
      <div class="col-4 text-right">
        <span class="line-h-text-by-input">Склад</span>
      </div>
      <div class="col-8">
        <VSelect
          :options="warehouses"
          :reduce="(m) => m.id"
          placeholder="Выберите склад"
          label="name"
          v-model="selectWarehouseId"
        >
          <template v-slot:no-options> Не найдено </template>
        </VSelect>
      </div>
    </div>

    <div class="row mb-2">
      <div class="col-4 text-right">
        <span class="line-h-text-by-input">Дата</span>
      </div>
      <div class="col-8">
        <template v-if="isCurrentTime == false">
          <VueCtkDateTimePicker
            class="date-input-vue-ctk"
            format="DD-MM-YYYY HH:mm"
            button-now-translation="Сейчас"
            output-format="YYYY-MM-DD HH:mm"
            :max-date="maxSelectDate"
            :no-clear-button="true"
            label=""
            v-model="selectDateTime"
          >
          </VueCtkDateTimePicker>
        </template>

        Текущее время
        <input type="checkbox" v-model="isCurrentTime" />
      </div>
    </div>

    <div class="row mb-4">
      <div class="col-12 text-center">
        <button class="btn btn-primary" @click="click_get_report">
          Получить
        </button>
      </div>
    </div>

    <template v-if="isNotWarehouseInventoryItemsAndIsNotLoadForm">
      <h4 class="text-center">У склада нет ТМЦ</h4>
    </template>

    <template v-if="isWarehouseInventoryItemsAndIsNotLoadForm">
      <h4 class="text-center">Отчет остатков по дате и времени</h4>

      <table class="table table-bordered table-hover table-sm">
        <thead class="thead-light thead-hermes">
          <tr class="text-center bg-light">
            <th class="align-middle"><b>Номенклатура</b></th>
            <th class="align-middle"><b>Остаток на складе</b></th>
          </tr>
        </thead>
        <tbody class="body-hermes c-pointer text-center">
          <tr
            v-for="(warehouseInventoryItem, index) in warehouseInventoryItems"
            :key="warehouseInventoryItem.id + '_' + index"
          >
            <td class="align-middle">
              {{ warehouseInventoryItem.inventoryItem.name }}
            </td>
            <td class="align-middle">{{ warehouseInventoryItem.count }}</td>
          </tr>
        </tbody>
      </table>
    </template>

    <template v-if="isLoadForm">
      <div class="row">
        <div class="col-12 text-center text-primary">Загрузка...</div>
      </div>
    </template>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { WarehouseDto } from "@/models/dto/warehouse-dto";
import moment from "moment";
import api from "@/store/api";
import { WarehouseInventoryItemDto } from "@/models/dto/warehouse-inventory-item-dto";
import mutations from "@/store/mutations";

@Component({})
export default class WarehouseBalanceReport extends Vue {
  selectWarehouseId: string | null = null;
  isCurrentTime = true;
  maxSelectDate: string = moment().format("YYYY-MM-DD");
  selectDateTime: string = moment().format("YYYY-MM-DD HH:mm");
  isLoadForm: boolean | null = null;
  get warehouses(): Array<WarehouseDto> {
    return this.$store.getters.warehouses;
  }

  get warehouseInventoryItems(): Array<WarehouseInventoryItemDto> {
    return this.$store.getters.warehouseInventoryItems;
  }

  get isWarehouseInventoryItemsAndIsNotLoadForm(): boolean {
    return this.warehouseInventoryItems.length > 0 && this.isLoadForm == false;
  }
  get isNotWarehouseInventoryItemsAndIsNotLoadForm(): boolean {
    return this.warehouseInventoryItems.length == 0 && this.isLoadForm == false;
  }

  async click_get_report(): Promise<void> {
    if (!this.selectWarehouseId) {
      this.$store.commit(mutations.setError, { msg: "Выберите склад" });
      return;
    }
    this.isLoadForm = true;
    let date = null;
    if (this.isCurrentTime == false) {
      date = moment(this.selectDateTime);
      date = date.utc();
      date = date.format("YYYY-MM-DD HH:mm:ss");
    }
    const selectWarehouseId = this.selectWarehouseId;
    await this.$store.dispatch(api.GetWarehouseBalanceReport, {
      warehouseId: selectWarehouseId,
      date: date,
    });

    this.isLoadForm = false;
  }
}
</script>

<style>
/*=========================================================
        vue-ctk-date-time-picker
===========================================================*/
.date-input-vue-ctk {
  padding-top: 0 !important;
}

.date-input-vue-ctk .field-input {
  padding-top: 0 !important;
  height: 31px !important;
  min-height: 25px !important;
}
</style>