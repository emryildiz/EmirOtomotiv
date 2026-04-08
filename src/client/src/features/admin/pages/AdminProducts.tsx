import { useRef, useState } from 'react'
import { useProducts } from '@/features/products/hooks/useProducts'
import {
  useCreateProduct,
  useUpdateProduct,
  useDeleteProduct,
  useUploadProductImages,
  useDeleteProductImage,
  useSetPrimaryImage,
} from '@/features/products/hooks/useAdminProducts'
import { useVehicles } from '@/features/vehicles/hooks/useVehicles'
import { useCategories } from '@/features/categories/hooks/useCategories'
import { LoadingSpinner } from '@/components/LoadingSpinner'
import { getImageUrl } from '@/lib/imageUrl'
import type { Product } from '@/features/products/types'
import styles from './AdminPage.module.css'
import imageStyles from './AdminProductImages.module.css'

const emptyForm = { name: '', description: '', vehicleId: '', categoryId: '' }

export default function AdminProducts() {
  const { data: products, isLoading } = useProducts()
  const { data: vehicles } = useVehicles()
  const { data: categories } = useCategories()
  const { mutate: create, isPending: creating } = useCreateProduct()
  const { mutate: update, isPending: updating } = useUpdateProduct()
  const { mutate: remove } = useDeleteProduct()
  const { mutate: uploadImages, isPending: uploading } = useUploadProductImages()
  const { mutate: deleteImage } = useDeleteProductImage()
  const { mutate: setPrimary } = useSetPrimaryImage()

  const [form, setForm] = useState(emptyForm)
  const [editing, setEditing] = useState<Product | null>(null)
  const [name, setName] = useState('')
  const [description, setDescription] = useState('')
  const fileInputRef = useRef<HTMLInputElement>(null)

  function handleCreate() {
    if (!form.name.trim() || !form.vehicleId || !form.categoryId) return
    create(form, { onSuccess: () => setForm(emptyForm) })
  }

  function startEdit(p: Product) {
    setEditing(p)
    setName(p.name)
    setDescription(p.description ?? '')
  }

  function handleSave() {
    if (!editing) return
    update(
      { id: editing.id, data: { name, description } },
      {
        onSuccess: (_, vars) => {
          // Refresh editing product from updated list
          setEditing(prev => prev ? { ...prev, name: vars.data.name, description: vars.data.description ?? '' } : null)
        },
      },
    )
  }

  function handleFileChange(e: React.ChangeEvent<HTMLInputElement>) {
    if (!editing || !e.target.files || e.target.files.length === 0) return
    const files = Array.from(e.target.files)
    uploadImages(
      { productId: editing.id, files },
      {
        onSuccess: () => {
          if (fileInputRef.current) fileInputRef.current.value = ''
        },
      },
    )
  }

  if (isLoading) return <LoadingSpinner />

  return (
    <div className={styles.page}>
      <h1 className={styles.title}>Ürünler</h1>
      <p className={styles.subtitle}>Tüm ürünleri görüntüleyin, düzenleyin veya silin.</p>

      {/* Create form */}
      <div className={styles.form}>
        <h3 style={{ fontWeight: 600, color: 'var(--color-primary)', marginBottom: '0.25rem' }}>
          Yeni Ürün
        </h3>
        <div className={styles.formRow}>
          <div className="form-group">
            <label className="form-label">Ürün Adı</label>
            <input className="form-input" value={form.name} onChange={e => setForm(f => ({ ...f, name: e.target.value }))} placeholder="örn. Ön Tampon" />
          </div>
          <div className="form-group">
            <label className="form-label">Açıklama</label>
            <input className="form-input" value={form.description} onChange={e => setForm(f => ({ ...f, description: e.target.value }))} placeholder="İsteğe bağlı" />
          </div>
        </div>
        <div className={styles.formRow}>
          <div className="form-group">
            <label className="form-label">Araç</label>
            <select className="form-input" value={form.vehicleId} onChange={e => setForm(f => ({ ...f, vehicleId: e.target.value }))}>
              <option value="">Seçiniz</option>
              {vehicles?.map(v => <option key={v.id} value={v.id}>{v.name} — {v.model} ({v.year})</option>)}
            </select>
          </div>
          <div className="form-group">
            <label className="form-label">Kategori</label>
            <select className="form-input" value={form.categoryId} onChange={e => setForm(f => ({ ...f, categoryId: e.target.value }))}>
              <option value="">Seçiniz</option>
              {categories?.map(c => <option key={c.id} value={c.id}>{c.name}</option>)}
            </select>
          </div>
        </div>
        <div>
          <button
            className="btn btn-primary"
            onClick={handleCreate}
            disabled={creating || !form.name.trim() || !form.vehicleId || !form.categoryId}
          >
            {creating ? 'Ekleniyor...' : 'Ekle'}
          </button>
        </div>
      </div>

      {/* Edit form */}
      {editing && (
        <div className={styles.form}>
          <h3 style={{ fontWeight: 600, color: 'var(--color-primary)', marginBottom: '0.25rem' }}>
            Ürün Düzenle — {editing.name}
          </h3>

          {/* Basic fields */}
          <div className="form-group">
            <label className="form-label">Ad</label>
            <input className="form-input" value={name} onChange={e => setName(e.target.value)} />
          </div>
          <div className="form-group">
            <label className="form-label">Açıklama</label>
            <textarea
              className="form-input"
              rows={3}
              value={description}
              onChange={e => setDescription(e.target.value)}
              style={{ resize: 'vertical' }}
            />
          </div>
          <div className={styles.formActions}>
            <button className="btn btn-primary" onClick={handleSave} disabled={updating}>
              {updating ? 'Kaydediliyor...' : 'Kaydet'}
            </button>
            <button className="btn btn-outline" onClick={() => setEditing(null)}>
              İptal
            </button>
          </div>

          {/* Image management */}
          <hr style={{ margin: '1.5rem 0', border: 'none', borderTop: '1px solid var(--color-border)' }} />
          <p style={{ fontWeight: 600, color: 'var(--color-primary)', marginBottom: '1rem' }}>Resimler</p>

          {/* Existing images */}
          {editing.productImages && editing.productImages.length > 0 ? (
            <div className={imageStyles.grid}>
              {editing.productImages.map(img => (
                <div key={img.id} className={`${imageStyles.item} ${img.primaryImage ? imageStyles.primary : ''}`}>
                  <img
                    src={getImageUrl(img.imageUrl) ?? ''}
                    alt="ürün resmi"
                    className={imageStyles.thumb}
                  />
                  <div className={imageStyles.actions}>
                    {!img.primaryImage && (
                      <button
                        className={imageStyles.btnPrimary}
                        title="Ana resim yap"
                        onClick={() => setPrimary({ productId: editing.id, imageId: img.id })}
                      >
                        ★
                      </button>
                    )}
                    {img.primaryImage && (
                      <span className={imageStyles.primaryBadge}>Ana</span>
                    )}
                    <button
                      className={imageStyles.btnDelete}
                      title="Sil"
                      onClick={() => {
                        if (window.confirm('Resim silinsin mi?')) {
                          deleteImage({ productId: editing.id, imageId: img.id })
                          setEditing(prev =>
                            prev
                              ? { ...prev, productImages: prev.productImages?.filter(i => i.id !== img.id) ?? [] }
                              : null
                          )
                        }
                      }}
                    >
                      ✕
                    </button>
                  </div>
                </div>
              ))}
            </div>
          ) : (
            <p style={{ fontSize: '0.875rem', color: 'var(--color-text-muted)', marginBottom: '1rem' }}>Henüz resim yok.</p>
          )}

          {/* Upload new images */}
          <div className={imageStyles.upload}>
            <label className={imageStyles.uploadLabel}>
              <input
                ref={fileInputRef}
                type="file"
                accept="image/*"
                multiple
                className={imageStyles.fileInput}
                onChange={handleFileChange}
                disabled={uploading}
              />
              <span className={imageStyles.uploadBtn}>
                {uploading ? 'Yükleniyor...' : '+ Resim Ekle'}
              </span>
            </label>
            <p className={imageStyles.uploadHint}>JPG, PNG, WEBP — maks. 10 MB/dosya</p>
          </div>
        </div>
      )}

      {/* Product table */}
      <div className={styles.tableWrapper}>
        <table className={styles.table}>
          <thead>
            <tr>
              <th>Resim</th>
              <th>Ürün Adı</th>
              <th>Ürün No</th>
              <th>Kategori</th>
              <th>Araç</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {products?.length === 0 && (
              <tr><td colSpan={6} className={styles.empty}>Ürün bulunamadı.</td></tr>
            )}
            {products?.map(p => {
              const primary = p.productImages?.find(i => i.primaryImage) ?? p.productImages?.[0]
              return (
                <tr key={p.id}>
                  <td>
                    {primary ? (
                      <img
                        src={getImageUrl(primary.imageUrl) ?? ''}
                        alt={p.name}
                        style={{ width: 48, height: 36, objectFit: 'cover', borderRadius: 4 }}
                      />
                    ) : (
                      <div style={{ width: 48, height: 36, background: '#f0f0f0', borderRadius: 4 }} />
                    )}
                  </td>
                  <td>{p.name}</td>
                  <td>{p.productNumber}</td>
                  <td>{p.category?.name ?? '—'}</td>
                  <td>{p.vehicle?.name ?? '—'}</td>
                  <td>
                    <div className={styles.actions}>
                      <button className={styles.btnEdit} onClick={() => startEdit(p)}>Düzenle</button>
                      <button
                        className={styles.btnDelete}
                        onClick={() => window.confirm('Silinsin mi?') && remove(p.id)}
                      >
                        Sil
                      </button>
                    </div>
                  </td>
                </tr>
              )
            })}
          </tbody>
        </table>
      </div>
    </div>
  )
}
