import styles from './LoadingSpinner.module.css'

export function LoadingSpinner() {
  return (
    <div className={styles.wrapper}>
      <div className={styles.spinner} />
    </div>
  )
}
