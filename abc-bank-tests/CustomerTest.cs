﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestApp()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "Total $100.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total $3,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,900.00", henry.GetStatement());
        }

        [TestMethod]
        public void TestOneAccount()
        {
            Customer oscar = new Customer("Oscar").OpenAccount(new Account(Account.SAVINGS));
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                 .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));
            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));
            oscar.OpenAccount(new Account(Account.MAXI_SAVINGS));
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTransfer()
        {

            Account checkingAccount = new Account(Account.CHECKING, "Jorge Checking");
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer jorge = new Customer("Jorge").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(100.0);

            jorge.TransferMoney("Savings", "Jorge Checking", 50);

            Assert.AreEqual("Statement for Jorge\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "  deposit $50.00\n" +
                    "Total $150.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $100.00\n" +
                    "  withdrawal $50.00\n" +
                    "Total $50.00\n" +
                    "\n" +
                    "Total In All Accounts $200.00", jorge.GetStatement());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "deposit account not found")]
        public void TestTransferException()
        {

            Account checkingAccount = new Account(Account.CHECKING, "Borge Checking");
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer jorge = new Customer("Borge").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(100.0);

            jorge.TransferMoney("Savings", "Borge Bank", 50);
        }
    }
}
