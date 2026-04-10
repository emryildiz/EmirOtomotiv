import { ContactSection } from '@/features/contacts/components/ContactSection'
import { SEOMeta } from '@/components/SEOMeta'

export default function ContactPage() {
  return (
    <section className="section">
      <SEOMeta
        title="İletişim – Emir Otomotiv Otobüs Yedek Parça İmalatı"
        description="Prestij ve Sultan araçları için depo, akü, bagaj kapakları ve kapı sistemleri hakkında sipariş ve fiyat bilgisi için atölyemizle iletişime geçin."
        canonical="/iletisim"
        keywords="Emir Otomotiv iletişim, otobüs yedek parça sipariş, fiyat bilgisi, Prestij Sultan yedek parça"
      />
      <div className="container">
        <h1 className="section-title">İletişim</h1>
        <p className="section-subtitle">Sorunsuz montaj ve uzun ömürlü kullanım için doğru parçayı birlikte bulalım.</p>
        <ContactSection />
      </div>
    </section>
  )
}
