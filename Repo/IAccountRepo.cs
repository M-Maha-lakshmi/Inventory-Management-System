using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repo
{
    public interface IAccountRepo
    {
        UserModel GetUserByUsername(string username);

        void Register(UserModel user);
    }
}
