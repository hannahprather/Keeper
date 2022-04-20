import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService.js"

class VaultKeepsService {
  async addKeepToVault(vaultKeep) {
    const res = await api.post('api/vaultkeeps', vaultKeep)
    logger.log('getall keeps', res.data)
    // AppState.keeps = res.data
  }


}
export const vaultKeepsService = new VaultKeepsService()