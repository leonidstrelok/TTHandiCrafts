using System.Collections.Generic;
using TTHandiCrafts.Models.Models.BaseModels;
using TTHandiCrafts.Models.Models.SharedEnums;

namespace TTHandiCrafts.Models.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип пользователя
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Фотография пользователя
        /// </summary>
        public byte[]? Image { get; set; }

        /// <summary>
        /// Пользовательские изделия
        /// </summary>
        public ICollection<UserWork> UserWorks { get; set; }
        public string IdentityUserId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}