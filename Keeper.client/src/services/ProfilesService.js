import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService {

  async getProfile(id) {
    const res = await api.get(`api/profiles/${id}`)
    AppState.profile = res.data
  }
  async getProfileKeeps(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    AppState.profileKeeps = res.data
    logger.log('profiles keeps', res.data)
  }

  async getProfileVaults(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.profileVaults = res.data
    logger.log('profiles vaults', res.data)
  }
}


export const profilesService = new ProfilesService