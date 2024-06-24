namespace proj011.v1
{
    internal class AccountManagerV3 : BaseAccountManager
    {
        private static readonly Mutex mutex = new Mutex();

        public AccountManagerV3(Account AccountFrom, Account AccountTo, double AmountTransfer) : base(AccountFrom, AccountTo, AmountTransfer)
        {
        }

        public override void FundTransfer()
        {
            object _lock1, _lock2;
            if (FromAccount.ID < ToAccount.ID)
            {
                _lock1 = FromAccount;
                _lock2 = ToAccount;
            }
            else
            {
                _lock1 = ToAccount;
                _lock2 = FromAccount;
            }
            Console.WriteLine($"{Thread.CurrentThread.Name} trying to acquire lock on {((Account)_lock1).ID}");

            lock (_lock1)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} acquired lock on {((Account)_lock1).ID}");
                Console.WriteLine($"{Thread.CurrentThread.Name} Doing Some work");
                Thread.Sleep(3000);
                Console.WriteLine($"{Thread.CurrentThread.Name} trying to acquire lock on {((Account)_lock2).ID}");
                lock (_lock2)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name} acquired lock on {((Account)_lock2).ID}");
                    FromAccount.WithdrawMoney(TransferAmount);
                    ToAccount.DepositMoney(TransferAmount);
                }
            }
        }
    }
}
