using System.Collections.Generic;
using TTHandiCrafts.Models.Models.UserModels.Advertisings;

namespace TTHandiCrafts.Models.Models.UserModels
{
    /// <summary>
    /// Администратор
    /// </summary>
    public class Admin : User
    {
        /// <summary>
        /// Реклама
        /// </summary>
        public virtual ICollection<Advertising> Advertisings { get; set; }   
        
    }
}