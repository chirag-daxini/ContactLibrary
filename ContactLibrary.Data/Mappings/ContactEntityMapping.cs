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
            Property(t => t.FirstName).IsRequired().HasMaxLength(20);
            Property(t => t.Email);
            Property(t => t.LastName).IsRequired().HasMaxLength(20);
            Property(t => t.PhoneNumber).IsRequired().HasMaxLength(10);
            //table
            ToTable("Contacts");
        }
    }
}
