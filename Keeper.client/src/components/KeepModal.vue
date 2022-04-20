<template>
  <Modal id="active-keep">
    <template #modal-body>
      <div class="container">
        <div class="row">
          <div class="col-md-6 p-4 rounded">
            <img
              height="500"
              class="w-100 object-fit-cover rounded"
              :src="activeKeep.img"
              alt=""
            />
          </div>
          <div class="col-md-6 p-2">
            <div class="mt-3">
              <h3 class="text-center">
                {{ activeKeep.name }}
              </h3>
              <div class="d-flex justify-content-around">
                <i class="mdi mdi-eye text-danger" alt="views"></i>
                <i class="mdi mdi-key text-danger" alk="saves"></i>
              </div>
              <p>{{ activeKeep.description }}</p>
            </div>

            <div class="d-flex justify-content-around">
              <div class="dropdown">
                <button
                  class="btn btn-secondary dropdown-toggle"
                  type="button"
                  id="dropdownMenu2"
                  data-bs-toggle="dropdown"
                  aria-expanded="false"
                >
                  + To Your Vault
                </button>
                <ul class="dropdown-menu" aria-labelledby="dropdownMenu2">
                  <li>
                    <button class="dropdown-item" type="button">vaults</button>
                  </li>
                  <li>
                    <button class="dropdown-item" type="button">vault</button>
                  </li>
                  <li>
                    <button class="dropdown-item" type="button">vault</button>
                  </li>
                </ul>
              </div>
              <i class="mdi mdi-delete text-dark" alt="views"></i>
            </div>
            <div
              class="d-flex p-info align-items-bottom selectable"
              @click="goToProfile(activeKeep.creator.id)"
            >
              <img
                height="50"
                class="w-70 object-fit-cover rounded"
                :src="activeKeep.creator?.picture"
                alt=""
              />
              <p class="p-2 text-small">
                {{ activeKeep.creator?.name }}
              </p>
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
    const keep = ref({})
    return {
      activeKeep: computed(() => AppState.activeKeep),
      goToProfile(id) {
        Modal.getOrCreateInstance(document.getElementById('active-keep')).hide()
        router.push({ name: 'Profile', params: { id } })
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
.p-info {
  position: fixed;
  bottom: 20%;
}
.v-add {
  position: fixed;
  bottom: 30%;
}
</style>