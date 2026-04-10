import { useState, type FormEvent } from 'react'
import { useNavigate } from 'react-router-dom'
import { useChangePassword } from '@/features/auth/hooks/useChangePassword'

export default function AdminChangePassword() {
  const navigate = useNavigate()
  const { mutate: changePassword, isPending, error, isSuccess } = useChangePassword()

  const [currentPassword, setCurrentPassword] = useState('')
  const [newPassword, setNewPassword] = useState('')
  const [confirmPassword, setConfirmPassword] = useState('')
  const [validationError, setValidationError] = useState('')

  function handleSubmit(e: FormEvent) {
    e.preventDefault()
    setValidationError('')

    if (newPassword.length < 8) {
      setValidationError('Yeni şifre en az 8 karakter olmalıdır.')
      return
    }

    if (newPassword !== confirmPassword) {
      setValidationError('Yeni şifreler eşleşmiyor.')
      return
    }

    changePassword(
      { currentPassword, newPassword },
      {
        onSuccess: () => {
          setTimeout(() => navigate('/admin'), 1500)
        },
      },
    )
  }

  return (
    <div style={{ maxWidth: 420, margin: '2rem auto' }}>
      <h1 style={{ marginBottom: '0.5rem' }}>Şifre Değiştir</h1>
      <p style={{ color: 'var(--color-text-muted)', marginBottom: '1.5rem', fontSize: '0.9rem' }}>
        Güvenliğiniz için lütfen şifrenizi değiştirin.
      </p>

      {isSuccess ? (
        <p style={{ color: 'var(--color-success, green)' }}>
          Şifreniz başarıyla güncellendi. Yönlendiriliyorsunuz...
        </p>
      ) : (
        <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '1rem' }}>
          <div className="form-group">
            <label className="form-label" htmlFor="current">Mevcut Şifre</label>
            <input
              id="current"
              className="form-input"
              type="password"
              value={currentPassword}
              onChange={e => setCurrentPassword(e.target.value)}
              required
              autoComplete="current-password"
            />
          </div>

          <div className="form-group">
            <label className="form-label" htmlFor="new">Yeni Şifre</label>
            <input
              id="new"
              className="form-input"
              type="password"
              value={newPassword}
              onChange={e => setNewPassword(e.target.value)}
              required
              autoComplete="new-password"
            />
          </div>

          <div className="form-group">
            <label className="form-label" htmlFor="confirm">Yeni Şifre (Tekrar)</label>
            <input
              id="confirm"
              className="form-input"
              type="password"
              value={confirmPassword}
              onChange={e => setConfirmPassword(e.target.value)}
              required
              autoComplete="new-password"
            />
          </div>

          {(validationError || error) && (
            <p className="error-text">
              {validationError || 'Mevcut şifre hatalı veya bir sorun oluştu.'}
            </p>
          )}

          <button className="btn btn-primary" type="submit" disabled={isPending}>
            {isPending ? 'Kaydediliyor...' : 'Şifremi Güncelle'}
          </button>
        </form>
      )}
    </div>
  )
}
