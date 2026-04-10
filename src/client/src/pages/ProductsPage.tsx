import { Helmet } from 'react-helmet-async'
import { ProductList } from '@/features/products/components/ProductList'
import { SEOMeta } from '@/components/SEOMeta'

export default function ProductsPage() {
  return (
    <section className="section">
      <SEOMeta
        title="Otobüs Yedek Parça Ürünleri – Depo Kapağı, Bagaj Kapağı, Akü Kapağı, Kapı Sistemi"
        description="Prestij ve Sultan başta olmak üzere ticari araçlar için kendi atölyemizde üretilen depo kapağı, bagaj kapağı, akü kapağı ve kapı sistemleri. Aradığınız parçayı hemen bulun."
        canonical="/urunler"
        keywords="otobüs yedek parça listesi, depo kapağı, bagaj kapağı, akü kapağı, kapı sistemi, Prestij yedek parça, Sultan yedek parça, ticari araç yedek parça"
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
          Kendi atölyemizde standartlara uygun ürettiğimiz parçaları keşfedin.
        </p>
        <ProductList />
      </div>
    </section>
  )
}
