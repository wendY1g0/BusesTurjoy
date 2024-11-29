﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using appConsola;

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

        // Ensure the database is created or migrated
        dbContext.Database.Migrate(); // Or dbContext.Database.EnsureCreated() for simple use

        // Start the console application loop
        RunConsoleApp(dbContext);
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                // Add the DbContext directly with MySQL connection string setup
                services.AddDbContext<TurjoyContext>(options =>
                    options.UseMySQL("server=localhost;port=3307;database=busmanagementapp;user=alumno;password=pass123"));
            });

    public static void ListarBuses(TurjoyContext dbContext)
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
            EsHabilitado = "SI", // Default availability as a string
            Kilometraje = 0,
        };

        try
        {
            // Add bus to database and save changes
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
            Console.WriteLine("Bus no encontrado.");
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
