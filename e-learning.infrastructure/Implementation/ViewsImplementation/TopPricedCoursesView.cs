using e_learning.Data.Entities.Views;
using e_learning.infrastructure.Context;
using e_learning.infrastructure.Repositories.Views;

namespace e_learning.infrastructure.Implementation.ViewsImplementation
{
    public class TopPricedCoursesView : ITopPricedCoursesView<TopPricedCourses>
    {

        #region Fields
        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors
        public TopPricedCoursesView(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        public IQueryable<TopPricedCourses> GetTableNoTracking()
        {
            return _context.TopPricedCourses.AsQueryable();
        }
    }
}
