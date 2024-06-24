namespace proj011.v1
{
    public class AccountManagerV1 : BaseAccountManager
    {
        public AccountManagerV1(Account AccountFrom, Account AccountTo, double AmountTransfer) : base(AccountFrom, AccountTo, AmountTransfer)
        {
        }

        public override void FundTransfer()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} trying to acquire lock on {FromAccount.ID}");
            lock (FromAccount)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} acquired lock on {FromAccount.ID}");
                Console.WriteLine($"{Thread.CurrentThread.Name} Doing Some work");
                Thread.Sleep(1000);
                Console.WriteLine($"{Thread.CurrentThread.Name} trying to acquire lock on {ToAccount.ID}");
                lock (ToAccount)
                {
                    FromAccount.WithdrawMoney(TransferAmount);
                    ToAccount.DepositMoney(TransferAmount);
                }
            }
        }
    }
}
