using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using System;
using System.Collections.Generic;

namespace ToDoList.Models.Tests
{
  [TestClass]
  public class CategoryTest : IDisposable
 {
    public CategoryTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=todo_test;";
    }
    
    public void Dispose()
    {
      Category.Clear();
    }
    
    [TestMethod]
    public void Save_SavesToDatabase_Void()
    {
      //Arrange
      //Act
      Category cat = new Category("test cat name");
      cat.Save();
      List<Category> result = Category.GetAll();
      List<Category> testList = new List<Category>{cat};

      //Assert
      //CollectionAssert.AreEqual(testList, result);
      Assert.AreEqual(testList.Count, result.Count);
    }
    
    
  }
}