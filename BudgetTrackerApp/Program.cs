using System;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using BudgetTrackerApp;


namespace BudgetTrackerApp
{
    // Step 1: Create our main Program class
    class Program
    {
        static List<Transaction> transactions = new List<Transaction>();
        static int transactionNextId = 1;

        static Dictionary<string, decimal> categoryBudgets = new Dictionary<string, decimal>();

        static void Main(string[] args)
        {
            Console.WriteLine("=== PERSONAL FINANCE TRACKER ===");

            ShowMainMenu();
            initialiseBudgetCategory();

        }

         static void initialiseBudgetCategory()
        {
            categoryBudgets.Add("Food", 500m);
            categoryBudgets.Add("Transportation", 200m);
            categoryBudgets.Add("Entertainment", 150m);
            categoryBudgets.Add("Utilities", 300m);
            categoryBudgets.Add("Shopping", 200m);
            categoryBudgets.Add("Income", 0m);      // Income categories don't need budgets
            categoryBudgets.Add("Other", 100m);
            
            Console.WriteLine("📋 Categories initialized with budgets!");
        }

        static string ChooseCategoryFromDictionary()
        {
            Console.WriteLine("\nAvailable categories:");
            //Convert Dictionary keys to a list to number them
            List<string> CategoryList = new List<string>();

            //Add categories to our list excluding income.

            foreach (var category in categoryBudgets)
            {
                if (category.Key != "Income")
                {
                    CategoryList.Add(category.Key);
                }
            }

            //Display number category
            for (int i = 0; i < CategoryList.Count; i++)
            {
                string category = CategoryList[i];
                decimal budgets = categoryBudgets[category];
                Console.WriteLine($"{i + 1}. {category} (Budget: {budgets:c}}");
            }
            //get User choice

            while (true)
            {
                Console.WriteLine($"\n choose category from 1 - {CategoryList.Count}");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= CategoryList.Count)
                {
                    return CategoryList[choice - 1];
                }
                else
                {
                    Console.WriteLine("Invalid choice");
                }

            }
        }

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
