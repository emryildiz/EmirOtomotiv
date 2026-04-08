import { useState } from 'react'
import { useVisitsStats } from '@/features/visits/hooks/useVisitsStats'
import { LoadingSpinner } from '@/components/LoadingSpinner'
import styles from './AdminVisits.module.css'

export default function AdminVisits() {
  const [days, setDays] = useState(30)
  const { data: stats, isLoading, isError } = useVisitsStats({ days })

  if (isLoading) return <LoadingSpinner />
  if (isError || !stats) return <div className={styles.error}>Veriler yüklenirken bir hata oluştu.</div>

  const maxDaily = Math.max(...stats.daily.map((d: any) => d.count), 1)

  return (
    <div className={styles.visitorPage}>
      <header className={styles.header}>
        <div>
          <h1 className={styles.title}>Ziyaretçi İstatistikleri</h1>
          <p className={styles.subtitle}>Web sitenizin ziyaret metrikleri</p>
        </div>
        <select 
          className={styles.daysSelect}
          value={days}
          onChange={(e) => setDays(Number(e.target.value))}
        >
          <option value={7}>Son 7 Gün</option>
          <option value={30}>Son 30 Gün</option>
          <option value={90}>Son 90 Gün</option>
        </select>
      </header>

      <div className={styles.topStatsGrid}>
        <div className={styles.topStatCard}>
          <span className={styles.statIcon}>👥</span>
          <div className={styles.statInfo}>
            <span className={styles.statLabel}>Toplam Ziyaretler ({days} Gün)</span>
            <span className={styles.statValue}>{stats.total}</span>
          </div>
        </div>
        <div className={styles.topStatCard}>
          <span className={styles.statIcon}>📅</span>
          <div className={styles.statInfo}>
            <span className={styles.statLabel}>Bugünkü Ziyaretler</span>
            <span className={styles.statValue}>{stats.today}</span>
          </div>
        </div>
        <div className={styles.topStatCard}>
          <span className={styles.statIcon}>🌍</span>
          <div className={styles.statInfo}>
            <span className={styles.statLabel}>Benzersiz IP'ler</span>
            <span className={styles.statValue}>{stats.uniqueIps}</span>
          </div>
        </div>
      </div>

      {/* Daily activity chart simulation */}
      <div className={styles.card}>
        <h2 className={styles.cardTitle}>Günlük Aktivite</h2>
        <div className={styles.barChart}>
          {stats.daily.map((d: any, i: number) => {
            const height = `${(d.count / maxDaily) * 100}%`
            return (
              <div key={i} className={styles.barWrapper} title={`${d.date}: ${d.count} Ziyaret`}>
                <div className={styles.barContainer}>
                  <div className={styles.barFill} style={{ height }} />
                </div>
                {/* Yalnızca ilk ve son/belirli aralıklarla tarih göstermek isteyebilirsiniz */}
                <span className={styles.barLabel}>
                  {i === 0 || i === stats.daily.length - 1 || i % 7 === 0 
                     ? new Date(d.date).toLocaleDateString('tr-TR', { day: 'numeric', month: 'short' })
                     : ''}
                </span>
              </div>
            )
          })}
        </div>
      </div>

      <div className={styles.metricsGrid}>
        
        {/* Top Pages */}
        <div className={styles.card}>
          <h2 className={styles.cardTitle}>En Çok Ziyaret Edilen Sayfalar</h2>
          <div className={styles.list}>
            {stats.topPages.map((page: any, i: number) => (
              <div key={i} className={styles.listItem}>
                <div className={styles.listLabel}>
                  <span className={styles.listRank}>{i + 1}</span>
                  <span className={styles.listName} title={page.name}>
                    {page.name || '/'}
                  </span>
                </div>
                <div className={styles.listData}>
                  <span className={styles.listCount}>{page.count}</span>
                  <span className={styles.listPct}>{page.pct}%</span>
                </div>
              </div>
            ))}
            {stats.topPages.length === 0 && <span className={styles.emptyText}>Veri bulunamadı.</span>}
          </div>
        </div>

        {/* Top Cities */}
        <div className={styles.card}>
          <h2 className={styles.cardTitle}>Lokasyonlar</h2>
          <div className={styles.list}>
            {stats.topCities.map((city: any, i: number) => (
              <div key={i} className={styles.listItem}>
                <div className={styles.listLabel}>
                  <span className={styles.listRank}>{i + 1}</span>
                  <span className={styles.listName}>{city.name || 'Bilinmiyor'}</span>
                </div>
                <div className={styles.listData}>
                  <span className={styles.listCount}>{city.count}</span>
                  <span className={styles.listPct}>{city.pct}%</span>
                </div>
              </div>
            ))}
            {stats.topCities.length === 0 && <span className={styles.emptyText}>Veri bulunamadı.</span>}
          </div>
        </div>

        {/* Devices */}
        <div className={styles.card}>
          <h2 className={styles.cardTitle}>Cihazlar</h2>
          <div className={styles.progressList}>
            {stats.devices.map((device: any, i: number) => (
              <div key={i} className={styles.progressItem}>
                <div className={styles.progressHeader}>
                  <span className={styles.progressName}>{device.name}</span>
                  <span className={styles.progressPct}>{device.pct}% ({device.count})</span>
                </div>
                <div className={styles.progressBarBg}>
                  <div className={styles.progressBarFill} style={{ width: `${device.pct}%` }} />
                </div>
              </div>
            ))}
            {stats.devices.length === 0 && <span className={styles.emptyText}>Veri bulunamadı.</span>}
          </div>
        </div>

        {/* Browsers */}
        <div className={styles.card}>
          <h2 className={styles.cardTitle}>Tarayıcılar</h2>
          <div className={styles.progressList}>
            {stats.browsers.map((browser: any, i: number) => (
              <div key={i} className={styles.progressItem}>
                <div className={styles.progressHeader}>
                  <span className={styles.progressName}>{browser.name}</span>
                  <span className={styles.progressPct}>{browser.pct}% ({browser.count})</span>
                </div>
                <div className={styles.progressBarBg}>
                  <div className={styles.progressBarFill} style={{ width: `${browser.pct}%` }} />
                </div>
              </div>
            ))}
            {stats.browsers.length === 0 && <span className={styles.emptyText}>Veri bulunamadı.</span>}
          </div>
        </div>

      </div>
    </div>
  )
}
