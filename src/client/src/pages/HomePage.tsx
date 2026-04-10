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
        description="Prestij ve Sultan başta olmak üzere ticari araçlar için depo, akü ve bagaj kapakları ile kapı sistemleri. Kendi atölyemizde standartlara uygun üretim."
        canonical="/"
        keywords="otobüs yedek parça, yedek parça imalatı, otobüs karoser parça, depo kapağı, bagaj kapağı, akü kapağı, kapı sistemi, Prestij otobüs, Sultan otobüs yedek parça"
      />
      {/* Hero */}
      <section className={styles.hero}>
        <div className="container">
          <h1 className={styles.heroTitle}>
            Otobüs Yedek <span>Parça</span> İmalatı
          </h1>
          <p className={styles.heroSub}>
            Prestij ve Sultan başta olmak üzere ticari araçlar için kendi atölyemizde üretilen, sorunsuz montajlı ve uzun ömürlü parçalar.
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
          <p className="section-subtitle">Kendi atölyemizden, standartlara uygun üretilmiş parçalar</p>

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
            Atölyemizle iletişime geçin; depo, akü, bagaj kapakları ve kapı sistemlerinde size özel çözüm üretelim.
          </p>
          <Link to="/iletisim" className="btn btn-primary">
            İletişime Geç
          </Link>
        </div>
      </section>
    </div>
  )
}
