import api from '@/lib/axios'
import type { Product } from '../types'

export interface CreateProductDto { name: string; description?: string; vehicleId: string; categoryId: string }
export interface UpdateProductDto { name: string; description?: string }

export const productService = {
  getAll: () =>
    api.get<Product[]>('/api/products').then(r => r.data),

  getById: (id: string) =>
    api.get<Product>(`/api/products/${id}`).then(r => r.data),

  create: (data: CreateProductDto) =>
    api.post('/api/products/create', data),

  update: (id: string, data: UpdateProductDto) =>
    api.put(`/api/products/${id}`, data),

  delete: (id: string) =>
    api.delete(`/api/products/${id}`),
}
