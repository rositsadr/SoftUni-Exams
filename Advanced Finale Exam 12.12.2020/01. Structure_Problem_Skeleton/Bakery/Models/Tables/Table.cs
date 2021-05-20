using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables.Contracts;
using Bakery.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Models.Tables
{
    public abstract class Table : ITable
    {
        private ICollection<IBakedFood> foodOrders;
        private ICollection<IDrink> drinkOrders;
        private int capacity;
        private int numberOfPeople;

        public Table(int tableNumber, int capacity, decimal pricePerPerson)
        {
            this.TableNumber = tableNumber;
            this.Capacity = capacity;
            this.PricePerPerson = pricePerPerson;

            foodOrders = new List<IBakedFood>();
            drinkOrders = new List<IDrink>();
        }

        public int TableNumber { get; }

        public int Capacity
        {
            get
            {
                return this.capacity;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidTableCapacity));
                }

                this.capacity = value;
            }
        }

        public int NumberOfPeople
        {

            get
            {
                return this.numberOfPeople;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidNumberOfPeople));
                }

                this.numberOfPeople = value;
            }
        }

        public decimal PricePerPerson { get; }

        public bool IsReserved { get; private set; }

        public decimal Price => this.numberOfPeople * this.PricePerPerson;

        public void Clear()
        {
            foodOrders.Clear();
            drinkOrders.Clear();
            this.numberOfPeople = 0;
        }

        public decimal GetBill()
        {
            decimal totalPrice = this.Price;

            foreach (IDrink drink in drinkOrders)
            {
                totalPrice += drink.Price;
            }

            foreach (IBakedFood food in foodOrders)
            {
                totalPrice += food.Price;
            }

            return totalPrice;
        }

        public string GetFreeTableInfo()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"Table: {this.TableNumber}");
            result.AppendLine($"Type: {this.GetType().Name}");
            result.AppendLine($"Capacity: {this.Capacity}");
            result.AppendLine($"Price per Person: {this.PricePerPerson}");

            return result.ToString().Trim();
        }

        public void OrderDrink(IDrink drink)
        {
            drinkOrders.Add(drink);
        }

        public void OrderFood(IBakedFood food)
        {
            foodOrders.Add(food);
        }

        public void Reserve(int numberOfPeople)
        {
            this.IsReserved = true;
            this.numberOfPeople = numberOfPeople;
        }
    }
}
