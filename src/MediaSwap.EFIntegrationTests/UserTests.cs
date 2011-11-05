using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaSwap.Core.Repositories;
using System.Transactions;
using MediaSwap.Core.Models;

namespace MediaSwap.EFIntegrationTests
{
    [TestClass]
    public class UserTests
    {
        private MediaSwapContext _context;

        [TestInitialize]
        public void Initialize()
        {
            _context = new MediaSwapContext();
        }

        [TestMethod]
        public void Should_be_able_to_add_user()
        {
            //using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            //{
                User user = new User();
                user.FirstName = "Jeaux";
                user.LastName = "Smith";
                user.Token = "1212121";
                user.UserName = "jeaux";
                user.Email = "im@jeaux.com";

                var items = new List<Item>();
                items.Add(new Item(){ ItemName = "Test Item"});
                items.Add(new Item(){ ItemName = "Test Item 2"});

                user.Items = items;

                _context.User.Add(user);

                _context.SaveChanges();

            //    scope.Complete();

            //}
        }
    }
}
