import { AboutSection } from '@/features/about/components/AboutSection'
import { SEOMeta } from '@/components/SEOMeta'

export default function AboutPage() {
  return (
    <section className="section">
      <SEOMeta
        title="Hakkımızda – Emir Otomotiv Otobüs Yedek Parça İmalatçısı"
        description="2008'den beri Prestij, Sultan ve Isuzu otobüsleri için yedek parça üreten Emir Otomotiv hakkında bilgi edinin. Kaliteli imalat, hızlı teslimat, uzman ekip."
        canonical="/hakkimizda"
        keywords="Emir Otomotiv hakkında, otobüs yedek parça üreticisi, yedek parça imalatçısı, Gaziantep otomotiv"
      />
      <div className="container">
        <h1 className="section-title">Hakkımızda</h1>
        <p className="section-subtitle">Emir Otomotiv olarak kimiz, ne yapıyoruz?</p>
        <AboutSection />
      </div>
    </section>
  )
}
