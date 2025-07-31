namespace BudgetTrackerApp
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }

        public Transaction(int id, string Desc, decimal amount, string category, TransactionType type)
        {
            Id = id;
            Description = Desc;
            Amount = amount;
            Category = category;
            Type = type;
            Date = DateTime.Now;

        }

        public void DisplayTransaction()
        {
            string SymbolType = Type == TransactionType.Income ? "+" : "-";
            Console.WriteLine($"{Id,3} | {Date:MM/dd/yyyy} | {SymbolType}{Amount,8:C} | {Category,-12} | {Description}");
        }
       
        public void ShowMainMenu()
        {
            throw new NotImplementedException();
        }

    }
    
     public enum TransactionType
        {
            Income,
            Expense
        }
}