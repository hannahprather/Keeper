<template>
  <div
    class="row row-cols-1 row-cols-md-3 g-4"
    data-bs-toggle="modal"
    data-bs-target="#active-restaurant"
    @click="setActive"
  >
    <div class="col">
      <div class="card h-100">
        <img :src="keep.img" class="card-img-top" alt="..." />
        <div class="card-body">
          <h5 class="card-title">{{ keep.name }}</h5>
        </div>
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

      }
    };
  },
};
</script>