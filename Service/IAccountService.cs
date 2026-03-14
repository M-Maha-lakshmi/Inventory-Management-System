using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Service
{
    public interface IAccountService
    {
        UserModel GetUserByUsername(string username);

        void Register(UserModel user);
    }
}
