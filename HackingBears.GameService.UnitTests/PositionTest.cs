using HackingBears.GameService.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HackingBears.GameService.UnitTests
{
    [TestClass]
    public sealed class PositionTest
    {
        #region Methods

        [TestMethod]
        public void Add_Position01_Position02()
        {
            #region Arrange

            Position pos01 = new Position
            {
                X = 5,
                Y = 3
            };

            Position pos02 = new Position
            {
                X = 1,
                Y = 4
            };

            #endregion

            #region Act

            Position pos = pos01 + pos02;

            #endregion

            #region Assert

            Assert.AreEqual(6, pos.X);
            Assert.AreEqual(7, pos.Y);

            #endregion
        }

        public void Add_Position01_Position02IsNegative()
        {
            #region Arrange

            Position pos01 = new Position
            {
                X = 5,
                Y = 3
            };

            Position pos02 = new Position
            {
                X = -1,
                Y = -4
            };

            #endregion

            #region Act

            Position pos = pos01 + pos02;

            #endregion

            #region Assert

            Assert.AreEqual(4, pos.X);
            Assert.AreEqual(-1, pos.Y);

            #endregion
        }

        [TestMethod]
        public void Add_Position01_Position02IsNull_Position01()
        {
            #region Arrange

            Position pos01 = new Position
            {
                X = 5,
                Y = 3
            };

            #endregion

            #region Act

            Position pos = pos01 + null;

            #endregion

            #region Assert

            Assert.AreEqual(5, pos.X);
            Assert.AreEqual(3, pos.Y);

            #endregion
        }

        [TestMethod]
        public void Add_Position01IsNull_Position02_Position02()
        {
            #region Arrange

            Position pos02 = new Position
            {
                X = 1,
                Y = 4
            };

            #endregion

            #region Act

            Position pos = null + pos02;

            #endregion

            #region Assert

            Assert.AreEqual(1, pos.X);
            Assert.AreEqual(4, pos.Y);

            #endregion
        }

        [TestMethod]
        public void Substract_Position01_Position02()
        {
            #region Arrange

            Position pos01 = new Position
            {
                X = 5,
                Y = 3
            };

            Position pos02 = new Position
            {
                X = 1,
                Y = 4
            };

            #endregion

            #region Act

            Position pos = pos01 - pos02;

            #endregion

            #region Assert

            Assert.AreEqual(4, pos.X);
            Assert.AreEqual(-1, pos.Y);

            #endregion
        }

        public void Substract_Position01_Position02IsNegative()
        {
            #region Arrange

            Position pos01 = new Position
            {
                X = 5,
                Y = 3
            };

            Position pos02 = new Position
            {
                X = -1,
                Y = -4
            };

            #endregion

            #region Act

            Position pos = pos01 + pos02;

            #endregion

            #region Assert

            Assert.AreEqual(6, pos.X);
            Assert.AreEqual(7, pos.Y);

            #endregion
        }

        [TestMethod]
        public void Substract_Position01_Position02IsNull_Position01()
        {
            #region Arrange

            Position pos01 = new Position
            {
                X = 5,
                Y = 3
            };

            #endregion

            #region Act

            Position pos = pos01 - null;

            #endregion

            #region Assert

            Assert.AreEqual(5, pos.X);
            Assert.AreEqual(3, pos.Y);

            #endregion
        }

        [TestMethod]
        public void Substract_Position01IsNull_Position02_Position02()
        {
            #region Arrange

            Position pos02 = new Position
            {
                X = 1,
                Y = 4
            };

            #endregion

            #region Act

            Position pos = null - pos02;

            #endregion

            #region Assert

            Assert.AreEqual(-1, pos.X);
            Assert.AreEqual(-4, pos.Y);

            #endregion
        }

        [TestMethod]
        public void Multiply_Position_PositiveFactor()
        {
            #region Arrange

            Position pos = new Position
            {
                X = 1,
                Y = 4
            };

            #endregion

            #region Act

            Position result = pos * 2;

            #endregion

            #region Assert

            Assert.AreEqual(2, result.X);
            Assert.AreEqual(8, result.Y);

            #endregion
        }

        [TestMethod]
        public void Multiply_Position_NegativeFactor()
        {
            #region Arrange

            Position pos = new Position
            {
                X = 1,
                Y = 4
            };

            #endregion

            #region Act

            Position result = pos * -2;

            #endregion

            #region Assert

            Assert.AreEqual(-2, result.X);
            Assert.AreEqual(-8, result.Y);

            #endregion
        }

        #endregion
    }
}