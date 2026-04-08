import { useQuery } from '@tanstack/react-query'
import { contactService } from '../services/contactService'

export function useContact() {
  return useQuery({
    queryKey: ['contact'],
    queryFn: contactService.get,
  })
}
