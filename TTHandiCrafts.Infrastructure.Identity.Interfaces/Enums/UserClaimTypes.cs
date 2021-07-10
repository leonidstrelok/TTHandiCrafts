namespace TTHandiCrafts.Infrastructure.Identity.Interfaces.Enums
{
    public enum UserClaimTypes
    {
        //Claim-ы Для роли администратора

        /// <summary>
        /// Справочник, Sprawoçnik
        /// </summary>
        DatabookEntry,
        /// <summary>
        /// Пользователь, Ulanyjy
        /// </summary>
        User,
        /// <summary>
        /// Роль пользователя, Ulanyjynyň Roly
        /// </summary>
        UserRole,
        /// <summary>
        /// Территориалный юнит, Ilatly Nokat
        /// </summary>
        TerritorialUnit,
        /// <summary>
        /// Недвижимость, Gozgalmaýan Emläk
        /// </summary>
        RealEstate,

        //Claim-ы для регистратора

        /// <summary>
        /// Заявления, Arza
        /// </summary>
        Statement,
        /// <summary> 
        /// Бланк, Blanka
        /// </summary>
        Blank,
        /// <summary>
        /// Blankanyň alyş-çalşygy
        /// </summary>
        BlankTransfer,
        /// <summary>
        /// Blanka ýok etmek
        /// </summary>
        BlankDestroy,
        /// <summary>
        /// История, Taryh
        /// </summary>
        History,
        /// <summary>
        /// Глобальный доступ, Doly ygtyýarly
        /// </summary>
        GlobalAccess
    }
}

