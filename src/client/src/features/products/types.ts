import type { VehicleDto, CategoryDto } from '@/types/api'

export interface ProductImage {
  id: string
  imageUrl: string
  primaryImage: boolean
}

export interface Product {
  id: string
  name: string
  description: string
  productNumber: string
  vehicle: VehicleDto
  category: CategoryDto
  productImages: ProductImage[] | null
}

export interface CreateProductRequest {
  name: string
  description: string
  vehicleId: string
  categoryId: string
}
