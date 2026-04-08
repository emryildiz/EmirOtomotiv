import { useMutation, useQueryClient } from '@tanstack/react-query'
import { productService, type CreateProductDto, type UpdateProductDto } from '../services/productService'

export function useCreateProduct() {
  const qc = useQueryClient()
  return useMutation({
    mutationFn: (data: CreateProductDto) => productService.create(data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['products'] }),
  })
}

export function useUpdateProduct() {
  const qc = useQueryClient()
  return useMutation({
    mutationFn: ({ id, data }: { id: string; data: UpdateProductDto }) =>
      productService.update(id, data),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['products'] }),
  })
}

export function useDeleteProduct() {
  const qc = useQueryClient()
  return useMutation({
    mutationFn: (id: string) => productService.delete(id),
    onSuccess: () => qc.invalidateQueries({ queryKey: ['products'] }),
  })
}
