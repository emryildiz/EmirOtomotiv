import { ContactSection } from '@/features/contacts/components/ContactSection'
import { SEOMeta } from '@/components/SEOMeta'

export default function ContactPage() {
  return (
    <section className="section">
      <SEOMeta
        title="İletişim – Emir Otomotiv Otobüs Yedek Parça"
        description="Otobüs yedek parça siparişi ve fiyat bilgisi için bizimle iletişime geçin. Gaziantep Organize Sanayi Bölgesi. Tel: +90 342 555 08 19. Pazartesi–Cumartesi 08:30–18:30."
        canonical="/iletisim"
        keywords="Emir Otomotiv iletişim, otobüs yedek parça sipariş, fiyat bilgisi, Gaziantep otomotiv"
      />
      <div className="container">
        <h1 className="section-title">İletişim</h1>
        <p className="section-subtitle">Bize ulaşmak için aşağıdaki bilgileri kullanabilirsiniz.</p>
        <ContactSection />
      </div>
    </section>
  )
}
