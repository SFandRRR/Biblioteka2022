using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Biblioteka2022
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //string SQLServer = "server=s217-pc12\\SQLEXPRESS2019;database=Sekretariat;Integrated Security=True";

        //SqlConnection.
        //SqlConnection sql = new SqlConnection(SQLServer);
        //sql.Open();
        //string Q1 = "INSERT INTO Uczniowie (Imie, Nazwisko, Klasa) VALUES('" + imie + "', '" + nazw + "', '" + klas + "');";
        //SqlCommand command = new SqlCommand(Q1, sql);
        //command.ExecuteReader();
        //sql.Close();
 
        private void input_wyswietl_tabela_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)input_wyswietl_tabela.SelectedItem;
            string tabela = typeItem.Content.ToString();

            pola_tabeli(tabela); 

            string z = wyswietl_rekordy(tabela, "", "", 0);
            Trace.WriteLine(z + "bbbbb");
        }

        private void button_wyswietl_szukaj_Click(object sender, RoutedEventArgs e)
        {
            string tabela = "";
            string pole = "";
            string filtr = "";
            int sposob = 0;

            ComboBoxItem typeItem = (ComboBoxItem)input_wyswietl_tabela.SelectedItem;
            string value = typeItem.Content.ToString();

            //tabela = input_wyswietl_tabela.Tag.ToString();
            textbox_wyswietl.Document.Blocks.Add(new Paragraph(new Run(typeItem.Content.ToString())));


        }

        string wyswietl_rekordy(
           string tabela = "",
           string pole = "",
           string filtr = "",
           int sposob = 0)
        {
            string Query = "SELECT ";
            if (tabela != "")
            {

                switch (pole)
                {
                    case "Wypożyczenia":
                        Query += " ";
                        break;
                    case "Klienci":
                        Query += " ";
                        break;
                    case "Ksiązki":
                        Query += " ";
                        break;
                    default:
                        Query += "* ";
                        break;
                }

                switch (tabela)
                {
                    case "Wypożyczenia":
                        Query += "FROM Wyporzyczenia";
                        break;
                    case "Klienci":
                        Query += "FROM Klienci";
                        break;
                    case "Ksiązki":
                        Query += "FROM Ksiazki";
                        break;
                    default:
                        break;
                }
                if (sposob != 0)
                {

                }
                Query += ";";
                return Query;
            }
            return "";
        }

        void pola_tabeli(string tabela)
        {
            try { 
            switch (tabela)
            {
                case "Wypożyczenia":
                    input_wyswietl_pole.Items.Clear();
                    input_wyswietl_pole.Items.Add("Id");
                    input_wyswietl_pole.Items.Add("Id Klienta");
                    input_wyswietl_pole.Items.Add("Id Książki");
                    input_wyswietl_pole.Items.Add("Data Wyporzyczenia");
                    input_wyswietl_pole.Items.Add("Data Zwrotu");

                    break;
                case "Klienci":

                    input_wyswietl_pole.Items.Clear();
                    input_wyswietl_pole.Items.Add("Id Książki");
                    input_wyswietl_pole.Items.Add("Tytuł");
                    input_wyswietl_pole.Items.Add("Autor");
                    input_wyswietl_pole.Items.Add("Opis");
                    input_wyswietl_pole.Items.Add("Rok Wydania");

                    break;
                case "Ksiązki":
                    input_wyswietl_pole.Items.Clear();
                    input_wyswietl_pole.Items.Add("Id Klienta");
                    input_wyswietl_pole.Items.Add("Imie");
                    input_wyswietl_pole.Items.Add("Nazwisko");
                    input_wyswietl_pole.Items.Add("Pesel");
                    input_wyswietl_pole.Items.Add("Telefon");

                    break;
                default:
                    Trace.WriteLine("RIP");
                    break;
            }
            }
            catch
            {

            }
        }


    }
}
