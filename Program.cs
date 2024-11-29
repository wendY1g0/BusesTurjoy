using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using personalMantencion;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.IO;
using System.Text.Json;

public class Program
{
    public static void Main(string[] args)
    {
        // Create the host and configure services
        var host = CreateHostBuilder(args).Build();

        // Retrieve the database context from the DI container
        var dbContext = host.Services.GetRequiredService<BusmanagementappContext>();

        // Start the console application loop
        RunConsoleApp(dbContext);
    }

    // Host builder configuration
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                // Add the DbContext and MySQL connection setup
                services.AddDbContext<BusmanagementappContext>((serviceProvider, options) =>
                {
                    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                    options.UseMySQL(configuration.GetConnectionString("BusManagementApp"));
                });

                // Add logging to the service container
                services.AddLogging();
            })
            .ConfigureLogging((context, logging) =>
            {
                logging.AddConsole(); // Ensure logs are output to the console
            });

    // Main console loop for interacting with the app
    public static void RunConsoleApp(BusmanagementappContext dbContext)
    {
        while (true)
        {
            // Clear the screen and show menu options
            Console.Clear();
            Console.WriteLine("Bus Management System");
            Console.WriteLine("1. Add Driver");
            Console.WriteLine("2. List Drivers");
            Console.WriteLine("3. Update Driver Availability");
            Console.WriteLine("4. Export Drivers to JSON");
            Console.WriteLine("5. Exit");
            Console.Write("\nChoose an option: ");

            // Read user input
            var choice = Console.ReadLine();

            // Switch based on the user's choice
            switch (choice)
            {
                case "1":
                    AddDriver(dbContext);
                    break;
                case "2":
                    ListDrivers(dbContext);
                    break;
                case "3":
                    UpdateDriverAvailability(dbContext);
                    break;
                case "4":
                    ExportDriversToJson(dbContext);
                    break;
                case "5":
                    return; // Exit the loop and end the application
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    // Method to add a new driver
    public static void AddDriver(BusmanagementappContext dbContext)
    {
        Console.Write("Enter driver's name: ");
        var name = Console.ReadLine();

        var driver = new Driver
        {
            Name = name,
            IsAvailable = "YES", // Default availability as a string
            KilometersDriven = 0 // Initialize kilometers driven as 0
        };

        try
        {
            // Add driver to database and save changes
            dbContext.Drivers.Add(driver);
            dbContext.SaveChanges();
            Console.WriteLine($"Driver {name} added successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding driver: {ex.Message}");
        }

        Console.ReadKey(); // Pause to allow the user to read the message
    }

    // Method to list all drivers
    public static void ListDrivers(BusmanagementappContext dbContext)
    {
        Console.WriteLine("Listing all drivers:");
        try
        {
            var drivers = dbContext.Drivers.ToList();

            foreach (var driver in drivers)
            {
                Console.WriteLine($"ID: {driver.DriverId}, Name: {driver.Name}, Available: {driver.IsAvailable}, Kilometers Driven: {driver.KilometersDriven}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error listing drivers: {ex.Message}");
        }

        Console.ReadKey(); // Pause to allow the user to read the list
    }

    // Method to update a driver's availability
    public static void UpdateDriverAvailability(BusmanagementappContext dbContext)
    {
        Console.Write("Enter driver ID to update availability: ");
        int driverId;

        // Use TryParse to safely handle invalid input
        if (!int.TryParse(Console.ReadLine(), out driverId))
        {
            Console.WriteLine("Invalid ID format.");
            Console.ReadKey();
            return;
        }

        var driver = dbContext.Drivers.Find(driverId);
        if (driver != null)
        {
            // Toggle the availability (yes/no)
            driver.IsAvailable = driver.IsAvailable == "YES" ? "NO" : "YES";

            try
            {
                // Save the changes to the database
                dbContext.SaveChanges();
                Console.WriteLine($"Driver {driver.Name}'s availability updated to {driver.IsAvailable}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating driver availability: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Driver not found.");
        }

        Console.ReadKey(); // Pause for user to read the message
    }

    // Method to export the list of drivers to a JSON file
    public static void ExportDriversToJson(BusmanagementappContext dbContext)
    {
        try
        {
            var drivers = dbContext.Drivers.ToList();

            // Serialize the list of drivers to JSON
            var json = JsonSerializer.Serialize(drivers, new JsonSerializerOptions { WriteIndented = true });

            // Define the path where the JSON file will be saved
            var filePath = "drivers.json";

            // Write the JSON to a file
            File.WriteAllText(filePath, json);

            Console.WriteLine($"Drivers exported to {filePath} successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error exporting drivers to JSON: {ex.Message}");
        }

        Console.ReadKey(); // Pause for user to read the message
    }
}
