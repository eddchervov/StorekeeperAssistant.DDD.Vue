<template>
  <div>
    <div class="row">
      <div class="col-md-6">
        <!-- Paging https://github.com/lokyoung/vuejs-paginate -->
        <template v-if="totalPage > 1">
          <VuePaginate
            v-model="currentPage"
            :page-count="totalPage"
            :page-range="5"
            :margin-pages="1"
            :click-handler="clickPaging"
            :prev-text="'Предыдущая'"
            :next-text="'Следующая'"
            :container-class="'pagination'"
            :page-link-class="'page-link'"
            :page-class="'page-item'"
            :prev-link-class="'page-link'"
            :prev-class="'page-item'"
            :next-link-class="'page-link'"
            :next-class="'page-item'"
          >
          </VuePaginate>
        </template>
      </div>
      <div class="col-md-6">
        <ul class="pagination float-md-end">
          <li class="pt-1 me-3">Кол-во элементов на страницу:</li>
          <li
            class="page-item"
            v-bind:class="{ active: pageSize === 20 }"
            v-on:click="changePageSize(20)"
          >
            <button class="page-link">20</button>
          </li>
          <li
            class="page-item"
            v-bind:class="{ active: pageSize === 40 }"
            v-on:click="changePageSize(40)"
          >
            <button class="page-link">40</button>
          </li>
          <li
            class="page-item"
            v-bind:class="{ active: pageSize === 60 }"
            v-on:click="changePageSize(60)"
          >
            <button class="page-link">60</button>
          </li>
        </ul>
      </div>
    </div>
    <template v-if="totalPage > 1 && isMessage">
      <div class="row">
        <div class="col-md-6">
          <span v-text="message"></span>
        </div>
      </div>
    </template>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component({})
export default class Paging extends Vue {
  @Prop() readonly totalCount!: number;
  @Prop() readonly isMessage!: boolean;

  pageSize = 20;
  currentPage = 1;

  get skipCount(): number {
    return (this.currentPage - 1) * this.pageSize;
  }
  get takeCount(): number {
    return this.pageSize;
  }
  get message(): string {
    if (this.totalPage == 0) return "";

    var from = (this.currentPage - 1) * this.pageSize + 1;
    var to = from + this.pageSize - 1;
    to = to >= this.totalCount ? this.totalCount : to;

    return (
      "Показаны с " + from + " по " + to + " из " + this.totalCount + " записей"
    );
  }
  get totalPage(): number {
    return Math.floor((this.totalCount + this.pageSize - 1) / this.pageSize);
  }

  clickPaging(e: number): void {
    this.currentPage = e;
    this.reloadEntityTable();
  }

  reloadEntityTable(): void {
    this.$emit("click-handler", this.skipCount, this.takeCount);
  }

  reloadAndGoFirstPageEntityTable(): void {
    this.currentPage = 1;
    this.reloadEntityTable();
  }

  changePageSize(count: number): void {
    this.pageSize = count;
    this.reloadAndGoFirstPageEntityTable();
  }
}
</script>
