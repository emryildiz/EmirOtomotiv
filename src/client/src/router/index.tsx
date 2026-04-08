import { createBrowserRouter, Outlet, ScrollRestoration } from 'react-router-dom'
import { Navbar } from '@/components/Navbar'
import { Footer } from '@/components/Footer'
import { lazy, Suspense } from 'react'
import { LoadingSpinner } from '@/components/LoadingSpinner'
import { AdminLayout } from '@/features/admin/components/AdminLayout'

const HomePage = lazy(() => import('@/pages/HomePage'))
const ProductsPage = lazy(() => import('@/pages/ProductsPage'))
const ProductDetailPage = lazy(() => import('@/pages/ProductDetailPage'))
const AboutPage = lazy(() => import('@/pages/AboutPage'))
const ContactPage = lazy(() => import('@/pages/ContactPage'))
const LoginPage = lazy(() => import('@/pages/LoginPage'))

const AdminDashboard = lazy(() => import('@/features/admin/pages/AdminDashboard'))
const AdminProducts = lazy(() => import('@/features/admin/pages/AdminProducts'))
const AdminCategories = lazy(() => import('@/features/admin/pages/AdminCategories'))
const AdminVehicles = lazy(() => import('@/features/admin/pages/AdminVehicles'))
const AdminAbout = lazy(() => import('@/features/admin/pages/AdminAbout'))
const AdminContact = lazy(() => import('@/features/admin/pages/AdminContact'))
const AdminVisits = lazy(() => import('@/features/admin/pages/AdminVisits'))

function RootLayout() {
  return (
    <div style={{ minHeight: '100vh', display: 'flex', flexDirection: 'column' }}>
      <ScrollRestoration />
      <Navbar />
      <main style={{ flex: 1 }}>
        <Suspense fallback={<LoadingSpinner />}>
          <Outlet />
        </Suspense>
      </main>
      <Footer />
    </div>
  )
}

export const router = createBrowserRouter([
  {
    element: <RootLayout />,
    children: [
      { path: '/', element: <HomePage /> },
      { path: '/urunler', element: <ProductsPage /> },
      { path: '/urunler/:id', element: <ProductDetailPage /> },
      { path: '/hakkimizda', element: <AboutPage /> },
      { path: '/iletisim', element: <ContactPage /> },
      { path: '/login', element: <LoginPage /> },
    ],
  },
  {
    element: (
      <Suspense fallback={<LoadingSpinner />}>
        <AdminLayout />
      </Suspense>
    ),
    children: [
      { path: '/admin', element: <AdminDashboard /> },
      { path: '/admin/urunler', element: <AdminProducts /> },
      { path: '/admin/kategoriler', element: <AdminCategories /> },
      { path: '/admin/araclar', element: <AdminVehicles /> },
      { path: '/admin/hakkimizda', element: <AdminAbout /> },
      { path: '/admin/iletisim', element: <AdminContact /> },
      { path: '/admin/ziyaretciler', element: <AdminVisits /> },
    ],
  },
])
