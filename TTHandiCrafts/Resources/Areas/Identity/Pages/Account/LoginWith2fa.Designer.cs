﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TTHandiCrafts.Resources.Areas.Identity.Pages.Account {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class LoginWith2fa {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal LoginWith2fa() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TTHandiCrafts.Resources.Areas.Identity.Pages.Account.LoginWith2fa", typeof(LoginWith2fa).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Двухфакторная аутентификация.
        /// </summary>
        public static string _2FA {
            get {
                return ResourceManager.GetString("2FA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Код аутентификатора.
        /// </summary>
        public static string AuthenticatorCode {
            get {
                return ResourceManager.GetString("AuthenticatorCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to У вас нет доступа к устройству аутентификации? Вы можете.
        /// </summary>
        public static string DontHaveAccessAuthenticator {
            get {
                return ResourceManager.GetString("DontHaveAccessAuthenticator", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Войти.
        /// </summary>
        public static string Log_in {
            get {
                return ResourceManager.GetString("Log in", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to войдите в систему с кодом восстановления.
        /// </summary>
        public static string LogInWithRecoveryCode {
            get {
                return ResourceManager.GetString("LogInWithRecoveryCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ваш логин защищен с помощью приложения Аутентификатора. Введите код аутентификации ниже.
        /// </summary>
        public static string ProtectedAuthenticatopApp {
            get {
                return ResourceManager.GetString("ProtectedAuthenticatopApp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Запомнить это устройство.
        /// </summary>
        public static string Remember_this_machine {
            get {
                return ResourceManager.GetString("Remember this machine", resourceCulture);
            }
        }
    }
}
