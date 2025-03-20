<template>
  <div>
    <Coming />
    <ArrivalWarehouse />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import ArrivalWarehouse from "./ArrivalWarehouse.vue";
import Coming from "./Coming.vue";
@Component({
  components: {
    Coming,
    ArrivalWarehouse,
  },
})
export default class ComingForm extends Vue {}
</script>
