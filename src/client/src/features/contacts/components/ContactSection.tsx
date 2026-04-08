import { useContact } from '../hooks/useContact'
import { LoadingSpinner } from '@/components/LoadingSpinner'
import styles from './ContactSection.module.css'

export function ContactSection() {
  const { data, isLoading, isError } = useContact()

  if (isLoading) return <LoadingSpinner />
  if (isError || !data) return null

  const mapSrc = data.adress
    ? `https://maps.google.com/maps?q=${encodeURIComponent(data.adress)}&output=embed&hl=tr`
    : null

  const items = [
    { icon: '📍', label: 'Adres', value: data.adress },
    { icon: '📞', label: 'Telefon', value: data.phoneNumber },
    { icon: '✉️', label: 'E-posta', value: data.mail },
    { icon: '🕐', label: 'Çalışma Saatleri', value: data.workingHours },
  ]

  return (
    <div className={styles.wrapper}>
      {data.description && (
        <p className={styles.description}>{data.description}</p>
      )}

      <div className={styles.grid}>
        {items.map(item =>
          item.value ? (
            <div key={item.label} className={`card ${styles.item}`}>
              <span className={styles.icon}>{item.icon}</span>
              <div>
                <p className={styles.label}>{item.label}</p>
                <p className={styles.value}>{item.value}</p>
              </div>
            </div>
          ) : null,
        )}
      </div>

      {mapSrc && (
        <div className={styles.mapWrapper}>
          <iframe
            src={mapSrc}
            className={styles.map}
            allowFullScreen
            loading="lazy"
            referrerPolicy="no-referrer-when-downgrade"
            title="Konum"
          />
        </div>
      )}
    </div>
  )
}
