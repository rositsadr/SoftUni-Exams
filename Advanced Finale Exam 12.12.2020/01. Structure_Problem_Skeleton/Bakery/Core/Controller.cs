using Bakery.Core.Contracts;
using Bakery.Models.BakedFoods;
using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables;
using Bakery.Models.Tables.Contracts;
using Bakery.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bakery.Core
{
    public class Controller : IController
    {
        private ICollection<IBakedFood> bakedFoods;

        private ICollection<IDrink> drinks;

        private ICollection<ITable> tables;

        private decimal totalIncome;

        public Controller()
        {
            bakedFoods = new List<IBakedFood>();
            drinks = new List<IDrink>();
            tables = new List<ITable>();
        }

        public string AddDrink(string type, string name, int portion, string brand)
        {
            if (type == "Tea" || type =="Water")
            {
                IDrink drink = null;

                switch (type)
                {
                    case "Tea":
                        drink = new Tea(name, portion, brand);
                        break;
                    case "Water":
                        drink = new Water(name, portion, brand);
                        break;
                }

                drinks.Add(drink);
                return String.Format(OutputMessages.DrinkAdded, name, brand);
            }
            return null;
        }

        public string AddFood(string type, string name, decimal price)
        {
            if (type == "Bread" || type == "Cake")
            {
                IBakedFood food= null;

                switch (type)
                {
                    case "Bread":
                        food = new Bread(name,price);
                        break;
                    case "Cake":
                        food = new Cake(name, price);
                        break;
                }

                bakedFoods.Add(food);
                return String.Format(OutputMessages.FoodAdded, name, type);
            }
            return null;
        }

        public string AddTable(string type, int tableNumber, int capacity)
        {
            if (type == "InsideTable" || type == "OutsideTable")
            {
                ITable table = null;

                switch (type)
                {
                    case "InsideTable":
                        table = new InsideTable(tableNumber, capacity);
                        break;
                    case "OutsideTable":
                        table = new OutsideTable(tableNumber, capacity);
                        break;
                }

                tables.Add(table);
                return String.Format(OutputMessages.TableAdded,tableNumber);
            }
            return null;
        }

        public string GetFreeTablesInfo()
        {
            StringBuilder result = new StringBuilder();

            foreach (ITable table in tables.Where(x=>x.IsReserved == false))
            {
                result.AppendLine(table.GetFreeTableInfo());
            }

            return result.ToString().Trim();
        }

        public string GetTotalIncome()
        {
            return String.Format(OutputMessages.TotalIncome, totalIncome);
        }

        public string LeaveTable(int tableNumber)
        {
            ITable table = tables.FirstOrDefault(x => x.TableNumber == tableNumber);

            if (table == null)
            {
                return null;
            }

            decimal total = table.GetBill();
            totalIncome += total;
            table.Clear();

            return $"Table: {tableNumber}{Environment.NewLine}Bill: {total:f2}";

        }

        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {
            ITable table = this.tables.FirstOrDefault(x => x.TableNumber == tableNumber);

            if (table == null)
            {
                return String.Format(OutputMessages.WrongTableNumber, tableNumber);
            }

            IDrink drink = this.drinks.FirstOrDefault(x => x.Name == drinkName && x.Brand == drinkBrand);

            if (drink == null)
            {
                return String.Format(OutputMessages.NonExistentDrink,drinkName,drinkBrand);
            }

            table.OrderDrink(drink);
            return String.Format(OutputMessages.DrinkOrderSuccessful,tableNumber,drinkName,drinkBrand);
        }

        public string OrderFood(int tableNumber, string foodName)
        {
            ITable table = this.tables.FirstOrDefault(x => x.TableNumber == tableNumber);
            
            if (table == null)
            {
                return String.Format(OutputMessages.WrongTableNumber, tableNumber);
            }

            IBakedFood food = this.bakedFoods.FirstOrDefault(x => x.Name == foodName);

            if (food == null)
            {
                return String.Format(OutputMessages.NonExistentFood, foodName);
            }

            table.OrderFood(food);
            return String.Format(OutputMessages.FoodOrderSuccessful, tableNumber, foodName);
        }

        public string ReserveTable(int numberOfPeople)
        {
            if (tables.Any(table => table.IsReserved == false && table.Capacity >= numberOfPeople))
            {
                ITable table = tables.FirstOrDefault(x => x.IsReserved == false && x.Capacity >= numberOfPeople);
                table.Reserve(numberOfPeople);

                return String.Format(OutputMessages.TableReserved, table.TableNumber, numberOfPeople);
            }

            return String.Format(OutputMessages.ReservationNotPossible, numberOfPeople);
        }
    }
}
