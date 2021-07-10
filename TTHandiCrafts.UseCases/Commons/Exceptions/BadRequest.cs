using System;

namespace TTHandiCrafts.UseCases.Commons.Exceptions
{
    public class BadRequest : Exception
    {
        public BadRequest() : base("Wagtyň geçmegi bilen, programmanyň ýagdaýy üýtgedilensoň, programma")
        {
        }
    }
}