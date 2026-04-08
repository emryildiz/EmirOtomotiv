import { Helmet } from 'react-helmet-async'

interface SEOMetaProps {
  title: string
  description: string
  canonical?: string
  keywords?: string
  ogTitle?: string
  ogDescription?: string
  noIndex?: boolean
}

const BASE_URL = 'https://emirotobusparca.com'

export function SEOMeta({
  title,
  description,
  canonical,
  keywords,
  ogTitle,
  ogDescription,
  noIndex = false,
}: SEOMetaProps) {
  const fullTitle = title.includes('Emir Otomotiv') ? title : `${title} | Emir Otomotiv`
  const canonicalUrl = canonical ? `${BASE_URL}${canonical}` : BASE_URL

  return (
    <Helmet>
      <title>{fullTitle}</title>
      <meta name="description" content={description} />
      {keywords && <meta name="keywords" content={keywords} />}
      <meta name="robots" content={noIndex ? 'noindex, nofollow' : 'index, follow'} />
      <link rel="canonical" href={canonicalUrl} />

      <meta property="og:title" content={ogTitle ?? fullTitle} />
      <meta property="og:description" content={ogDescription ?? description} />
      <meta property="og:url" content={canonicalUrl} />
      <meta property="og:type" content="website" />

      <meta name="twitter:title" content={ogTitle ?? fullTitle} />
      <meta name="twitter:description" content={ogDescription ?? description} />
    </Helmet>
  )
}
