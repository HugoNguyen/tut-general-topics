namespace proj011.v1
{
    public abstract class BaseAccountManager
    {
        protected Account FromAccount;
        protected Account ToAccount;
        protected double TransferAmount;
        public BaseAccountManager(Account AccountFrom, Account AccountTo, double AmountTransfer)
        {
            FromAccount = AccountFrom;
            ToAccount = AccountTo;
            TransferAmount = AmountTransfer;
        }

        public abstract void FundTransfer();
    }
}
