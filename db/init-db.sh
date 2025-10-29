#!/bin/bash
set -e

SQL_HOST=${SQL_HOST:-sqlserver}
SQL_PORT=${SQL_PORT:-1433}
SQL_USER=${SQL_USER:-sa}
SQL_PASS=${SQLSERVER_SA_PASSWORD}
DB_NAME=${DB_NAME:-TorneioHubDb}

echo "Aguardando SQL Server em ${SQL_HOST}:${SQL_PORT}..."
for i in {1..60}; do
  if /opt/mssql-tools18/bin/sqlcmd -S ${SQL_HOST},${SQL_PORT} -U ${SQL_USER} -P "${SQL_PASS}" -C -Q "SELECT 1" >/dev/null 2>&1; then
    echo "SQL Server respondeu."
    break
  fi
  echo "Aguardando (${i}/60)..."
  sleep 5
done

echo "Rodando seed.sql no banco '${DB_NAME}' (criando se necessário)..."
/opt/mssql-tools18/bin/sqlcmd -S ${SQL_HOST},${SQL_PORT} -U ${SQL_USER} -P "${SQL_PASS}" -C -i /seed/seed.sql

echo "Seed finalizado."
