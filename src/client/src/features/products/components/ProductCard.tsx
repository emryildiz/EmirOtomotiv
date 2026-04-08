import { Link } from 'react-router-dom'
import type { Product } from '../types'
import styles from './ProductCard.module.css'

interface Props {
  product: Product
}

function getPrimaryImage(product: Product): string | null {
  if (!product.productImages || product.productImages.length === 0) return null
  const primary = product.productImages.find(img => img.primaryImage)
  return primary?.imageUrl ?? product.productImages[0].imageUrl
}

export function ProductCard({ product }: Props) {
  const imageUrl = getPrimaryImage(product)

  return (
    <Link to={`/urunler/${product.id}`} className={`card ${styles.card}`}>
      <div className={styles.imageWrapper}>
        {imageUrl ? (
          <img src={imageUrl} alt={product.name} className={styles.image} />
        ) : (
          <div className={styles.imagePlaceholder}>
            <span className={styles.placeholderIcon}>&#9881;</span>
          </div>
        )}
        {product.category?.name && (
          <span className={`badge badge-primary ${styles.categoryBadge}`}>
            {product.category.name}
          </span>
        )}
      </div>

      <div className={styles.body}>
        <p className={styles.vehicle}>{product.vehicle?.name}</p>
        <h3 className={styles.name}>{product.name}</h3>
        {product.description && (
          <p className={styles.desc}>{product.description}</p>
        )}
      </div>

      <div className={styles.footer}>
        <span className={styles.number}>No: {product.productNumber}</span>
        <span className={styles.link}>Detay &rarr;</span>
      </div>
    </Link>
  )
}
