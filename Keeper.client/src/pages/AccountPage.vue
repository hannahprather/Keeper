<template>
  <div class="about pt-2 px-4 pl-4">
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
  </div>
  <div class="px-5">
    <h2>Vaults:</h2>
    {{}}
    <h2>Keeps:</h2>
  </div>

  <form @submit="createKeep">
    <div class="form-group">
      <label for="exampleInputEmail1">Name</label>
      <input
        type="text"
        class="form-control"
        id="keepName"
        aria-describedby="keepName"
        placeholder="Name"
        v-model="state.newKeep.name"
      />
    </div>
    <div class="form-group">
      <label for="exampleInputPassword1">Description</label>
      <input
        type="text"
        class="form-control"
        id="keepDescription"
        placeholder="Description"
        v-model="state.newKeep.description"
      />
      <div class="form-group">
        <label for="exampleInputEmail1">Image URL</label>
        <input
          type="text"
          class="form-control"
          id="keepImageUrl"
          aria-describedby="keepImageUrl"
          placeholder="Image URL"
          v-model="state.newKeep.img"
        />
      </div>
    </div>
    <button type="submit" class="btn btn-primary">Submit</button>
  </form>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from "../services/KeepsService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
export default {
  name: 'Account',
  setup() {
    const state = reactive({
      newKeep: {}
    })
    return {
      state,
      account: computed(() => AppState.account),
      async createKeep() {
        try {
          const created = await keepsService.createKeep(state.newKeep)
          state.newKeep = {}
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message, "error");
        }
      }
      //get my vaults => with the keeps ive saved
      //get keeps ive made

    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
