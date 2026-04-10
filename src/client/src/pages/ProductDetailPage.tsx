import { useState } from 'react'
import { useParams, Link } from 'react-router-dom'
import { Helmet } from 'react-helmet-async'
import { useProduct } from '@/features/products/hooks/useProduct'
import { LoadingSpinner } from '@/components/LoadingSpinner'
import { SEOMeta } from '@/components/SEOMeta'
import { getImageUrl } from '@/lib/imageUrl'
import type { ProductImage } from '@/features/products/types'
import styles from './ProductDetailPage.module.css'

function getPrimaryImage(images: ProductImage[]): ProductImage {
  return images.find(img => img.primaryImage) ?? images[0]
}

export default function ProductDetailPage() {
  const { slug } = useParams<{ slug: string }>()
  const { data: product, isLoading, isError } = useProduct(slug ?? '')
  const [activeImage, setActiveImage] = useState<string | null>(null)

  if (isLoading) return <LoadingSpinner />

  if (isError || !product) {
    return (
      <div className="section">
        <div className="container">
          <p className="error-text">Ürün bulunamadı.</p>
          <Link to="/urunler" className="btn btn-outline" style={{ marginTop: '1rem' }}>
            Ürünlere Dön
          </Link>
        </div>
      </div>
    )
  }

  const images = product.productImages ?? []
  const displayUrl = activeImage ?? (images.length > 0 ? getImageUrl(getPrimaryImage(images).imageUrl) : null)

  const vehicleInfo = product.vehicle
    ? `${product.vehicle.name ?? ''} ${product.vehicle.model ?? ''}`.trim()
    : ''
  const seoDescription = [
    product.name,
    vehicleInfo ? `${vehicleInfo} için yedek parça` : 'otobüs yedek parça',
    product.description ?? '',
    'Ürün No:',
    product.productNumber,
  ]
    .filter(Boolean)
    .join(' – ')
    .slice(0, 160)

  return (
    <section className="section">
      <SEOMeta
        title={`${product.name} – ${vehicleInfo || 'Otobüs Yedek Parça'}`}
        description={seoDescription}
        canonical={`/urunler/${product.slug}`}
        keywords={`${product.name}, ${vehicleInfo} yedek parça, otobüs yedek parça, ${product.category?.name ?? ''}`}
      />
      <Helmet>
        <script type="application/ld+json">{JSON.stringify({
          "@context": "https://schema.org",
          "@type": "Product",
          "name": product.name,
          "description": product.description ?? seoDescription,
          "sku": product.productNumber,
          "image": displayUrl ?? undefined,
          "brand": { "@type": "Brand", "name": "Emir Otomotiv" },
          "offers": {
            "@type": "Offer",
            "availability": "https://schema.org/InStock",
            "seller": { "@type": "Organization", "name": "Emir Otomotiv" }
          }
        })}</script>
        <script type="application/ld+json">{JSON.stringify({
          "@context": "https://schema.org",
          "@type": "BreadcrumbList",
          "itemListElement": [
            { "@type": "ListItem", "position": 1, "name": "Ana Sayfa", "item": "https://emirotobusparca.com/" },
            { "@type": "ListItem", "position": 2, "name": "Ürünler", "item": "https://emirotobusparca.com/urunler" },
            { "@type": "ListItem", "position": 3, "name": product.name, "item": `https://emirotobusparca.com/urunler/${product.slug}` }
          ]
        })}</script>
      </Helmet>
      <div className="container">
        <Link to="/urunler" className={styles.back}>
          &larr; Ürünlere Dön
        </Link>

        <div className={styles.layout}>
          {/* Resim galerisi */}
          <div className={styles.gallery}>
            <div className={styles.mainImageWrapper}>
              {displayUrl ? (
                <img src={displayUrl} alt={product.name} className={styles.mainImage} />
              ) : (
                <div className={styles.imagePlaceholder}>
                  <span>&#9881;</span>
                </div>
              )}
            </div>

            {images.length > 1 && (
              <div className={styles.thumbs}>
                {images.map((img, i) => (
                  <button
                    key={i}
                    className={`${styles.thumb} ${displayUrl === getImageUrl(img.imageUrl) ? styles.thumbActive : ''}`}
                    onClick={() => setActiveImage(getImageUrl(img.imageUrl))}
                  >
                    <img src={getImageUrl(img.imageUrl) ?? ''} alt={`${product.name} ${i + 1}`} />
                  </button>
                ))}
              </div>
            )}
          </div>

          {/* Ürün bilgileri */}
          <div className={styles.info}>
            <div className={styles.tags}>
              {product.category?.name && (
                <span className="badge badge-primary">{product.category.name}</span>
              )}
            </div>

            <h1 className={styles.name}>{product.name}</h1>
            <p className={styles.number}>Ürün No: <strong>{product.productNumber}</strong></p>

            {product.description && (
              <p className={styles.description}>{product.description}</p>
            )}

            {product.vehicle && (
              <div className={styles.vehicleCard}>
                <p className={styles.vehicleTitle}>Araç Bilgisi</p>
                <div className={styles.vehicleGrid}>
                  {product.vehicle.name && (
                    <div className={styles.vehicleItem}>
                      <span className={styles.vehicleLabel}>Araç</span>
                      <span className={styles.vehicleValue}>{product.vehicle.name}</span>
                    </div>
                  )}
                  {product.vehicle.model && (
                    <div className={styles.vehicleItem}>
                      <span className={styles.vehicleLabel}>Model</span>
                      <span className={styles.vehicleValue}>{product.vehicle.model}</span>
                    </div>
                  )}
                  {product.vehicle.year && (
                    <div className={styles.vehicleItem}>
                      <span className={styles.vehicleLabel}>Yıl</span>
                      <span className={styles.vehicleValue}>{product.vehicle.year}</span>
                    </div>
                  )}
                </div>
              </div>
            )}
          </div>
        </div>
      </div>
    </section>
  )
}
