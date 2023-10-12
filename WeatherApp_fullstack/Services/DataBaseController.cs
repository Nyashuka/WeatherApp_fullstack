using System.Data.SQLite;
using System.Text;

namespace WeatherApp_fullstack.Services;

public class DataBaseController
{
    private readonly SQLiteConnection _sqLiteConnection;

    public DataBaseController()
    {
        _sqLiteConnection = new SQLiteConnection("Data Source=weather_db.db");
        CreateTables();
    }

    public void AddToDataBase(WeatherData data)
    {
        string searchQuery =
            $"SELECT * FROM weathers WHERE city_id={data.CityId} and weather_time_utc={data.DateTimeUtc};";

        _sqLiteConnection.Open();
        SQLiteCommand command = new SQLiteCommand(_sqLiteConnection);
        command.CommandText = searchQuery;

        using (SQLiteDataReader reader = command.ExecuteReader())
        {
            if (reader.HasRows)
            {
                return;
            }
        }

        StringBuilder query = new StringBuilder();
        query.Append(
            $"INSERT INTO weathers (city_id, weather_time_utc, city_name, temperature, wind_speed,status, description) VALUES");
        query.Append(
            $"({data.CityId}, {data.DateTimeUtc}, '{data.CityName}', {data.MainData.Temp}, {data.Wind.Speed}, '{data.Weathers[0].Status}', '{data.Weathers[0].Description}')");

        command.CommandText = query.ToString();
        command.ExecuteNonQuery();

        _sqLiteConnection.Close();
    }

    public void ShowAllRecords()
    {
        string searchQuery = $"SELECT * FROM weathers;";

        _sqLiteConnection.Open();
        SQLiteCommand command = new SQLiteCommand(_sqLiteConnection);
        command.CommandText = searchQuery;

        using (SQLiteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine($"-----{reader.GetInt32(0)}-----");
                Console.WriteLine($"City id: {reader.GetInt32(1)}");
                Console.WriteLine(
                    $"Weather time: {new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(reader.GetInt32(2))}");
                Console.WriteLine($"City name: {reader.GetString(3)}");
                Console.WriteLine($"Temperature: {reader.GetFloat(4)}");
                Console.WriteLine($"Wind speed: {reader.GetFloat(5)}");
                Console.WriteLine($"Status: {reader.GetString(6)}");
                Console.WriteLine($"Description: {reader.GetString(7)}");
            }
        }

        _sqLiteConnection.Close();
    }

    private void CreateTables()
    {
        StringBuilder query = new StringBuilder();
        query.Append("CREATE TABLE IF NOT EXISTS weathers (");
        query.Append("id INTEGER PRIMARY KEY AUTOINCREMENT,");
        query.Append("city_id INTEGER,");
        query.Append("weather_time_utc INTEGER,");
        query.Append("city_name TEXT,");
        query.Append("temperature REAL,");
        query.Append("wind_speed REAL,");
        query.Append("status TEXT,");
        query.Append("description TEXT);");

        _sqLiteConnection.Open();
        SQLiteCommand command = new SQLiteCommand(_sqLiteConnection);
        command.CommandText = query.ToString();
        command.ExecuteNonQuery();
        _sqLiteConnection.Close();
    }
}