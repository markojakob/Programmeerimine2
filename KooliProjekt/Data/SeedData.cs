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
            if (context.Customers.Any())
            {
                return;
            }
            var renting1 = new Renting
            {
                RentalNo = 1,
                RentalDate = DateTime.Now,
                RentalDueTime = DateTime.Now,
                DriveDistance = 23000,
                CustomerId = 2,

            };


            context.Customers.AddRange();

            context.SaveChanges();
        }
    }


}
