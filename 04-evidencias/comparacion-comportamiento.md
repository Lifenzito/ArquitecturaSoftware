# Evidencia de preservación del comportamiento

Los mismos 11 archivos de entrada se ejecutaron contra el sistema original
(rama `caracterizacion-original`) y contra el sistema rediseñado
(rama `fase4-implementacion`), con la misma redirección de entrada y salida.
Los `diff` completos están en `04-evidencias/caracterizacion/diff/`.

## Tabla 1 — Sistema rediseñado completo (con SC-1)

| Caso | Escenario | Resultado | Líneas que difieren |
|------|-----------|-----------|---------------------|
| 1 | Arranque, login correcto y alertas | Idéntico | 0 |
| 2 | Login incorrecto | Idéntico | 0 |
| 3 | Listar productos | Difiere (esperado) | +6 |
| 4 | Buscar producto existente | Idéntico | 0 |
| 5 | Buscar producto inexistente | Idéntico | 0 |
| 6 | Buscar subcadena con varias coincidencias | Idéntico | 0 |
| 7 | Venta con stock suficiente | Difiere (esperado) | +12 |
| 8 | Venta con stock insuficiente (stock negativo) | Difiere (esperado) | +12 |
| 9 | Acumular puntos | Idéntico | 0 |
| 10 | Ver alertas desde el menú | Idéntico | 0 |
| 11 | Salir | Idéntico | 0 |

8 de 11 casos son byte a byte idénticos. Los 3 restantes difieren
**únicamente** por la excepción autorizada.

## Tabla 2 — Sistema rediseñado con el `productos.txt` original

Esta corrida aísla el refactor de la solicitud de cambio: es el **código
rediseñado** ejecutando el `productos.txt` **original**, sin las seis filas que
agrega SC-1 (verificado con `diff` contra el archivo de la rama
`caracterizacion-original`: idéntico). Las salidas están en
`04-evidencias/caracterizacion/rediseniado-sin-sc1/`.

| Caso | Escenario | Resultado | Líneas que difieren |
|------|-----------|-----------|---------------------|
| 1 | Arranque, login correcto y alertas | Idéntico | 0 |
| 2 | Login incorrecto | Idéntico | 0 |
| 3 | Listar productos | Idéntico | 0 |
| 4 | Buscar producto existente | Idéntico | 0 |
| 5 | Buscar producto inexistente | Idéntico | 0 |
| 6 | Buscar subcadena con varias coincidencias | Idéntico | 0 |
| 7 | Venta con stock suficiente | Idéntico | 0 |
| 8 | Venta con stock insuficiente (stock negativo) | Idéntico | 0 |
| 9 | Acumular puntos | Idéntico | 0 |
| 10 | Ver alertas desde el menú | Idéntico | 0 |
| 11 | Salir | Idéntico | 0 |

**11 de 11 idénticos byte a byte.**

## Conclusión

La refactorización preserva el comportamiento al 100 %. Las únicas diferencias
de la tabla 1 provienen de la solicitud de cambio SC-1 — seis filas de datos
nuevas en `productos.txt` — y no del rediseño de la arquitectura. Con el mismo
archivo de datos, el sistema viejo y el nuevo producen exactamente la misma
salida en los 11 casos.

## La única diferencia, explicada

SC-1 agrega seis filas al final de `AppFarmaciaConsola/productos.txt`
(tres cosméticos y tres comestibles). El sistema carga todo el archivo, así que
el listado de la opción 1 del menú muestra seis líneas más al final:

```
+Shampoo                12      18000
+JabonCorporal          7       9500
+CremaFacial            6       32000
+GaseosaCola            24      4000
+AguaBotella            30      2500
+HeladoVainilla         8       6500
```

- El caso 3 lista una vez, así que suma 6 líneas. Los casos 7 y 8 listan dos
  veces (antes y después de la venta), así que suman 12.
- Las filas se **agregaron al final** sin reordenar ni modificar las
  existentes: la búsqueda por `Contains` resuelve por primera coincidencia y
  cambiar el orden habría alterado el resultado del caso 6, que sigue
  devolviendo Amoxicilina.
- Las fechas de vencimiento de las filas nuevas son de 2027 y su stock está por
  encima del mínimo, así que **no** disparan alertas nuevas: los casos 1 y 10
  siguen imprimiendo exactamente las mismas 4 alertas de stock y 10 de
  vencimiento, en el mismo orden.

## Qué NO cambió, a propósito

| Conducta conservada | Evidencia |
|---------------------|-----------|
| El stock puede quedar negativo: no se agregó validación en la venta | Caso 8: `Amoxicilina -49` y `Venta registrada` |
| Los validadores existen pero no están conectados a ningún servicio | `ValidadorCliente` y `ValidadorProducto` no se referencian desde ningún servicio ni desde `Program.cs` |
| Las contraseñas siguen en texto plano y el formato de los `.txt` no cambió | `usuarios.txt` intacto |
| La regla de venta sigue en el `case 4` de `Program.cs` | No se creó clase de venta: no está en el diagrama |
| El sistema sigue sin calcular el valor de una venta | El `case 4` no multiplica precio por cantidad |
| La carga sigue abortando en silencio ante una fila mal formada | El `try/catch` que devuelve `ex.Message` se movió tal cual a los repositorios |
| Mensajes, colores y orden de impresión | Los `Console.ForegroundColor` y los literales se conservan uno a uno en `Program.cs` |

## Recorrido de demostración

El caso 12 (`caso-12-recorrido-demostracion.in`) ejercita la opción **8**, que
existe solo en el sistema rediseñado y **no se imprime en el menú**: agregar la
línea `8. ...` al menú habría cambiado la salida observable de todas las
opciones existentes, que es justamente lo que la restricción prohíbe. Por eso
la opción es alcanzable escribiendo `8` pero no aparece listada. En el sistema
original ese mismo `8` cae en `Opción inválida`.

El recorrido muestra: catálogo con medicamentos, cosméticos, comestibles y
procedimientos; una venta de producto con stock (2 → 1); una venta de un
procedimiento que **no** descuenta stock porque `Procedimiento` no implementa
`IProductoConStock`; y el descuento de un convenio de universidad
(15 % sobre 15000 = 2250).
