import { useMutation, useQueryClient } from '@tanstack/react-query'
import { categoryService } from '../services/categoryService'

export function useCreateCategory() {
  const qc = useQueryClient()
  return useMutation({
    mutationFn: (name: string) => categoryService.create(name),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['categories'] }),
  })
}

export function useUpdateCategory() {
  const qc = useQueryClient()
  return useMutation({
    mutationFn: ({ id, name }: { id: string; name: string }) =>
      categoryService.update(id, name),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['categories'] }),
  })
}

export function useDeleteCategory() {
  const qc = useQueryClient()
  return useMutation({
    mutationFn: (id: string) => categoryService.delete(id),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['categories'] }),
  })
}
