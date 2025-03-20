<template>
  <div>
    <div class="toast-container">
      <template v-for="(message, index) in messages">
        <div
          :key="message + '_' + index"
          class="toast d-block"
          role="alert"
          aria-live="assertive"
          aria-atomic="false"
        >
          <div class="toast-header">
            <img src="" class="rounded me-2" alt="" />
            <strong class="me-auto text-danger">Ошибка</strong>
            <small class="text-muted">just now</small>
          </div>
          <div class="toast-body">{{ message }}.</div>
        </div>
      </template>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component({})
export default class NotificationToasts extends Vue {
  get messages(): Array<string> {
    return this.$store.getters.serverErrors;
  }
}
</script>

<style>
.toast-container {
  position: absolute;
  right: 15px;
  z-index: 100;
}
</style>
