namespace DataGenerator_Core.Services
{
    public sealed class TypeService
    {
        private DatabaseContext _context;

        public TypeService(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all types
        /// </summary>
        /// <returns>All types</returns>
        public IEnumerable<Entites.Type> GetAll() => _context.Types.ToList();

        /// <summary>
        /// Get one type
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Entites.Type? GetOne(string name) => _context.Types.FirstOrDefault(t => t.Name == name);
    }
}
