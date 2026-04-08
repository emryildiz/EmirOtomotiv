import { Link } from 'react-router-dom'
import { useProducts } from '@/features/products/hooks/useProducts'
import { useCategories } from '@/features/categories/hooks/useCategories'
import { useVehicles } from '@/features/vehicles/hooks/useVehicles'
import styles from './AdminPage.module.css'

export default function AdminDashboard() {
  const { data: products } = useProducts()
  const { data: categories } = useCategories()
  const { data: vehicles } = useVehicles()

  const stats = [
    { label: 'Ürünler', value: products?.length ?? '—', to: '/admin/urunler' },
    { label: 'Kategoriler', value: categories?.length ?? '—', to: '/admin/kategoriler' },
    { label: 'Araçlar', value: vehicles?.length ?? '—', to: '/admin/araclar' },
  ]

  const shortcuts = [
    { label: 'Ürün Yönetimi', to: '/admin/urunler' },
    { label: 'Kategori Yönetimi', to: '/admin/kategoriler' },
    { label: 'Araç Yönetimi', to: '/admin/araclar' },
    { label: 'Hakkımızda Düzenle', to: '/admin/hakkimizda' },
    { label: 'İletişim Düzenle', to: '/admin/iletisim' },
  ]

  return (
    <div className={styles.page}>
      <h1 className={styles.title}>Dashboard</h1>
      <p className={styles.subtitle}>Genel bakış</p>

      <div className={styles.statGrid}>
        {stats.map(s => (
          <Link key={s.label} to={s.to} className={styles.statCard}>
            <span className={styles.statValue}>{s.value}</span>
            <span className={styles.statLabel}>{s.label}</span>
          </Link>
        ))}
      </div>

      <h2 className={styles.sectionTitle}>Hızlı Erişim</h2>
      <div className={styles.shortcutGrid}>
        {shortcuts.map(s => (
          <Link key={s.to} to={s.to} className={styles.shortcut}>
            {s.label} →
          </Link>
        ))}
      </div>
    </div>
  )
}
