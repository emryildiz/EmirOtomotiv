import api from '@/lib/axios'
import type { Vehicle, CreateVehicleRequest, UpdateVehicleRequest } from '../types'

export const vehicleService = {
  getAll: () =>
    api.get<Vehicle[]>('/api/vehicles').then(r => r.data),

  create: (data: CreateVehicleRequest) =>
    api.post('/api/vehicles/create', data),

  update: (id: string, data: UpdateVehicleRequest) =>
    api.put(`/api/vehicles/${id}`, data),

  delete: (id: string) =>
    api.delete(`/api/vehicles/${id}`),
}
