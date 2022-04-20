<template>
  <div
    class="row pt-3 row-cols-1 row-cols-md-3 g-4"
    data-bs-toggle="modal"
    data-bs-target="#active-keep"
    @click="setActive"
  >
    <div class="card bg-dark p-0 text-white">
      <img :src="keep.img" class="card-img p-0" alt="..." />
      <div class="card-img-overlay">
        <div class="selectable">
          <h5 class="card-title align-items-end">{{ keep.name }}</h5>
        </div>
        <p class="card-text"></p>
      </div>
    </div>
  </div>
</template>

<script>
import { AppState } from "../AppState";
import { keepsService } from "../services/KeepsService";
export default {
  props: {
    keep: {
      type: Object,
      required: true,
    },
  },
  setup(props) {

    return {
      async setActive() {
        try {
          AppState.activeKeep = props.keep
          await keepsService.setActive(props.keep.id)
        } catch (error) {
          // close modal?
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },

    };
  },
}
</script>

<style lang="scss" scoped>
.masonry-with-columns {
  columns: 6 200px;
  column-gap: 1rem;
  div {
    display: inline-block;
    width: 100%;
  }
}
</style>