using InventoryManagementSystem.Entities;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repo;

public class AccountRepo : IAccountRepo
{
    private readonly InventoryDbContext _context;

    public AccountRepo(InventoryDbContext context)
    {
        _context = context;
    }

    public UserModel GetUserByUsername(string username)
    {
        var user = _context.Users.FirstOrDefault(x => x.Username == username);

        if (user == null) return null;

        return new UserModel
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password
        };
    }

    public void Register(UserModel user)
    {
        var entity = new User
        {
            Username = user.Username,
            Email = user.Email,
            Password = user.Password
        };

        _context.Users.Add(entity);
        _context.SaveChanges();
    }
}