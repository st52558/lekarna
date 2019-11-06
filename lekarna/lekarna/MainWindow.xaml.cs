using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lekarna
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Subgroup> subgroups = new List<Subgroup>();
        List<RecipeBuy> recipes = new List<RecipeBuy>();
        List<FreeBuy> freeBuys = new List<FreeBuy>();
        public MainWindow()
        {
            InitializeComponent();
            SetSubgroupsToGrid();
        }

        private void SetSubgroupsToGrid()
        {
            SubgroupsGrid.ItemsSource = null;
            subgroups.Clear();
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=.\lekarnademo.db;"))
            {
                conn.Open();

                SQLiteCommand command = new SQLiteCommand("select id_podskupina, skupina.nazev, podskupina.nazev, typ_stav.popis_k from podskupina join skupina on podskupina.id_skupina=skupina.id_skupina join typ_stav on typ_stav.tabulka='PODSKUPINA' and typ_stav.hodnota=podskupina.stav", conn);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Subgroup item = new Subgroup();
                    item.Id = reader.GetInt32(0);
                    item.Group = reader.GetString(1);
                    item.Name = reader.GetString(2);
                    item.State = reader.GetString(3);

                    SQLiteCommand command2 = new SQLiteCommand("select popis, hodnota_rp from ex_lekprirazka where vyber_l_rp='A' and id_podskupina=" + item.Id, conn);
                    SQLiteDataReader reader2 = command2.ExecuteReader();
                    while (reader2.Read())
                    {
                        item.RecipeCategory = reader2.GetString(0);
                        item.RecipeBuy = reader2.GetInt32(1);
                    }
                    reader2.Close();
                    command2 = new SQLiteCommand("select popis, hodnota_o from ex_lekprirazka where vyber_l_o='A' and id_podskupina=" + item.Id, conn);
                    reader2 = command2.ExecuteReader();
                    while (reader2.Read())
                    {

                        item.FreeBuyCategory = reader2.GetString(0);
                        item.FreeBuy = reader2.GetInt32(1);
                    }
                    reader2.Close();
                    subgroups.Add(item);

                }
                reader.Close();
            }
            SubgroupsGrid.ItemsSource = subgroups;
            /*SubgroupsGrid.Columns.ElementAt(0).Header = "ID";
            SubgroupsGrid.Columns.ElementAt(1).Header = "Název";
            SubgroupsGrid.Columns.ElementAt(2).Header = "Skupina";
            SubgroupsGrid.Columns.ElementAt(3).Header = "Stav";
            SubgroupsGrid.Columns.ElementAt(4).Header = "Volný prodej";
            SubgroupsGrid.Columns.ElementAt(5).Header = "Prodej na recept";
            SubgroupsGrid.Columns.ElementAt(6).Header = "VP %";
            SubgroupsGrid.Columns.ElementAt(7).Header = "RP %";*/
        }

        private void SubgroupChanged(object sender, SelectionChangedEventArgs e)
        {
            
            
            RecipeGrid.ItemsSource = null;
            recipes.Clear();
            Subgroup selectedItem = (Subgroup)SubgroupsGrid.SelectedItem;
            if (selectedItem != null)
            {

            
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=.\lekarnademo.db;"))
            {
                conn.Open();

                SQLiteCommand command = new SQLiteCommand("select id_lekkategorie, popis, hodnota_rp from ex_lekprirazka where id_podskupina=" + selectedItem.Id, conn);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    RecipeBuy item = new RecipeBuy();
                    item.Id = reader.GetInt32(0);
                    item.Name = reader.GetString(1);
                    item.Value = reader.GetInt32(2);
                    recipes.Add(item);
                }
                RecipeGrid.ItemsSource = recipes;
            }


            FreeBuyGrid.ItemsSource = null;
            freeBuys.Clear();
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=.\lekarnademo.db;"))
            {
                conn.Open();

                SQLiteCommand command = new SQLiteCommand("select id_lekkategorie, popis, hodnota_o from ex_lekprirazka where id_podskupina=" + selectedItem.Id, conn);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    FreeBuy item = new FreeBuy();
                    item.Id = reader.GetInt32(0);
                    item.Name = reader.GetString(1);
                    item.Value = reader.GetInt32(2);
                    freeBuys.Add(item);
                }
                FreeBuyGrid.ItemsSource = freeBuys;
            }
            }
        }

        private void FreeBuyChanged(object sender, MouseButtonEventArgs e)
        {
            Subgroup selectedItem = (Subgroup)SubgroupsGrid.SelectedItem;
            FreeBuy selectedCharge = (FreeBuy)FreeBuyGrid.SelectedItem;
            if (selectedCharge != null) { 
                using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=.\lekarnademo.db;"))
                {
                    conn.Open();

                    SQLiteCommand command = new SQLiteCommand("update ex_lekprirazka set vyber_l_o='N' where id_podskupina=" + selectedItem.Id, conn);
                    command.ExecuteReader();
                    command = new SQLiteCommand("update ex_lekprirazka set vyber_l_o='A' where id_podskupina=" + selectedItem.Id + " and id_lekkategorie=" + selectedCharge.Id, conn);
                    command.ExecuteReader();
                }
            }
            SetSubgroupsToGrid();
            FreeBuyGrid.ItemsSource = null;
            freeBuys.Clear();
            RecipeGrid.ItemsSource = null;
            recipes.Clear();
        }

        private void RecipeBuyChanged(object sender, MouseButtonEventArgs e)
        {
            Subgroup selectedItem = (Subgroup)SubgroupsGrid.SelectedItem;
            RecipeBuy selectedCharge = (RecipeBuy)RecipeGrid.SelectedItem;
            if (selectedCharge != null)
            {
                using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=.\lekarnademo.db;"))
                {
                    conn.Open();

                    SQLiteCommand command = new SQLiteCommand("update ex_lekprirazka set vyber_l_rp='N' where id_podskupina=" + selectedItem.Id, conn);
                    command.ExecuteReader();
                    command = new SQLiteCommand("update ex_lekprirazka set vyber_l_rp='A' where id_podskupina=" + selectedItem.Id + " and id_lekkategorie=" + selectedCharge.Id, conn);
                    command.ExecuteReader();
                }
            }
        SetSubgroupsToGrid();
            RecipeGrid.ItemsSource = null;
            recipes.Clear();
            FreeBuyGrid.ItemsSource = null;
            freeBuys.Clear();
        }
        /* v prvním datagridu bude Název podskupiny, Stav, vybaná kategorie přirážky (ComboBox, když není žádná tak prázdno), hodnota1, hodnota2
* v druhém datagridu bude popis vybrané kategorie přirážky, hodnota1, hodnota2
* po kliknutí v prvním datagridu se načtou data do druhého podle vybraného řádku
* při změně v comboboxu se všem nevybraným změní položka na N a vybranému na A.. bude tam vobla "žádná přirážka" a ta nastaví všechno na N
* načítání z databáze do datagridu1 při inicializaci, zápis při změně comboboxu
* načítání z databáze do datagridu2 při něčem vybraném z datagridu1
* 
* inicializace - do datagridu1 načíst všechno, 
*/
    }
}
