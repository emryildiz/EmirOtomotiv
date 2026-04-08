import { Helmet } from 'react-helmet-async'
import { ProductList } from '@/features/products/components/ProductList'
import { SEOMeta } from '@/components/SEOMeta'

export default function ProductsPage() {
  return (
    <section className="section">
      <SEOMeta
        title="Otobüs Yedek Parça Ürünleri – Depo Kapağı, Bagaj Kapağı, Akü Kapağı"
        description="Prestij, Sultan ve Isuzu otobüsleri için depo kapağı, bagaj kapağı, akü kapağı ve karoser parçaları. Kategoriye göre filtreleyin, aradığınız parçayı hemen bulun."
        canonical="/urunler"
        keywords="otobüs yedek parça listesi, depo kapağı, bagaj kapağı, akü kapağı, karoser parça, Prestij yedek parça, Sultan yedek parça, Isuzu yedek parça"
      />
      <Helmet>
        <script type="application/ld+json">{JSON.stringify({
          "@context": "https://schema.org",
          "@type": "BreadcrumbList",
          "itemListElement": [
            { "@type": "ListItem", "position": 1, "name": "Ana Sayfa", "item": "https://emirotobusparca.com/" },
            { "@type": "ListItem", "position": 2, "name": "Ürünler", "item": "https://emirotobusparca.com/urunler" }
          ]
        })}</script>
      </Helmet>
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
