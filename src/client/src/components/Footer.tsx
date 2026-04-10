import { Link } from 'react-router-dom'
import styles from './Footer.module.css'

const menuColumns = [
  {
    title: 'Menü',
    links: [
      { to: '/', label: 'Ana Sayfa' },
      { to: '/urunler', label: 'Ürünler' },
      { to: '/hakkimizda', label: 'Hakkımızda' },
      { to: '/iletisim', label: 'İletişim' },
    ],
  },
  {
    title: 'Ürün Kategorileri',
    links: [
      { to: '/urunler?q=depo', label: 'Depo Kapakları' },
      { to: '/urunler?q=bagaj', label: 'Bagaj Kapakları' },
      { to: '/urunler?q=akü', label: 'Akü Kapakları' },
    ],
  },
  {
    title: 'Araçlar',
    links: [
      { to: '/urunler?q=prestij', label: 'Prestij' },
      { to: '/urunler?q=sultan', label: 'Sultan' },
    ],
  },
]

export function Footer() {
  return (
    <footer className={styles.footer}>
      <div className={`container ${styles.top}`}>
        {/* Marka */}
        <div className={styles.brand}>
          <Link to="/" className={styles.logo}>
            Emir<span>Otomotiv</span>
          </Link>
          <p className={styles.tagline}>
            3 yıldır imalatçı kimliğiyle Prestij ve Sultan<br />başta olmak üzere ticari araçlara yedek parça üretiyoruz.
          </p>
        </div>

        {/* Link sütunları */}
        {menuColumns.map(col => (
          <div key={col.title} className={styles.column}>
            <p className={styles.columnTitle}>{col.title}</p>
            <ul className={styles.columnLinks}>
              {col.links.map(link => (
                <li key={link.to}>
                  <Link to={link.to} className={styles.columnLink}>
                    {link.label}
                  </Link>
                </li>
              ))}
            </ul>
          </div>
        ))}
      </div>

      <div className={styles.bottom}>
        <div className="container">
          <p className={styles.copy}>
            &copy; {new Date().getFullYear()} Emir Otomotiv. Tüm hakları saklıdır.
          </p>
        </div>
      </div>
    </footer>
  )
}
