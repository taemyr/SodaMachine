using System;
using ConsoleApplication1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace SodaMachineUnitTests
{
    [TestClass]
    public class SodaMachineShould
    {
        SodaMachine sut = new SodaMachine();

        [TestMethod]
        public void OnInsert_UpdateBalance()
        {
            //Arrange
            //Act
            sut.Insert(5);
            sut.Insert(20);
            //Assert
            sut.Balance.Should();
        }
    }
}
