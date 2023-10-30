using Microsoft.Data.Sqlite;

public record Card(int id, string name, string meaning);
class TarotdeckDB
{
    private static string _connectionString = "Data Source=tarotdeck.db";

    public TarotdeckDB()
    {
        // tietokantayhteyden luonti
        var connection = new SqliteConnection(_connectionString);
        connection.Open();

        // luodaan tietokantaan taulu korteille
        // jätetäänkö tähän vai luodaanko vain Program.cs:ssä?
        var command = connection.CreateCommand();
        command.CommandText = "CREATE TABLE IF NOT EXISTS Cards (id INTEGER PRIMARY KEY, nimi TEXT NOT NULL, meaning TEXT NOT NULL)";
        command.ExecuteNonQuery();

        connection.Close();
    }


    // metodi, joka hakee kaikki tietokannan kortit ja palauttaa ne helposti JSONiksi muunnettavassa muodossa
    public Dictionary<int, string> GetCards()
    {
        // tietokantayhteyden luonti
        var connection = new SqliteConnection(_connectionString);
        connection.Open();

        // haetaan kortit tietokannasta
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Cards";
        var reader = command.ExecuteReader();

        // luodaan dictionary korteille
        // voidaanko käyttää eri muotoa tallentamiseen / json muuntamiseen, dictionary ei tue int, string, string -yhdistelmää?
        var cards = new Dictionary<int, string>();

        // käydään kortit läpi ja lisätään ne dictionaryyn
        // miksi ei voi lisätä kolmea muuttujaa?
        while (reader.Read())
        {
            var id = reader.GetInt32(0);
            var name = reader.GetString(1);
            var meaning = reader.GetString(2);
            cards.Add(id, name, meaning);
        }

        connection.Close();

        return cards;
    }
}
