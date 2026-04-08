import { useState, useEffect } from 'react'
import { useAbout } from '@/features/about/hooks/useAbout'
import { useUpdateAbout } from '@/features/about/hooks/useUpdateAbout'
import { LoadingSpinner } from '@/components/LoadingSpinner'
import styles from './AdminPage.module.css'

export default function AdminAbout() {
  const { data, isLoading } = useAbout()
  const { mutate: update, isPending, isSuccess } = useUpdateAbout()

  const [description, setDescription] = useState('')
  const [imageUrl, setImageUrl] = useState('')

  useEffect(() => {
    if (data) {
      setDescription(data.description ?? '')
      setImageUrl(data.imageUrl ?? '')
    }
  }, [data])

  if (isLoading) return <LoadingSpinner />

  return (
    <div className={styles.page}>
      <h1 className={styles.title}>Hakkımızda</h1>
      <p className={styles.subtitle}>Şirket tanıtım bilgilerini düzenleyin.</p>

      <div className={styles.form}>
        <div className="form-group">
          <label className="form-label">Açıklama</label>
          <textarea
            className="form-input"
            rows={6}
            value={description}
            onChange={e => setDescription(e.target.value)}
            style={{ resize: 'vertical' }}
          />
        </div>
        <div className="form-group">
          <label className="form-label">Resim URL</label>
          <input
            className="form-input"
            value={imageUrl}
            onChange={e => setImageUrl(e.target.value)}
            placeholder="https://..."
          />
        </div>
        {imageUrl && (
          <img src={imageUrl} alt="Önizleme" style={{ width: '100%', maxHeight: 240, objectFit: 'cover', borderRadius: 8 }} />
        )}
        <div className={styles.formActions}>
          <button
            className="btn btn-primary"
            onClick={() => update({ description, imageUrl })}
            disabled={isPending}
          >
            {isPending ? 'Kaydediliyor...' : 'Kaydet'}
          </button>
          {isSuccess && <span style={{ fontSize: '0.875rem', color: 'green' }}>Kaydedildi!</span>}
        </div>
      </div>
    </div>
  )
}
