using ContactLibrary.Core;
using System.Data.Entity.ModelConfiguration;
namespace ContactLibrary.Data.Mappings
{
    class ContactEntityMapping : EntityTypeConfiguration<ContactEntity>
    {
        public ContactEntityMapping()
        {
            //key
            HasKey(t => t.ID);
            //properties
            Property(t => t.FirstName).IsRequired();
            Property(t => t.Email).IsRequired();
            Property(t => t.LastName).IsRequired();
            Property(t => t.PhoneNumber).IsRequired();
            //table
            ToTable("Contacts");
        }
    }
}
