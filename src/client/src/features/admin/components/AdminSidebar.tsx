import { NavLink, Link } from 'react-router-dom'
import { useLogout } from '@/features/auth/hooks/useLogout'
import { useMe } from '@/features/auth/hooks/useMe'
import styles from './AdminSidebar.module.css'

const links = [
  { to: '/admin', label: 'Dashboard', icon: '▦' },
  { to: '/admin/urunler', label: 'Ürünler', icon: '⊞' },
  { to: '/admin/kategoriler', label: 'Kategoriler', icon: '◈' },
  { to: '/admin/araclar', label: 'Araçlar', icon: '◉' },
  { to: '/admin/hakkimizda', label: 'Hakkımızda', icon: '◎' },
  { to: '/admin/iletisim', label: 'İletişim', icon: '◌' },
]

export function AdminSidebar() {
  const { data: user } = useMe()
  const { mutate: logout } = useLogout()

  return (
    <aside className={styles.sidebar}>
      <div className={styles.brand}>
        <Link to="/" className={styles.logo}>
          Emir<span>Otomotiv</span>
        </Link>
        <span className={styles.panel}>Admin Panel</span>
      </div>

      <nav className={styles.nav}>
        {links.map(link => (
          <NavLink
            key={link.to}
            to={link.to}
            end={link.to === '/admin'}
            className={({ isActive }) =>
              `${styles.link} ${isActive ? styles.active : ''}`
            }
          >
            <span className={styles.icon}>{link.icon}</span>
            {link.label}
          </NavLink>
        ))}
      </nav>

      <div className={styles.footer}>
        {user && <p className={styles.username}>{user.username}</p>}
        <button className={styles.logout} onClick={() => logout()}>
          Çıkış Yap
        </button>
      </div>
    </aside>
  )
}
