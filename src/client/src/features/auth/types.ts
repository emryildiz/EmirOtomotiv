export interface LoginRequest {
  username: string
  password: string
  rememberMe: boolean
}

export interface LoginResponse {
  token: string
  refreshToken: string
  refreshTokenExpiry: string
  role: string
  mustChangePassword: boolean
}

export interface MeResponse {
  username: string
  role: string
}
