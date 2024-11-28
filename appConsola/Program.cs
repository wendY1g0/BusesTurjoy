﻿using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using appConsola;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.IO;
using System.Text.Json;


public partial class Program

{

    public static void Main(string[] args)
    {

        
        
        // Create the host and configure services
        var host = CreateHostBuilder(args).Build();
        
        // Retrieve the database context from the DI container
        var dbContext = host.Services.GetRequiredService<TurjoyContext>();

        if (dbContext == null)
        {
        Console.WriteLine("No se pudo obtener el contexto de base de datos.");
        return;
        }

        // Start the console application loop
        RunConsoleApp(dbContext);
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((hostContext, services) =>
            {
                // Add the DbContext and MySQL connection setup
                services.AddDbContext<TurjoyContext>((serviceProvider, options) =>
                {
                    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                    options.UseMySQL(configuration.GetConnectionString("turjoy"));
                });

                // Add logging to the service container
                services.AddLogging();
            })
            .ConfigureLogging((context, logging) =>
            {
                logging.AddConsole(); // Ensure logs are output to the console
            });

    public static void ListarBuses (TurjoyContext dbContext)
    {
        Console.WriteLine("Listando todos los buses:");
        try
        {
            var buses = dbContext.Bus.ToList();

            foreach (var bus in buses)
            {
                Console.WriteLine($"ID: {bus.IdBus}, Codigo: {bus.CodigoBus}, Disponible: {bus.EsHabilitado}, Kilometros: {bus.Kilometraje}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error listando los buses: {ex.Message}");
        }

        Console.ReadKey(); // Pause to allow the user to read the list
    }



    public static void AgregarBuses(TurjoyContext dbContext)
    {
        Console.Write("Ingrese el codigo del bus: ");
        var codigoBus = Console.ReadLine();
       
        var bus = new Bus
        {
            CodigoBus = codigoBus,
            EsHabilitado = "SI", // Default availability as a int
            Kilometraje = 0,
        };

        try
        {
            // Add driver to database and save changes
            dbContext.Bus.Add(bus);
            dbContext.SaveChanges();
            Console.WriteLine($"Bus {codigoBus} agregado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error agregando bus: {ex.Message}");
        }

        Console.ReadKey(); // Pause to allow the user to read the message
    }

     public static void CambiarEstado(TurjoyContext dbContext)
    {
        Console.Write("Ingrese la ID del bus para actualizar su estado: ");
        int IdBus;

        // Use TryParse to safely handle invalid input
        if (!int.TryParse(Console.ReadLine(), out IdBus))
        {
            Console.WriteLine("ID invalida");
            Console.ReadKey();
            return;
        }

        var bus = dbContext.Bus.Find(IdBus);
        if (bus != null)
        {
            // Toggle the availability (yes/no)
            bus.EsHabilitado = bus.EsHabilitado == "SI" ? "NO" : "SI";


            try
            {
                // Save the changes to the database
                dbContext.SaveChanges();
                Console.WriteLine($"Bus {bus.CodigoBus} availability updated to {bus.EsHabilitado}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating bus availability: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("bus not found.");
        }

        Console.ReadKey(); // Pause for user to read the message
    }

    public static void RunConsoleApp(TurjoyContext dbContext)
    {
        while (true)
        {
            // Clear the screen and show menu options
            Console.Clear();
            Console.WriteLine("Sistema Turjoy");
            Console.WriteLine("1. Agregar bus");
            Console.WriteLine("2. Listar buses");
            Console.WriteLine("3. Cambiar estado de bus");
            Console.WriteLine("4. Salir");
            Console.Write("\nElija una opcion: ");

            // Read user input
            var choice = Console.ReadLine();

            // Switch based on the user's choice
            switch (choice)
            {
                case "1":
                    AgregarBuses(dbContext);
                    break;
                case "2":
                    ListarBuses(dbContext);
                    break;
                case "3":
                    CambiarEstado(dbContext);
                    break;
                case "4":
                    return; // Exit the loop and end the application
                default:
                    Console.WriteLine("Opcion invalida. Por favor intente de nuevo.");
                    break;
            }
        }
    }


}
