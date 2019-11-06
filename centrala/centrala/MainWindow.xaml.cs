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

namespace centrala
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Subgroup> freeBuys = new List<Subgroup>();
        List<Subgroup> recipes = new List<Subgroup>();
        public MainWindow()
        {
            InitializeComponent();
            SetValuesToGrids();
        }

        private void SetValuesToGrids()
        {
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=.\lekarnademo.db;"))
            {
                recipes.Clear();
                freeBuys.Clear();
                FreeBuyGrid.ItemsSource = null;
                RecipeBuyGrid.ItemsSource = null;
                conn.Open();
                SQLiteCommand command = new SQLiteCommand("select id_podskupina, nazev from podskupina", conn);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Subgroup recipeItem = new Subgroup();
                    recipeItem.Id = reader.GetInt32(0);
                    recipeItem.Name = reader.GetString(1);
                    SQLiteCommand command2 = new SQLiteCommand("select id_lekkategorie, hodnota_rp from ex_lekprirazka where id_podskupina=" + recipeItem.Id, conn);
                    SQLiteDataReader reader2 = command2.ExecuteReader();
                    while (reader2.Read())
                    {
                        switch (reader2.GetInt32(0))
                        {
                            case 1:
                                recipeItem.A = reader2.GetInt32(1);
                                break;
                            case 2:
                                recipeItem.B = reader2.GetInt32(1);
                                break;
                            case 3:
                                recipeItem.C = reader2.GetInt32(1);
                                break;
                            case 4:
                                recipeItem.D = reader2.GetInt32(1);
                                break;
                            case 5:
                                recipeItem.E = reader2.GetInt32(1);
                                break;
                            case 6:
                                recipeItem.F = reader2.GetInt32(1);
                                break;
                            case 7:
                                recipeItem.G = reader2.GetInt32(1);
                                break;
                            case 8:
                                recipeItem.H = reader2.GetInt32(1);
                                break;
                            case 9:
                                recipeItem.I = reader2.GetInt32(1);
                                break;
                            default:
                                break;
                        }
                    }
                    reader2.Close();
                    recipes.Add(recipeItem);

                    Subgroup freeBuyItem = new Subgroup();
                    freeBuyItem.Id = reader.GetInt32(0);
                    freeBuyItem.Name = reader.GetString(1);
                    command2 = new SQLiteCommand("select id_lekkategorie, hodnota_o from ex_lekprirazka where id_podskupina=" + freeBuyItem.Id, conn);
                    reader2 = command2.ExecuteReader();
                    while (reader2.Read())
                    {
                        switch (reader2.GetInt32(0))
                        {
                            case 1:
                                freeBuyItem.A = reader2.GetInt32(1);
                                break;
                            case 2:
                                freeBuyItem.B = reader2.GetInt32(1);
                                break;
                            case 3:
                                freeBuyItem.C = reader2.GetInt32(1);
                                break;
                            case 4:
                                freeBuyItem.D = reader2.GetInt32(1);
                                break;
                            case 5:
                                freeBuyItem.E = reader2.GetInt32(1);
                                break;
                            case 6:
                                freeBuyItem.F = reader2.GetInt32(1);
                                break;
                            case 7:
                                freeBuyItem.G = reader2.GetInt32(1);
                                break;
                            case 8:
                                freeBuyItem.H = reader2.GetInt32(1);
                                break;
                            case 9:
                                freeBuyItem.I = reader2.GetInt32(1);
                                break;
                            default:
                                break;
                        }
                    }
                    reader2.Close();
                    freeBuys.Add(freeBuyItem);
                }
                reader.Close();
                RecipeBuyGrid.ItemsSource = recipes;
                FreeBuyGrid.ItemsSource = freeBuys;
            }
        }


        private void RecipeValueEdit(object sender, DataGridCellEditEndingEventArgs e)
        {
            int a = (int)e.Column.DisplayIndex;
            RecipeBuyGrid.
            Console.WriteLine(a);
            Subgroup selectedItem = (Subgroup)RecipeBuyGrid.SelectedItem;
            DataGridCellInfo cellInfo = RecipeBuyGrid.CurrentCell;
            DataGridColumn column = cellInfo.Column;
            int updatedCategory = column.DisplayIndex - 2;
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=.\lekarnademo.db;"))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand("update ex_lekprirazka set hodnota_rp=""id_podskupina, nazev from podskupina " + updatedCategory, conn);
                SQLiteDataReader reader = command.ExecuteReader();
            }
        }
        private void FreeBuyValueEdit(object sender, DataGridCellEditEndingEventArgs e)
        {

        }
    }
}
