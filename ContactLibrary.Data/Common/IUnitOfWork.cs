using ContactLibrary.Data.Repositories;
namespace ContactLibrary.Data.Common
{
    public interface IUnitOfWork
    {
        IContactRepository ContactRepository { get; }
        int Commit();
    }
}
