import { Outlet } from 'react-router-dom'
import { AdminSidebar } from './AdminSidebar'
import { ProtectedRoute } from '@/features/auth/components/ProtectedRoute'
import styles from './AdminLayout.module.css'

export function AdminLayout() {
  return (
    <ProtectedRoute role="admin">
      <div className={styles.layout}>
        <AdminSidebar />
        <main className={styles.main}>
          <Outlet />
        </main>
      </div>
    </ProtectedRoute>
  )
}
