using System;
using System.ComponentModel.DataAnnotations;

namespace ContactLibrary.Domain
{
    public class ContactObject
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "First Name must be between 3 characters to 20 characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Last Name must be between 3 characters to 20 characters")]
        public string LastName { get; set; }

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
        [StringLength(254, ErrorMessage = "Maximum length is 254 characters")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Phone number is not valid")]
        public string PhoneNumber { get; set; }

        public Int64 ID { get; set; }

        public bool IsActive { get; set; }
    }
}