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

            string z = wyswietl_rekordy(tabela, "", "", "");
            Trace.WriteLine(z + "bbbbb");

        }

        private void button_wyswietl_szukaj_Click(object sender, RoutedEventArgs e)
        {
            string tabela = "";
            string pole = "";                  
            string sposob = "";
            string filtr = "";

            filtr = input_wyswietl_filtr.Text.ToString();
            string value = "";

            ComboBoxItem ItemTabela = (ComboBoxItem)input_wyswietl_tabela.SelectedItem;
            if (ItemTabela != null) {
                tabela = ItemTabela.Content.ToString();
            }

            if (input_wyswietl_pole.SelectedItem != null)
            {
                pole = input_wyswietl_pole.SelectedItem.ToString();
            }

            ComboBoxItem ItemSposob = (ComboBoxItem)input_wyswietl_sposob.SelectedItem;
            if (ItemSposob != null)
            {
                sposob = ItemSposob.Content.ToString();          
            }

            value = wyswietl_rekordy(tabela, pole, sposob, filtr);

            textbox_wyswietl.Document.Blocks.Clear();
            textbox_wyswietl.Document.Blocks.Add(new Paragraph(new Run(value.ToString())));


        }

        string wyswietl_rekordy(
           string tabela = "",
           string pole = "",
           string sposob = "",
           string filtr = ""
           )
        {
            string Query = "SELECT * ";
            string TheInformation = "";
            if (tabela != "")
            {               

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

                switch (pole)
                {
                    case "Id":
                        Query += " WHERE Id ";
                        break;
                    case "Id Klienta":
                        Query += " WHERE Id_Klient ";
                        break;
                    case "Id Ksiązki":
                        Query += " WHERE Id_Ksiazka ";
                        break;
                    case "Data Wyporzyczenia":
                        Query += " WHERE DataWyporzyczenia ";
                        break;
                    case "Data Zwrotu":
                        Query += " WHERE DataZwrotu ";
                        break;
                    case "Tytuł":
                        Query += " WHERE Tytul ";
                        break;
                    case "Autor":
                        Query += " WHERE Autor ";
                        break;
                    case "Opis":
                        Query += " WHERE Opis ";
                        break;
                    case "Rok Wydania":
                        Query += "WHERE RokWydania ";
                        break;
                    case "Imie":
                        Query += " WHERE Imie ";
                        break;
                    case "Nazwisko":
                        Query += " WHERE Nazwisko ";
                        break;
                    case "Pesel":
                        Query += " WHERE Pesel ";
                        break;
                    case "Telefon":
                        Query += " WHERE Telefon ";
                        break;

                    default:
                        Query += "";
                        break;
                }

                if (filtr == "")
                {
                    filtr = "0";
                }

                if (pole != "[Brak]") {

                switch (sposob)
                    {
                        case "Rozpoczyna się od":
                        
                            break;
                        case "Jest równe":
                            Query += "= "+filtr.ToString();
                            break;
                        case "Zawiera":

                            break;

                        default:
                            Query += " = 0 ";
                            Trace.WriteLine("> This code should not be reachable switch(sposob)");
                            break;
                    }
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
                            TheInformation += reader[0] + " | " + reader[1] + " | " + reader[2] + " | " + reader[3] + " | " + reader[4] + "\n";
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
                        input_wyswietl_pole.Items.Add("[Brak]");
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
                        input_wyswietl_pole.Items.Add("[Brak]");
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
                        input_wyswietl_pole.Items.Add("[Brak]");
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
