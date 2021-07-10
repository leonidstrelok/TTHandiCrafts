using System.Collections.Generic;
using System.IO;

namespace TTHandiCrafts.UseCases.Dtos
{
    public class VersionFile
    {
        public Stream Stream { get; set; }
        public string FileName { get; set; }
    }
}