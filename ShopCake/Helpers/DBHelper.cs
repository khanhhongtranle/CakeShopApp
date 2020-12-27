using System.Collections.Generic;
using System.Data.SQLite;
using System.Collections.Specialized;

public class DBHelper
{
    private readonly SQLiteConnection conn;

    public DBHelper()
    {
        this.conn = new SQLiteConnection(@"URI=file:/Users/tkt/Desktop/khanhhongtranle/dbhelper123/dbhelper123/db.sqlite");
        this.conn.Open();

        var cmd = new SQLiteCommand(this.conn);
        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS cakes(
id INTEGER PRIMARY KEY,
name TEXT, description TEXT,
date_entered DATETIME,
kindofcake_id int,
unit_price INTEGER,
in_stock INTEGER)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS kindofcakes(
id INTEGER PRIMARY KEY,
name TEXT)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS orders(
id INTEGER PRIMARY KEY,
date_entered DATETIME,
total INTEGER)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS cake_img(
cake_id INTEGER,
img_id INTEGER)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS images(
id INTEGER PRIMARY KEY,
link TEXT)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS order_cake(
order_id INTEGER,
cake_id INTEGER,
quantity INTEGER,
price INTEGER,
amount INTEGER);";
        cmd.ExecuteNonQuery();
    }

    public List<NameValueCollection> query(string sql, bool returnResult = false)
    {
        List<NameValueCollection> dataList = new List<NameValueCollection>();
        string stm = sql;
            
        if (returnResult)
        {
            using var cmd = new SQLiteCommand(stm, this.conn);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                dataList.Add(rdr.GetValues());
            }
        }
        else
        {
            using var cmd = new SQLiteCommand(this.conn);
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery(); 
        }
        return dataList;
    }

    ~DBHelper()
    {
        conn.Close();
    }

     
}