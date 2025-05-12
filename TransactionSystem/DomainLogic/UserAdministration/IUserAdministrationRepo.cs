using TransactionSystem.Entityes;

namespace TransactionSystem.DomainLogic.UserAdministration
{
    public interface IUserAdministrationRepo
    {
        Task AddUserAsync(User newUser);
    }
}