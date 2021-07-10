namespace TTHandiCrafts.Authorization.Policies
{
    public static class PermisionPolicy
    {
        //Политики для роли администратора

        public const string ManipulateWithDataBookEntries = "Политика для управление записями в справочниках";
        public const string ManipulateWithDataBookEntriesRead = "Политика для чтение записей в справочниках";

        public const string UserRegistration = "Политика для регистрации пользователя";
        public const string UserRegistrationRead = "Политика для чтение зарегистрированных пользователей";

        public const string UserClaimRoleRegistration = "Политика для регистрации роли пользователя";
        public const string UserClaimRoleRegistrationRead = "Политика для чтение роли зарегистрированных пользователей";

        //Политики для регистратора

        public const string RegistrationOfStatement = "Политика для регистрации заявления";
        public const string RegistrationOfStatementRead = "Политика для чтение зарегистрированных заявлении";

        public const string RegistrationOfRight = "Политика для регистрация права";
        public const string RegistrationOfRightRead = "Политика для чтение зарегистрированных прав";

        public const string RightToChange = "Политика для право на изменение";
        public const string RightToChangeRead = "Политика для чтение прав на изменение";

        public const string RightToTechnicalChange = "Политика для право на технические изменения";
        public const string RightToTechnicalChangeRead = "Политика для чтение прав на технические изменения";

        public const string RegistrationOfArrest = "Политика для Регистрация ареста";
        public const string RegistrationOfArrestRead = "Политика для чтение зарегистрированных арестов";

        public const string RegistrationOfBail = "Политика для Регистрация залога";
        public const string RegistrationOfBailRead = "Политика для чтение зарегистрированных залог";

        //Политики  для архиватора

        public const string AcceptanceToArchive = "Политика для Принятие в архив";
        public const string AcceptanceToArchiveRead = "Политика для чтение принятих в архив";
    }
}