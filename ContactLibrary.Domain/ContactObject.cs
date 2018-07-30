using System;

namespace ContactLibrary.Domain
{
    public class ContactObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Int64 ID { get; set; }

        public bool IsActive { get; set; }
    }
}