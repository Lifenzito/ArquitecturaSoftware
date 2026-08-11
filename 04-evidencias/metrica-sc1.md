# Métrica de la solicitud de cambio implementada (SC-1)

**SC-1: la farmacia necesita vender también cosméticos y productos comestibles.**

## Por qué se eligió SC-1

- Es la única de las tres solicitudes que, según la medición de la Fase 2,
  tiene **más clases agregadas que modificadas**: es la que mejor exhibe el
  principio abierto/cerrado.
- Es demostrable de extremo a extremo con el sistema corriendo: los productos
  nuevos se cargan del archivo, aparecen en el catálogo, se buscan y se venden
  por las mismas opciones del menú que los medicamentos, sin tocar esas
  opciones.
- SC-2 (procedimientos) y SC-3 (convenios) también quedaron implementadas para
  ser fieles al diagrama, pero la métrica formal se levanta sobre SC-1.

## Regla de conteo

Se usa la misma regla declarada en la Fase 2, para que las dos mediciones sean
comparables. Una **unidad de código modificada** es un archivo `.cs` existente
cuyo contenido debe cambiar. En esta solución hay un tipo por archivo, de modo
que unidades y clases coinciden, salvo en `Program.cs`, que no declara un tipo
con nombre y aun así se cuenta como unidad modificada. Los archivos de datos se
cuentan aparte por no ser código.

## Tabla comparativa

| | Arquitectura vieja (AS-IS) | Arquitectura nueva (TO-BE) |
|---|---|---|
| Clases **creadas** | 2 (`Cosmetico`, `Comestible`) | 4 (`Cosmetico`, `Comestible`, `CosmeticoFactory`, `ComestibleFactory`) |
| **Unidades de código modificadas** | 1 | 1 |
| — de ellas, con reglas de negocio | **1** (`ServicioProducto.CargarDesdeArchivo`) | **0** |
| — de ellas, solo cableado | 0 | 1 (`Program.cs`, dos líneas en la lista de fábricas) |
| Archivos de datos modificados | 1 (`productos.txt`) | 1 (`productos.txt`) |
| Relación creadas : modificadas | 2 : 1 | 4 : 1 |

## Qué significa la diferencia

El número de unidades modificadas es el mismo en ambas arquitecturas: una. Lo
que cambia, y es donde está el valor del rediseño, es **qué** se modifica y qué
riesgo trae modificarlo.

En la arquitectura vieja hay que editar el cuerpo de
`ServicioProducto.CargarDesdeArchivo`, que hoy instancia `MedicamentoCapsula`
de forma incondicional. Ese método es el punto por donde entra el cien por
ciento de los datos del sistema: un error ahí rompe la carga de medicamentos,
las alertas de stock, las de vencimiento y el listado. El costo del cambio no
es escribir dos clases, sino el riesgo de regresión sobre funcionalidad
existente que ninguna prueba cubría.

En la arquitectura nueva, `RepositoryProducto` ya no sabe qué tipos existen:
recibe `List<IProductoFactory>` y despacha por el discriminador de la fila.
La única unidad modificada es `Program.cs`, y ahí está el punto: `Program.cs`
es el composition root, es decir, **el único lugar que el diseño designa para
conocer implementaciones concretas**. El principio abierto/cerrado pide que los
módulos con reglas de negocio queden cerrados a modificación, no que el
cableado nunca cambie. Ninguna clase con reglas de negocio se tocó.

Dicho en una línea: se pasó de modificar el método por donde entran todos los
datos, a agregar dos líneas a una lista de fábricas.

La prueba empírica de que el cambio no rompió nada está en
`04-evidencias/comparacion-comportamiento.md`.

## Costo marginal del siguiente tipo de producto

Con el diseño nuevo, agregar un tipo más (por ejemplo, productos veterinarios)
cuesta: 1 clase de dominio, 1 fábrica y 1 línea en la lista de fábricas.
`RepositoryProducto`, los servicios, los verificadores y los eventos no se
tocan. Ese fue el punto de SC-1.

## Coherencia con la Fase 2

La línea base de la Fase 2 midió, sobre el código original, 1 unidad de código
modificada (`ServicioProducto`) y 2 clases nuevas para SC-1. Esta tabla
confirma esa medición en la columna de arquitectura vieja. Las cifras de las
dos fases usan la misma regla de conteo y no se contradicen.
