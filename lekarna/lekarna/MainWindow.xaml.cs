using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();
            SetSubgroupsToGrid(); // potom setnout selected prvek na první
            SetChargesToGrid();
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
