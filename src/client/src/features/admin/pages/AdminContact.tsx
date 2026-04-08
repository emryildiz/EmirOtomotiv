import { useState, useEffect } from 'react'
import { useContact } from '@/features/contacts/hooks/useContact'
import { useUpdateContact } from '@/features/contacts/hooks/useUpdateContact'
import { LoadingSpinner } from '@/components/LoadingSpinner'
import styles from './AdminPage.module.css'

export default function AdminContact() {
  const { data, isLoading } = useContact()
  const { mutate: update, isPending, isSuccess } = useUpdateContact()

  const [form, setForm] = useState({
    description: '', adress: '', phoneNumber: '', workingHours: '', mail: '',
  })

  useEffect(() => {
    if (data) {
      setForm({
        description: data.description ?? '',
        adress: data.adress ?? '',
        phoneNumber: data.phoneNumber ?? '',
        workingHours: data.workingHours ?? '',
        mail: data.mail ?? '',
      })
    }
  }, [data])

  function set(key: keyof typeof form) {
    return (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) =>
      setForm(f => ({ ...f, [key]: e.target.value }))
  }

  if (isLoading) return <LoadingSpinner />

  return (
    <div className={styles.page}>
      <h1 className={styles.title}>İletişim</h1>
      <p className={styles.subtitle}>İletişim bilgilerini düzenleyin.</p>

      <div className={styles.form}>
        <div className="form-group">
          <label className="form-label">Açıklama</label>
          <textarea className="form-input" rows={3} value={form.description} onChange={set('description')} style={{ resize: 'vertical' }} />
        </div>
        <div className={styles.formRow}>
          <div className="form-group">
            <label className="form-label">Adres</label>
            <input className="form-input" value={form.adress} onChange={set('adress')} />
          </div>
          <div className="form-group">
            <label className="form-label">Telefon</label>
            <input className="form-input" value={form.phoneNumber} onChange={set('phoneNumber')} />
          </div>
          <div className="form-group">
            <label className="form-label">E-posta</label>
            <input className="form-input" value={form.mail} onChange={set('mail')} />
          </div>
          <div className="form-group">
            <label className="form-label">Çalışma Saatleri</label>
            <input className="form-input" value={form.workingHours} onChange={set('workingHours')} />
          </div>
        </div>
        <div className={styles.formActions}>
          <button className="btn btn-primary" onClick={() => update(form)} disabled={isPending}>
            {isPending ? 'Kaydediliyor...' : 'Kaydet'}
          </button>
          {isSuccess && <span style={{ fontSize: '0.875rem', color: 'green' }}>Kaydedildi!</span>}
        </div>
      </div>
    </div>
  )
}
