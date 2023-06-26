using DataGenerator_Core.Entites;
using Microsoft.EntityFrameworkCore;

namespace DataGenerator_Core.Services
{
    public sealed class TemplateService
    {
        private readonly DatabaseContext context;
        private readonly TypeService service;

        public TemplateService(DatabaseContext context, TypeService service)
        {
            this.context = context;
            this.service = service;
        }

        /// <summary>
        /// Получает все шаблоны
        /// </summary>
        /// <returns>Все шаблоны</returns>
        public IEnumerable<Template> GetAll() => context.Templates.ToList();

        /// <summary>
        /// Получает один шаблон
        /// </summary>
        /// <param name="data">Данные(имя) шаблона</param>
        /// <returns>Один шаблон</returns>
        public Template? GetOne(string data) => context.Templates.FirstOrDefault(t => t.Data == data);

        /// <summary>
        /// Получает один случайный шаблон по имени типа
        /// </summary>
        /// <param name="typeName">Имя типа</param>
        /// <returns>Один случайный шаблон по имени типа</returns>
        /// <exception cref="ArgumentNullException">Если тип не найден</exception>
        public Template? GetOneByType(string typeName)
        {   
            TypeService service = new TypeService(context);

            Entites.Type type = service.GetOne(typeName);

            if (type == null)
                throw new ArgumentNullException($"Тип с именем {typeName} не найден");

            List<Template> templates = (from Template in context.Templates.Include(t => t.Type)
                                    where Template.Type.Name == type.Name
                                    select Template).ToList();

            return templates[new Random().Next(templates.Count)];
        }

        /// <summary>
        /// Создает новый шаблон
        /// </summary>
        /// <param name="dataName">Данные(имя) шаблона</param>
        /// <param name="typeID">Идентификатор типа</param>
        /// <returns>Новый шаблон</returns>
        public Template? Create(string data, int typeID)
        {
            if (GetOne(data) != null)
                throw new ArgumentException($"Данный шаблон уже существует");

            Template template = new Template() { Data = data, TypeID = typeID };

            context.Templates.Add(template);
            context.SaveChanges();

            return template;
        }

        /// <summary>
        /// Изменяет данные(имя) шаблона
        /// </summary>
        /// <param name="currentData">Текущие данные(имя) шаблона</param>
        /// <param name="newData">Новые данные(имя) шаблона</param>
        /// <returns>Измененный шаблон</returns>
        public Template? EditData(string currentData, string newData)
        { 
            Template? template = GetOne(currentData);

            if (template == null)
                throw new ArgumentException($"Шаблон с данными {currentData} не найден");

            template.Data = newData;
            context.SaveChanges();

            return template;
        }

        /// <summary>
        /// Изменяет тип шаблона
        /// </summary>
        /// <param name="currentData">Текущие данные(имя) шаблона</param>
        /// <param name="typeName">Имя нового типа</param>
        /// <returns>Шаблон с новым типом</returns>
        public Template? EditType(string data, string typeName)
        {
            Template? template = GetOne(data);
            Entites.Type type = service.GetOne(typeName);

            if (template == null)
                throw new ArgumentException($"Шаблон с данными {data} не найден");

            if (type == null)
                throw new ArgumentException($"Тип с именем {typeName} не найден");

            template.Type = type;
            context.SaveChanges();

            return template;
        }

        /// <summary>
        /// Удаляет шаблон
        /// </summary>
        /// <param name="data">Данные(имя) шаблона</param>
        /// <returns>Удаленный шаблон</returns>
        public Template? Delete(string data)
        {
            Template? template = GetOne(data);

            if (template == null)
                throw new ArgumentException($"Шаблон с данными {data} не найден");

            context.Remove(template);
            context.SaveChanges();

            return template;
        }
    }
}