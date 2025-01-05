using InventorySystem.Shared.DTOs.Core;
using InventorySystem.Shared.Entities.Core;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class PersonMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<PersonMapper> _logger = loggerFactory.CreateLogger<PersonMapper>();

        public PersonDataModel MapEntityToDataModel(Person source)
        {
            if (source == null)
                return new();

            PersonDataModel target = new();

            // Map navigation properties
            if (source.User != null)
            {
                UserMapper userMapper = new(_httpContextDataProvider, _loggerFactory);
                target.User = userMapper.MapEntityToDataModel(source.User);
            }

            // Map regular properties
            target.Name = source.Name;
            target.Reference = source.Reference;
            target.Type = source.Type;
            target.Cost = source.Cost;
            target.UserId = source.UserId;

            target.FormatDates(_httpContextDataProvider.GetTimeZone());
            return target;
        }

        public Person? MapDataFormToEntity(PersonFormModel form)
        {
            if (form == null)
                return null;

            Person target = new()
            {
                Name = form.Name,
                Reference = form.Reference,
                Type = form.Type,
                Cost = form.Cost,
                UserId = form.UserId,
            };

            if (form.Id.HasValue)
            {
                target.Id = form.Id.Value;
            }

            return target;
        }

        public List<PersonDataModel> MapCollectionToDataModel(List<Person> collection)
        {
            if (collection == null)
                return [];

            return collection.Select(MapEntityToDataModel).ToList();
        }

        public Person? MapUpdateDataFormToEntity(PersonFormModel form, Person current)
        {
            if (form == null || current == null)
                return current;

            current.Name = form.Name;
            current.Reference = form.Reference;
            current.Type = form.Type;
            current.Cost = form.Cost;
            current.UserId = form.UserId;

            return current;
        }
    }
}
