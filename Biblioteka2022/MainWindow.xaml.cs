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

            ComboBoxItem typeItem = (ComboBoxItem)input_wyswietl_tabela.SelectedItem;
            string value = typeItem.Content.ToString();

            tabela = typeItem.Content.ToString();
            value = wyswietl_rekordy(tabela, "", "", 0);

            //tabela = input_wyswietl_tabela.Tag.ToString();
            textbox_wyswietl.Document.Blocks.Add(new Paragraph(new Run(value.ToString())));


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
                try
                {
                    string SQLServer = "server=DESKTOP-8JDEIA5;database=Biblioteka2022;Integrated Security=True";
                    SqlConnection sql = new SqlConnection(SQLServer);
                   sql.Open();
                    SqlCommand command = new SqlCommand(Query, sql);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Trace.WriteLine(String.Format("{0}", reader["id"]));
                        }
                    }

                    sql.Close();

                    return Query;
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
                case "Klienci":

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
                case "Ksiązki":

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
