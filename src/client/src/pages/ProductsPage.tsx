import { ProductList } from '@/features/products/components/ProductList'

export default function ProductsPage() {
  return (
    <section className="section">
      <div className="container">
        <h1 className="section-title">Ürünlerimiz</h1>
        <p className="section-subtitle">
          Kategoriye göre filtreleyin, aradığınız parçayı bulun.
        </p>
        <ProductList />
      </div>
    </section>
  )
}
