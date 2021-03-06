using System;
using System.Collections.Generic;
using TTHandiCrafts.Models.Models.BaseModels;
using TTHandiCrafts.Models.Models.Products;
using TTHandiCrafts.Models.Models.SharedEnums;

namespace TTHandiCrafts.Models.Models.UserModels
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
        /// Дата создания
        /// </summary>
        public DateTime CreatedDate { get; set; }

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
        /// Телефонный номер
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }

        public int BasketId { get; set; }

        /// <summary>
        /// Корзина
        /// </summary>
        public virtual Basket Basket { get; set; }
    }
}