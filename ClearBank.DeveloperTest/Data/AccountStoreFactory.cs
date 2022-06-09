namespace ClearBank.DeveloperTest.Data
{
    public class AccountStoreFactory : IAccountStoreFactory
    {
        public IAccountStore GetAccountStore(string key)
        {
            if (key == "Backup")
            {
                return new BackupAccountDataStore();
            }

            return new AccountDataStore();
        }
    }
}