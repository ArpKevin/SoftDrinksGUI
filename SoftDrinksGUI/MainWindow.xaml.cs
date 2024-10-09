using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoftDrinksGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal List<Drink> drinks { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            drinks = new List<Drink>();

            using StreamReader srSoftDrinks = new(@"..\..\..\src\softDrinks.txt");

            _ = srSoftDrinks.ReadLine();

            while (!srSoftDrinks.EndOfStream)
            {
                var x = srSoftDrinks.ReadLine().Split(";");
                drinks.Add(new(x[0], x[1], int.Parse(x[2]), x[3], int.Parse(x[4]), int.Parse(x[5])));
            }

            //4
            Random r = new Random();

            var sugaryDrinks = drinks.Where(d => d.Sweetener == "cukor").ToList();

            lblRecommendation.Content += $" {sugaryDrinks[r.Next(sugaryDrinks.Count)].DrinkName}";

            //5

            var f5 = drinks.Where(d => d.FruitContentInPercentage == 0).MinBy(d => d.PricePerLiterInForint);

            lblCheapest.Content = $"{f5.DrinkName} - {f5.PricePerLiterInForint} Ft/l";

            //6

            lblHowManyManufacturersToChooseFrom.Content = $"{drinks.GroupBy(d => d.BrandName).Count()} féle gyártó termékei közül választhatnak!";

            //7

            using StreamWriter swAll = new(@"..\..\..\src\all.txt");

            var f7 = drinks.GroupBy(d => d.DrinkName);

            foreach (var item in f7)
            {
                swAll.WriteLine($"{item.Key} {Math.Round(item.Average(d => d.FruitContentInPercentage), 2)}");
            }

            //8

            using StreamWriter swSweetening = new(@"..\..\..\src\sweetening.txt");

            var f8 = drinks.GroupBy(d => d.Sweetener);

            foreach (var item in f8)
            {
                swSweetening.WriteLine($"{item.Key}-{item.Count()}");
            }
        }

        private void btnBidSave_Click(object sender, RoutedEventArgs e)
        {
            string searchedDrinkName = textboxSearchedDrinkName.Text;

            if (!string.IsNullOrWhiteSpace(searchedDrinkName) &&
                drinks.Any(d => d.BrandName.Contains(searchedDrinkName)))
            {
                using StreamWriter swOffer = new($@"..\..\..\src\offer{searchedDrinkName}.txt", false);

                var drinksMatchingUserInput = drinks.Where(d => d.BrandName == searchedDrinkName).ToList();

                foreach (var item in drinksMatchingUserInput)
                {
                    swOffer.WriteLine(item.DrinkName);
                }

                textboxSearchedDrinkName.Clear();

                MessageBox.Show($"{drinksMatchingUserInput.Count} db termék íródott be a fájlba, melyeknek átlagára: {drinksMatchingUserInput.Average(d => d.PricePerLiterInForint)} Ft.", "siker", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Nincs ilyen üdítőnk. Kérjük, válasszon mást!", "hiba", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }

        private void NewProductButtonClick(object sender, RoutedEventArgs e)
        {
            var newDrinkName = txtboxDrinkName.Text;
            var newSweetener = txtboxSweetener.Text;
            var newProductPrice = txtboxPrice.Text;
            var newDrinkFruitContent = txtboxFruitContent.Text;
            var newDrinkPackage = txtboxPackage.Text;


            if (newDrinkName.Length == 0 || newSweetener.Length == 0 || newDrinkPackage.Length == 0
                || !int.TryParse(newProductPrice, out var _)
                || !int.TryParse(newDrinkFruitContent, out var _))
            {
                MessageBox.Show("Valamelyik adat hiányzik vagy nem helyes formátumú!", "hiba", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }

            else
            {
                using StreamWriter swSoftDrinks = new(@"..\..\..\src\softDrinks.txt", true);

                swSoftDrinks.WriteLine($"{newDrinkName};{newSweetener};{newProductPrice};{newDrinkPackage};{newDrinkFruitContent};12");

                MessageBox.Show("Az üdítő hozzáadásra került.", "siker", MessageBoxButton.OK, MessageBoxImage.Information);

                //form resetelése
                txtboxDrinkName.Clear();txtboxSweetener.Clear();txtboxPrice.Clear();txtboxFruitContent.Clear();txtboxPackage.Clear();
            }

        }
    }
}