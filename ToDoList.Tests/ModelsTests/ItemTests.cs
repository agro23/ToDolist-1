using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using System;
using System.Collections.Generic;

namespace ToDoList.Models.Tests
{
  [TestClass]
  public class ItemTest : IDisposable
 {
    public ItemTest()
    {
      //Console.WriteLine("The port number and database name probably need to be changed");
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=todo_test;";
    }
    
    public void Dispose()
    {
      Item.Clear();
    }
    
    [TestMethod]
    public void Save_SavesToDatabase_ItemList()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn");

      //Act
      testItem.Save();
      List<Item> result = Item.GetAll();
      List<Item> testList = new List<Item>{testItem};

      //Assert
      CollectionAssert.AreEqual(testList, result);
      Assert.AreEqual(1, Item.GetAll().Count);
    }
    
    [TestMethod]
    public void GetAll_ReturnListOfAllItems_ListItem()
    {
      Assert.AreEqual(0, Item.GetAll().Count);
    }
    
    [TestMethod]
    public void Find_FindsItemInDatabase_Item()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn");
      testItem.Save();

      //Act
      Item foundItem = Item.Find(testItem.GetId());

      //Assert
      Assert.AreEqual(testItem, foundItem);
      Assert.AreEqual(testItem.GetId(), foundItem.GetId());
      Assert.AreEqual(testItem.GetDescription(), foundItem.GetDescription());
    }
  }
}