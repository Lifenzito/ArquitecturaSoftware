using BibFarmacia.Clases;
using BibFarmacia.Convenios;
using BibFarmacia.Enum;
using BibFarmacia.Eventos;
using BibFarmacia.Factories;
using BibFarmacia.Interfaces;
using BibFarmacia.Repositorios;
using BibFarmacia.Servicios;
using BibFarmacia.Verificadores;

Console.Title = "Sistema Farmacia";

List<IProductoFactory> fabricas =
    new List<IProductoFactory>
    {
        new MedicamentoCapsulaFactory(),
        new MedicamentoLiquidoFactory(),
        new CosmeticoFactory(),
        new ComestibleFactory(),
        new InyectologiaFactory(),
        new CuracionBasicaFactory(),
        new CambioVendajeFactory()
    };

IRepositoryProducto repositoryProducto =
    new RepositoryProducto(fabricas);

IClienteRepository clienteRepository =
    new ClienteRepository();

IRepositoryUsuario repositoryUsuario =
    new RepositoryUsuario();

IMovimientoRepository movimientoRepository =
    new MovimientoRepository();

EventoStockMinimo eventoStock =
    new EventoStockMinimo();

EventoVencimiento eventoVencimiento =
    new EventoVencimiento();

List<IVerificador> verificadores =
    new List<IVerificador>
    {
        new VerificadorStock(eventoStock),
        new VerificadorVencimiento(
            eventoVencimiento)
    };

ServicioCliente servicioCliente =
    new ServicioCliente(
        clienteRepository);

ServicioUsuario servicioUsuario =
    new ServicioUsuario(
        repositoryUsuario);

ServicioMovimiento servicioMovimiento =
    new ServicioMovimiento(
        movimientoRepository);

IServicioAutenticacion servicioAutenticacion =
    new ServicioAutenticacion(
        repositoryUsuario);

List<IEntidadConvenio> convenios =
    new List<IEntidadConvenio>
    {
        new ConvenioEmpresa("Sofka"),
        new ConvenioBanco("Bancolombia"),
        new ConvenioCooperativa("Coomeva"),
        new ConvenioMutual("Mutual Ser"),
        new ConvenioUniversidad("UPB")
    };

ServicioDescuento servicioDescuento =
    new ServicioDescuento(convenios);

List<IEvento> eventos =
    new List<IEvento>
    {
        eventoStock,
        eventoVencimiento,
        servicioCliente.EventoPuntos,
        servicioMovimiento.EventoMovimiento
    };

ServicioProducto servicioProducto =
    new ServicioProducto(
        repositoryProducto,
        verificadores,
        eventos);

// ================= EVENTOS =================

eventoStock.StockMinimo +=
    mensaje =>
    {
        Console.ForegroundColor =
            ConsoleColor.Red;

        Console.WriteLine(mensaje);

        Console.ResetColor();
    };

eventoVencimiento.Vencimiento +=
    mensaje =>
    {
        Console.ForegroundColor =
            ConsoleColor.Yellow;

        Console.WriteLine(mensaje);

        Console.ResetColor();
    };

servicioCliente.EventoPuntos.PuntosAcumulados +=
    mensaje =>
    {
        Console.ForegroundColor =
            ConsoleColor.Green;

        Console.WriteLine(mensaje);

        Console.ResetColor();
    };

servicioMovimiento.EventoMovimiento
    .MovimientoRegistrado +=
    mensaje =>
    {
        Console.ForegroundColor =
            ConsoleColor.Cyan;

        Console.WriteLine(mensaje);

        Console.ResetColor();
    };

// ================= CARGA TXT =================

Console.ForegroundColor =
    ConsoleColor.DarkGreen;

Console.WriteLine(
    "Cargando información del sistema...\n");

Console.ResetColor();

Console.WriteLine(
    servicioProducto.CargarDesdeArchivo(
        "productos.txt"));

Console.WriteLine(
    servicioCliente.Cargar(
        "clientes.txt"));

Console.WriteLine(
    servicioUsuario.Cargar(
        "usuarios.txt"));

Console.WriteLine();

// ================= LOGIN =================

Console.ForegroundColor =
    ConsoleColor.Blue;

Console.WriteLine(
    "=========== LOGIN ===========");

Console.ResetColor();

Console.Write("Usuario: ");
string user =
    Console.ReadLine()!;

Console.Write("Contraseña: ");
string password =
    Console.ReadLine()!;

bool login =
    servicioAutenticacion.Login(
        user,
        password);

