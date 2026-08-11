# Correspondencia diagrama TO-BE ↔ código

Comparación literal, nombre por nombre, entre los 61 elementos UML del
`diagramaSolucion.dia` y los tipos declarados en el código rediseñado.

## 1. Clases del diagrama que NO quedaron en el código

**Ninguna.** Los 61 elementos del diagrama existen en el código con el mismo
nombre:

Persona, Usuario, Cliente, Producto, Medicamento, MedicamentoCapsula,
MedicamentoLiquido, Laboratorio, Movimiento, IEntidadConvenio,
IServicioNotificacion, ConvenioEmpresa, ServicioNotificacion, EventoMovimiento,
EventoPuntos, EventoStockMinimo, EventoVencimiento, ServicioMovimiento,
ServicioCliente, ServicioProducto, ServicioUsuario, IVencimiento, Marca,
Proveedor, Cosmetico, Comestible, CosmeticoFactory, ComestibleFactory,
IRepositoryProducto, RepositoryProducto, IVerificador, VerificadorStock,
VerificadorVencimiento, IEvento, MedicamentoLiquidoFactory,
MedicamentoCapsulaFactory, ValidadorCliente, ValidadorProducto, IValidador,
IRepositoryUsuario, RepositoryUsuario, IServicioAutenticacion,
ServicioAutenticacion, IMovimientoRepository, MovimientoRepository,
IClienteRepository, ClienteRepository, Procedimiento, Inyectologia,
CambioVendaje, CuracionBasica, InyectologiaFactory, CambioVendajeFactory,
CuracionBasicaFactory, ConvenioBanco, ConvenioCooperativa, ConvenioMutual,
ConvenioUniversidad, IProductoConStock, ServicioDescuento, Convenio.

## 2. Clases del código que NO están en el diagrama

**Una sola: `IProductoFactory`.** Es la interfaz común de las siete fábricas.
Se agregó a conciencia y con justificación: sin ella `RepositoryProducto`
tendría que conocer todos los tipos de producto y agregar un tipo nuevo
obligaría a modificarla, lo que anularía la demostración de abierto/cerrado
que se mide en la métrica de SC-1.

Se eliminaron, por no estar en el diagrama: `IDescuento`, `ProductoFactory`,
`AspectoValidacion` y `AspectoAutenticacion` (esta última reemplazada por
`IServicioAutenticacion` / `ServicioAutenticacion`).

## 3. Enums

`TipoRelleno` y `MaterialEnvase` ya existían; `TipoConvenio` y `TipoBeneficio`
se crearon porque el diagrama los referencia como tipos de atributo
(`Convenio.TipoConvenio` e `IEntidadConvenio.tipoBeneficio`) sin dibujarlos
como elementos. No son clases, así que no alteran el conteo anterior.

## 4. Diferencias de detalle frente al diagrama (para defender en la sustentación)

Todas son de miembros, no de clases. Ninguna agrega ni quita tipos.

| Elemento | Diagrama | Código | Razón |
|---|---|---|---|
| Convenios | Las cinco entidades implementan `IEntidadConvenio` y no heredan de `Convenio`; declaran `CalcularDescuento` | `Convenio` implementa `IEntidadConvenio` y las cinco heredan de `Convenio`; la firma es `CalcularBeneficio` / `AplicaA` | Decisión explícita del equipo: `Cliente` apunta a `Convenio`, así que sin la herencia la cadena `Cliente → Convenio → cálculo` queda rota. La firma se unificó con la que declara la interfaz. |
| `ServicioDescuento` | `convenios : List<IConvenio>` | `List<IEntidadConvenio>` | `IConvenio` no existe en el diagrama. |
| `ServicioDescuento.CalcularDescuento` | Sin tipo de retorno | `decimal` | El diagrama lo omite. |
| `MedicamentoCapsulaFactory.crear` | Devuelve `MedicamentoLiquido` | Devuelve `MedicamentoCapsula` | Error de tipado del diagrama. |
| `InyectologiaFactory`, `CambioVendajeFactory`, `CuracionBasicaFactory` | Las tres devuelven `Cosmetico` | Cada una devuelve su procedimiento | Error de tipado del diagrama. |
| `MedicamentoLiquido` | Sin herencia dibujada hacia `Medicamento` | `MedicamentoLiquido : Medicamento` | La relación falta en el diagrama; el código original ya la tenía. |
| `Procedimiento`, `Inyectologia`, `CuracionBasica`, `CambioVendaje` | Vacías | `Procedimiento` abstracta con `DuracionMinutos`; las tres hijas con constructor | El diagrama las dibuja sin miembros; se define el mínimo para que compilen y funcionen. |
| `Producto` | Constructor con `stock` y `stockMinimo` | Constructor `(nombre, precio, proveedor)` | El propio diagrama ya quitó `Stock` de los atributos de `Producto`: el constructor dibujado quedó desactualizado. |
| `IRepositoryUsuario` | `agregarUsuario`, `cargar` | Además `ObtenerUsuarios()` | `ServicioAutenticacion` necesita la lista para validar el login; el diagrama omite el método. |
| `ServicioProducto` | Solo constructor | Además `ObtenerProductos`, `AgregarProducto`, `CargarDesdeArchivo`, `Verificar` | El diagrama no lista los métodos; son los que ya existían y `Program.cs` invoca. |
| `ServicioMovimiento.ResgistrarMovimiento` | Con la errata | `RegistrarMovimiento` | Errata del diagrama; el nombre correcto ya existía en el código original. |
| Nombres de métodos en minúscula (`crear`, `validar`, `verificar`, `obtenerProductos`, `cargar`…) | camelCase | PascalCase | Convención de C# y del código original. Los nombres de clases e interfaces se respetan exactamente. |
| `IEvento` | `Disparar(entidad : T)` | `Disparar(object entidad)` implementado de forma explícita en los cuatro eventos | Con `T` genérico no se puede tener una `List<IEvento>` no genérica como la que declara `ServicioProducto`. Cada evento conserva su `Disparar` tipado, que es el que se usa. |
| `IValidador` | `validar(entidad : T)` | `IValidador<T>` | Ningún punto del diseño necesita una lista heterogénea de validadores, así que el genérico se conserva tal cual. |
| `EventoPuntos` | No implementa `IEvento` en el diagrama | Lo implementa | Instrucción explícita del equipo. |
