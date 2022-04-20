import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService.js"

class KeepsService {
  async getAll() {
    const res = await api.get('api/keeps')
    logger.log('getall keeps', res.data)
    AppState.keeps = res.data
  }
  async setActive(id) {
    const res = await api.get(`api/keeps/${id}`)
    AppState.activeKeep = res.data
    logger.log('set active', AppState.activeKeep)
  }

  async getProfileKeeps(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    AppState.keeps = res.data
  }

  async createKeep(keepData) {
    try {
      const res = await api.post('api/keeps', keepData)
      AppState.keeps.push(res.data)
      console.log("YO", AppState.keeps);
      this.getAll()
      return res.data
    } catch (error) {
      logger.error(error)

    }
  }
  async deleteKeep(id) {
    try {
      await api.delete(`api/keeps/${id}`)
      this.getAll()
    } catch {
      logger.error(error)
    }
  }

}
export const keepsService = new KeepsService()