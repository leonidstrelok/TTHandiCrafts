using System.Threading;
using System.Threading.Tasks;

namespace TTHandiCrafts.Infrastructure.Interfaces.Interfaces
{
    public interface IIdentityService
    {
        /// <summary>
        /// Создает нового работника
        /// </summary>
        /// <param name="userName">Имя работника</param>
        /// <param name="email">Почта работника</param>
        /// <param name="password">Пароль работника</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> CreateUserAsync(string userName, string password, bool needChangePassword = true,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Изменение пароля для работника
        /// </summary>
        /// <param name="userId">Id работника</param>
        /// <param name="newPassword">Новый пароль </param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ChangeUserPassword(string userId, string newPassword, CancellationToken cancellationToken = default);

        /// <summary>
        /// Сменить логин пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="userName">Новый логин</param>
        /// <returns></returns>
        Task UpdateUsernameAsync(string userId, string userName);

        /// <summary>
        /// Добавление утверждение для работника
        /// </summary>
        /// <param name="userId">Id работника</param>
        /// <param name="claimType">Тип утверждения</param>
        /// <param name="claimValue">Значение утверждение</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddToClaimAsync(string userId, string claimType, string claimValue,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Добавление роли к пользователю
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddToRoleUser(string userId, string role, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получение логина пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<string> GetUserName(string userId);

        /// <summary>
        /// Удаление пользователя 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task RemoveUser(string userId);
    }
}