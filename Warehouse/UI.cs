using System;
using System.Collections.Generic;
using System.Linq;
using Warehouse.Entity;
using Warehouse.Repositories;

namespace Warehouse
{
    class UI
    {
        ConsignmentRepositories consignmentRepositories = new ConsignmentRepositories();
        GoodsRepositories goodsRepositories = new GoodsRepositories();

        public void MeinMenu()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("*****Mein menu*****");
                Console.WriteLine("1: ShowGoods\n2: AddGoods\n3: AddConsignment\n4: Exit");
                string take = Console.ReadLine();
                switch (take)
                {
                    case "1":
                        ShowGoods();
                        break;
                    case "2":
                        AddGoods();
                        break;
                    case "3":
                        AddConsignment();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Error invalid number, try again");
                        break;
                }
            }
        }

        private void AddGoods()
        {
            string name, unit;
            double price = 0, quantity = 0;

            Console.Clear();
            Console.WriteLine("*****AddGoods menu*****");
            Console.WriteLine("Enter goods name");
            while (true)
            {
                name = Console.ReadLine().Trim();
                if (name == null || name == "")
                {
                    Console.WriteLine("Error, name can not be empty");
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("Enter goods unit");
            while (true)
            {
                unit = Console.ReadLine().Trim();
                if (unit == null || unit == "")
                {
                    Console.WriteLine("Error, name can not be empty");
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("Enter price");
            while (true)
            {
                try
                {
                    price = Convert.ToDouble(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Error, try again");
                }
            }
            Console.WriteLine("Enter quantity");
            while (true)
            {
                try
                {
                    quantity = Convert.ToDouble(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Error, try again");
                }
            }

            try
            {
                goodsRepositories.AddGoods(new Goods
                {
                    GoodsName = name,
                    Unit = unit,
                    Price = price,
                    Quantity = quantity
                });
            }
            catch
            {
                Console.WriteLine("Error");
            }
            Console.WriteLine("Added");
            Console.WriteLine("Press any key to go to the main menu");
            Console.ReadKey();
            Console.Clear();
        }

        private void ShowGoods()
        {
            Console.Clear();
            Console.WriteLine("*****All Goods*****");
            foreach (var g in goodsRepositories.GetGoods())
            {
                Console.WriteLine($"ID: {g.GoodsId}, {g.GoodsName}, {g.Quantity} {g.Unit}, price {g.Price}$");
            }
            Console.WriteLine("Press any key to go to the main menu");
            Console.ReadKey();
            Console.Clear();
        }

        private void AddConsignment() 
        {
            string type;

            Console.Clear();
            Console.WriteLine("*****Consignment menu*****");
            Console.WriteLine("Pick deal type");
            Console.WriteLine("1: Add\n2: Take");
            while (true)
            {
                string var = Console.ReadLine();
                if (var == "1")
                {
                    type = "Add";
                    break;
                }
                else if (var == "2")
                {
                    type = "Take";
                    break;
                }
                else
                {
                    Console.WriteLine("Error, try again");
                }
            }

            var allGoods = goodsRepositories.GetGoods();

            foreach (var g in allGoods)
            {
                Console.WriteLine($"ID: {g.GoodsId}, {g.GoodsName}, {g.Quantity} {g.Unit}, price {g.Price}$");
            }

            List<ConsignmentSize> cs = new List<ConsignmentSize>();
            while (true)
            {
                cs.Add(SomeMethod(allGoods, cs));
                Console.WriteLine("For finish press 0 or press any key to continue");
                string finish = Console.ReadLine();
                if (finish == "0")
                {
                    break;
                }
            }

            try
            {
                consignmentRepositories.AddConsignment(new Consignment
                {
                    Date = DateTime.Now,
                    Type = type
                }, cs);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Consignment added, press any key to go to the main menu");
            Console.ReadKey();
            Console.Clear();
        }

        private ConsignmentSize SomeMethod(IEnumerable<Goods> goods, IEnumerable<ConsignmentSize> cs)
        {
            int id, q;

            while (true)
            {
                try
                {
                    Console.WriteLine("Pick Id");
                    id = Convert.ToInt32(Console.ReadLine());
                    if (goods.Where(x => x.GoodsId == id).Count() == 0)
                    {
                        Console.WriteLine("Wrong Id");
                        throw new Exception();
                    }
                    if (cs.Where(x => x.GoodsId == id).Count() == 1)
                    {
                        Console.WriteLine("This Id was added");
                        throw new Exception();
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("Error, try again");
                }
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("Pick quantity");
                    q = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Error, try again");
                }
            }

            return new ConsignmentSize
            {
                GoodsId = id,
                Quantity = q
            };
        }
    }
}
