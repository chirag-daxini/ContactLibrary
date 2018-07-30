namespace ContactLibrary.Data.Common
{
    public interface IUnitOfWork
    {
        int Commit();
    }
}
