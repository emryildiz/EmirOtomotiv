import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import { vehicleService } from '../services/vehicleService'
import type { CreateVehicleRequest, UpdateVehicleRequest } from '../types'

export function useVehicles() {
  return useQuery({ queryKey: ['vehicles'], queryFn: vehicleService.getAll })
}

export function useCreateVehicle() {
  const qc = useQueryClient()
  return useMutation({
    mutationFn: (data: CreateVehicleRequest) => vehicleService.create(data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['vehicles'] }),
  })
}

export function useUpdateVehicle() {
  const qc = useQueryClient()
  return useMutation({
    mutationFn: ({ id, data }: { id: string; data: UpdateVehicleRequest }) =>
      vehicleService.update(id, data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['vehicles'] }),
  })
}

export function useDeleteVehicle() {
  const qc = useQueryClient()
  return useMutation({
    mutationFn: (id: string) => vehicleService.delete(id),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['vehicles'] }),
  })
}
