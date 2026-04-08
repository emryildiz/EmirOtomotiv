export interface NamedCount {
  name: string
  count: number
  pct: number
}

export interface DailyCount {
  date: string
  count: number
}

export interface VisitStatsResponse {
  total: number
  today: number
  uniqueIps: number
  daily: DailyCount[]
  topPages: NamedCount[]
  topCities: NamedCount[]
  devices: NamedCount[]
  browsers: NamedCount[]
}
