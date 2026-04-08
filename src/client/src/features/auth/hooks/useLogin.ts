import { useMutation, useQueryClient } from '@tanstack/react-query'
import { authService } from '../services/authService'
import type { LoginRequest } from '../types'

export function useLogin() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: (data: LoginRequest) => authService.login(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['auth', 'me'] })
    },
  })
}
