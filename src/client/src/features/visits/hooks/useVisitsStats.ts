import { useQuery } from '@tanstack/react-query'
import api from '@/lib/axios'
import { VisitStatsResponse } from '../types'

interface VisitsStatsParams {
  days?: number
}

export function useVisitsStats(params?: VisitsStatsParams) {
  return useQuery({
    queryKey: ['visits-stats', params?.days],
    queryFn: async () => {
      const response = await api.get<VisitStatsResponse>('/api/visits/stats', {
        params: { days: params?.days ?? 30 }
      })
      return response.data
    },
  })
}
