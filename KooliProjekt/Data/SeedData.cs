using Microsoft.EntityFrameworkCore.Migrations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KooliProjekt.Data
{
    public static class SeedData
    {
        public static void CarsGenerate(ApplicationDbContext context)
        {
            if (context.Cars.Any())
            {
                return;
            }

            // Esimene auto
            var car1 = new Car
            {
                Model = "A4",
                CarMaker = "Audi",
                Price = 5000,
                Colour = "Green",
                Description = "Fast",
                Category = "Sedan",
                KmTariff = 40000
            };

            // Teine auto
            var car2 = new Car
            {
                Model = "Civic",
                CarMaker = "Honda",
                Price = 6000,
                Colour = "Blue",
                Description = "Reliable and economical",
                Category = "Hatchback",
                KmTariff = 25000
            };

            // Kolmas auto
            var car3 = new Car
            {
                Model = "Model 3",
                CarMaker = "Tesla",
                Price = 35000,
                Colour = "Black",
                Description = "Electric car with high performance",
                Category = "Sedan",
                KmTariff = 15000
            };

            // Neljas auto
            var car4 = new Car
            {
                Model = "X5",
                CarMaker = "BMW",
                Price = 45000,
                Colour = "Silver",
                Description = "Luxury SUV with great features",
                Category = "SUV",
                KmTariff = 30000
            };

            // Viies auto
            var car5 = new Car
            {
                Model = "Focus",
                CarMaker = "Ford",
                Price = 4000,
                Colour = "Red",
                Description = "Compact car for everyday use",
                Category = "Sedan",
                KmTariff = 50000
            };

            // Kuues auto
            var car6 = new Car
            {
                Model = "Mustang",
                CarMaker = "Ford",
                Price = 25000,
                Colour = "Yellow",
                Description = "Sporty muscle car",
                Category = "Coupe",
                KmTariff = 20000
            };

            // Seitsmes auto
            var car7 = new Car
            {
                Model = "Corolla",
                CarMaker = "Toyota",
                Price = 8000,
                Colour = "White",
                Description = "Reliable sedan for family",
                Category = "Sedan",
                KmTariff = 35000
            };

            // Kaheksas auto
            var car8 = new Car
            {
                Model = "Fiesta",
                CarMaker = "Ford",
                Price = 5000,
                Colour = "Orange",
                Description = "Compact city car",
                Category = "Hatchback",
                KmTariff = 22000
            };

            // Üheksas auto
            var car9 = new Car
            {
                Model = "Q7",
                CarMaker = "Audi",
                Price = 60000,
                Colour = "Black",
                Description = "Luxury SUV with great comfort",
                Category = "SUV",
                KmTariff = 15000
            };

            // Kümnes auto
            var car10 = new Car
            {
                Model = "Golf",
                CarMaker = "Volkswagen",
                Price = 7000,
                Colour = "Grey",
                Description = "Popular compact car",
                Category = "Hatchback",
                KmTariff = 30000
            };



            // Lisa kõik autod andmebaasi
            context.Cars.AddRange(car1, car2, car3, car4, car5, car6, car7, car8, car9, car10);

            // Salvestage muudatused
            context.SaveChanges();
        }
        public static void CustomerGenerate(ApplicationDbContext context)
        {
            if (context.Customers.Any())
            {
                return;
            }
            var customer1 = new Customer
            {
                FirstName = "Mati",
                LastName = "Maasikas",
                PhoneNum = 57934854,
                Address = "Pärnu"
            };

            var customer2 = new Customer
            {
                FirstName = "Kati",
                LastName = "Kask",
                PhoneNum = 56473829,
                Address = "Tallinn"
            };

            var customer3 = new Customer
            {
                FirstName = "Jüri",
                LastName = "Jõgi",
                PhoneNum = 51325874,
                Address = "Tartu"
            };

            var customer4 = new Customer
            {
                FirstName = "Anna",
                LastName = "Kivi",
                PhoneNum = 51234567,
                Address = "Narva"
            };

            var customer5 = new Customer
            {
                FirstName = "Margo",
                LastName = "Mänd",
                PhoneNum = 55997733,
                Address = "Kuressaare"
            };

            var customer6 = new Customer
            {
                FirstName = "Liis",
                LastName = "Lepik",
                PhoneNum = 56892345,
                Address = "Pärnu"
            };

            var customer7 = new Customer
            {
                FirstName = "Toomas",
                LastName = "Tamm",
                PhoneNum = 57892211,
                Address = "Viljandi"
            };

            var customer8 = new Customer
            {
                FirstName = "Eero",
                LastName = "Erdmann",
                PhoneNum = 55223344,
                Address = "Jõgeva"
            };

            var customer9 = new Customer
            {
                FirstName = "Helena",
                LastName = "Helm",
                PhoneNum = 55112233,
                Address = "Paide"
            };

            var customer10 = new Customer
            {
                FirstName = "Jaanus",
                LastName = "Järv",
                PhoneNum = 56112244,
                Address = "Rakvere"
            };

            // Lisa kõik kliendid andmebaasi
            context.Customers.AddRange(customer1, customer2, customer3, customer4, customer5, customer6, customer7, customer8, customer9, customer10);


            context.SaveChanges();
        }

        public static void RentingsGenerate(ApplicationDbContext context)
        {
            if (context.Rentings.Any())
            {
                return;
            }

            var customer1 = context.Customers.FirstOrDefault(); 
            var customer2 = context.Customers.Skip(1).Take(1).FirstOrDefault();
            var customer3 = context.Customers.Skip(2).Take(2).FirstOrDefault();
            var customer4 = context.Customers.Skip(3).Take(3).FirstOrDefault();
            var customer5 = context.Customers.Skip(4).Take(4).FirstOrDefault();
            var customer6 = context.Customers.Skip(5).Take(5).FirstOrDefault();
            var customer7 = context.Customers.Skip(6).Take(6).FirstOrDefault();
            var customer8 = context.Customers.Skip(7).Take(7).FirstOrDefault();
            var customer9 = context.Customers.Skip(8).Take(8).FirstOrDefault();
            var customer10 = context.Customers.Skip(9).Take(9).FirstOrDefault();


            var renting1 = new Renting
            {
                RentalNo = 1,
                RentalDate = new DateTime(2024, 11, 25),
                RentalDueTime = new DateTime(2024, 12, 25),
                DriveDistance = 23000,
                CustomerId = customer1.Id,

            };

            var renting2 = new Renting
            {
                RentalNo = 2,
                RentalDate = new DateTime(2024,11, 16),
                RentalDueTime = new DateTime(2024, 11, 21), 
                DriveDistance = 21000,
                CustomerId = customer2.Id,
            };

            var renting3 = new Renting
            {
                RentalNo = 3,
                RentalDate = new DateTime(2024, 11, 16),
                RentalDueTime = new DateTime(2024, 11, 19),
                DriveDistance = 15000,
                CustomerId = customer3.Id,
            };

            var renting4 = new Renting
            {
                RentalNo = 4,
                RentalDate = new DateTime(2024, 11, 18),
                RentalDueTime = new DateTime(2024, 11, 26),
                DriveDistance = 17000,
                CustomerId = customer4.Id,
            };

            var renting5 = new Renting
            {
                RentalNo = 5,
                RentalDate = new DateTime(2024, 11, 19),
                RentalDueTime = new DateTime(2024, 11, 27),
                DriveDistance = 25000,
                CustomerId = customer5.Id,
            };

            var renting6 = new Renting
            {
                RentalNo = 6,
                RentalDate = new DateTime(2024, 11, 20),
                RentalDueTime = new DateTime(2024, 11, 27),
                DriveDistance = 23000,
                CustomerId = customer6.Id,
            };

            var renting7 = new Renting
            {
                RentalNo = 7,
                RentalDate = new DateTime(2024, 12, 05),
                RentalDueTime = new DateTime(2024, 12, 11),
                DriveDistance = 21000,
                CustomerId = customer7.Id,
            };

            var renting8 = new Renting
            {
                RentalNo = 8,
                RentalDate = new DateTime(2024, 11, 23),
                RentalDueTime = new DateTime(2024, 11, 29),
                DriveDistance = 22000,
                CustomerId = customer8.Id,
            };

            var renting9 = new Renting
            {
                RentalNo = 9,
                RentalDate = new DateTime(2024, 11, 16),
                RentalDueTime = new DateTime(2024, 11, 19),
                DriveDistance = 24000,
                CustomerId = customer9.Id,
               
            };

            var renting10 = new Renting
            {
                RentalNo = 10,
                RentalDate = new DateTime(2024, 11, 17),
                RentalDueTime = new DateTime(2024, 11, 20),
                DriveDistance = 26000,
                CustomerId = customer10.Id,
            };



            context.Rentings.AddRange(renting1, renting2, renting3, renting4, renting5, renting6, renting7, renting8, renting9, renting10);

            context.SaveChanges();
        }
    }
     

}

