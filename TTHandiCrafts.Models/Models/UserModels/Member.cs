using System.Collections.Generic;
using TTHandiCrafts.Models.Models.BaseModels;

namespace TTHandiCrafts.Models.Models.UserModels
{
    /// <summary>
    /// Участник
    /// </summary>
    public class Member : User
    {
        /// <summary>
        /// Пользовательские изделия
        /// </summary>
        public virtual ICollection<UserWork> UserWorks { get; set; }
    }
}