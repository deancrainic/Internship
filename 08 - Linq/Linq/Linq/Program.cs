﻿using System;
using Linq;

namespace Linq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var carList = PopulateCarList();

            //Where
            var filteredCarList = carList.Where(x => x.HorsePower > 200)
                                         .Where(x => x.HorsePower < 1000);

            Console.WriteLine("Where------");
            PrintCarList(filteredCarList);
            Console.WriteLine();

            //Take
            var firstThreeCars = carList.Take(3);

            Console.WriteLine("Take-------");
            PrintCarList(firstThreeCars);
            Console.WriteLine();

            //Skip
            var allButFirstThreeCars = carList.Skip(3);

            Console.WriteLine("Skip-------");
            PrintCarList(allButFirstThreeCars);
            Console.WriteLine();

            //TakeWhile
            var HpSmallerThanFiveHundred = carList.TakeWhile(x => x.HorsePower < 500);

            Console.WriteLine("TakeWhile--");
            PrintCarList(HpSmallerThanFiveHundred);
            Console.WriteLine();

            //Distinct
            var distincCarList = carList.Distinct(new CarEqualityComparer());

            Console.WriteLine("Distinct--");
            PrintCarList(distincCarList);
            Console.WriteLine();

            //Select
            var biggerHP = carList.Select(x => x.HorsePower * 2);

            Console.WriteLine("Select----");
            foreach (var item in biggerHP)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            //OrderBy, ThenBy
            var orderedCarList = carList.OrderBy(x => x.Brand).ThenBy(x => x.Model);

            Console.WriteLine("OrderBy---");
            PrintCarList(orderedCarList);
            Console.WriteLine();

            //Reverse
            var reversedCarList = carList.Reverse();

            Console.WriteLine("Reverse---");
            PrintCarList(reversedCarList);
            Console.WriteLine();

            //Group
            var groupedCarList = carList.GroupBy(x => x.Brand);

            Console.WriteLine("Group-----");
            foreach (var brand in groupedCarList)
            {
                Console.WriteLine(brand.Key);
                PrintCarList(brand);
            }
            Console.WriteLine();

            //Concat
            var concatCarList = HpSmallerThanFiveHundred.Concat(allButFirstThreeCars);

            Console.WriteLine("Concat----");
            PrintCarList(concatCarList);
            Console.WriteLine();

            //Union
            var unionCarList = HpSmallerThanFiveHundred.Union(allButFirstThreeCars);

            Console.WriteLine("Union-----");
            PrintCarList(unionCarList);
            Console.WriteLine();

            //Intersect
            var intersectCarList = HpSmallerThanFiveHundred.Intersect(allButFirstThreeCars);

            Console.WriteLine("Intersect-");
            PrintCarList(intersectCarList);
            Console.WriteLine();

            //Except
            var exceptCarList = HpSmallerThanFiveHundred.Except(allButFirstThreeCars);

            Console.WriteLine("Except----");
            PrintCarList(exceptCarList);
            Console.WriteLine();

            //OfType

            var electricCarList = carList.OfType<ElectricCar>();

            Console.WriteLine("OfType----");
            PrintCarList(electricCarList);
            Console.WriteLine();

            //ToArray
            Car[] carArray = carList.ToArray();

            Console.WriteLine("ToArray---");
            Console.WriteLine($"carArray length: {carArray.Length}");
            Console.WriteLine();

            //ToDictionary
            Dictionary<int, Car> carDictionary = carList.ToDictionary(car => car.HorsePower, car => car);

            Console.WriteLine("ToDictionary");
            foreach (var car in carDictionary)
            {
                Console.WriteLine(car.Key);
                Console.WriteLine(car.Value);
            }
            Console.WriteLine();

            //First
            var firstCarBMW = carList.First(x => x.Brand.Equals("BMW"));

            Console.WriteLine("First-------");
            Console.WriteLine($"first bmw: {firstCarBMW}");
            Console.WriteLine();

            //Count
            var numberOfAudiCars = carList.Count(x => x.Brand.Equals("Audi"));

            Console.WriteLine("Count-------");
            Console.WriteLine($"audi cars: {numberOfAudiCars}");
            Console.WriteLine();

            //Any
            var containsDacia = carList.Any(x => x.Brand.Equals("Dacia"));

            Console.WriteLine("Contains----");
            Console.WriteLine("contains Dacia: {0}", containsDacia);
            Console.WriteLine();

            //Join
            var brandList = PopulateBrandList();

            var joinedCarBrands = from car in carList
                                   join brand in brandList
                                   on car.Brand equals brand.Name
                                   select new { BrandName = car.Brand, ModelName = car.Model, BrandCountry = brand.Country };

            Console.WriteLine("Join--------");
            foreach (var car in joinedCarBrands)
            {
                Console.WriteLine($"{car.BrandName}, {car.ModelName}, {car.BrandCountry}");
            }
            Console.WriteLine();

            
        }

        public static IEnumerable<Car> PopulateCarList()
        {
            List<Car> cars = new List<Car> 
            {
                new Car { Brand = "Audi", Model = "A5", HorsePower = 190, Color = "gray" },
                new Car { Brand = "Audi", Model = "RS4", HorsePower = 320, Color = "red" },
                new Car { Brand = "Volkswagen", Model = "Golf 7", HorsePower = 90, Color = "blue" },
                new Car { Brand = "Dacia", Model = "Duster", HorsePower = 75, Color = "orange" },
                new ElectricCar { Brand = "Tesla", Model = "S", HorsePower = 1060, Color = "white", BatteryAutonomy = 600 },
                new ElectricCar { Brand = "Volkswagen", Model = "e-Up!", HorsePower = 46, Color = "white", BatteryAutonomy = 350 },
                new Car { Brand = "BMW", Model = "525e", HorsePower = 600, Color = "dark gray" },
                new Car { Brand = "Hyundai", Model = "Elantra", HorsePower = 120, Color = "blue" },
                new Car { Brand = "BMW", Model = "525e", HorsePower = 602, Color = "dark gray" }
            };

            return cars;
        }

        public static IEnumerable<CarBrand> PopulateBrandList()
        {
            List<CarBrand> brands = new List<CarBrand>
            {
                new CarBrand { Name = "BMW", Year = 1900, Country = "Germany" },
                new CarBrand { Name = "Audi", Year = 1920, Country = "Germany" },
                new CarBrand { Name = "Volkswagen", Year = 1890, Country = "Germany" },
                new CarBrand { Name = "Dacia", Year = 1945, Country = "Romania" },
                new CarBrand { Name = "Hyundai", Year = 1930, Country = "South Korea" }
            };

            return brands;
        }

        public static void PrintCarList(IEnumerable<Car> carList)
        {
            foreach (Car car in carList)
                Console.WriteLine(car);
        }
    }
}