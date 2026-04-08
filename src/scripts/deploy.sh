#!/bin/bash
# Sunucuda ilk kurulum ve güncelleme için çalıştırılır.
# Kullanım: bash scripts/deploy.sh

set -e

ENV_FILE=".env"

if [ ! -f "$ENV_FILE" ]; then
  echo "HATA: .env dosyası bulunamadı."
  echo "  cp .env.prod.example .env  →  sonra doldurun."
  exit 1
fi

# ---------- İlk kurulumda SSL sertifikası al ----------
CERT_PATH="./certbot/conf/live/emirotobusparca.com/fullchain.pem"

if [ ! -f "$CERT_PATH" ]; then
  echo ">>> SSL sertifikası alınıyor (port 80 boş olmalı)..."
  docker compose -f docker-compose.init.yml up --abort-on-container-exit
  echo ">>> Sertifika alındı."
fi

# ---------- Prod stack'i derle ve başlat ----------
echo ">>> Prod stack build ediliyor..."
docker compose -f docker-compose.prod.yml up -d --build

echo ""
echo "✓ Uygulama ayakta → https://emirotobusparca.com"
