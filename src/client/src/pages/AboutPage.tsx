import { AboutSection } from '@/features/about/components/AboutSection'
import { SEOMeta } from '@/components/SEOMeta'

export default function AboutPage() {
  return (
    <section className="section">
      <SEOMeta
        title="Hakkımızda – Emir Otomotiv Otobüs Yedek Parça İmalatçısı"
        description="3 yıldır imalatçı kimliğiyle Prestij ve Sultan başta olmak üzere ticari araçlar için depo, akü ve bagaj kapakları ile kapı sistemleri üreten Emir Otomotiv hakkında bilgi edinin."
        canonical="/hakkimizda"
        keywords="Emir Otomotiv hakkında, otobüs yedek parça üreticisi, yedek parça imalatçısı, Prestij Sultan yedek parça"
      />
      <div className="container">
        <h1 className="section-title">Hakkımızda</h1>
        <p className="section-subtitle">3 yıldır imalatçı kimliğiyle otobüs yedek parça sektöründe fark yaratıyoruz.</p>
        <AboutSection />
      </div>
    </section>
  )
}
