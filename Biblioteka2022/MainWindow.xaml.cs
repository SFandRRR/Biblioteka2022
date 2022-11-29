using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Animation;

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
            UpdateComboBoxWyporzyczenia();
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
                            if (pole == "Data Wyporzyczenia" || pole == "Data Zwrotu") {
                                Query += "Like '%" + filtr.ToString() + "'";
                            } else {
                                Query += "Like '" + filtr.ToString() + "%'";
                            }                        
                            break;
                        case "Jest równe":
                            Query += "= "+filtr.ToString();
                            break;
                        case "Zawiera":
                            Query += "Like '%" + filtr.ToString()+ "%'";
                            break;

                        default:
                            Query += " = 0 ";
                            Trace.WriteLine("> This code should not be reachable switch(sposob)");
                            break;
                    }
                }
                Query += ";";
                //return Query;
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
                        while(reader.Read())
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

        private void button_ksiazki_dodaj_Click(object sender, RoutedEventArgs e)
        {
            label_ksiazki.Content = "";

            bool Check = true;

            string autor = input_ksiazki_autor.Text;
            string tytul = input_ksiazki_tytul.Text;
            string opis = input_ksiazki_opis.Text;
            string wydanie = input_ksiazki_wydanie.Text;

            if(!Regex.IsMatch(wydanie, @"^[0-9]+$"))
            {
                label_ksiazki.Content = "Rok wydania może zawierać jedynie liczby!";
                Check = false;
            }

            if ((wydanie.Length>4) == true)
            {
                label_ksiazki.Content = "Zadługi rok wydania!";
                Check = false;
            }

            if ( autor.Equals(" ")|| autor.Equals("")|| tytul.Equals(" ") || tytul.Equals("") || opis.Equals(" ") || opis.Equals("") || wydanie.Equals(" ") || wydanie.Equals(""))
            {
                label_ksiazki.Content = "Należy Wypełnić wszystkie pola!";
                Check = false;
            }

            if (Check)
            {
                try
                {
                    string Query = "INSERT INTO Ksiazki(Tytul, Autor, Opis, RokWydania) VALUES('"+tytul+"', '"+autor+"', '"+opis+"', '"+wydanie+"');";

                    string SQLServer = "server=s217-pc12\\SQLEXPRESS2019;database=Biblioteka2022;Integrated Security=True";
                    //s217-pc12\SQLEXPRESS2019
                    //DESKTOP-8JDEIA5
                    SqlConnection sql = new SqlConnection(SQLServer);
                    sql.Open();
                    SqlCommand command = new SqlCommand(Query, sql);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                    }

                    sql.Close();

                }
                catch
                {
                    Trace.WriteLine("> Error with fetching SQL Query");
                }
            }         

            UpdateComboBoxWyporzyczenia();

        }

        private void button_klient_dodaj_Click(object sender, RoutedEventArgs e)
        {
            label_klient.Content = "";

            bool Check = true;

            string imie = input_klient_imie.Text;
            string nazwisko = input_klient_nazwisko.Text;
            string pesel = input_klient_pesel.Text;
            string telefon = input_klient_telefon.Text;

            if (!Regex.IsMatch(pesel, @"^[0-9]+$"))
            {
                label_klient.Content = "Pesel musi zawierać jedynie liczby!";
                Check = false;
            }

            if (pesel.Length != 11)
            {
                label_klient.Content = "Nie odpowiednia długość peselu!";
                Check = false;
            }

            if (!Regex.IsMatch(telefon, @"^[0-9]+$"))
            {
                label_klient.Content = "Telefon musi zawierać jedynie liczby!";
                Check = false;
            }

            if (telefon.Length != 9)
            {
                label_klient.Content = "Nie odpowiednia długość numeru telefonu!";
                Check = false;
            }

            if (pesel.Equals(" ") || pesel.Equals("") || telefon.Equals(" ") || telefon.Equals("") || imie.Equals(" ") || imie.Equals("") || nazwisko.Equals(" ") || nazwisko.Equals(""))
            {
                label_klient.Content = "Należy Wypełnić wszystkie pola!";
                Check = false;
            }

            if (Check)
            {
                try
                {
                    string Query = "INSERT INTO Klienci(Imie, Nazwisko, Pesel, Telefon) VALUES('" + imie + "', '" + nazwisko + "', '" + pesel + "', '" + telefon + "');";

                    string SQLServer = "server=s217-pc12\\SQLEXPRESS2019;database=Biblioteka2022;Integrated Security=True";
                    //s217-pc12\SQLEXPRESS2019
                    //DESKTOP-8JDEIA5
                    SqlConnection sql = new SqlConnection(SQLServer);
                    sql.Open();
                    SqlCommand command = new SqlCommand(Query, sql);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                    }

                    sql.Close();

                }
                catch
                {
                    Trace.WriteLine("> Error with fetching SQL Query");
                }
            }

            UpdateComboBoxWyporzyczenia();
        }


        void UpdateComboBoxWyporzyczenia()
        {
            string Query1 = "SELECT * FROM Klienci;";
            string Query2 = "SELECT * FROM Ksiazki;";
            try
            {
                input_wyporzyczenia_klient.Items.Clear();
                input_wyporzyczenia_ksiazka.Items.Clear();

                //input_wyswietl_pole.Items.Add("[Brak]");
            
                string SQLServer = "server=s217-pc12\\SQLEXPRESS2019;database=Biblioteka2022;Integrated Security=True";
                //s217-pc12\SQLEXPRESS2019
                //DESKTOP-8JDEIA5
                SqlConnection sql = new SqlConnection(SQLServer);
                sql.Open();

                SqlCommand command1 = new SqlCommand(Query1, sql);
                using (SqlDataReader reader = command1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        input_wyporzyczenia_klient.Items.Add(reader[0] + " " + reader[1] + " " + reader[2] + " " + reader[3] );
                    }
                }

                SqlCommand command2 = new SqlCommand(Query2, sql);
                using (SqlDataReader reader = command2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        input_wyporzyczenia_ksiazka.Items.Add(reader[0] + " " + reader[2] + " - " + reader[1] + " (" + reader[4]+")");
                    }
                }

                sql.Close();
            }
            catch
            {
                Trace.WriteLine("> Error with fetching SQL Query in tabitemfocus");
            }

        }

        private void button_wyporzyczenia_dodaj_Click(object sender, RoutedEventArgs e)
        {
            label_wyporzyczenia.Content = "";

            int klient = -1;
            int ksiazka = -1;

            string DataWyporzyczenia = "";
            string DataZwrotu = "";

            try
            {
                if (input_wyporzyczenia_klient.SelectedItem != null)
                {
                    klient = input_wyporzyczenia_klient.SelectedIndex+1;
                }
                else
                {
                    label_wyporzyczenia.Content = "Należy wybrać klienta!";
                }

                if (input_wyporzyczenia_ksiazka.SelectedItem != null)
                {
                    ksiazka = input_wyporzyczenia_ksiazka.SelectedIndex+1;
                }
                else
                {
                    label_wyporzyczenia.Content = "Należy wybrać książke!";
                }
            } catch { Trace.WriteLine("ComboBoxItem Null"); }

            if (input_wyporzyczenia_datawyporzyczenia.ToString() != "")
            {
                DataWyporzyczenia = input_wyporzyczenia_datawyporzyczenia.ToString();
            }
            else
            {
                label_wyporzyczenia.Content = "Należy wybrać datę wyporzyczenia!";
            }

            if (input_wyporzyczenia_datazwrot.ToString() != "")
            {
                DataZwrotu = input_wyporzyczenia_datazwrot.ToString();
            }
            else
            {
                label_wyporzyczenia.Content = "Należy wybrać datę zwrotu!";
            }

            TimeSpan? DataDiffrence = (input_wyporzyczenia_datazwrot.SelectedDate - input_wyporzyczenia_datawyporzyczenia.SelectedDate);
            if (DataDiffrence.ToString()[0] == '-')
            {
                label_wyporzyczenia.Content = "Data zwrotu nie może być przed datą wyporzyczenia!";
            }




            //label_wyporzyczenia.Content = DataDiffrence.ToString();
        }
    }
}
