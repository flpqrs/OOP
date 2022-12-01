using Library.Entities;
using Newtonsoft.Json;

namespace Library.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        private static readonly string DbFilePathTemplate = @"C:\PhoneDbFolder\db_{0}.json";
        private static string DbFilePath = string.Format(DbFilePathTemplate, typeof(TEntity).Name);
        private List<TEntity> _storage = ReadStorage();

        public TEntity Get(int id)
        {
            var storageEntity = _storage.FirstOrDefault(x => x.Id == id);

            return storageEntity;
        }

        public void Insert(TEntity entity)
        {
            if (entity.Id <= 0)
            {
                entity.Id = (_storage.OrderByDescending(ent => ent.Id).FirstOrDefault()?.Id ?? 0) + 1;
            }

            if (_storage.Any(ent => ent.Id == entity.Id))
                throw new Exception("Duplicated Id.");

            _storage.Add(entity);
            SaveStorage(_storage);

        }
        public IList<TEntity> GetAll()
        {
            return _storage;
        }
        private static List<TEntity> ReadStorage()
        {
            EnshureDbFile();

            var text = File.ReadAllText(DbFilePath);

            var entities = JsonConvert.DeserializeObject<List<TEntity>>(text);

            return entities ?? new List<TEntity>();
        }
        private static void SaveStorage(List<TEntity> entities)
        {
            var text = JsonConvert.SerializeObject(entities);

            File.WriteAllText(DbFilePath, text);
        }

        private static void EnshureDbFile()
        {
            if (!File.Exists(DbFilePath))
            {
                using (File.Create(DbFilePath))
                {

                }
            }
        }
    }
}
