import { useState } from 'react'
import {
  useVehicles,
  useCreateVehicle,
  useUpdateVehicle,
  useDeleteVehicle,
} from '@/features/vehicles/hooks/useVehicles'
import { LoadingSpinner } from '@/components/LoadingSpinner'
import type { Vehicle } from '@/features/vehicles/types'
import styles from './AdminPage.module.css'

const empty = { name: '', model: '', year: '' }

export default function AdminVehicles() {
  const { data: vehicles, isLoading } = useVehicles()
  const { mutate: create, isPending: creating } = useCreateVehicle()
  const { mutate: update, isPending: updating } = useUpdateVehicle()
  const { mutate: remove } = useDeleteVehicle()

  const [form, setForm] = useState(empty)
  const [editing, setEditing] = useState<Vehicle | null>(null)
  const [editForm, setEditForm] = useState(empty)

  function handleCreate() {
    if (!form.name.trim()) return
    create(form, { onSuccess: () => setForm(empty) })
  }

  function startEdit(v: Vehicle) {
    setEditing(v)
    setEditForm({ name: v.name, model: v.model, year: v.year })
  }

  function handleSave() {
    if (!editing) return
    update({ id: editing.id, data: editForm }, { onSuccess: () => setEditing(null) })
  }

  if (isLoading) return <LoadingSpinner />

  return (
    <div className={styles.page}>
      <h1 className={styles.title}>Araçlar</h1>
      <p className={styles.subtitle}>Araç tiplerini yönetin.</p>

      <div className={styles.form}>
        <h3 style={{ fontWeight: 600, color: 'var(--color-primary)' }}>Yeni Araç</h3>
        <div className={styles.formRow}>
          <div className="form-group">
            <label className="form-label">Araç Adı</label>
            <input className="form-input" value={form.name} onChange={e => setForm(f => ({ ...f, name: e.target.value }))} placeholder="örn. Prestij" />
          </div>
          <div className="form-group">
            <label className="form-label">Model</label>
            <input className="form-input" value={form.model} onChange={e => setForm(f => ({ ...f, model: e.target.value }))} placeholder="örn. Standart" />
          </div>
          <div className="form-group">
            <label className="form-label">Yıl</label>
            <input className="form-input" value={form.year} onChange={e => setForm(f => ({ ...f, year: e.target.value }))} placeholder="örn. 2024" />
          </div>
        </div>
        <div>
          <button className="btn btn-primary" onClick={handleCreate} disabled={creating || !form.name.trim()}>
            {creating ? 'Ekleniyor...' : 'Ekle'}
          </button>
        </div>
      </div>

      {editing && (
        <div className={styles.form}>
          <h3 style={{ fontWeight: 600, color: 'var(--color-primary)' }}>Düzenle</h3>
          <div className={styles.formRow}>
            <div className="form-group">
              <label className="form-label">Araç Adı</label>
              <input className="form-input" value={editForm.name} onChange={e => setEditForm(f => ({ ...f, name: e.target.value }))} />
            </div>
            <div className="form-group">
              <label className="form-label">Model</label>
              <input className="form-input" value={editForm.model} onChange={e => setEditForm(f => ({ ...f, model: e.target.value }))} />
            </div>
            <div className="form-group">
              <label className="form-label">Yıl</label>
              <input className="form-input" value={editForm.year} onChange={e => setEditForm(f => ({ ...f, year: e.target.value }))} />
            </div>
          </div>
          <div className={styles.formActions}>
            <button className="btn btn-primary" onClick={handleSave} disabled={updating}>
              {updating ? 'Kaydediliyor...' : 'Kaydet'}
            </button>
            <button className="btn btn-outline" onClick={() => setEditing(null)}>İptal</button>
          </div>
        </div>
      )}

      <div className={styles.tableWrapper}>
        <table className={styles.table}>
          <thead>
            <tr><th>Araç</th><th>Model</th><th>Yıl</th><th></th></tr>
          </thead>
          <tbody>
            {vehicles?.map(v => (
              <tr key={v.id}>
                <td>{v.name}</td>
                <td>{v.model}</td>
                <td>{v.year}</td>
                <td>
                  <div className={styles.actions}>
                    <button className={styles.btnEdit} onClick={() => startEdit(v)}>Düzenle</button>
                    <button
                      className={styles.btnDelete}
                      onClick={() => window.confirm('Silinsin mi?') && remove(v.id)}
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
