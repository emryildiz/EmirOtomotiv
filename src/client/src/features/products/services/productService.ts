import api from '@/lib/axios'
import type { Product, ProductImage } from '../types'

export interface CreateProductDto { name: string; description?: string; vehicleId: string; categoryId: string }
export interface UpdateProductDto { name: string; description?: string }

export const productService = {
  getAll: () =>
    api.get<Product[]>('/api/products').then(r => r.data),

  getById: (id: string) =>
    api.get<Product>(`/api/products/${id}`).then(r => r.data),

  getBySlug: (slug: string) =>
    api.get<Product>(`/api/products/slug/${slug}`).then(r => r.data),

  create: (data: CreateProductDto) =>
    api.post('/api/products/create', data),

  update: (id: string, data: UpdateProductDto) =>
    api.put(`/api/products/${id}`, data),

  delete: (id: string) =>
    api.delete(`/api/products/${id}`),

  uploadImages: (productId: string, files: File[]) => {
    const form = new FormData()
    files.forEach(f => form.append('files', f))
    return api.post<ProductImage[]>(`/api/products/${productId}/images`, form, {
      headers: { 'Content-Type': 'multipart/form-data' },
    }).then(r => r.data)
  },

  deleteImage: (productId: string, imageId: string) =>
    api.delete(`/api/products/${productId}/images/${imageId}`),

  setPrimaryImage: (productId: string, imageId: string) =>
    api.put(`/api/products/${productId}/images/${imageId}/primary`),
}
