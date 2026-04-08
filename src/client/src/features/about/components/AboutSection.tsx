import { useAbout } from '../hooks/useAbout'
import { LoadingSpinner } from '@/components/LoadingSpinner'
import styles from './AboutSection.module.css'

export function AboutSection() {
  const { data, isLoading, isError } = useAbout()

  if (isLoading) return <LoadingSpinner />
  if (isError || !data) return null

  return (
    <div className={styles.wrapper}>
      {data.imageUrl && (
        <div className={styles.imageWrapper}>
          <img src={data.imageUrl} alt="Hakkımızda" className={styles.image} />
        </div>
      )}
      <div className={styles.content}>
        <p className={styles.description}>{data.description}</p>
      </div>
    </div>
  )
}
