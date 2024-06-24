namespace proj011.v1
{
    public class AccountManagerV2 : BaseAccountManager
    {
        public AccountManagerV2(Account AccountFrom, Account AccountTo, double AmountTransfer) : base(AccountFrom, AccountTo, AmountTransfer)
        {
        }

        public override void FundTransfer()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} trying to acquire lock on {FromAccount.ID}");

            lock (FromAccount)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} acquired lock on {FromAccount.ID}");
                Console.WriteLine($"{Thread.CurrentThread.Name} Doing Some work");
                Thread.Sleep(3000);

                int timeout = (new Random().Next(3, 10)) * 1000;

                Console.WriteLine($"{Thread.CurrentThread.Name} trying to acquire lock on {ToAccount.ID} in {timeout / 1000}s");

                if (Monitor.TryEnter(ToAccount, timeout))
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name} acquired lock on {ToAccount.ID}");
                    try
                    {
                        FromAccount.WithdrawMoney(TransferAmount);
                        ToAccount.DepositMoney(TransferAmount);
                    }
                    finally
                    {
                        Monitor.Exit(ToAccount);
                    }
                }
                else
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name} Unable to acquire lock on {ToAccount.ID}, So existing.");
                }
            }
        }
    }
}
