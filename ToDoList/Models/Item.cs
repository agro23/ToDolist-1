using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ToDoList;

namespace ToDoList.Models
{
  public class Item
  { 
    private int _id;
    private int _catId;
    private string _description;
    
    public Item(string description, int catId = 0)
    {
      _description = description;
      _id = 0;
      _catId = catId;
    }
    
    public int GetId() {return _id;}
    public int GetCategoryId() {return _catId;}
    public string GetDescription() {return _description;}
    
    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Item))
        return false;
      Item newItem = (Item) otherItem;
      return _description == newItem._description && _id == newItem._id && _catId == newItem._catId;
    }
    public override int GetHashCode()
    {
        return (_description + _id + _catId).GetHashCode();
    }
    
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO items (description, category) VALUES (@ItemDescription, @ItemCategory);";

      cmd.Parameters.Add(new MySqlParameter("@ItemDescription", _description));
      cmd.Parameters.Add(new MySqlParameter("@ItemCategory", _catId));
      
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

      cmd.Parameters.Add(new MySqlParameter("@idSearch", id));
      
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      
      int itemId = 0;
      int itemCategory = 0;
      string itemDescription = "";

      while (rdr.Read())
      {
        itemId = rdr.GetInt32(0);
        itemDescription = rdr.GetString(1);
        itemCategory = rdr.GetInt32(2);
      }

      Item foundItem = new Item(itemDescription, itemCategory);
      foundItem._id = itemId;

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
        int itemCategory = rdr.GetInt32(2);
        Item item = new Item(itemDescription, itemCategory);
        item._id = itemId;
        allItems.Add(item);
      }
      
      conn.Close();
      if (conn != null)
        conn.Dispose();
      
      return allItems;
    }
    
    public static List<Item> GetInCategory(int categoryId)
    {
      List<Item> categoryItems = new List<Item>();
      
      MySqlConnection conn = DB.Connection();
      conn.Open();
      
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items WHERE category = @category;";
      
      cmd.Parameters.Add(new MySqlParameter("@category", categoryId));
      
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        int itemCat = rdr.GetInt32(2);
        Item item = new Item(itemDescription, itemCat);
        item._id = itemId;
        categoryItems.Add(item);
      }
      
      conn.Close();
      if (conn != null)
        conn.Dispose();
      
      return categoryItems;
    }
    
    public static void Remove(int id) 
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items WHERE id = @idSearch;";

      cmd.Parameters.Add(new MySqlParameter("@idSearch", id));
      
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
        conn.Dispose();
    }
    
    public static void Clear()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items; ALTER TABLE items AUTO_INCREMENT = 1;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
        conn.Dispose();
    }
  }
}