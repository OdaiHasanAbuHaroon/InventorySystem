using InventorySystem.Shared.DTOs.Business;
using InventorySystem.Shared.Entities.Business;
using InventorySystem.Shared.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;

namespace InventorySystem.Mappers.Mappers
{
    [Mapper]
    public partial class SupplierMapper(IHttpContextDataProvider httpContextDataProvider, ILoggerFactory loggerFactory)
    {
        private readonly IHttpContextDataProvider _httpContextDataProvider = httpContextDataProvider;
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly ILogger<SupplierMapper> _logger = loggerFactory.CreateLogger<SupplierMapper>();

        public SupplierDataModel MapEntityToDataModel(Supplier source)
        {
            if (source == null)
                return new();

            SupplierDataModel target = new();

            // Map navigation properties
            if (source.Country != null)
            {
                CountryMapper countryMapper = new(_httpContextDataProvider, _loggerFactory);
                target.Country = countryMapper.MapEntityToDataModel(source.Country);
            }

            // Map regular properties
            target.Name = source.Name;
            target.ContactEmail = source.ContactEmail;
            target.ContactName = source.ContactName;
            target.ContactNumber = source.ContactNumber;
            target.Address = source.Address;
            target.Description = source.Description;
            target.CountryId = source.CountryId;

            target.FormatDates(_httpContextDataProvider.GetTimeZone());
            return target;
        }

        public Supplier? MapDataFormToEntity(SupplierFormModel form)
        {
            if (form == null)
                return null;

            Supplier target = new()
            {
                Name = form.Name,
                ContactEmail = form.ContactEmail,
                ContactName = form.ContactName,
                ContactNumber = form.ContactNumber,
                Address = form.Address,
                Description = form.Description,
                CountryId = form.CountryId,
            };

            if (form.Id.HasValue)
            {
                target.Id = form.Id.Value;
            }

            return target;
        }

        public SupplierFormModel MapEntityToDataForm(Supplier source)
        {
            SupplierFormModel target = new()
            {
                Id = source.Id,
                Name = source.Name,
                ContactEmail = source.ContactEmail,
                ContactName = source.ContactName,
                ContactNumber = source.ContactNumber,
                Address = source.Address,
                Description = source.Description,
                CountryId = source.CountryId
            };

            return target;
        }

        public List<SupplierDataModel> MapCollectionToDataModel(List<Supplier> collection)
        {
            if (collection == null)
                return [];

            return collection.Select(MapEntityToDataModel).ToList();
        }

        public Supplier? MapUpdateDataFormToEntity(SupplierFormModel form, Supplier current)
        {
            if (form == null || current == null)
                return current;

            current.Name = form.Name;
            current.ContactEmail = form.ContactEmail;
            current.ContactName = form.ContactName;
            current.ContactNumber = form.ContactNumber;
            current.Address = form.Address;
            current.Description = form.Description;
            current.CountryId = form.CountryId;

            return current;
        }
    }
}
