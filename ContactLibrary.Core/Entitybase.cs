using System;
namespace ContactLibrary.Core
{
    public abstract class Entitybase
    {
        public Int64 ID { get; set; }

        public bool IsActive { get; set; }
    }
}
