/**
 * Returns the full URL for an image.
 * - If imageUrl starts with "http" it's already absolute (e.g. Unsplash seeds).
 * - Otherwise it's a relative path served by the API (e.g. /uploads/products/xyz.jpg).
 */
export function getImageUrl(imageUrl: string | null | undefined): string | null {
  if (!imageUrl) return null
  if (imageUrl.startsWith('http')) return imageUrl
  return `${import.meta.env.VITE_API_URL}${imageUrl}`
}
