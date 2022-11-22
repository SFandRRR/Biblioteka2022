using System;
using System.Data.SqlClient;
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

            string value = "";

            ComboBoxItem ItemTabela = (ComboBoxItem)input_wyswietl_tabela.SelectedItem;
            if (ItemTabela != null) {
                tabela = ItemTabela.Content.ToString();
            }

            /*ComboBoxItem ItemPole = (ComboBoxItem)input_wyswietl_pole.SelectedItem;
            if (ItemPole != null)
            {
                pole = ItemPole.Content.ToString();
            }*/

            if (input_wyswietl_pole.SelectedItem != null)
            {
                pole = input_wyswietl_pole.SelectedItem.ToString();
            }

            value = wyswietl_rekordy(tabela, pole, "", 0);

            textbox_wyswietl.Document.Blocks.Clear();
            textbox_wyswietl.Document.Blocks.Add(new Paragraph(new Run(value.ToString())));


        }

        string wyswietl_rekordy(
           string tabela = "",
           string pole = "",
           string filtr = "",
           int sposob = 0)
        {
            string Query = "SELECT ";
            string TheInformation = "";
            if (tabela != "")
            {

                switch (pole)
                {
                    case "Id":
                        Query += "Id ";
                        break;
                    case "Id Klienta":
                        Query += "Id_Klient ";
                        break;
                    case "Id Ksiązki":
                        Query += "Id_Ksiazka ";
                        break;
                    case "Data Wyporzyczenia":
                        Query += "DataWyporzyczenia ";
                        break;
                    case "Data Zwrotu":
                        Query += "DataZwrotu ";
                        break;
                    case "Tytuł":
                        Query += "Tytul ";
                        break;
                    case "Autor":
                        Query += "Autor ";
                        break;
                    case "Opis":
                        Query += "Opis ";
                        break;
                    case "Rok Wydania":
                        Query += "RokWydania ";
                        break;
                    case "Imie":
                        Query += "Imie ";
                        break;
                    case "Nazwisko":
                        Query += "Nazwisko ";
                        break;
                    case "Pesel":
                        Query += "Pesel ";
                        break;
                    case "Telefon":
                        Query += "Telefon ";
                        break;

                    default:
                        Query += "* ";
                        break;
                }

                switch (tabela)
                {
                    case "Wypożyczenia":
                        Query += "FROM Wyporzyczenia";
                        TheInformation = "[Id] | [Id_Klient] | [Id_Ksiazka] | [DataWyporzyczenia] | [DataZwrotu] \n";
                        break;
                    case "Klienci":
                        Query += "FROM Klienci";
                        TheInformation = "[Id_Klient] | [Imie] | [Nazwisko] | [Pesel] | [Telefon] \n";
                        break;
                    case "Ksiązki":
                        Query += "FROM Ksiazki";
                        TheInformation = "[Id_Ksiazka] | [Tytul] | [Autor] | [Opis] | [RokWydania] \n";
                        break;
                    default:
                        return "Nie wybrano tabeli";
                        break;
                }
                if (sposob != 0)
                {

                }
                Query += ";";

                try
                {
                    string SQLServer = "server=s217-pc12\\SQLEXPRESS2019;database=Biblioteka2022;Integrated Security=True";
                    //s217-pc12\SQLEXPRESS2019
                    //DESKTOP-8JDEIA5
                    SqlConnection sql = new SqlConnection(SQLServer);
                   sql.Open();
                    SqlCommand command = new SqlCommand(Query, sql);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            TheInformation += reader[0] + " | " + reader[1] + " | " + reader[2] + " | " + reader[3].ToString().Substring(0, 10) + " | " + reader[4].ToString().Substring(0,10) + "\n";
                        }
                    }

                    sql.Close();

                    //return Query;
                    return TheInformation;
                }
                catch
                {
                    Trace.WriteLine("> Error with fetching SQL Query");
                }

            }
            return "";
        }

        void pola_tabeli(string tabela)
        {
            
            switch (tabela)
            {
                case "Wypożyczenia":
                    if(input_wyswietl_pole != null) { 
                        input_wyswietl_pole.Items.Clear();
                        input_wyswietl_pole.Items.Add("Id");
                        input_wyswietl_pole.Items.Add("Id Klienta");
                        input_wyswietl_pole.Items.Add("Id Książki");
                        input_wyswietl_pole.Items.Add("Data Wyporzyczenia");
                        input_wyswietl_pole.Items.Add("Data Zwrotu");
                    }

                    break;
                case "Ksiązki":

                    if (input_wyswietl_pole != null)
                    {
                        input_wyswietl_pole.Items.Clear();
                        input_wyswietl_pole.Items.Add("Id Książki");
                        input_wyswietl_pole.Items.Add("Tytuł");
                        input_wyswietl_pole.Items.Add("Autor");
                        input_wyswietl_pole.Items.Add("Opis");
                        input_wyswietl_pole.Items.Add("Rok Wydania");
                    }

                    break;
                case "Klienci":

                    if (input_wyswietl_pole != null)
                    {
                        input_wyswietl_pole.Items.Clear();
                        input_wyswietl_pole.Items.Add("Id Klienta");
                        input_wyswietl_pole.Items.Add("Imie");
                        input_wyswietl_pole.Items.Add("Nazwisko");
                        input_wyswietl_pole.Items.Add("Pesel");
                        input_wyswietl_pole.Items.Add("Telefon");
                    }

                    break;
                default:
                    Trace.WriteLine("> RIP pola_tabeli()");
                    break;
            }
            
        }


    }
}
