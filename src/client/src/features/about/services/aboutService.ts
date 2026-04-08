import api from '@/lib/axios'
import type { About } from '../types'

export const aboutService = {
  get: () =>
    api.get<About>('/api/aboutus').then(r => r.data),

  update: (data: About) =>
    api.put('/api/aboutus', data),
}
