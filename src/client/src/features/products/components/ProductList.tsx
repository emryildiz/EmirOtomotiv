import { useState } from 'react'
import { useSearchParams } from 'react-router-dom'
import { useProducts } from '../hooks/useProducts'
import { useCategories } from '@/features/categories/hooks/useCategories'
import { ProductCard } from './ProductCard'
import { LoadingSpinner } from '@/components/LoadingSpinner'
import styles from './ProductList.module.css'

export function ProductList() {
  const { data: products, isLoading, isError } = useProducts()
  const { data: categories } = useCategories()
  const [activeCategory, setActiveCategory] = useState<string | null>(null)
  const [searchParams, setSearchParams] = useSearchParams()
  const searchQuery = searchParams.get('q') ?? ''

  function handleCategoryClick(name: string | null) {
    setActiveCategory(name)
    setSearchParams(name ? {} : {})
  }

  if (isLoading) return <LoadingSpinner />
  if (isError || !products) return <p className="error-text">Ürünler yüklenemedi.</p>

  const filtered = products.filter(p => {
    const matchesCategory = activeCategory ? p.category?.name === activeCategory : true
    const matchesSearch = searchQuery
      ? p.name.toLowerCase().includes(searchQuery.toLowerCase()) ||
        p.productNumber.toLowerCase().includes(searchQuery.toLowerCase()) ||
        p.description?.toLowerCase().includes(searchQuery.toLowerCase())
      : true
    return matchesCategory && matchesSearch
  })

  return (
    <div>
      {searchQuery && (
        <p className={styles.searchInfo}>
          "<strong>{searchQuery}</strong>" için {filtered.length} sonuç
        </p>
      )}

      <div className={styles.filters}>
        <button
          className={`btn ${activeCategory === null ? 'btn-primary' : 'btn-outline'}`}
          onClick={() => handleCategoryClick(null)}
        >
          Tümü
        </button>
        {categories?.map(cat => (
          <button
            key={cat.id}
            className={`btn ${activeCategory === cat.name ? 'btn-primary' : 'btn-outline'}`}
            onClick={() => handleCategoryClick(cat.name)}
          >
            {cat.name}
          </button>
        ))}
      </div>

      {filtered.length === 0 ? (
        <p className={styles.empty}>Ürün bulunamadı.</p>
      ) : (
        <div className="grid-3">
          {filtered.map(product => (
            <ProductCard key={product.id} product={product} />
          ))}
        </div>
      )}
    </div>
  )
}
