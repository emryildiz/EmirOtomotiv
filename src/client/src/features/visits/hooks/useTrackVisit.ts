import { useEffect } from 'react'
import { useLocation } from 'react-router-dom'
import api from '@/lib/axios'

export function useTrackVisit() {
  const location = useLocation()

  useEffect(() => {
    // Prevent tracking admin routes to keep metrics focused on customers
    if (location.pathname.startsWith('/admin')) {
      return
    }

    const payload = {
      path: location.pathname,
      referrer: document.referrer || null,
    }

    // Fire and forget
    api.post('/api/Visits', payload).catch((err) => {
      console.error('Failed to track visit', err)
    })
  }, [location.pathname])
}
