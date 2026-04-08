import { ContactSection } from '@/features/contacts/components/ContactSection'

export default function ContactPage() {
  return (
    <section className="section">
      <div className="container">
        <h1 className="section-title">İletişim</h1>
        <p className="section-subtitle">Bize ulaşmak için aşağıdaki bilgileri kullanabilirsiniz.</p>
        <ContactSection />
      </div>
    </section>
  )
}
