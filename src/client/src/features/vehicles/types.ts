export interface Vehicle {
  id: string
  name: string
  model: string
  year: string
}

export interface CreateVehicleRequest {
  name: string
  model: string
  year: string
}

export interface UpdateVehicleRequest {
  name: string
  model: string
  year: string
}
