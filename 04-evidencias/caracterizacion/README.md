# Casos de caracterización

Evidencia de conducta observable del sistema. Los mismos archivos de entrada se
ejecutan contra el código **original** (rama `caracterizacion-original`) y
contra el código **rediseñado**, y las salidas se comparan con `diff`.

## Estructura

```
04-evidencias/caracterizacion/
├── entradas/            # un archivo .in por caso: las teclas que se digitan
├── original/            # salida de consola del sistema original
├── rediseniado/         # salida de consola del sistema rediseñado
├── ejecutar-casos.sh    # ejecuta todos los casos con redirección
└── README.md
```

## Cómo se ejecutan

Requiere .NET SDK 8.

El script compila y ejecuta **el código que esté en la copia de trabajo**, así
que la salida `original` solo se regenera estando en la rama
`caracterizacion-original`:

```bash
# todos los casos
git checkout caracterizacion-original
./04-evidencias/caracterizacion/ejecutar-casos.sh original      # sobre el AS-IS

git checkout fase4-implementacion
./04-evidencias/caracterizacion/ejecutar-casos.sh rediseniado   # sobre el TO-BE

# un caso suelto, manualmente
dotnet build SolucionFarmacia.sln -c Release
cd AppFarmaciaConsola/bin/Release/net8.0
dotnet AppFarmaciaConsola.dll \
  < ../../../../04-evidencias/caracterizacion/entradas/caso-01-arranque-login-correcto.in \
  > salida.txt 2>&1
```

El script copia los `.txt` de datos al directorio de ejecución antes de cada
caso, de modo que todos parten del mismo estado inicial.

## Comparación

```bash
for f in 04-evidencias/caracterizacion/entradas/*.in; do
  c=$(basename "$f" .in)
  diff 04-evidencias/caracterizacion/original/$c.out \
       04-evidencias/caracterizacion/rediseniado/$c.out
done
```

## Catálogo de casos

| Caso | Archivo de entrada | Teclas | Qué caracteriza |
|------|--------------------|--------|-----------------|
| 1 | `caso-01-arranque-login-correcto.in` | `admin`, `1234`, `7` | Arranque completo: carga de los tres `.txt`, login correcto, 4 alertas de stock mínimo y 10 de vencimiento, menú y salida. |
| 2 | `caso-02-login-incorrecto.in` | `admin`, `claveMala` | Login fallido: imprime `Acceso denegado` y termina sin mostrar el menú ni `FIN DEL SISTEMA`. |
| 3 | `caso-03-listar-productos.in` | `1`, `7` | Listado completo del catálogo con el formato de tabulaciones actual. |
| 4 | `caso-04-buscar-producto-existente.in` | `3`, `Dolex`, `7` | Búsqueda que encuentra: nombre, precio y stock. |
| 5 | `caso-05-buscar-producto-inexistente.in` | `3`, `Zzzzz`, `7` | Búsqueda sin resultados: `Producto no encontrado`. |
| 6 | `caso-06-buscar-subcadena-multiple.in` | `3`, `a`, `7` | Búsqueda por `Contains` con varias coincidencias: devuelve **Amoxicilina**, la primera del archivo que contiene la letra `a` en minúscula. Documenta que el resultado depende del orden de las filas de `productos.txt`. |
| 7 | `caso-07-venta-stock-suficiente.in` | `1`, `4`, `Ibuprofeno`, `3`, `1`, `7` | Venta normal: stock antes y después (10 → 7), evento de movimiento y `Venta registrada`. No se calcula valor de la venta. |
| 8 | `caso-08-venta-stock-insuficiente.in` | `1`, `4`, `Amoxicilina`, `50`, `1`, `7` | Venta por encima del stock: el stock queda en **-49** y el sistema imprime `Venta registrada` igual. Deuda consciente, se conserva. |
| 9 | `caso-09-acumular-puntos.in` | `2`, `5`, `Carlos`, `100`, `2`, `7` | Acumulación de puntos con listado de clientes antes y después, y el evento de puntos. |
| 10 | `caso-10-ver-alertas.in` | `6`, `7` | Alertas disparadas desde la opción 6 del menú. |
| 11 | `caso-11-salir.in` | `7` | Salida limpia: `Saliendo del sistema...` y `FIN DEL SISTEMA`. |
| 12 | `caso-12-recorrido-demostracion.in` | `8`, `7` | Recorrido de demostración del sistema rediseñado (opción 8). **No tiene contraparte en el original**: allí la opción 8 cae en `Opción inválida`. No entra en la comparación. |

## Notas sobre determinismo

- Las salidas capturadas **no contienen colores** porque la redirección a
  archivo descarta las secuencias de la consola. Los colores se verifican
  aparte, por inspección del código: rojo para stock mínimo y acceso denegado,
  amarillo para vencimiento, verde para puntos y login correcto, cian para
  movimientos y encabezado de productos, magenta para el menú, azul para el
  login, verde oscuro para el mensaje de carga.
- Todas las fechas de vencimiento de `productos.txt` ya pasaron, así que los
  diez productos disparan la alerta de vencimiento en cualquier fecha de
  ejecución actual o futura. No hay salida dependiente de la fecha del día.
- Ninguna salida imprime fecha ni hora, aunque `Movimiento` guarde
  `DateTime.Now` internamente.
