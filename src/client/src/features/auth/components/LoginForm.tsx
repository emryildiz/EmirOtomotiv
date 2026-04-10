import { useState, type FormEvent } from 'react'
import { useNavigate } from 'react-router-dom'
import { useLogin } from '../hooks/useLogin'
import styles from './LoginForm.module.css'

export function LoginForm() {
  const navigate = useNavigate()
  const { mutate: login, isPending, error } = useLogin()

  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')
  const [rememberMe, setRememberMe] = useState(false)

  function handleSubmit(e: FormEvent) {
    e.preventDefault()
    login(
      { username, password, rememberMe },
      { onSuccess: (data) => {
          localStorage.setItem('token', data.token)
          if (data.mustChangePassword) {
            navigate('/admin/sifre-degistir')
          } else {
            navigate(data.role === 'admin' ? '/admin' : '/')
          }
        }
      },
    )
  }

  return (
    <form className={styles.form} onSubmit={handleSubmit}>
      <h2 className={styles.title}>Giriş Yap</h2>

      <div className="form-group">
        <label className="form-label" htmlFor="username">
          Kullanıcı Adı
        </label>
        <input
          id="username"
          className="form-input"
          type="text"
          value={username}
          onChange={e => setUsername(e.target.value)}
          required
          autoComplete="username"
        />
      </div>

      <div className="form-group">
        <label className="form-label" htmlFor="password">
          Şifre
        </label>
        <input
          id="password"
          className="form-input"
          type="password"
          value={password}
          onChange={e => setPassword(e.target.value)}
          required
          autoComplete="current-password"
        />
      </div>

      <div className={styles.remember}>
        <input
          id="rememberMe"
          type="checkbox"
          checked={rememberMe}
          onChange={e => setRememberMe(e.target.checked)}
        />
        <label htmlFor="rememberMe">Beni hatırla</label>
      </div>

      {error && (
        <p className="error-text">Kullanıcı adı veya şifre hatalı.</p>
      )}

      <button className={`btn btn-primary ${styles.submit}`} type="submit" disabled={isPending}>
        {isPending ? 'Giriş yapılıyor...' : 'Giriş Yap'}
      </button>
    </form>
  )
}
