namespace DataGenerator_Core.Services
{
    public sealed class TypeService
    {
        private DatabaseContext context;

        public TypeService(DatabaseContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Получает все типы
        /// </summary>
        /// <returns>Все типы</returns>
        public IEnumerable<Entites.Type> GetAll() => context.Types.ToList();

        /// <summary>
        /// Получает один тип
        /// </summary>
        /// <param name="name">Имя типа</param>
        /// <returns>Один тип</returns>
        public Entites.Type? GetOne(string name) => context.Types.FirstOrDefault(t => t.Name == name);

        /// <summary>
        /// Создает один тип
        /// </summary>
        /// <param name="name">Имя нового типа</param>
        /// <returns>Новый тип</returns>
        /// <exception cref="ArgumentException">Если тип уже существует</exception>
        public Entites.Type? Create(string name)
        {
            if (GetOne(name) != null)
                throw new ArgumentException($"Тип с именем {name} уже существует");

            Entites.Type type = new () { Name = name };

            context.Add(type);
            context.SaveChanges();

            return type;
        }

        /// <summary>
        /// Редактирует имя типа
        /// </summary>
        /// <param name="currentName">Текущее имя типа</param>
        /// <param name="newName">Новое имя типа</param>
        /// <returns>Измененный тип</returns>
        /// <exception cref="ArgumentException">Если тип не найден</exception>
        public Entites.Type? EditName(string currentName, string newName)
        {
            Entites.Type type = GetOne(currentName);

            if (type == null)
                throw new ArgumentException($"Тип с именем {currentName} не найден");

            type.Name = newName;
            context.SaveChanges();

            return type;
        }

        /// <summary>
        /// Удаляет тип, если нет зависимых шаблонов
        /// </summary>
        /// <param name="name">Имя типа</param>
        /// <returns>Удаленный тип</returns>
        /// <exception cref="ArgumentException">Если тип не найден</exception>
        public Entites.Type? Delete(string name)
        {
            Entites.Type type = GetOne(name);

            if (type == null)
                throw new ArgumentException($"Тип с именем {name} не найден");

            context.Remove(type);
            context.SaveChanges();

            return type;
        }
    }
}
