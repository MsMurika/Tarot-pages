using System;
using System.Data;
using Microsoft.Data.Sqlite;

namespace Tarotdatabase // database for storing card information. 
{
    internal class Program
    {
        static void Main()
        {
            // tuleeko tänne tämä? 
            /*
            var deck = new TarotdeckDB();

            // app. ei tässä yhteydessä toimi, koska console app eikä asp.net core proggis, miten ohitetaan?
            app.MapGet("/cards", () => deck.GetCards());
            */

            // here we create the new database, tarotdeck.db
            using (var connection = new SqliteConnection("Data Source = tarotdeck.db"))
            {
                // opening connection
                connection.Open();

                // method call for creating the needed tables for database, currently tarotdeck.db with just Cards table
                CreateTables(connection);

                static void CreateTables(SqliteConnection connection)
                {
                    // here we create a database for the cards of the tarot deck:
                    var createTableCmd = connection.CreateCommand();

                    createTableCmd.CommandText = "CREATE TABLE IF NOT EXISTS Cards (id INTEGER PRIMARY KEY, name TEXT NOT NULL, meaning TEXT NOT NULL)";
                    createTableCmd.ExecuteNonQuery();

                    // users would also be objects, thus making it easier to store their readings after this basic stuff works <3

                    // generate random object, should this be done here or inside the draw card -method? 
                    // hidden for a while Random random = new Random();

                    while (true)
                    {
                        Console.WriteLine("Mitä haluat tehdä? (1) Arvo kortti (2) Listaa kortit (3) Lopeta");
                        string? input = Console.ReadLine();

                        switch (input)
                        {
                            case "1":
                                DrawCard(connection);
                                break;

                            case "2":
                                ListCards(connection).ForEach(Console.WriteLine);
                                break;

                            case "3":
                                Console.WriteLine("Kiitos vierailusta tarotin maailmaan ja mukavaa elämänjatkoa!");
                                connection.Close();
                                Environment.Exit(0);
                                break;
                        }
                    }

                }


            }

            static void DrawCard(SqliteConnection connection)
            {
                // Query to retrieve a random card from the database
                string query = "SELECT name, meaning FROM Cards ORDER BY RANDOM() LIMIT 1";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = reader["name"].ToString();
                            string meaning = reader["meaning"].ToString();

                            // Print the random card information
                            Console.WriteLine($"Name: {name}");
                            Console.WriteLine($"Meaning: {meaning}");
                        }
                        else
                        {
                            Console.WriteLine("No cards found in the database.");
                        }
                    }
                }
            }
        }

        static List<string> ListCards(SqliteConnection connection)
        {
            // here we print out the current list of cards in the database
            List<string> cards = new List<string>();
            var selectSql = connection.CreateCommand();
            selectSql.CommandText = "SELECT name FROM Cards";
            var cardList = selectSql.ExecuteReader();

            while (cardList.Read())
            {
                cards.Add(cardList.GetString(0));
            }

            return cards;
        }

        // nämä metodit eivät tule minimum viable productiin!
        // tässä tallennetaan ennustus käyttäjän tietoihin

        /* static void AddUserReading(SqliteConnection connection, string lemmikinNimi, string lemmikinOmistajanNimi, string laji)
        {
            // method for adding new user reading to the database.
            var insertSql2 = connection.CreateCommand();
            insertSql2.CommandText = "INSERT INTO Lemmikit (nimi, omistajaId, laji) VALUES (@lemmikinNimi, @lemmikinOmistajanNimi, @laji)";
            insertSql2.Parameters.AddWithValue("@lemmikinNimi", lemmikinNimi);
            insertSql2.Parameters.AddWithValue("@lemmikinOmistajanNimi", lemmikinOmistajanNimi);
            insertSql2.Parameters.AddWithValue("@laji", laji);
            insertSql2.ExecuteNonQuery();
            Console.WriteLine("Kiitos! " + cardName + " on nyt lisätty Aiemmat ennustukset -tietokantaan.");
        }

        // ja tässä haetaan kaikki kyseisen käyttäjän aiemmat ennustukset
        static void GetPastReadings(SqliteConnection connection, string haettavaLemmikki)
        {
            // Retrieve the past readings for the user who is currently logged in 
            var selectSql = connection.CreateCommand();
            selectSql.CommandText = @"SELECT Users.readings
            tähän SQL query

            var reader = selectSql.ExecuteReader();

            if (reader.Read())
            {
                string puhelinnumero = reader["puhelin"].ToString();
                reader.Close(); // Close the reader when done
                Console.WriteLine(puhelinnumero);
            }
            else
            {
                reader.Close(); // Close the reader when done and inform the user that no such owner was found.
                Console.WriteLine("Tietoja ei löytynyt.");
            }
        }

        */


    }
}

