import { AboutSection } from '@/features/about/components/AboutSection'

export default function AboutPage() {
  return (
    <section className="section">
      <div className="container">
        <h1 className="section-title">Hakkımızda</h1>
        <p className="section-subtitle">Emir Otomotiv olarak kimiz, ne yapıyoruz?</p>
        <AboutSection />
      </div>
    </section>
  )
}
