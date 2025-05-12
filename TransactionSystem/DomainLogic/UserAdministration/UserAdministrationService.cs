using TransactionSystem.Entityes;

namespace TransactionSystem.DomainLogic.UserAdministration
{
    public class UserAdministrationService : IUserAdministrationRepo
    {
        private readonly List<User> _users = new();
        private readonly object _usersLock = new();

        public Task AddUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.Run(() =>
            {
                lock (_usersLock)
                {
                    _users.Add(user);
                }
            });
        }
    }
}