if (!login)
{
    Console.ForegroundColor =
        ConsoleColor.Red;

    Console.WriteLine(
        "\nAcceso denegado");

    Console.ResetColor();

    return;
}

Console.ForegroundColor =
    ConsoleColor.Green;

Console.WriteLine(
    "\nLogin correcto");

Console.ResetColor();

// ================= ALERTAS =================

servicioProducto.Verificar();

// ================= MENÚ =================

int opcion = 0;

while (opcion != 7)
{
    Console.ForegroundColor =
        ConsoleColor.Magenta;

    Console.WriteLine("\n==============================");
    Console.WriteLine("      SISTEMA FARMACIA");
    Console.WriteLine("==============================");

    Console.ResetColor();

    Console.WriteLine("1. Ver productos");
    Console.WriteLine("2. Ver clientes");
    Console.WriteLine("3. Buscar producto");
    Console.WriteLine("4. Registrar venta");
    Console.WriteLine("5. Acumular puntos");
    Console.WriteLine("6. Ver alertas");
    Console.WriteLine("7. Salir");

    Console.Write("\nSeleccione opción: ");

    opcion =
        int.Parse(Console.ReadLine()!);

    switch (opcion)
    {
        case 1:

            Console.ForegroundColor =
                ConsoleColor.Cyan;

            Console.WriteLine(
                "\n===== PRODUCTOS =====");

            Console.ResetColor();

            Console.WriteLine(
                "Nombre\t\tStock\tPrecio");

            Console.WriteLine(
                "-----------------------------------");

            foreach (var producto in
                servicioProducto.ObtenerProductos())
            {
                Console.WriteLine(
                    $"{producto.Nombre}\t\t" +
                    $"{(producto as IProductoConStock)?.Stock}\t" +
                    $"{producto.Precio}");
            }

            break;

        case 2:

            Console.ForegroundColor =
                ConsoleColor.Green;

            Console.WriteLine(
                "\n===== CLIENTES =====");

            Console.ResetColor();

            foreach (var cliente in
                servicioCliente.ObtenerClientes())
            {
                Console.WriteLine(
                    $"{cliente.Nombre} - " +
                    $"Puntos: {cliente.Puntos}");
            }

            break;

        case 3:

            Console.Write(
                "\nIngrese nombre producto: ");

            string nombre =
                Console.ReadLine()!;

            var productoBuscado =
                servicioProducto
                .ObtenerProductos()
                .FirstOrDefault(p =>
                    p.Nombre.ToLower()
                    .Contains(nombre.ToLower()));

            if (productoBuscado != null)
            {
                Console.WriteLine(
                    $"\nProducto: " +
                    $"{productoBuscado.Nombre}");

                Console.WriteLine(
                    $"Precio: " +
                    $"{productoBuscado.Precio}");

                Console.WriteLine(
                    $"Stock: " +
                    $"{(productoBuscado as IProductoConStock)?.Stock}");
            }
            else
            {
                Console.WriteLine(
                    "\nProducto no encontrado");
            }

            break;

        case 4:

            Console.Write(
                "\nNombre producto: ");

            string nombreVenta =
                Console.ReadLine()!;

            var productoVenta =
                servicioProducto
                .ObtenerProductos()
                .FirstOrDefault(p =>
                    p.Nombre.ToLower()
                    .Contains(
                        nombreVenta.ToLower()));

            if (productoVenta != null)
            {
                Console.Write(
                    "Cantidad: ");

                int cantidad =
                    int.Parse(
                        Console.ReadLine()!);

                if (productoVenta is
                    IProductoConStock productoConStock)
                {
                    productoConStock.Stock -=
                        cantidad;
                }

                Movimiento venta =
                    new Movimiento(
                        DateTime.Now,
                        cantidad,
                        "Venta",
                        productoVenta);

                servicioMovimiento
                    .RegistrarMovimiento(
                        venta);

                Console.WriteLine(
                    "\nVenta registrada");
            }
            else
            {
                Console.WriteLine(
                    "\nProducto no encontrado");
            }

            break;

        case 5:

            Console.Write(
                "\nNombre cliente: ");

            string nombreCliente =
                Console.ReadLine()!;

            var clientePuntos =
                servicioCliente
                .ObtenerClientes()
                .FirstOrDefault(c =>
                    c.Nombre.ToLower()
                    .Contains(
                        nombreCliente.ToLower()));

            if (clientePuntos != null)
            {
                Console.Write(
                    "Puntos: ");

                int puntos =
                    int.Parse(
                        Console.ReadLine()!);

                servicioCliente
                    .AcumularPuntos(
                        clientePuntos,
                        puntos);
            }
            else
            {
                Console.WriteLine(
                    "\nCliente no encontrado");
            }

            break;

        case 6:

            Console.WriteLine(
                "\nVerificando alertas...");

            servicioProducto
                .Verificar();

            break;

        case 8:

            // Recorrido de demostracion. No se lista en el menu para no
            // alterar la salida observable de las opciones originales.

            Console.ForegroundColor =
                ConsoleColor.Cyan;

            Console.WriteLine(
                "\n===== DEMOSTRACIÓN =====");

            Console.ResetColor();

            InyectologiaFactory inyectologiaFactory =
                new InyectologiaFactory();

            CuracionBasicaFactory curacionFactory =
                new CuracionBasicaFactory();

            CambioVendajeFactory vendajeFactory =
                new CambioVendajeFactory();

            Marca marcaServicios =
                new Marca(
                    "Servicios Farmacia",
                    "Medellin",
                    "4444444");

            List<Procedimiento> procedimientos =
                new List<Procedimiento>
                {
                    inyectologiaFactory.Crear(
                        "Inyectologia",
                        15000,
                        marcaServicios,
                        10),
                    curacionFactory.Crear(
                        "CuracionBasica",
                        25000,
                        marcaServicios,
                        20),
                    vendajeFactory.Crear(
                        "CambioVendaje",
                        18000,
                        marcaServicios,
                        15)
                };

            Console.WriteLine(
                "\n--- Catálogo por tipo ---");

            foreach (var producto in
                servicioProducto.ObtenerProductos())
            {
                Console.WriteLine(
                    $"{producto.GetType().Name}\t" +
                    $"{producto.Nombre}\t" +
                    $"{producto.Precio}\t" +
                    $"Proveedor: " +
                    $"{producto.Proveedor.Nombre}");
            }

            foreach (var procedimiento in
                procedimientos)
            {
                Console.WriteLine(
                    $"{procedimiento.GetType().Name}\t" +
                    $"{procedimiento.Nombre}\t" +
                    $"{procedimiento.Precio}\t" +
                    $"Duración: " +
                    $"{procedimiento.DuracionMinutos} min");
            }

            Console.WriteLine(
                "\n--- Venta de producto con stock ---");

            var productoDemo =
                servicioProducto
                .ObtenerProductos()
                .OfType<IProductoConStock>()
                .First();

            Console.WriteLine(
                $"Stock antes: " +
                $"{productoDemo.Stock}");

            productoDemo.Stock -= 1;

            servicioMovimiento
                .RegistrarMovimiento(
                    new Movimiento(
                        DateTime.Now,
                        1,
                        "Venta",
                        (Producto)productoDemo));

            Console.WriteLine(
                $"Stock después: " +
                $"{productoDemo.Stock}");

            Console.WriteLine(
                "\n--- Venta de procedimiento ---");

            Procedimiento procedimientoDemo =
                procedimientos[0];

            servicioMovimiento
                .RegistrarMovimiento(
                    new Movimiento(
                        DateTime.Now,
                        1,
                        "Venta",
                        procedimientoDemo));

            Console.WriteLine(
                $"{procedimientoDemo.Nombre} no " +
                $"implementa IProductoConStock: " +
                $"no hay stock que descontar");

            Console.WriteLine(
                "\n--- Descuento por convenio ---");

            var clienteDemo =
                servicioCliente
                .ObtenerClientes()
                .First();

            clienteDemo.Convenio =
                new ConvenioUniversidad("UPB");

            decimal descuento =
                servicioDescuento
                .CalcularDescuento(
                    procedimientoDemo.Precio,
                    clienteDemo);

            Console.WriteLine(
                $"Cliente: {clienteDemo.Nombre} - " +
                $"Convenio: " +
                $"{clienteDemo.Convenio.NombreEntidad} " +
                $"({clienteDemo.Convenio.TipoConvenio})");

            Console.WriteLine(
                $"Precio: {procedimientoDemo.Precio} - " +
                $"Descuento: {descuento} - " +
                $"Total: " +
                $"{procedimientoDemo.Precio - descuento}");

            break;

        case 7:

            Console.ForegroundColor =
                ConsoleColor.Red;

            Console.WriteLine(
                "\nSaliendo del sistema...");

            Console.ResetColor();

            break;

        default:

            Console.WriteLine(
                "\nOpción inválida");

            break;
    }
}

Console.WriteLine(
    "\nFIN DEL SISTEMA");