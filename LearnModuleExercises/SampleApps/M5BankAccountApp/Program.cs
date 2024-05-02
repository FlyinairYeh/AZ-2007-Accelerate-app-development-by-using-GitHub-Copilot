﻿namespace BankAccountApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<BankAccount> accounts = new List<BankAccount>();

            // Create bank accounts with random balances
            int numberOfAccounts = 4;
            int createdAccounts = 0;
            while (createdAccounts < numberOfAccounts)
            {
                    double initialBalance = GenerateRandomBalance(10, 50000);
                    string accountHolderName = GenerateRandomAccountHolder();
                    string accountType = GenerateRandomAccountType();
                    DateTime dateOpened = GenerateRandomDateOpened();
                    BankAccount account = new BankAccount($"Account {createdAccounts + 1}", initialBalance, accountHolderName, accountType, dateOpened);
                    accounts.Add(account);
                    createdAccounts++;
            }

            // Simulate 5 transactions for each account
            foreach (BankAccount account in accounts)
            {
                for (int i = 0; i < 5; i++)
                {
                    double transactionAmount = GenerateRandomBalance(-500, 500);
                        if (transactionAmount >= 0)
                        {
                            account.Credit(transactionAmount);
                            Console.WriteLine($"Credit: {transactionAmount}, Balance: {account.Balance.ToString("C")}, Account Holder: {account.AccountHolderName}, Account Type: {account.AccountType}");
                        }
                        else
                        {
                            account.Debit(-transactionAmount);
                            Console.WriteLine($"Debit: {transactionAmount}, Balance: {account.Balance.ToString("C")}, Account Holder: {account.AccountHolderName}, Account Type: {account.AccountType}");
                        }
                }

                Console.WriteLine($"Account: {account.AccountNumber}, Balance: {account.Balance.ToString("C")}, Account Holder: {account.AccountHolderName}, Account Type: {account.AccountType}");
            }

            // Simulate transfers between accounts
            foreach (BankAccount fromAccount in accounts)
            {
                foreach (BankAccount toAccount in accounts)
                {
                    if (fromAccount != toAccount)
                    {
                            double transferAmount = GenerateRandomBalance(0, fromAccount.Balance);
                            fromAccount.Transfer(toAccount, transferAmount);
                            Console.WriteLine($"Transfer: {transferAmount.ToString("C")} from {fromAccount.AccountNumber} ({fromAccount.AccountHolderName}, {fromAccount.AccountType}) to {toAccount.AccountNumber} ({toAccount.AccountHolderName}, {toAccount.AccountType})");
                    }
                }
            }
        }

        static double GenerateRandomBalance(double min, double max)
        {
            Random random = new Random();
            double balance = random.NextDouble() * (max - min) + min;
            return Math.Round(balance, 2);
        }

        static string GenerateRandomAccountHolder()
        {
            string[] accountHolderNames = { "John Smith", "Maria Garcia", "Mohammed Khan", "Sophie Dubois", "Liam Johnson", "Emma Martinez", "Noah Lee", "Olivia Kim", "William Chen", "Ava Wang", "James Brown", "Isabella Nguyen", "Benjamin Wilson", "Mia Li", "Lucas Anderson", "Charlotte Liu", "Alexander Taylor", "Amelia Patel", "Daniel Garcia", "Sophia Kim" };

            Random random = new Random();
            string accountHolderName = accountHolderNames[random.Next(0, accountHolderNames.Length)];
            return accountHolderName;
        }

        static string GenerateRandomAccountType()
        {
            string[] accountTypes = { "Savings", "Checking", "Money Market", "Certificate of Deposit", "Retirement" };
            Random random = new Random();
            return accountTypes[random.Next(0, accountTypes.Length)];
        }

        static DateTime GenerateRandomDateOpened()
        {
            Random random = new Random();
            DateTime startDate = new DateTime(DateTime.Today.Year - 10, 1, 1);
            int range = (DateTime.Today - startDate).Days;
            DateTime randomDate = startDate.AddDays(random.Next(range));

            if (randomDate.Year == DateTime.Today.Year && randomDate >= DateTime.Today)
            {
                randomDate = randomDate.AddDays(-1);
            }

            return randomDate;
        }
    }

    class BankAccount
    {
        public string AccountNumber { get; }
        public double Balance { get; private set; }
        public string AccountHolderName { get; }
        public string AccountType { get; }
        public DateTime DateOpened { get; }

        public BankAccount(string accountNumber, double initialBalance, string accountHolderName, string accountType, DateTime dateOpened)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
            AccountHolderName = accountHolderName;
            AccountType = accountType;
            DateOpened = dateOpened;
        }

        public void Credit(double amount)
        {
            Balance += amount;
        }

        public void Debit(double amount)
        {
                Balance -= amount;
        }

        public double GetBalance()
        {
            return Balance;
        }

        public void Transfer(BankAccount toAccount, double amount)
        {
            if (Balance >= amount)
            {
                Debit(amount);
                toAccount.Credit(amount);
            }
        }

        public void PrintStatement()
        {
            Console.WriteLine($"Account Number: {AccountNumber}, Balance: {Balance}");
            // Add code here to print recent transactions
        }

        public double CalculateInterest(double interestRate)
        {
            return Balance * interestRate;
        }
    }
}
