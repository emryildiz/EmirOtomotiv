import { Navigate } from 'react-router-dom'
import { LoginForm } from '@/features/auth/components/LoginForm'
import { useMe } from '@/features/auth/hooks/useMe'
import styles from './LoginPage.module.css'

export default function LoginPage() {
  const { data: user, isLoading } = useMe()

  if (!isLoading && user) return <Navigate to="/" replace />

  return (
    <div className={styles.wrapper}>
      <div className={`card ${styles.card}`}>
        <LoginForm />
      </div>
    </div>
  )
}
