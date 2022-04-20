import { AppState } from "../AppState"
import { router } from "../router"
import { logger } from "../utils/Logger"
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
      this.getVaults()
    } catch (error) {
      logger.error(error)
    }
  }

  async deleteVault(id) {
    try {
      await api.delete(`api/vaults/${id}`)
      this.getVaults()
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
    }
  }


}

export const vaultsService = new VaultsService