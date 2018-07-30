using ContactLibrary.Core;
using System.Data.Entity.ModelConfiguration;
namespace ContactLibrary.Data.Mappings
{
    public class ContactEntityMapping : EntityTypeConfiguration<ContactEntity>
    {
        public ContactEntityMapping()
        {
            //key
            HasKey(t => t.ID);
            //properties
            Property(t => t.FirstName).IsRequired().HasMaxLength(50);
            Property(t => t.Email).IsRequired().HasMaxLength(50);
            Property(t => t.LastName).IsRequired().HasMaxLength(50);
            Property(t => t.PhoneNumber).IsRequired().HasMaxLength(20);
            //table
            ToTable("Contacts");
        }
    }
}
