using System.Linq;


namespace DevBooks.Data
{
    interface IRepository<T> where T : class
    {
        IQueryable GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        void Detach(T entity);
    }
}
