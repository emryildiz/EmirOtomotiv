import { useState } from 'react'
import { Outlet, ScrollRestoration } from 'react-router-dom'
import { AdminSidebar } from './AdminSidebar'
import { ProtectedRoute } from '@/features/auth/components/ProtectedRoute'
import styles from './AdminLayout.module.css'

export function AdminLayout() {
  const [sidebarOpen, setSidebarOpen] = useState(false)

  return (
    <ProtectedRoute role="admin">
      <ScrollRestoration />
      <div className={styles.layout}>
        <AdminSidebar open={sidebarOpen} onClose={() => setSidebarOpen(false)} />
        <div className={styles.content}>
          {/* Mobile topbar */}
          <header className={styles.topbar}>
            <button
              className={styles.hamburger}
              onClick={() => setSidebarOpen(o => !o)}
              aria-label="Menüyü aç"
            >
              <span /><span /><span />
            </button>
            <span className={styles.topbarTitle}>Admin Panel</span>
          </header>

          <main className={styles.main}>
            <Outlet />
          </main>
        </div>
      </div>
    </ProtectedRoute>
  )
}
