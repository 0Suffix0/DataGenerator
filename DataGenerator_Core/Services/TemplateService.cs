using DataGenerator_Core.Entites;
using Microsoft.EntityFrameworkCore;

namespace DataGenerator_Core.Services
{
    public sealed class TemplateService
    {
        private DatabaseContext context;

        public TemplateService(DatabaseContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get all templates
        /// </summary>
        /// <returns>All templates</returns>
        public IEnumerable<Template> GetAll() => context.Templates.ToList();

        /// <summary>
        /// Get one template
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Template? GetOne(string data) => context.Templates.FirstOrDefault(t => t.Data == data);

        /// <summary>
        /// Get one template by type
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Template? GetOneByType(string typeName)
        {   TypeService service = new TypeService(context);

            Entites.Type type = service.GetOne(typeName);

            if (type == null)
                throw new ArgumentNullException($"Type is null");

            List<Template> templates = (from Template in context.Templates.Include(t => t.Type)
                                    where Template.Type.Name == type.Name
                                    select Template).ToList();

            return templates[new Random().Next(templates.Count)];
        }

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

                context.Templates.Add(template);
                context.SaveChanges();

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
                context.SaveChanges();

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
                context.SaveChanges();

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
                context.Templates.Remove(template);
                context.SaveChanges();

                return template;
            }
            else return null;
        }
    }
}