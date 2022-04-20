import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService.js"

class VaultKeepsService {
  async addKeepToVault(vaultKeep) {
    const res = await api.post('api/vaultkeeps', vaultKeep)
    logger.log('adding keep to vault', res.data)
  }

  async getVaultKeeps(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    logger.log('these our your keeps', res.data)
    AppState.vaultKeeps = res.data
  }

  async deleteVaultKeep(id) {
    const res = await api.delete(`api/vaultkeeps/${id}`)
    logger.log('deleting vault keep from service')
    AppState.vaultKeeps = res.data
  }
}
export const vaultKeepsService = new VaultKeepsService()