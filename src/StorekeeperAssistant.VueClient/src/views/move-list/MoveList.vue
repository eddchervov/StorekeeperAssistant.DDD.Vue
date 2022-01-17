<template>
  <div>
    <h4>Список перемещений</h4>
    <hr class="mb-4" />

    <template v-if="isMovingAndIsLoadForm">
      <div class="row">
        <div
          class="col-12 mb-2"
          v-for="(moving, index) in movings"
          :key="moving.id + '_' + index"
        >
          <div class="card">
            <div class="card-body">
              <h6 class="card-title mb-3">
                Дата: {{ moving.transferDate | toLocalFormat }}
              </h6>
              <h6 class="card-subtitle mb-2">
                <template v-if="moving.departureWarehouse">
                  {{ moving.departureWarehouse.name }}
                </template>
                <template v-else> Извне </template>
                <img src="57116.png" class="right-btn" />
                <template v-if="moving.arrivalWarehouse">
                  {{ moving.arrivalWarehouse.name }}
                </template>
                <template v-else> Убрано со складов </template>
              </h6>
              <div class="card-text">
                <p
                  class="mb-0"
                  v-for="movingDetail in moving.movingDetails"
                  :key="moving.id + '_' + movingDetail.id"
                >
                  {{ movingDetail.inventoryItem.name }}:
                  {{ movingDetail.count }} шт.
                </p>
              </div>
              <img
                class="del-btn"
                src="trash.png"
                @click="deleteMovings(moving.id)"
              />
            </div>
          </div>
        </div>
      </div>
    </template>

    <template v-if="isLoadForm">
      <div class="row">
        <div class="col-12 text-center">
          <span class="text-primary">Загрузка...</span>
        </div>
      </div>
    </template>

    <template v-if="isNotMovingAndIsLoadForm">
      <div class="row">
        <div class="col-12 text-center">
          <h5>Нет перемещений</h5>
        </div>
      </div>
    </template>
  </div>
</template>

<script lang="ts">
import { MovingDto } from "@/models/dto/moving-dto";
import api from "@/store/api";
import moment from "moment";
import { Component, Vue } from "vue-property-decorator";

@Component({
  filters: {
    toLocalFormat(value: Date): string {
      var stillUtc = moment.utc(value).toDate();
      return moment(stillUtc).local().format("DD.MM.YYYY HH:mm:ss");
    },
  },
})
export default class MoveList extends Vue {
  isLoadForm: boolean | null = null;

  get movings(): Array<MovingDto> {
    return this.$store.getters.movings;
  }

  get isMovingAndIsLoadForm(): boolean {
    return this.movings.length > 0 && this.isLoadForm == false;
  }
  get isNotMovingAndIsLoadForm(): boolean {
    return this.movings.length == 0 && this.isLoadForm == false;
  }

  deleteMovings(movingId: string): void {
    return;
  }

  async getMovings(skipCount = 0, takeCount = 20): Promise<void> {
    this.isLoadForm = true;
    await this.$store.dispatch(api.GetMovings, {
      skipCount,
      takeCount,
    });

    this.isLoadForm = false;
  }

  async mounted(): Promise<void> {
    await this.getMovings();
  }
}
</script>