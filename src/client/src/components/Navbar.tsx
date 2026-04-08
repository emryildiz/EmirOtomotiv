import { useState, type FormEvent } from 'react'
import { Link, NavLink, useNavigate } from 'react-router-dom'
import styles from './Navbar.module.css'

const navLinks = [
  { to: '/', label: 'Ana Sayfa' },
  { to: '/urunler', label: 'Ürünler' },
  { to: '/hakkimizda', label: 'Hakkımızda' },
  { to: '/iletisim', label: 'İletişim' },
]

export function Navbar() {
  const navigate = useNavigate()
  const [menuOpen, setMenuOpen] = useState(false)
  const [query, setQuery] = useState('')

  function handleSearch(e: FormEvent) {
    e.preventDefault()
    if (query.trim()) {
      navigate(`/urunler?q=${encodeURIComponent(query.trim())}`)
      setMenuOpen(false)
    }
  }

  return (
    <header className={styles.header}>
      <div className={`container ${styles.inner}`}>
        {/* Logo */}
        <Link to="/" className={styles.logo} onClick={() => setMenuOpen(false)}>
          Emir<span>Otomotiv</span>
        </Link>

        {/* Desktop nav — ortalı */}
        <nav className={styles.nav}>
          {navLinks.map(link => (
            <NavLink
              key={link.to}
              to={link.to}
              end={link.to === '/'}
              className={({ isActive }) =>
                `${styles.navLink} ${isActive ? styles.active : ''}`
              }
            >
              {link.label}
            </NavLink>
          ))}
        </nav>

        {/* Search — sağ */}
        <form className={styles.searchForm} onSubmit={handleSearch}>
          <input
            className={styles.searchInput}
            type="search"
            placeholder="Ürün ara..."
            value={query}
            onChange={e => setQuery(e.target.value)}
          />
          <button className={styles.searchBtn} type="submit" aria-label="Ara">
            <svg width="15" height="15" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.2" strokeLinecap="round" strokeLinejoin="round">
              <circle cx="11" cy="11" r="8" />
              <line x1="21" y1="21" x2="16.65" y2="16.65" />
            </svg>
          </button>
        </form>

        {/* Hamburger */}
        <button
          className={styles.hamburger}
          onClick={() => setMenuOpen(o => !o)}
          aria-label="Menü"
          aria-expanded={menuOpen}
        >
          <span className={`${styles.bar} ${menuOpen ? styles.barOpen1 : ''}`} />
          <span className={`${styles.bar} ${menuOpen ? styles.barOpen2 : ''}`} />
          <span className={`${styles.bar} ${menuOpen ? styles.barOpen3 : ''}`} />
        </button>
      </div>

      {/* Mobile menu */}
      <div className={`${styles.mobileMenu} ${menuOpen ? styles.mobileMenuOpen : ''}`}>
        <nav className={styles.mobileNav}>
          {navLinks.map(link => (
            <NavLink
              key={link.to}
              to={link.to}
              end={link.to === '/'}
              className={({ isActive }) =>
                `${styles.mobileNavLink} ${isActive ? styles.mobileActive : ''}`
              }
              onClick={() => setMenuOpen(false)}
            >
              {link.label}
            </NavLink>
          ))}
        </nav>
        <form className={styles.mobileSearch} onSubmit={handleSearch}>
          <input
            className={styles.searchInput}
            type="search"
            placeholder="Ürün ara..."
            value={query}
            onChange={e => setQuery(e.target.value)}
          />
          <button className={styles.searchBtn} type="submit" aria-label="Ara">
            <svg width="15" height="15" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.2" strokeLinecap="round" strokeLinejoin="round">
              <circle cx="11" cy="11" r="8" />
              <line x1="21" y1="21" x2="16.65" y2="16.65" />
            </svg>
          </button>
        </form>
      </div>
    </header>
  )
}
