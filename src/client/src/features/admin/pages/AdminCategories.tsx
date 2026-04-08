import { useState } from 'react'
import { useCategories } from '@/features/categories/hooks/useCategories'
import {
  useCreateCategory,
  useUpdateCategory,
  useDeleteCategory,
} from '@/features/categories/hooks/useAdminCategories'
import { LoadingSpinner } from '@/components/LoadingSpinner'
import type { Category } from '@/features/categories/types'
import styles from './AdminPage.module.css'

export default function AdminCategories() {
  const { data: categories, isLoading } = useCategories()
  const { mutate: create, isPending: creating } = useCreateCategory()
  const { mutate: update, isPending: updating } = useUpdateCategory()
  const { mutate: remove } = useDeleteCategory()

  const [newName, setNewName] = useState('')
  const [editing, setEditing] = useState<Category | null>(null)
  const [editName, setEditName] = useState('')

  function handleCreate() {
    if (!newName.trim()) return
    create(newName.trim(), { onSuccess: () => setNewName('') })
  }

  function startEdit(c: Category) {
    setEditing(c)
    setEditName(c.name)
  }

  function handleSave() {
    if (!editing) return
    update({ id: editing.id, name: editName }, { onSuccess: () => setEditing(null) })
  }

  if (isLoading) return <LoadingSpinner />

  return (
    <div className={styles.page}>
      <h1 className={styles.title}>Kategoriler</h1>
      <p className={styles.subtitle}>Ürün kategorilerini yönetin.</p>

      <div className={styles.form}>
        <h3 style={{ fontWeight: 600, color: 'var(--color-primary)' }}>Yeni Kategori</h3>
        <div style={{ display: 'flex', gap: '0.75rem', alignItems: 'flex-end' }}>
          <div className="form-group" style={{ flex: 1 }}>
            <label className="form-label">Kategori Adı</label>
            <input className="form-input" value={newName} onChange={e => setNewName(e.target.value)} placeholder="örn. Ön Tampon" />
          </div>
          <button className="btn btn-primary" onClick={handleCreate} disabled={creating || !newName.trim()}>
            {creating ? 'Ekleniyor...' : 'Ekle'}
          </button>
        </div>
      </div>

      {editing && (
        <div className={styles.form}>
          <h3 style={{ fontWeight: 600, color: 'var(--color-primary)' }}>Düzenle</h3>
          <div style={{ display: 'flex', gap: '0.75rem', alignItems: 'flex-end' }}>
            <div className="form-group" style={{ flex: 1 }}>
              <label className="form-label">Kategori Adı</label>
              <input className="form-input" value={editName} onChange={e => setEditName(e.target.value)} />
            </div>
            <div className={styles.formActions}>
              <button className="btn btn-primary" onClick={handleSave} disabled={updating}>
                {updating ? 'Kaydediliyor...' : 'Kaydet'}
              </button>
              <button className="btn btn-outline" onClick={() => setEditing(null)}>İptal</button>
            </div>
          </div>
        </div>
      )}

      <div className={styles.tableWrapper}>
        <table className={styles.table}>
          <thead>
            <tr><th>Kategori Adı</th><th></th></tr>
          </thead>
          <tbody>
            {categories?.map(c => (
              <tr key={c.id}>
                <td>{c.name}</td>
                <td>
                  <div className={styles.actions}>
                    <button className={styles.btnEdit} onClick={() => startEdit(c)}>Düzenle</button>
                    <button
                      className={styles.btnDelete}
                      onClick={() => window.confirm('Silinsin mi?') && remove(c.id)}
                    >
                      Sil
                    </button>
                  </div>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  )
}
