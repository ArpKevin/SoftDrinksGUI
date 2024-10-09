using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftDrinksGUI
{
    internal class Drink
    {
        public Drink(string drinkName, string sweetener, int pricePerLiterInForint, string packageType, int fruitContentInPercentage, int packageUnit)
        {
            DrinkName = drinkName;
            Sweetener = sweetener;
            PricePerLiterInForint = pricePerLiterInForint;
            PackageType = packageType;
            FruitContentInPercentage = fruitContentInPercentage;
            PackageUnit = packageUnit;
        }

        public string DrinkName { get; set; }
        public string Sweetener { get; set; }
        public int PricePerLiterInForint { get; set; }
        public string PackageType { get; set; }
        public int FruitContentInPercentage { get; set; }
        public int PackageUnit { get; set; }

        public string BrandName {get => DrinkName.Split(" ")[0];}
    }
}
