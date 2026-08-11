# Sistema Farmacia — Reto de Modernización Arquitectónica (SOLID)

Sistema legado de farmacia en C# / .NET 8 modernizado según el diagrama de
clases objetivo (TO-BE) de la Fase 3, **sin cambiar el comportamiento
observable**: las mismas entradas producen exactamente las mismas salidas, con
el mismo texto, el mismo orden y los mismos colores de consola.

## Cómo compilar y ejecutar

Requiere el SDK de .NET 8.

```bash
dotnet build SolucionFarmacia.sln -c Release
dotnet run --project AppFarmaciaConsola
```

Credenciales de la demo: usuario `admin`, contraseña `1234`
(están en `AppFarmaciaConsola/usuarios.txt`, en texto plano, tal como en el
sistema original).

El menú tiene las opciones 1 a 7 del sistema original. Existe además una
**opción 8 no listada**, un recorrido de demostración que muestra el catálogo
por tipo, una venta que descuenta stock, una venta de procedimiento que **no**
descuenta stock y el cálculo de un descuento por convenio. No se imprime en el
menú a propósito: listarla habría cambiado la salida de todas las opciones y
roto los casos de caracterización.

## Estructura

```
SolucionFarmacia.sln
├── BibFarmacia/                 biblioteca de clases (dominio y servicios)
│   ├── Clases/                  Persona, Producto, Medicamento, Procedimiento…
│   ├── Convenios/               Convenio y las cinco entidades de convenio
│   ├── Enums/                   TipoRelleno, MaterialEnvase, TipoConvenio…
│   ├── Eventos/                 los cuatro eventos del dominio
│   ├── Factories/               las siete fábricas de producto
│   ├── Interfaces/              contratos (IRepository*, IVerificador, IEvento…)
│   ├── Repositorios/            acceso a los archivos .txt
│   ├── Servicios/               lógica de aplicación
│   ├── Validadores/             ValidadorCliente y ValidadorProducto
│   └── Verificadores/           VerificadorStock y VerificadorVencimiento
├── AppFarmaciaConsola/          aplicación de consola y datos
│   ├── Program.cs               menú + cableado de las implementaciones
│   ├── productos.txt            catálogo (las 6 últimas filas son de SC-1)
│   ├── clientes.txt, usuarios.txt
│   └── convenios.txt            porcentajes de descuento por convenio
└── 04-evidencias/               evidencia académica de la Fase 4
```

## Decisiones de diseño

- **`Producto` solo tiene `Nombre`, `Precio` y `Proveedor`.** El stock y el
  vencimiento se modelan con interfaces de capacidad, `IProductoConStock` e
  `IVencimiento`. `Procedimiento` no implementa ninguna de las dos: ese es el
  punto del diseño, y por eso venderlo no descuenta stock.
- **`Proveedor` es abstracto**, con `Laboratorio` y `Marca` debajo: un
  medicamento tiene laboratorio y un cosmético tiene marca, sin campos nulos.
- **Los repositorios concentran el acceso a archivos.** El parseo se *movió*
  desde los servicios sin reescribirlo, conservando el mismo manejo de errores,
  incluida la carga parcial silenciosa.
- **`RepositoryProducto` no conoce los tipos de producto**: recibe
  `List<IProductoFactory>` y despacha por el discriminador de la fila. Agregar
  un tipo nuevo cuesta una clase de dominio, una fábrica y una línea en la
  lista de fábricas de `Program.cs`. Las filas de 6 columnas (formato heredado)
  se interpretan como medicamento cápsula para no romper la carga existente.
- **Los porcentajes de descuento están en `convenios.txt`**, no en el código:
  son un parámetro del negocio. Se leen en ejecución, así que cambiarlos no
  exige recompilar.
- `IProductoFactory` es el **único** tipo del código que no está dibujado en el
  diagrama; se agregó a conciencia porque sin él las siete fábricas quedan
  sueltas y se pierde el argumento de abierto/cerrado.

## Deuda técnica consciente

Está declarada a propósito y **no** se corrigió, porque arreglarla cambiaría el
comportamiento observable que la restricción del reto prohíbe alterar:

- La venta no valida disponibilidad: el stock puede quedar negativo.
- `ValidadorCliente` y `ValidadorProducto` existen pero no están conectados a
  ningún servicio.
- Las contraseñas se guardan en texto plano.
- La regla de venta vive en el `case 4` de `Program.cs`.
- El sistema no calcula el valor de ninguna venta.
- Un dato no numérico en "Seleccione opción" lanza una excepción no controlada
  (`int.Parse` sin validar), igual que en el código original.

## Evidencias (`04-evidencias/`)

| Archivo | Qué contiene |
|---|---|
| `caracterizacion/` | 11 casos de caracterización: entradas, salidas del sistema original, del rediseñado y del rediseñado con el `productos.txt` original, más el README de ejecución |
| `comparacion-comportamiento.md` | Diff de las salidas: 11/11 idénticos con el mismo archivo de datos; las únicas diferencias vienen de las filas nuevas de SC-1, no del rediseño |
| `metrica-sc1.md` | Métrica de SC-1: clases creadas frente a modificadas, arquitectura vieja contra nueva |
| `correspondencia-diagrama-codigo.md` | Comparación literal, nombre por nombre, entre el diagrama y el código |
| `bitacora-ia.md` | Bitácora de uso de IA: qué propuso la herramienta, qué decidió el equipo y con qué argumento |
| `plan-pruebas-consola.md` | Plan de pruebas manuales de la consola |

Para volver a ejecutar los casos de caracterización:

```bash
./04-evidencias/caracterizacion/ejecutar-casos.sh rediseniado
```
