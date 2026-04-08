import { useMutation, useQueryClient } from '@tanstack/react-query'
import { useNavigate } from 'react-router-dom'
import { authService } from '../services/authService'

export function useLogout() {
  const queryClient = useQueryClient()
  const navigate = useNavigate()

  return useMutation({
    mutationFn: authService.logout,
    onSuccess: () => {
      localStorage.removeItem('token')
      queryClient.clear()
      navigate('/login')
    },
    onError: () => {
      localStorage.removeItem('token')
      queryClient.clear()
      navigate('/login')
    },
  })
}
