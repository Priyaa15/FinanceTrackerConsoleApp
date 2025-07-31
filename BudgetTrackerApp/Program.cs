using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using BudgetTrackerApp;


namespace BudgetTrackerApp
{
    // Step 1: Create our main Program class
    class Program
    {
        static List<Transaction> transactions = new List<Transaction>();
        static int transactionNextId = 1;

        static void Main(string[] args)
        {
            Console.WriteLine("=== PERSONAL FINANCE TRACKER ===");

            ShowMainMenu();


            static void ShowMainMenu()
            {
                bool AppRunning = true;
                while (AppRunning)
                {
                    Console.Clear();
                    Console.WriteLine("=== PERSONAL FINANCE TRACKER ===");
                    Console.WriteLine("1. Add Income");
                    Console.WriteLine("2. Add Expense");
                    Console.WriteLine("3. View All Transactions");
                    Console.WriteLine("4. View Summary");
                    Console.WriteLine("5. Exit");
                    Console.Write("\nChoose an option (1-5): ");

                    string userChoice = Console.ReadLine();

                    switch (userChoice)
                    {
                        case "1":
                            AddIncome();
                            break;
                        case "2":
                            AddExpense();
                            break;
                        case "3":
                            ViewAllTransactions();
                            break;
                        case "4":
                            ViewSummary();
                            break;
                        case "5":
                            AppRunning = false;
                            Console.WriteLine("Thanks for using Finance Tracker");
                            break;
                        default:
                            Console.WriteLine("Choose a valid option between 1 and 5");
                            Console.WriteLine("Press Any Key to continue");
                            Console.ReadKey();
                            break;

                    }
                }
            }

            static void AddIncome()
            {
                Console.Clear();
                Console.WriteLine("=== ADD INCOME ===");

                Console.WriteLine("Add Description");
                string description = Console.ReadLine();

                Console.WriteLine("Amount : $");
                string amountInput = Console.ReadLine();

                if (decimal.TryParse(amountInput, out decimal amount) && amount > 0)
                {
                    transactions.Add(new Transaction(transactionNextId, description, amount, "Income", TransactionType.Income));
                    transactionNextId++;
                    Console.WriteLine($"\n✅ Income of {amount:C} added successfully!");
                }
                else
                {
                    Console.WriteLine("❌ Invalid amount. Please enter a positive number.");
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }

            static void AddExpense()
            {
                Console.Clear();
                Console.WriteLine("=== ADD Expense ===");

                Console.WriteLine("Enter Desciption");
                string description = Console.ReadLine();

                Console.WriteLine("Enter Expense amount $");
                string expenseinput = Console.ReadLine();

                if (decimal.TryParse(expenseinput, out decimal expenseAmount) && expenseAmount > 0)
                {
                    transactions.Add(new Transaction(transactionNextId, description, expenseAmount, "Expense", TransactionType.Expense));
                    transactionNextId++;
                    Console.WriteLine($"\n✅ Expense of {expenseAmount:C} added successfully!");
                }
                else
                {
                    Console.WriteLine("❌ Invalid amount. Please enter a positive number.");
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }


            static void ViewAllTransactions()
            {
                Console.Clear();
                Console.WriteLine("=== View Transactions ===");

                if (transactions.Count == 0)
                {
                    Console.WriteLine("=== No Transactions to display ===");
                }
                else
                {
                    Console.WriteLine("\n ID |    Date    |   Amount   |   Category   | Description");
                    Console.WriteLine("----+------------+------------+--------------+------------------");

                    foreach (Transaction transaction in transactions)
                    {
                        transaction.DisplayTransaction();
                    }

                    Console.WriteLine($"\n Transactions are {transactions.Count}");

                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

            }

             static void ViewSummary()
            {
                Console.Clear();
                Console.WriteLine("=== View Summary ===");

                if (transactions.Count == 0)
                {
                    Console.WriteLine("=== No Summary to display ===");
                }
                else
                {
                    foreach (Transaction transaction in transactions)
                    {
                        transaction.DisplayTransaction();
                    }
                }
                decimal totalIncome = 0;
                decimal expenseBalance = 0;

                foreach (Transaction t in transactions)
                {
                    if (t.Type == TransactionType.Income)
                    {
                        totalIncome = +t.Amount;

                    }
                    else if (t.Type == TransactionType.Expense)
                    {
                        expenseBalance = +t.Amount;
                    }
                }

                decimal netbalance = totalIncome - expenseBalance;

                Console.WriteLine($"\nTransactions After All Income and expenses count: {transactions.Count} transactions");
                Console.WriteLine($"NetBalance: {netbalance:C}");
                
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();


            }            

        
        }
       
    }
}