import api from '@/lib/axios'
import type { Category } from '../types'

export const categoryService = {
  getAll: () =>
    api.get<Category[]>('/api/categories').then(r => r.data),

  create: (name: string) =>
    api.post('/api/categories/create', { name }),

  update: (id: string, name: string) =>
    api.put(`/api/categories/${id}`, { name }),

  delete: (id: string) =>
    api.delete(`/api/categories/${id}`),
}
