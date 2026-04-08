import { useMutation, useQueryClient } from '@tanstack/react-query'
import { aboutService } from '../services/aboutService'
import type { About } from '../types'

export function useUpdateAbout() {
  const qc = useQueryClient()
  return useMutation({
    mutationFn: (data: About) => aboutService.update(data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['about'] }),
  })
}
