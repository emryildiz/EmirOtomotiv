import { useMutation, useQueryClient } from '@tanstack/react-query'
import { contactService } from '../services/contactService'
import type { Contact } from '../types'

export function useUpdateContact() {
  const qc = useQueryClient()
  return useMutation({
    mutationFn: (data: Contact) => contactService.update(data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['contact'] }),
  })
}
