using System;
using System.Collections.Generic;

namespace ToDoList.Models
{
  
  public class Category
  {
    private static List<Category> _instances = new List<Category>();
    private int _id;
    private string _name;
    private List<Item> _items;
    
    public Category(string name)
    {
      _name = name;
      _id = _instances.Count;
      _instances.Add(this);
      _items = new List<Item>();
    }
    
    public int GetId() {return _id;}
    public string GetName() {return _name;}
    public List<Item> GetItems() {return _items;}
    
    public void AddItem(Item item)
    {
      _items.Add(item);
    }
    
    public static List<Category> GetAll()
    {
      return _instances;
    }
    
    public static void Clear()
    {
      _instances.Clear();
    }
    
    public static Category Find(int id)
    {
      return _instances[id];
    }
    
    public static void Remove(int id) 
    {
      _instances.RemoveAt(id);
      for(int i = id; i < _instances.Count; i++)
      {
        _instances[i]._id = i;
      }
    }
    
    
  }
}