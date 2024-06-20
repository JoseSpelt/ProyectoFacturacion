using System;
using System.Diagnostics.Metrics;
using examenOptativoP.Modelos;
using Repository.Data.Clientes;
using Repository.Data.Facturas;
using Repository.Data.Sucursal;
using Service.Logica;

public class Program
{
    private static void Main(string[] args)
    {
        string connectionString = "Host=localhost;port=5432;Database=examen;Username=postgres;Password=Invocador1998;";
        ClienteService clienteService = new ClienteService(connectionString);
        FacturaService facturaService = new FacturaService(connectionString);
        SucursalService sucursalService = new SucursalService(connectionString);
        ProductoService productoService = new ProductoService(connectionString);
        Console.WriteLine("<-------------------MENU PRINCIPAL---------------------------->");
        Console.WriteLine("<-----------SELECCIONE UNA DE LAS OPCIONES-------------------->");
        Console.WriteLine("(1) CLIENTE \n(2) FACTURA \n(3) SUCURSAL \n(4) PRODUCTO");
        string opcionMenu = Console.ReadLine();
        if (opcionMenu == "1")
        {
            Console.WriteLine("---Selecciona una opcion de Cliente---  \n1 -> Insertar Cliente \n2 -> Listar Clientes \n3 -> Modificar un Cliente por ID \n4 -> Eliminar un Cliente por ID \n5 - Buscar un Cliente por ID ");
            string opcion = Console.ReadLine();
            int opcionSelect = int.Parse(opcion);
            switch (opcionSelect)
            {
                case 1:
                    clienteService.add(new ClienteModel
                    {
                        id_banco = 2,
                        nombre = "Jose",
                        apellido = "Spelt",
                        documento = "5729790",
                        direccion = "Avda Von Grutter",
                        email = "josespelt16@gmail.com",
                        celular = "0994114128",
                        estado = "Activo"
                    });
                    break;
                case 2:
                    clienteService.listar().ToList().ForEach(cliente =>
                    Console.WriteLine(
                        $"Nombre: {cliente.nombre} \n " +
                        $"ID Banco: {cliente.id_banco} \n " +
                        $"Apellido: {cliente.apellido} \n " +
                        $"Cedula: {cliente.documento} \n " +
                        $"Correo: {cliente.email} \n " +
                        $"Direccion: {cliente.direccion} \n " +
                        $"Celular: {cliente.celular} \n " +
                        $"Estado: {cliente.estado} \n "
                    ));
                    break;
                case 3:
                    clienteService.update(new ClienteModel
                    {
                        id_cliente = 2,
                        id_banco = 2,
                        nombre = "Jose Alberto",
                        apellido = "Spelt Avalos",
                        documento = "5729790",
                        direccion = "Avda Von Grutter - Limpio",
                        email = "josespelt16@gmail.com",
                        celular = "0994114128",
                        estado = "Activo"
                    });
                    break;
                case 4:
                    Console.WriteLine("Ingrese el id del cliente que quieres eliminar:");
                    string input = Console.ReadLine();
                    int idSelect = int.Parse(input);
                    clienteService.delete(idSelect);
                    break;
                case 5:
                    Console.WriteLine("Ingrese el id del cliente que quieres buscar:");
                    string buscar = Console.ReadLine();
                    int idbuscar = int.Parse(buscar);
                    ClienteModel cliente = clienteService.get(idbuscar);
                    if (cliente != null)
                    {
                        Console.WriteLine(
                            $"Nombre: {cliente.nombre} \n" +
                            $"ID Banco: {cliente.id_banco} \n" +
                            $"Apellido: {cliente.apellido} \n" +
                            $"Documento: {cliente.documento} \n" +
                            $"Correo: {cliente.email} \n" +
                            $"Direccion: {cliente.direccion} \n" +
                            $"Celular: {cliente.celular} \n" +
                            $"Estado: {cliente.estado} \n"
                        );
                    }
                    else
                    {
                        Console.WriteLine("Cliente no encontrado.");
                    }
                    break;
                default:
                    Console.WriteLine("Esa opcion no es valida");
                    break;
            }

        }
        else if (opcionMenu == "2")
        {
            Console.WriteLine("---Selecciona una opcion de Factura---  \n1 -> Insertar Factura \n2 -> Listar Facturas \n3 -> Modificar una Factura por ID \n4 -> Eliminar una Factura por ID \n5 -> Buscar una Factura por ID ");
            string opcion = Console.ReadLine();
            int opcionSelect = int.Parse(opcion);

            switch (opcionSelect)
            {
                case 1:
                    var nuevaFactura = new FacturaModel
                    {
                        id_cliente = 3,
                        id_sucursal = 3,
                        nro_factura = "123-456-789-000123",
                        fecha_hora = new DateTime(2024, 05, 21),
                        total = 1100000,
                        total_iva5 = 0,
                        total_iva10 = 100000,
                        total_iva = 100000,
                        total_letras = "Un Millon Cien Mil Guaranies",
                        sucursal = "Asuncion",
                        detalleFactura = new List<DetalleFacturaModel>
                        {
                            new DetalleFacturaModel { id_producto = 1, cantidad_producto = 2, subtotal = 200000 },
                            new DetalleFacturaModel { id_producto = 2, cantidad_producto = 3, subtotal = 300000 }
                        }
                     };
                    facturaService.add(nuevaFactura);
                    break;
                case 2:
                    facturaService.listar().ToList().ForEach(factura =>
                    {
                        Console.WriteLine(
                            $"ID Factura: {factura.id_factura}\n" +
                            $"ID Cliente: {factura.id_cliente}\n" +
                            $"ID Sucursal: {factura.id_sucursal}\n" +
                            $"Numero de Factura: {factura.nro_factura}\n" +
                            $"Fecha: {factura.fecha_hora}\n" +
                            $"Total: {factura.total}\n" +
                            $"IVA 5%: {factura.total_iva5}\n" +
                            $"IVA 10%: {factura.total_iva10}\n" +
                            $"Total IVA: {factura.total_iva}\n" +
                            $"Total en Letras: {factura.total_letras}\n" +
                            $"Sucursal: {factura.sucursal}\n");

                        if (factura.detalleFactura != null && factura.detalleFactura.Any())
                        {
                            Console.WriteLine("Detalles de la Factura:");
                            foreach (var detalle in factura.detalleFactura)
                            {
                                Console.WriteLine(
                                    $"  ID Producto: {detalle.id_producto}\n" +
                                    $"  Cantidad: {detalle.cantidad_producto}\n" +
                                    $"  Subtotal: {detalle.subtotal}\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay detalles de la factura.");
                        }
                    });
                    break;
                case 3:
                    var facturaActualizada = new FacturaModel
                    {
                        id_factura = 5,
                        nro_factura = "123-456-789-000123",
                        fecha_hora = new DateTime(2024, 05, 22),
                        total = 1500000,
                        total_iva5 = 0,
                        total_iva10 = 500000,
                        total_iva = 500000,
                        total_letras = "Un Millon Quinientos Mil Guaranies",
                        sucursal = "Asuncion",
                        detalleFactura = new List<DetalleFacturaModel>
                        {
                            new DetalleFacturaModel { id_detalle = 1, id_producto = 1, cantidad_producto = 2, subtotal = 300000 },
                            new DetalleFacturaModel { id_detalle = 2, id_producto = 2, cantidad_producto = 4, subtotal = 400000 }
                        }
                    };
                    facturaService.update(facturaActualizada);
                    break;
                case 4:
                    Console.WriteLine("Ingrese el id de la factura que quiere eliminar:");
                    string input = Console.ReadLine();
                    int idSelect = int.Parse(input);
                    facturaService.delete(idSelect);
                    break;
                case 5:
                    Console.WriteLine("Ingrese el id de la factura que quieres buscar:");
                    string buscar = Console.ReadLine();
                    int idbuscar = int.Parse(buscar);
                    FacturaModel factura = facturaService.get(idbuscar);
                    if (factura != null)
                    {
                        Console.WriteLine(
                           $"ID Factura: {factura.id_factura} \n " +
                           $"ID Cliente: {factura.id_cliente} \n " +
                           $"ID Sucursal: {factura.id_sucursal} \n " +
                           $"Numero de Factura: {factura.nro_factura} \n " +
                           $"Fecha: {factura.fecha_hora} \n " +
                           $"Total: {factura.total} \n " +
                           $"IVA 5%: {factura.total_iva5} \n " +
                           $"IVA 10%: {factura.total_iva10} \n " +
                           $"Total IVA: {factura.total_iva} \n " +
                           $"Total en Letras: {factura.total_letras} \n " +
                           $"Sucursal: {factura.sucursal} \n "
                        );
                        Console.WriteLine("Detalles de la Factura:");
                        factura.detalleFactura.ToList().ForEach(detalle =>
                        {
                            Console.WriteLine(
                                $"ID Producto: {detalle.id_producto} \n " +
                                $"Cantidad Producto: {detalle.cantidad_producto} \n " +
                                $"Subtotal: {detalle.subtotal} \n"
                            );
                        });
                    }
                    else
                    {
                        Console.WriteLine("Factura no encontrada.");
                    }
                    break;
                default:
                    Console.WriteLine("Esa opcion no es valida");
                    break;
            }
        }
        else if (opcionMenu == "3")
        {
            Console.WriteLine("---Selecciona una opcion de Sucursal---  \n1 -> Insertar Sucursal \n2 -> Listar Sucursal \n3 -> Modificar un Sucursal por ID \n4 -> Eliminar un Sucursal por ID \n5 -> Buscar un Sucursal por ID ");
            string opcion = Console.ReadLine();
            int opcionSelect = int.Parse(opcion);

            switch (opcionSelect)
            {
                case 1:
                    sucursalService.add(new SucursalModel
                    {
                        descripcion = "Sucursal Central",
                        direccion = "Avda Mariscal Lopez",
                        telefono = "02145187123",
                        whatsapp = "0994114128",
                        email = "josespelt@gmail.com",
                        estado = "Activo",
                    });
                    break;
                case 2:
                    sucursalService.listar().ToList().ForEach(sucursal =>
                    Console.WriteLine(
                        $"ID Sucursal: {sucursal.id_sucursal} \n " +
                        $"Descripcion: {sucursal.descripcion} \n " +
                        $"Direccion: {sucursal.direccion} \n " +
                        $"Telefono: {sucursal.telefono} \n " +
                        $"Whatsapp: {sucursal.whatsapp} \n " +
                        $"Correo: {sucursal.email} \n " +
                        $"Estado: {sucursal.estado} \n " 
                    ));
                    break;
                case 3:
                    sucursalService.update(new SucursalModel
                    {
                        id_sucursal = 1,
                        descripcion = "Sucursal Central",
                        direccion = "Avda Estigarribia",
                        telefono = "02145187123",
                        whatsapp = "0994114128",
                        email = "josespelt@gmail.com",
                        estado = "Activo",
                    });
                    break;
                case 4:
                    Console.WriteLine("Ingrese el id de la sucursal que quiere eliminar:");
                    string input = Console.ReadLine();
                    int idSelect = int.Parse(input);
                    sucursalService.delete(idSelect);
                    break;
                case 5:
                    Console.WriteLine("Ingrese el id de la sucursal que quieres buscar:");
                    string buscar = Console.ReadLine();
                    int idbuscar = int.Parse(buscar);
                    SucursalModel sucursal = sucursalService.get(idbuscar);
                    if (sucursal != null)
                    {
                        Console.WriteLine(
                            $"ID Sucursal: {sucursal.id_sucursal} \n " +
                            $"Descripcion: {sucursal.descripcion} \n " +
                            $"Direccion: {sucursal.direccion} \n " +
                            $"Telefono: {sucursal.telefono} \n " +
                            $"Whatsapp: {sucursal.whatsapp} \n " +
                            $"Correo: {sucursal.email} \n " +
                            $"Estado: {sucursal.estado} \n "
                        );
                    }
                    else
                    {
                        Console.WriteLine("Sucursal no encontrado.");
                    }
                    break;
                default:
                    Console.WriteLine("Esa opcion no es valida");
                    break;
            }
        }
        else if (opcionMenu == "4")
        {
            Console.WriteLine("---Selecciona una opcion de Producto---  \n1 -> Insertar Producto \n2 -> Listar Productos \n3 -> Modificar un Producto por ID \n4 -> Eliminar un Producto por ID \n5 -> Buscar un Producto por ID ");
            string opcion = Console.ReadLine();
            int opcionSelect = int.Parse(opcion);

            switch (opcionSelect)
            {
                case 1:
                    productoService.add(new ProductoModel
                    {
                        descripcion = "Gaseosa 2L",
                        cantidad_minima = 50,
                        cantidad_stock = 150,
                        precio_compra = 10000,
                        precio_venta = 13000,
                        categoria = "Bebidas",
                        marca = "Coca Cola",
                        estado = "En Stock",
                    });
                    break;
                case 2:
                    productoService.listar().ToList().ForEach(producto =>
                    Console.WriteLine(
                        $"ID Producto: {producto.id_producto} \n " +
                        $"Descripcion: {producto.descripcion} \n " +
                        $"Cantidad Mininma: {producto.cantidad_minima} \n " +
                        $"Cantidad Stock: {producto.cantidad_stock} \n " +
                        $"Precio Compra: {producto.precio_compra} \n " +
                        $"Precio Venta: {producto.precio_venta} \n " +
                        $"Categoria: {producto.categoria} \n " +
                        $"Marca: {producto.marca} \n " +
                        $"Estado: {producto.estado} \n "
                    ));
                    break;
                case 3:
                    productoService.update(new ProductoModel
                    {
                        id_producto = 1,
                        descripcion = "Gaseosa 1L",
                        cantidad_minima = 24,
                        cantidad_stock = 500,
                        precio_compra = 6000,
                        precio_venta = 8500,
                        categoria = "Bebidas",
                        marca = "Coca Cola",
                        estado = "En Stock",
                    });
                    break;
                case 4:
                    Console.WriteLine("Ingrese el id del producto que quiere eliminar:");
                    string input = Console.ReadLine();
                    int idSelect = int.Parse(input);
                    productoService.delete(idSelect);
                    break;
                case 5:
                    Console.WriteLine("Ingrese el id del producto que quieres buscar:");
                    string buscar = Console.ReadLine();
                    int idbuscar = int.Parse(buscar);
                    ProductoModel producto = productoService.get(idbuscar);
                    if (producto != null)
                    {
                        Console.WriteLine(
                            $"ID Producto: {producto.id_producto} \n " +
                            $"Descripcion: {producto.descripcion} \n " +
                            $"Cantidad Mininma: {producto.cantidad_minima} \n " +
                            $"Cantidad Stock: {producto.cantidad_stock} \n " +
                            $"Precio Compra: {producto.precio_compra} \n " +
                            $"Precio Venta: {producto.precio_venta} \n " +
                            $"Categoria: {producto.categoria} \n " +
                            $"Marca: {producto.marca} \n " +
                            $"Estado: {producto.estado} \n "
                        );
                    }
                    else
                    {
                        Console.WriteLine("Producto no encontrado.");
                    }
                    break;
                default:
                    Console.WriteLine("Esa opcion no es valida");
                    break;
            }
        }
    }
}
