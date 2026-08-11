#!/usr/bin/env bash
# Ejecuta todos los casos de caracterizacion redirigiendo entrada y salida.
# Uso: ./ejecutar-casos.sh <original|rediseniado>
set -u

DESTINO="${1:-original}"
RAIZ="$(cd "$(dirname "$0")/../.." && pwd)"
ENTRADAS="$RAIZ/04-evidencias/caracterizacion/entradas"
SALIDAS="$RAIZ/04-evidencias/caracterizacion/$DESTINO"
APP="$RAIZ/AppFarmaciaConsola"

mkdir -p "$SALIDAS"

dotnet build "$RAIZ/SolucionFarmacia.sln" -c Release >/dev/null || exit 1
EJEC="$APP/bin/Release/net8.0"

for entrada in "$ENTRADAS"/*.in; do
    caso="$(basename "$entrada" .in)"
    # El caso 12 ejercita la opcion 8 y solo se corre sobre el rediseniado
    # completo: no entra en ninguna comparacion.
    if [ "$caso" = "caso-12-recorrido-demostracion" ] &&
       [ "$DESTINO" != "rediseniado" ]; then
        continue
    fi
    # Cada caso arranca con una copia limpia de los .txt: el estado en memoria
    # no se persiste, pero asi se garantiza aislamiento total entre casos.
    cp "$APP"/*.txt "$EJEC"/
    (cd "$EJEC" && dotnet AppFarmaciaConsola.dll < "$entrada" > "$SALIDAS/$caso.out" 2>&1)
    echo "$caso -> $SALIDAS/$caso.out"
done
