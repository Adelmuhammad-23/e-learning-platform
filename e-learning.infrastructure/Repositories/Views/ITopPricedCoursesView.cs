using e_learning.Data.Entities.Views;

namespace e_learning.infrastructure.Repositories.Views
{
    public interface ITopPricedCoursesView<T>
    {
        public IQueryable<TopPricedCourses> GetTableNoTracking();

    }
}
