import { Link } from 'react-router-dom'
import { useProducts } from '@/features/products/hooks/useProducts'
import { ProductCard } from '@/features/products/components/ProductCard'
import { LoadingSpinner } from '@/components/LoadingSpinner'
import { SEOMeta } from '@/components/SEOMeta'
import styles from './HomePage.module.css'

export default function HomePage() {
  const { data: products, isLoading } = useProducts()
  const featured = products?.slice(0, 3)

  return (
    <div>
      <SEOMeta
        title="Emir Otomotiv – Otobüs Yedek Parça İmalatı | emirotobusparca.com"
        description="Prestij, Sultan ve Isuzu otobüsleri için orijinal kalite yedek parça imalatı. Depo kapağı, bagaj kapağı, akü kapağı ve karoser parçaları stoktan hızlı teslimat."
        canonical="/"
        keywords="otobüs yedek parça, yedek parça imalatı, otobüs karoser parça, depo kapağı, bagaj kapağı, akü kapağı, Prestij otobüs, Sultan otobüs, Isuzu otobüs yedek parça"
      />
      {/* Hero */}
      <section className={styles.hero}>
        <div className="container">
          <h1 className={styles.heroTitle}>
            Otomotiv Yedek <span>Parça</span> Çözümleri
          </h1>
          <p className={styles.heroSub}>
            Prestij, Sultan ve Isuzu araçları için orijinal kalite yedek parçalar.
          </p>
          <div className={styles.heroActions}>
            <Link to="/urunler" className="btn btn-primary">
              Ürünleri Keşfet
            </Link>
            <Link to="/iletisim" className="btn btn-outline">
              Bize Ulaşın
            </Link>
          </div>
        </div>
      </section>

      {/* Featured Products */}
      <section className="section">
        <div className="container">
          <h2 className="section-title">Öne Çıkan Ürünler</h2>
          <p className="section-subtitle">Stoktan hızlı temin edilen parçalar</p>

          {isLoading ? (
            <LoadingSpinner />
          ) : (
            <div className="grid-3">
              {featured?.map(product => (
                <ProductCard key={product.id} product={product} />
              ))}
            </div>
          )}

          <div className={styles.viewAll}>
            <Link to="/urunler" className="btn btn-outline">
              Tüm Ürünleri Gör
            </Link>
          </div>
        </div>
      </section>

      {/* CTA Banner */}
      <section className={styles.cta}>
        <div className="container">
          <h2 className={styles.ctaTitle}>Aradığınız parçayı bulamadınız mı?</h2>
          <p className={styles.ctaSub}>
            Uzman ekibimizle iletişime geçin, size en kısa sürede yardımcı olalım.
          </p>
          <Link to="/iletisim" className="btn btn-primary">
            İletişime Geç
          </Link>
        </div>
      </section>
    </div>
  )
}
