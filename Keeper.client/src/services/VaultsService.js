import { AppState } from "../AppState"
import { router } from "../router"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"

class VaultsService {
  async getVaults() {
    try {
      const res = await api.get('api/vaults')
      AppState.vaults = res.data
    } catch (error) {
      logger.error(error)
    }
  }
  async getMyVaults() {
    try {
      const res = await api.get('account/vaults')
      AppState.myVaults = res.data
      logger.log('my vaults', res.data)
    } catch (error) {
      logger.error(error)
    }
  }

  // async getProfileVaults(id) {
  //   try {
  //     const res = await api.get('api/profile/' + id + '/vaults')
  //     AppState.profileVaults = res.data
  //   } catch (error) {
  //     logger.log(errror)
  //   }
  // } already in the profile service

  async createVault(vaultData) {
    try {
      const res = await api.post('api/vaults', vaultData)
      logger.log('new vault', res.data)
      AppState.vaults.push(res.data)
      this.getVaults()
      return res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async deleteVault(id) {
    try {
      await api.delete(`api/vaults/${id}`)
    } catch (error) {
      logger.error(error)
    }
  }

  async setActiveVault(id) {
    try {
      const res = await api.get(`api/vaults/${id}`)
      AppState.activeVault = res.data
    } catch (error) {
      logger.error(error)
      Pop.toast("HANDS UP! *shoots gun, blows off smoke, twirls... holsters")
      router.push({ name: 'Home', params: { id } })
    }
  }
}

export const vaultsService = new VaultsService