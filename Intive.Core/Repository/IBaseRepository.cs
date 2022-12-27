namespace Intive.Core.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        void Create<T>(T entity);
        List<T> GetAll();
    }
}
