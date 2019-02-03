using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartFridge.DataServices;

namespace SmartFridge.Test
{
    [TestClass]
    public class FridgeItemsServiceTest
    {
        FridgeItemsService service = new FridgeItemsService();

        public void SeedTestData()
        {
            service.ClearSmartFridge();
            service.AddItem(24601, "ABC1", "Peanut Butter", 0);
            service.AddItem(24602, "ABC2", "Jelly", 0.2);
            service.AddItem(24602, "ABC3", "Jelly Peanutbutter", 0.4);
            service.AddItem(24602, "ABC4", "Jellybutter Peanut", 0.6);
            service.AddItem(24603, "ABC5", "Baseball Bat", 0.8);
            service.AddItem(24604, "ABC6", "Pen Pineapple Apple Pen", 1);
        }

        [TestMethod]
        public void TestForgetItem1()
        {
            // Arrange
            SeedTestData();
            // Act
            service.ForgetItem(24601);
            var expectedItems = service.GetItems(1).Where(i => i.IsActive == true).ToList();
            // Assert
            Assert.AreEqual(expectedItems.Count, 5);
        }

        [TestMethod]
        public void TestForgetItem2()
        {
            // Arrange
            SeedTestData();
            // Act
            service.ForgetItem(24602);
            var expectedItems = service.GetItems(1).Where(i => i.IsActive == true).ToList();
            // Assert
            Assert.AreEqual(expectedItems.Count, 3);
        }

        [TestMethod]
        public void TestGetFillFactor1()
        {
            // Arrange
            SeedTestData();
            // Act
            var expectedItems = (decimal)(service.GetFillFactor(24602));
            // Assert
            Assert.AreEqual(expectedItems, 0.4m);
        }

        [TestMethod]
        public void TestGetItems1()
        {
            // Arrange
            SeedTestData();
            // Act
            var expectedItems1 = service.GetItems(1);
            var expectedItems2 = service.GetItems(0.8);
            var expectedItems3 = service.GetItems(0.5);
            // Assert
            Assert.AreEqual(expectedItems1.Count, 6);
            Assert.AreEqual(expectedItems2.Count, 5);
            Assert.AreEqual(expectedItems3.Count, 3);
        }

        [TestMethod]
        public void TestAddItem1()
        {
            // Arrange
            SeedTestData();
            // Act
            service.AddItem(24609, "ABC9", "Mysterious Blob", 0.9);
            var expectedItems = service.GetItems(1);
            var addedItem = expectedItems.Where(i => i.ItemUUID == "ABC9").FirstOrDefault();
            // Assert
            Assert.AreEqual(expectedItems.Count, 7);
            Assert.AreEqual(addedItem.Name, "Mysterious Blob");
        }

        [TestMethod]
        public void TestRemoveItemByUUID1()
        {
            // Arrange
            SeedTestData();
            // Act
            service.RemoveItemByUUID("ABC4");
            var expectedItems = service.GetItems(1);
            // Assert
            Assert.AreEqual(expectedItems.Count, 5);
        }

        [TestMethod]
        public void TestClearSmartFridge1()
        {
            // Arrange
            SeedTestData();
            // Act
            service.ClearSmartFridge();
            var expectedItems = service.GetItems(1);
            // Assert
            Assert.AreEqual(expectedItems.Count, 0);
        }
    }
}
