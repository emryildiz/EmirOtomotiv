import { Navigate } from 'react-router-dom'
import { useMe } from '../hooks/useMe'
import { LoadingSpinner } from '@/components/LoadingSpinner'
import type { ReactNode } from 'react'

interface Props {
  children: ReactNode
  role?: string
}

export function ProtectedRoute({ children, role }: Props) {
  const { data: user, isLoading, isError } = useMe()

  if (isLoading) return <LoadingSpinner />
  if (isError || !user) return <Navigate to="/login" replace />
  if (role && user.role !== role) return <Navigate to="/" replace />

  return <>{children}</>
}
