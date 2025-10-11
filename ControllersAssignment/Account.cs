namespace ControllersAssignment
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public int CurrentBalance { get; set; }

        public Account(int AccountNumber, string AccountHolderName, int currentBalance)
        {
            this.AccountNumber = AccountNumber;
            this.AccountHolderName = AccountHolderName;
            this.CurrentBalance = currentBalance;
        }
    }
}
