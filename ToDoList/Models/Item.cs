using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ToDoList;

namespace ToDoList.Models
{
  public class Item
  { 
    private int _id;
    private string _description;
    
    public Item(string description, int id = 0)
    {
      _description = description;
      _id = id;
    }
    
    public int GetId() {return _id;}
    public string GetDescription() {return _description;}
    
    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Item))
        return false;
      Item newItem = (Item) otherItem;
      return _description == newItem._description && _id == newItem._id;
    }
    public override int GetHashCode()
    {
        return (_description + _id).GetHashCode();
    }
    
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `items` (`description`) VALUES (@ItemDescription);";

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@ItemDescription";
      description.Value = this._description;
      cmd.Parameters.Add(description);
      
      cmd.ExecuteNonQuery();
      _id = (int)cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
        conn.Dispose();
    }
    
    public static Item Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items WHERE id = @idSearch;";

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@idSearch";
      description.Value = id;
      cmd.Parameters.Add(description);
      
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      
      int itemId = 0;
      string itemDescription = "";

      while (rdr.Read())
      {
        itemId = rdr.GetInt32(0);
        itemDescription = rdr.GetString(1);
      }

      Item foundItem = new Item(itemDescription, itemId);  // This line is new!

      conn.Close();
      if (conn != null)
        conn.Dispose();
      return foundItem;
    }
    
    public static List<Item> GetAll()
    {
      List<Item> allItems = new List<Item>();
      
      MySqlConnection conn = DB.Connection();
      conn.Open();
      
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        Item item = new Item(itemDescription, itemId);
        allItems.Add(item);
      }
      
      conn.Close();
      if (conn != null)
        conn.Dispose();
      
      return allItems;
    }
    
    // public static void Remove(int id) 
    // {
      // _instances.RemoveAt(id);
      // for(int i = id; i < _instances.Count; i++)
      // {
        // _instances[i]._id = i;
      // }
    // }
    
    public static void Clear()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
        conn.Dispose();
    }
  }
}