using System;
using System.Collections.Generic;
using System.Text;
using TheBookParlour.Core.Helpers;
using TheBookParlour.Data.Entities;

namespace TheBookParlour.TestUnits
{
    public class CartServiceTests
    {
        [Fact]
        public void GetTotalSum_ShouldReturnTotalSum() //Ändra namn? Mer specifikt?
        {
            //Arrange
            var items = new List<CartItem>
            {
                new CartItem { Price = 199, Quantity = 2 },
                new CartItem { Price = 299, Quantity = 1 }
            };

            //Act
            decimal total = CartHelper.GetTotalSum(items);

            //Assert
            Assert.Equal(697, total);

        }
    }
}
