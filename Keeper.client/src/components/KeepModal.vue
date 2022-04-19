<template>
  <Modal id="active-keep">
    <template #modal-title> {{ activeKeep.name }} </template>
    <template #modal-body>
      <div class="container">
        <div class="row">
          <div class="col-md-6 p-0">
            <img
              height="500"
              class="w-100 object-fit-cover"
              :src="activeKeep.img"
              alt=""
            />
          </div>
          <div class="col-md-6 p-2">
            <div class="mt-3">
              <h3>
                {{ activeKeep.name }} <i class="mdi mdi-star text-warning"></i>
              </h3>
              <p>{{ activeKeep.description }}</p>
            </div>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed, ref } from '@vue/reactivity'
import { AppState } from '../AppState.js'
import { Modal } from 'bootstrap'
import { useRouter } from 'vue-router'
export default {
  setup() {
    const router = useRouter()
    const review = ref({})
    return {
      activeKeep: computed(() => AppState.activeKeep),
      goToProfile(id) {
        Modal.getOrCreateInstance(document.getElementById('active-restaurant')).hide()
        // router.push({ name: 'Profile', params: { id } })
      },
    }
  }
}
</script>


<style lang="scss" scoped>
.right-col {
  max-height: 400px;
  overflow-y: auto;
}
</style>