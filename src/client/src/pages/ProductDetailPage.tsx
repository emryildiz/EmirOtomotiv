import { useState } from 'react'
import { useParams, Link } from 'react-router-dom'
import { useProduct } from '@/features/products/hooks/useProduct'
import { LoadingSpinner } from '@/components/LoadingSpinner'
import type { ProductImage } from '@/features/products/types'
import styles from './ProductDetailPage.module.css'

function getPrimaryImage(images: ProductImage[]): ProductImage {
  return images.find(img => img.primaryImage) ?? images[0]
}

export default function ProductDetailPage() {
  const { id } = useParams<{ id: string }>()
  const { data: product, isLoading, isError } = useProduct(id ?? '')
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
  const displayUrl = activeImage ?? (images.length > 0 ? getPrimaryImage(images).imageUrl : null)

  return (
    <section className="section">
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
                    className={`${styles.thumb} ${displayUrl === img.imageUrl ? styles.thumbActive : ''}`}
                    onClick={() => setActiveImage(img.imageUrl)}
                  >
                    <img src={img.imageUrl} alt={`${product.name} ${i + 1}`} />
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
