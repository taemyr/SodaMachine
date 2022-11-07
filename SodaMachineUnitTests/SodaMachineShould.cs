using System;
using ConsoleApplication1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using ConsoleApplication1.Actions;
using System.Linq;

namespace SodaMachineUnitTests
{
    [TestClass]
    public class SodaMachineShould
    {
        Inventory cola1;
        Inventory sprite1;
        Inventory fanta1;
        Inventory cola2;
        InventoryCollection inv;
        SodaMachine sut;

        public SodaMachineShould()
        {
            cola1 = new Inventory(ReferenceData.Cola, 10);
            sprite1 = new Inventory(ReferenceData.Sprite, 0);
            fanta1 = new Inventory(ReferenceData.Fanta, 5);
            cola2 = new Inventory(ReferenceData.Cola, 0);
            inv = new InventoryCollection(cola1, sprite1, fanta1, cola2);
            sut = new SodaMachine(inv);
        }

        [TestMethod]
        public void OnInsert_UpdateBalance()
        {
            //Arrange
            //Act
            var result1 = sut.Insert(5);
            var result2 = sut.Insert(20);
            //Assert
            sut.Balance.Should().Be(25);
            var firstResult=result1.Should().HaveCount(1).And.Subject.First();
            firstResult.Should().BeOfType<NoOp>();
            firstResult.Msg.Should().Be("Adding 5 to credit");
            firstResult = result2.Should().HaveCount(1).And.Subject.First();
            firstResult.Should().BeOfType<NoOp>();
            firstResult.Msg.Should().Be("Adding 20 to credit");
        }

        [TestMethod]
        public void OnOrder_UpdateBalanceInventoryAndEmitSoda()
        {
            //Arrange
            int intialInsert = 40;
            var remainder = intialInsert - ReferenceData.Cola.Price;
            sut.Insert(intialInsert);
            //Act
            var result = sut.Order("coke").ToArray();
            //Assert
            sut.Balance.Should().Be(0);
            result.Should().HaveCount(2);
            result[0].Should().BeOfType<EmitSoda>();
            result[0].Msg.Should().Be("Giving coke out");
            result[1].Should().BeOfType<ReturnMoney>();
            result[1].Msg.Should().Be($"Returning {remainder} to customer");
            cola1.Amount.Should().Be(9);
        }

        [TestMethod]
        public void OnSmsOrder_UpdateInventoryAndEmitSoda()
        {
            //Arrange
            int intialInsert = 40;
            sut.Insert(intialInsert);
            //Act
            var result = sut.SmsOrder("coke").ToArray();
            //Assert
            sut.Balance.Should().Be(40);
            result.Should().HaveCount(1);
            result[0].Should().BeOfType<EmitSoda>();
            result[0].Msg.Should().Be("Giving coke out");
            cola1.Amount.Should().Be(9);
        }

        [TestMethod]
        public void OnOrder_WithInsufficentCash_DisplayWarning()
        {
            //Arrange
            int intialInsert = ReferenceData.Cola.Price-5;
            sut.Insert(intialInsert);
            //Act
            var result = sut.Order("coke").ToArray();
            //Assert
            sut.Balance.Should().Be(intialInsert);
            result.Should().HaveCount(1);
            result[0].Should().BeOfType<DisplayWarning>();
            result[0].Msg.Should().Be("Need 5 more");
            cola1.Amount.Should().Be(10);
        }

        [TestMethod]
        public void OnOrder_WithEmptyInventory_DisplayWarning()
        {
            //Arrange
            int intialInsert = 100;
            sut.Insert(intialInsert);
            //Act
            var result = sut.Order("sprite").ToArray();
            //Assert
            sut.Balance.Should().Be(intialInsert);
            result.Should().HaveCount(1);
            result[0].Should().BeOfType<DisplayWarning>();
            result[0].Msg.Should().Be("No sprite left");
            cola1.Amount.Should().Be(10);
        }

        [TestMethod]
        public void OnOrder_WithUnknownSoda_DisplayWarning()
        {
            //Arrange
            int intialInsert = 100;
            sut.Insert(intialInsert);
            //Act
            var result = sut.Order("NotASoda").ToArray();
            //Assert
            sut.Balance.Should().Be(intialInsert);
            result.Should().HaveCount(1);
            result[0].Should().BeOfType<DisplayWarning>();
            result[0].Msg.Should().Be("No such soda");
            cola1.Amount.Should().Be(10);
        }

        [TestMethod]
        public void OnRecall_ReturnMoneyAndUpdateBalance()
        {
            //Arrange
            int intialInsert = 40;
            sut.Insert(intialInsert);
            int insert2 = 2;
            sut.Insert(insert2);
            //Act
            var result = sut.Recall().ToArray();
            //Assert
            sut.Balance.Should().Be(0);
            result.Should().HaveCount(1);
            result[0].Should().BeOfType<ReturnMoney>();
            result[0].Msg.Should().Be($"Returning {intialInsert+insert2} to customer");
        }

    }
}
