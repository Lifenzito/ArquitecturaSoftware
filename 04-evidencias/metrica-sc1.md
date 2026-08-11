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

## Tabla comparativa

| | Arquitectura vieja (AS-IS) | Arquitectura nueva (TO-BE) |
|---|---|---|
| Clases **creadas** | 2 (`Cosmetico`, `Comestible`) | 4 (`Cosmetico`, `Comestible`, `CosmeticoFactory`, `ComestibleFactory`) |
| Clases **modificadas** | 1 (`ServicioProducto.CargarDesdeArchivo`) | 0 |
| Archivos de cableado tocados | 1 (`Program.cs`, para poder instanciarlos) | 1 (`Program.cs`: dos líneas en la lista de fábricas) |
| Archivos de datos tocados | 1 (`productos.txt`) | 1 (`productos.txt`) |
| Relación creadas : modificadas | 2 : 1 | 4 : 0 |

## Qué significa la diferencia

En la arquitectura vieja, la única forma de cargar un tipo nuevo era **editar
el cuerpo de `ServicioProducto.CargarDesdeArchivo`**, que hoy instancia
`MedicamentoCapsula` de forma incondicional. Ese método es el punto por donde
entra el 100 % de los datos del sistema: cualquier error ahí rompe la carga de
medicamentos, de las alertas de stock y de vencimiento, y del listado. Es
decir, el costo del cambio no es escribir dos clases, sino **el riesgo de
regresión sobre la funcionalidad existente**, que ninguna prueba cubría.

En la arquitectura nueva, `RepositoryProducto` ya no sabe qué tipos existen:
recibe `List<IProductoFactory>` y despacha por el discriminador de la fila.
Agregar cosméticos y comestibles fue **crear clases nuevas y registrarlas**;
ninguna clase existente cambió de comportamiento. La única línea tocada fuera
de las clases nuevas está en el cableado de `Program.cs`, que es precisamente
el lugar donde el diseño admite que se conozcan las implementaciones
concretas.

La prueba empírica de que el cambio no rompió nada es la comparación de
`04-evidencias/comparacion-comportamiento.md`: 8 de 11 casos idénticos byte a
byte y los 3 restantes con la única diferencia de las filas nuevas del archivo
de datos.

## Costo marginal del siguiente tipo de producto

Con el diseño nuevo, agregar un tipo más (por ejemplo, productos veterinarios)
cuesta: 1 clase de dominio + 1 fábrica + 1 línea en la lista de fábricas.
`RepositoryProducto`, los servicios, los verificadores y los eventos no se
tocan. Ese fue el punto de la SC-1.
