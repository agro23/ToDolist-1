using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using System;

namespace ToDoList.Models.Tests
{
  [TestClass]
  public class ItemTest : IDisposable
 {
    public ItemTest()
    {
      Console.WriteLine("The port number and database name probably need to be changed");
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=my_database_name_test;";
    }
    
    public void Dispose()
    {
      Item.Clear();
    }
    
    [TestMethod]
    public void Constructor_CreateNewItem_Item()
    {
      Assert.IsInstanceOfType(new Item("Important Thing", "Gotta Do it"), typeof(Item));
    }
    
    [TestMethod]
    public void Find_ReturnItemWithId_Item()
    {
      Item item = new Item("Important Thing", "Gotta Do it");
      Assert.AreEqual(item, Item.Find(item.GetId()));
    }
    
    [TestMethod]
    public void GetAll_ReturnListOfAllItems_ListItem()
    {
      Item item = new Item("Important Thing", "Gotta Do it");
      Assert.AreEqual(1, Item.GetAll().Count);
    }
    
    [TestMethod]
    public void Remove_DeleteItemFromList_Void()
    {
      Item item = new Item("Important Thing", "Gotta Do it");
      item = new Item("Important Thing", "Gotta Do it");
      item = new Item("Important Thing", "Gotta Do it");
      item = new Item("Important Thing", "Gotta Do it");
      Assert.AreEqual(4, Item.GetAll().Count);
      Item.Remove(2);
      Assert.AreEqual(3, Item.GetAll().Count);
      Assert.AreEqual(2, Item.Find(2).GetId());
    } 
  }
}