using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repo;

namespace InventoryManagementSystem.Service
{ 
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _accountRepo;

        public AccountService(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public UserModel GetUserByUsername(string username)
        {
            return _accountRepo.GetUserByUsername(username);
        }

        public void Register(UserModel user)
        {
            _accountRepo.Register(user);
        }
    }
}
