using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TwentiethTask
{
    public class Shop
    {
        public List<Product> Goods { get; set; }
        public decimal Income { get; set; }

        public Shop()
        {
            Income = 0m;
            Goods = new List<Product>();
        }

        public void Ordering(Customer cus, Order ord)
        {
            cus.Orders.Add(ord);
            ord.ComputeTotalPrice();
            Console.WriteLine("Заказ оформлен {2} на {0} {1}.\nЗаказ состоит из:", cus.Name, cus.Surname, ord.Date);
            int counter = 1;
            foreach (Product pr in ord.Products)
            {
                Console.WriteLine("{0}. {1}", counter++, pr.ToString());
            }
            Console.WriteLine("Итого к оплате: {0}\n", ord.TotalPrice);
        }

        public void Purchasing(Customer cus)
        {
            decimal money = cus.Orders.Sum(p => p.TotalPrice);
            Income += money;
            cus.Cash -= money;
            cus.Orders.Clear();
            Console.WriteLine("Покупка осуществлена.\n");
        }
    }

    public class Customer
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal Cash { get; set; }

        public List<Order> Orders { get; set; }

        public Customer(string _name, string _surname, decimal _cash)
        {
            Name = _name;
            Surname = _surname;
            Cash = _cash;
            Orders = new List<Order>();
        }

        public override string ToString()
        {
            return string.Format("Имя: {0}, Фамилия: {1}, Cash: {2}", Name, Surname, Cash);
        }
    }

    public class Order
    {
        public DateTime Date { get; set; }
        public List<Product> Products { get; set; }
        public decimal TotalPrice { get; set; }

        public void ComputeTotalPrice()
        {
            TotalPrice = Products.Sum(p => p.Price);
        }

        public Order(DateTime _date)
        {
            Date = _date;
            Products = new List<Product>();
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return string.Format("ID: {0}, Название: {1}, Описание: {2}, Цена: {3}", Id, Name, Description, Price);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Shop shop = new Shop();
            shop.Goods.Add(new Product { Id = 1, Name = "Обод", Description = "Обод ровный", Price = 120m });
            shop.Goods.Add(new Product { Id = 2, Name = "Вилка", Description = "Вилка крепкая", Price = 1000m });
            shop.Goods.Add(new Product { Id = 3, Name = "Рама", Description = "Рама неубиваемая", Price = 5000m });
            shop.Goods.Add(new Product { Id = 4, Name = "Втулка", Description = "Втулка крутая", Price = 450m });

            Customer cus1 = new Customer("Аркадий", "Тимошенко", 3000m);

            Console.WriteLine("{0} до похода в магазин: Cash = {1}\nДоход магазина: {2}\n", cus1.Name, cus1.Cash, shop.Income);

            Order ord1 = new Order(DateTime.Now);
            ord1.Products.Add(shop.Goods[0]);
            ord1.Products.Add(shop.Goods[3]);

            shop.Ordering(cus1, ord1); // Аркадий заказывает товар
            Thread.Sleep(2000);
            shop.Purchasing(cus1); // Аркадий покупает товар

            Console.WriteLine("{0} после похода в магазин: Cash = {1}\nДоход магазина: {2}\n", cus1.Name, cus1.Cash, shop.Income);
            Console.WriteLine("-----------------------------------------------------------------");

            Customer cus2 = new Customer("Петр", "Иванов", 10000m);

            Console.WriteLine("{0} до похода в магазин: Cash = {1}\nДоход магазина: {2}\n", cus2.Name, cus2.Cash, shop.Income);

            Order ord2 = new Order(DateTime.Now);
            ord2.Products.Add(shop.Goods[2]);

            shop.Ordering(cus2, ord2); // Петр заказывает товар
            Thread.Sleep(2000);
            shop.Purchasing(cus2); // Петр покупает товар

            Console.WriteLine("{0} после похода в магазин: Cash = {1}\nДоход магазина: {2}\n", cus2.Name, cus2.Cash, shop.Income);
        }
    }
}