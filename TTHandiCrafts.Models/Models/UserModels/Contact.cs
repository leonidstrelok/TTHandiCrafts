using TTHandiCrafts.Models.Models.BaseModels;

namespace TTHandiCrafts.Models.Models.UserModels
{
    /// <summary>
    /// Контактные данные
    /// </summary>
    public class Contact : BaseEntity
    {
        /// <summary>
        /// Телефонный номер
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Телеграм
        /// </summary>
        public string Telegram { get; set; }
        /// <summary>
        /// Инстаграм
        /// </summary>
        public string Instagram { get; set; }
    }
}