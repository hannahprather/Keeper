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
                <i class="mdi mdi-eye text-danger" alt="views">
                  {{ activeKeep.views }}
                </i>
                <i class="mdi mdi-key text-danger" alk="saves"></i>
              </div>
              <p>{{ activeKeep.description }}</p>
            </div>

            <!-- <div class="d-flex justify-content-around">
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
            </div> -->
            <select
              v-model="state.vaultId"
              name=""
              id=""
              class=""
              data-option-label="Select a Vault"
              required
            >
              <option disabled value="">Select a vault</option>
              <option v-for="v in userVaults" :key="v.id" :value="v.id">
                {{ v.name }}
              </option>
            </select>
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
import { computed, reactive, ref } from '@vue/reactivity'
import { AppState } from '../AppState.js'
import { Modal } from 'bootstrap'
import { useRouter } from 'vue-router'
import { vaultKeepsService } from "../services/VaultKeepsService.js"
export default {
  setup() {
    const router = useRouter()
    const keep = ref({})
    const state = reactive({
      vaultId: null
    })
    return {
      state,
      userVaults: computed(() => AppState.profileVaults),
      activeKeep: computed(() => AppState.activeKeep),
      goToProfile(id) {
        Modal.getOrCreateInstance(document.getElementById('active-keep')).hide()
        router.push({ name: 'Profile', params: { id } })
      },
      addKeepToVault(vaultId, keepId) {
        const newKeep = {
          vaultId: vaultId,
          keepId: keepId,
        }
        vaultKeepsService.addKeepToVault(newKeep)
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