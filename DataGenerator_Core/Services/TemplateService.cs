using DataGenerator_Core.Entites;

namespace DataGenerator_Core.Services
{
    public sealed class TemplateService
    {
        private DatabaseContext _context;

        public TemplateService(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all templates
        /// </summary>
        /// <returns>All templates</returns>
        public IEnumerable<Template> GetAll() => _context.Templates.ToList();

        /// <summary>
        /// Get one template
        /// </summary>
        /// <param name="dataName"></param>
        /// <returns></returns>
        public Template? GetOne(string data) => _context.Templates.FirstOrDefault(t => t.Data == data); 

        /// <summary>
        /// Create new template if not exists
        /// </summary>
        /// <param name="dataName"></param>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public Template? Create(string data, int typeID)
        {
            if (GetOne(data) == null)
            {
                Template template = new Template() { Data = data, TypeID = typeID };

                _context.Templates.Add(template);
                _context.SaveChanges();

                return template;
            }
            else return null;
        }

        /// <summary>
        /// Edit data of the template, if exists
        /// </summary>
        /// <param name="currentData"></param>
        /// <param name="newData"></param>
        /// <returns></returns>
        public Template? EditData(string currentData, string newData)
        {
            Template? template = GetOne(currentData);
            if (template != null)
            {
                template.Data = newData;
                _context.SaveChanges();

                return template;
            }
            else return null;
        }

        /// <summary>
        /// Edit type of the template, if exists
        /// </summary>
        /// <param name="currentData"></param>
        /// <param name="newTypeID"></param>
        /// <returns></returns>
        public Template? EditType(string data, int newTypeID)
        {
            Template? template = GetOne(data);
            if (template != null)
            {
                template.TypeID = newTypeID;
                _context.SaveChanges();

                return template;
            }
            else return null;
        }

        /// <summary>
        /// Delete one template, if exists
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Template? Delete(string data)
        {
            Template? template = GetOne(data);
            if (template != null)
            {
                _context.Templates.Remove(template);
                _context.SaveChanges();

                return template;
            }
            else return null;
        }
    }
}
