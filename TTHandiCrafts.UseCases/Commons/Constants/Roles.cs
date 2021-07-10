using System.Collections.Generic;

namespace TTHandiCrafts.UseCases.Commons.Constants
{
    /// <summary>
    /// Роли
    /// </summary>
    public class Roles
    {
        /// <summary>
        /// Администратор
        /// </summary>
        public const string ADMIN = "Admin";

        /// <summary>
        /// Изготовитель
        /// </summary>
        public const string MANUFACTURER = "MANUFACTURER";
        
        /// <summary>
        /// Оптовые закупщики
        /// </summary>
        public const string BUYERS = "BUYERS";
        
        /// <summary>
        /// Розничные покупатели
        /// </summary>
        public const string RETAIL_BUYERS = "RETAIL_BUYERS";
        
        
        public static IEnumerable<string> GetBaseRoles()
        {
            yield return ADMIN;
            yield return MANUFACTURER;
            yield return BUYERS;
            yield return RETAIL_BUYERS;
        }
    }
}