using System;
using System.Collections.Generic;

namespace ToDoList.Models
{
  public class Item
  {
    private static List<Item> _instances = new List<Item>();
    
    private int _id;
    private string _name;
    private string _description;
    
    public Item(string name, string description)
    {
      _name = name;
      _description = description;
      Save();
    }
    
    public int GetId() {return _id;}
    public string GetName() {return _name;}
    public string GetDescription() {return _description;}
    
    private void Save()
    {
      _id = _instances.Count;
      _instances.Add(this);
    }
    
    public static Item Find(int id)
    {
      return _instances[id];
    }
    
    public static List<Item> GetAll()
    {
      return _instances;
    }
    
    public static void Remove(int id) 
    {
      _instances.RemoveAt(id);
      for(int i = id; i < _instances.Count; i++)
      {
        _instances[i]._id = i;
      }
    }
    
    public static void Clear()
    {
      _instances.Clear();
    }
  }
}