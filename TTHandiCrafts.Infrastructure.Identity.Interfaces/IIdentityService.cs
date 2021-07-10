using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TTHandiCrafts.Infrastructure.Identity.Interfaces.Dtos;

namespace TTHandiCrafts.Infrastructure.Identity.Interfaces
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
        Task<string> CreateUserAsync(string userName, string email, bool needChangePassword = true, CancellationToken cancellationToken = default);
        /// <summary>
        /// Автоматическое создание администратора
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="needChangePassword"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> CreateAdminAsync(string userName, string email, string password, bool needChangePassword = false, CancellationToken cancellationToken = default);
        /// <summary>
        /// Блокировка работник
        /// </summary>
        /// <param name="userId">Id работника</param>
        /// <param name="until">Дата конца блокировки</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task LockUserAsync(string userId, DateTimeOffset? until, CancellationToken cancellationToken = default);
        /// <summary>
        /// Разблокировка работника
        /// </summary>
        /// <param name="userId">Id работника</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UnlockUserAsync(string userId, CancellationToken cancellationToken = default);
        /// <summary>
        /// Добавить пользователя к роли
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task AddToRoleAsync(string userId, string roleName);
        /// <summary>
        /// Добавление утверждение для работника
        /// </summary>
        /// <param name="userId">Id работника</param>
        /// <param name="claimType">Тип утверждения</param>
        /// <param name="claimValue">Значение утверждение</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddToClaimAsync(string userId, string claimType, string claimValue, CancellationToken cancellationToken = default);
        /// <summary>
        /// Обновление утверждение для работника
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="claimType"></param>
        /// <param name="claimValue"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task UpdateClaimAsync(string userId, string claimType, string claimValue, CancellationToken cancellation = default);
        /// <summary>
        /// Установить email для пользователя
        /// </summary>
        /// <param name="userId">Id работника</param>
        /// <param name="email">Новый Email</param>
        /// <returns></returns>
        Task SetConfirmedEmailAsync(string userId, string email);
        /// <summary>
        /// Добавление утверждение для работника
        /// </summary>
        /// <param name="userId">Id работника</param>
        /// <param name="claimType">Тип утверждения</param>
        /// <param name="claimValue">Значение утверждение</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddToClaimAsync(string userId, string claimType, int claimValue, CancellationToken cancellationToken = default);
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
        Task RemoveUser(string userId);

        Task RemoveFromRoleAsync(string userId);

        Task<RoleDto> GetUserRoleAsync(string userId);

        Task<bool> GetUsersInRoleAsync(string roleId);

        Task<IList<Claim>> GetAllUserClaims(string userId);
    }
}
