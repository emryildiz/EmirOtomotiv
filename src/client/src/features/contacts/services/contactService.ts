import api from '@/lib/axios'
import type { Contact } from '../types'

export const contactService = {
  get: () =>
    api.get<Contact>('/api/contact').then(r => r.data),

  update: (data: Contact) =>
    api.put('/api/contact', data),
}
