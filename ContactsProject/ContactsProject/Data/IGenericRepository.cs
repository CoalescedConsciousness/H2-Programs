namespace ContactsProject.Data
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetByID(object id);
        void Create(T entity);
        void Update(T entity);
        void Delete(object entity);
        void Save();
    }
}
