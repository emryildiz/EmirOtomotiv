import { useQuery } from '@tanstack/react-query'
import { productService } from '../services/productService'

export function useProduct(slug: string) {
  return useQuery({
    queryKey: ['products', slug],
    queryFn: () => productService.getBySlug(slug),
    enabled: !!slug,
  })
}
