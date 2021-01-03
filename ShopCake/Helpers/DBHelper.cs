using System.Collections.Generic;
using System.Data.SQLite;
using System.Collections.Specialized;

public class DBHelper
{
    private readonly SQLiteConnection conn;

    public DBHelper()
    {
        this.conn = new SQLiteConnection(@"URI=file:Database\db.sqlite");
        this.conn.Open();
    }

    public void createDatabase()
    {
        var cmd = new SQLiteCommand(this.conn);
        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS cakes(
                            id TEXT PRIMARY KEY,
                            name TEXT, 
                            description TEXT,
                            date_entered DATETIME,
                            kindofcake_id int,
                            unit_price INTEGER)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS kindofcakes(
                            id INTEGER PRIMARY KEY,
                            name TEXT)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS orders(
                            id TEXT PRIMARY KEY,
                            date_entered DATETIME,
                            total INTEGER)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS cake_img(
                            cake_id TEXT,
                            img_id TEXT)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS images(
                            id TEXT PRIMARY KEY,
                            link TEXT)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS order_cake(
                            order_id TEXT,
                            cake_id TEXT,
                            quantity INTEGER,
                            price INTEGER,
                            amount INTEGER);";
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS config(
                            id INTEGER,
                            name TEXT,
                            value INTEGER);";
        cmd.ExecuteNonQuery();
    }
    
    public List<NameValueCollection> query(string sql, bool returnResult = false)
    {
        List<NameValueCollection> dataList = new List<NameValueCollection>();
        string stm = sql;
            
        if (returnResult)
        {
             var cmd = new SQLiteCommand(stm, this.conn);
             SQLiteDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                dataList.Add(rdr.GetValues());
            }
        }
        else
        {
             var cmd = new SQLiteCommand(this.conn);
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery(); 
        }
        return dataList;
    }
    
    ~DBHelper()
    {
        conn.Close();
    }

    public void close()
    {
        conn.Close();
    } 
}