import api from '@/lib/axios'
import type { LoginRequest, LoginResponse, MeResponse } from '../types'

export const authService = {
  login: (data: LoginRequest) =>
    api.post<LoginResponse>('/api/auth/login', data).then(r => r.data),

  me: () =>
    api.get<MeResponse>('/api/auth/me').then(r => r.data),

  logout: () =>
    api.post('/api/auth/logout').then(r => r.data),

  changePassword: (data: { currentPassword: string; newPassword: string }) =>
    api.post('/api/auth/change-password', data).then(r => r.data),
}
