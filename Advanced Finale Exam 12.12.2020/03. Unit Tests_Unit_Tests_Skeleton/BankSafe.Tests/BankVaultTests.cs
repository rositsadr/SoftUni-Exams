using NUnit.Framework;
using System;

namespace BankSafe.Tests
{
    public class BankVaultTests
    {
        BankVault vault;
        Item item;

        [SetUp]
        public void Setup()
        {
            vault = new BankVault();
            item = new Item("Dimitrov", "TXP324");
        }

        [Test]
        public void ConstructorSetsTheVaultWith12Cells()
        {
            int expectedValue = 12;
            int actualValue = vault.VaultCells.Count;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void CreatingVaultItsCellsHavePriciseNameAndNullValue()
        {
            Assert.AreEqual(vault.VaultCells.ContainsKey("A1"), true);
            Assert.AreEqual(vault.VaultCells.ContainsKey("A2"), true);
            Assert.AreEqual(vault.VaultCells.ContainsKey("A3"), true);
            Assert.AreEqual(vault.VaultCells.ContainsKey("A4"), true);
            Assert.AreEqual(vault.VaultCells.ContainsKey("B1"), true);
            Assert.AreEqual(vault.VaultCells.ContainsKey("B2"), true);
            Assert.AreEqual(vault.VaultCells.ContainsKey("B3"), true);
            Assert.AreEqual(vault.VaultCells.ContainsKey("B4"), true);
            Assert.AreEqual(vault.VaultCells.ContainsKey("C1"), true);
            Assert.AreEqual(vault.VaultCells.ContainsKey("C2"), true);
            Assert.AreEqual(vault.VaultCells.ContainsKey("C3"), true);
            Assert.AreEqual(vault.VaultCells.ContainsKey("C4"), true);
            Assert.AreEqual(vault.VaultCells["A1"], null);
            Assert.AreEqual(vault.VaultCells["A2"], null);
            Assert.AreEqual(vault.VaultCells["A3"], null);
            Assert.AreEqual(vault.VaultCells["A4"], null);
            Assert.AreEqual(vault.VaultCells["B1"], null);
            Assert.AreEqual(vault.VaultCells["B2"], null);
            Assert.AreEqual(vault.VaultCells["B3"], null);
            Assert.AreEqual(vault.VaultCells["B4"], null);
            Assert.AreEqual(vault.VaultCells["C1"], null);
            Assert.AreEqual(vault.VaultCells["C2"], null);
            Assert.AreEqual(vault.VaultCells["C3"], null);
            Assert.AreEqual(vault.VaultCells["C4"], null);
        }

        [Test]
        public void CanNotAddItemToNonExistingCell()
        {
            string cell = "G1";
            Assert.AreEqual(vault.VaultCells.ContainsKey(cell), false);

            Assert.That(()=>vault.AddItem(cell, item),Throws.ArgumentException);
        }

        [Test]
        public void CanNotAddItemToFullCell()
        {
            string cell = "A1";
            vault.AddItem(cell, item);
            Assert.That(() => vault.AddItem(cell, new Item("m", "32")), Throws.ArgumentException);
        }

        [Test]
        public void CanNotAddItemToACellIfItIsAlreadyInAnotherCell()
        {
            string cell1 = "A1";
            string cell2 = "A2";

            vault.AddItem(cell1, item);
            Assert.That(()=>vault.AddItem(cell2, item), Throws.InvalidOperationException);
        }

        [Test]
        public void AddOperationAddsNonExistingItemInExistingCell()
        {
            string cellName = "A1";
            Assert.AreEqual(vault.VaultCells[cellName], null);

            vault.AddItem(cellName, item);
            Assert.IsTrue(vault.VaultCells[cellName] != null);
        }

        [Test]
        public void CanNotRemoveFromNonExistingCell()
        {
            string cell = "G1";
            Assert.IsTrue(!vault.VaultCells.ContainsKey(cell));

            Assert.That(() => vault.RemoveItem(cell, item), Throws.ArgumentException);
        }

        [Test]
        public void CanNotRemoveItemIfItIsNotInTheCell()
        {
            string cellName = "A1";
            vault.AddItem(cellName, item);
            Item newItem = new Item("M", "32");

            Assert.That(() => vault.RemoveItem(cellName, newItem), Throws.ArgumentException);
        }

        [Test]
        public void RemoveOperationRemovesItemFromCell()
        {
            string cellName = "A1";
            vault.AddItem(cellName, item);
            vault.RemoveItem(cellName, item);

            Assert.IsTrue(vault.VaultCells[cellName] == null);
        }

    }
}