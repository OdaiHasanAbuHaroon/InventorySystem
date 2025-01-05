using InventorySystem.Shared.Entities.Core;

namespace InventorySystem.Shared.Entities.Enumerations.SeedEnumeration
{
    /// <summary>
    /// Provides enumerations and methods related to TimeZoneInformation entities.
    /// </summary>
    public static class TimeZoneInformationEnum
    {
        public readonly static TimeZoneInformation Dateline_Standard_Time = new()
        {
            Id = 1,
            Value = "Dateline Standard Time",
            DisplayName = "(UTC-12:00) International Date Line West",
            StandardName = "Dateline Standard Time",
            DaylightName = "Dateline Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-12:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = -12,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation UTC_11 = new()
        {
            Id = 2,
            Value = "UTC-11",
            DisplayName = "(UTC-11:00) Coordinated Universal Time-11",
            StandardName = "UTC-11",
            DaylightName = "UTC-11",
            BaseUtcOffset = TimeSpan.Parse("-11:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = -11,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Aleutian_Standard_Time = new()
        {
            Id = 3,
            Value = "Aleutian Standard Time",
            DisplayName = "(UTC-10:00) Aleutian Islands",
            StandardName = "Aleutian Standard Time",
            DaylightName = "Aleutian Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-10:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -10,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Hawaiian_Standard_Time = new()
        {
            Id = 4,
            Value = "Hawaiian Standard Time",
            DisplayName = "(UTC-10:00) Hawaii",
            StandardName = "Hawaiian Standard Time",
            DaylightName = "Hawaiian Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-10:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = -10,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Marquesas_Standard_Time = new()
        {
            Id = 5,
            Value = "Marquesas Standard Time",
            DisplayName = "(UTC-09:30) Marquesas Islands",
            StandardName = "Marquesas Standard Time",
            DaylightName = "Marquesas Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-09:30:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = -9,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Alaskan_Standard_Time = new()
        {
            Id = 6,
            Value = "Alaskan Standard Time",
            DisplayName = "(UTC-09:00) Alaska",
            StandardName = "Alaskan Standard Time",
            DaylightName = "Alaskan Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-09:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -9,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation UTC_09 = new()
        {
            Id = 7,
            Value = "UTC-09",
            DisplayName = "(UTC-09:00) Coordinated Universal Time-09",
            StandardName = "UTC-09",
            DaylightName = "UTC-09",
            BaseUtcOffset = TimeSpan.Parse("-09:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = -9,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Pacific_Standard_Time_Mexico = new()
        {
            Id = 8,
            Value = "Pacific Standard Time (Mexico)",
            DisplayName = "(UTC-08:00) Baja California",
            StandardName = "Pacific Standard Time (Mexico)",
            DaylightName = "Pacific Daylight Time (Mexico)",
            BaseUtcOffset = TimeSpan.Parse("-08:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -8,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation UTC_08 = new()
        {
            Id = 9,
            Value = "UTC-08",
            DisplayName = "(UTC-08:00) Coordinated Universal Time-08",
            StandardName = "UTC-08",
            DaylightName = "UTC-08",
            BaseUtcOffset = TimeSpan.Parse("-08:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = -8,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Pacific_Standard_Time = new()
        {
            Id = 10,
            Value = "Pacific Standard Time",
            DisplayName = "(UTC-08:00) Pacific Time (US & Canada)",
            StandardName = "Pacific Standard Time",
            DaylightName = "Pacific Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-08:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -8,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation US_Mountain_Standard_Time = new()
        {
            Id = 11,
            Value = "US Mountain Standard Time",
            DisplayName = "(UTC-07:00) Arizona",
            StandardName = "US Mountain Standard Time",
            DaylightName = "US Mountain Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-07:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = -7,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Mountain_Standard_Time_Mexico = new()
        {
            Id = 12,
            Value = "Mountain Standard Time (Mexico)",
            DisplayName = "(UTC-07:00) La Paz, Mazatlan",
            StandardName = "Mountain Standard Time (Mexico)",
            DaylightName = "Mountain Daylight Time (Mexico)",
            BaseUtcOffset = TimeSpan.Parse("-07:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -7,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Mountain_Standard_Time = new()
        {
            Id = 13,
            Value = "Mountain Standard Time",
            DisplayName = "(UTC-07:00) Mountain Time (US & Canada)",
            StandardName = "Mountain Standard Time",
            DaylightName = "Mountain Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-07:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -7,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Yukon_Standard_Time = new()
        {
            Id = 14,
            Value = "Yukon Standard Time",
            DisplayName = "(UTC-07:00) Yukon",
            StandardName = "Yukon Standard Time",
            DaylightName = "Yukon Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-07:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -7,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Central_America_Standard_Time = new()
        {
            Id = 15,
            Value = "Central America Standard Time",
            DisplayName = "(UTC-06:00) Central America",
            StandardName = "Central America Standard Time",
            DaylightName = "Central America Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-06:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = -6,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Central_Standard_Time = new()
        {
            Id = 16,
            Value = "Central Standard Time",
            DisplayName = "(UTC-06:00) Central Time (US & Canada)",
            StandardName = "Central Standard Time",
            DaylightName = "Central Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-06:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -6,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Easter_Island_Standard_Time = new()
        {
            Id = 17,
            Value = "Easter Island Standard Time",
            DisplayName = "(UTC-06:00) Easter Island",
            StandardName = "Easter Island Standard Time",
            DaylightName = "Easter Island Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-06:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -6,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Central_Standard_Time_Mexico = new()
        {
            Id = 18,
            Value = "Central Standard Time (Mexico)",
            DisplayName = "(UTC-06:00) Guadalajara, Mexico City, Monterrey",
            StandardName = "Central Standard Time (Mexico)",
            DaylightName = "Central Daylight Time (Mexico)",
            BaseUtcOffset = TimeSpan.Parse("-06:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -6,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Canada_Central_Standard_Time = new()
        {
            Id = 19,
            Value = "Canada Central Standard Time",
            DisplayName = "(UTC-06:00) Saskatchewan",
            StandardName = "Canada Central Standard Time",
            DaylightName = "Canada Central Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-06:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = -6,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation SA_Pacific_Standard_Time = new()
        {
            Id = 20,
            Value = "SA Pacific Standard Time",
            DisplayName = "(UTC-05:00) Bogota, Lima, Quito, Rio Branco",
            StandardName = "SA Pacific Standard Time",
            DaylightName = "SA Pacific Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-05:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = -5,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Eastern_Standard_Time_Mexico = new()
        {
            Id = 21,
            Value = "Eastern Standard Time (Mexico)",
            DisplayName = "(UTC-05:00) Chetumal",
            StandardName = "Eastern Standard Time (Mexico)",
            DaylightName = "Eastern Daylight Time (Mexico)",
            BaseUtcOffset = TimeSpan.Parse("-05:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -5,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Eastern_Standard_Time = new()
        {
            Id = 22,
            Value = "Eastern Standard Time",
            DisplayName = "(UTC-05:00) Eastern Time (US & Canada)",
            StandardName = "Eastern Standard Time",
            DaylightName = "Eastern Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-05:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -5,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Haiti_Standard_Time = new()
        {
            Id = 23,
            Value = "Haiti Standard Time",
            DisplayName = "(UTC-05:00) Haiti",
            StandardName = "Haiti Standard Time",
            DaylightName = "Haiti Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-05:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -5,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Cuba_Standard_Time = new()
        {
            Id = 24,
            Value = "Cuba Standard Time",
            DisplayName = "(UTC-05:00) Havana",
            StandardName = "Cuba Standard Time",
            DaylightName = "Cuba Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-05:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -5,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation US_Eastern_Standard_Time = new()
        {
            Id = 25,
            Value = "US Eastern Standard Time",
            DisplayName = "(UTC-05:00) Indiana (East)",
            StandardName = "US Eastern Standard Time",
            DaylightName = "US Eastern Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-05:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -5,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Turks_And_Caicos_Standard_Time = new()
        {
            Id = 26,
            Value = "Turks And Caicos Standard Time",
            DisplayName = "(UTC-05:00) Turks and Caicos",
            StandardName = "Turks and Caicos Standard Time",
            DaylightName = "Turks and Caicos Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-05:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -5,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Paraguay_Standard_Time = new()
        {
            Id = 27,
            Value = "Paraguay Standard Time",
            DisplayName = "(UTC-04:00) Asuncion",
            StandardName = "Paraguay Standard Time",
            DaylightName = "Paraguay Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-04:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -4,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Atlantic_Standard_Time = new()
        {
            Id = 28,
            Value = "Atlantic Standard Time",
            DisplayName = "(UTC-04:00) Atlantic Time (Canada)",
            StandardName = "Atlantic Standard Time",
            DaylightName = "Atlantic Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-04:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -4,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Venezuela_Standard_Time = new()
        {
            Id = 29,
            Value = "Venezuela Standard Time",
            DisplayName = "(UTC-04:00) Caracas",
            StandardName = "Venezuela Standard Time",
            DaylightName = "Venezuela Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-04:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -4,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Central_Brazilian_Standard_Time = new()
        {
            Id = 30,
            Value = "Central Brazilian Standard Time",
            DisplayName = "(UTC-04:00) Cuiaba",
            StandardName = "Central Brazilian Standard Time",
            DaylightName = "Central Brazilian Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-04:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -4,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation SA_Western_Standard_Time = new()
        {
            Id = 31,
            Value = "SA Western Standard Time",
            DisplayName = "(UTC-04:00) Georgetown, La Paz, Manaus, San Juan",
            StandardName = "SA Western Standard Time",
            DaylightName = "SA Western Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-04:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = -4,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Pacific_SA_Standard_Time = new()
        {
            Id = 32,
            Value = "Pacific SA Standard Time",
            DisplayName = "(UTC-04:00) Santiago",
            StandardName = "Pacific SA Standard Time",
            DaylightName = "Pacific SA Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-04:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -4,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Newfoundland_Standard_Time = new()
        {
            Id = 33,
            Value = "Newfoundland Standard Time",
            DisplayName = "(UTC-03:30) Newfoundland",
            StandardName = "Newfoundland Standard Time",
            DaylightName = "Newfoundland Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-03:30:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -3,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Tocantins_Standard_Time = new()
        {
            Id = 34,
            Value = "Tocantins Standard Time",
            DisplayName = "(UTC-03:00) Araguaina",
            StandardName = "Tocantins Standard Time",
            DaylightName = "Tocantins Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-03:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -3,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation E_South_America_Standard_Time = new()
        {
            Id = 35,
            Value = "E. South America Standard Time",
            DisplayName = "(UTC-03:00) Brasilia",
            StandardName = "E. South America Standard Time",
            DaylightName = "E. South America Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-03:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -3,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation SA_Eastern_Standard_Time = new()
        {
            Id = 36,
            Value = "SA Eastern Standard Time",
            DisplayName = "(UTC-03:00) Cayenne, Fortaleza",
            StandardName = "SA Eastern Standard Time",
            DaylightName = "SA Eastern Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-03:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = -3,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Argentina_Standard_Time = new()
        {
            Id = 37,
            Value = "Argentina Standard Time",
            DisplayName = "(UTC-03:00) City of Buenos Aires",
            StandardName = "Argentina Standard Time",
            DaylightName = "Argentina Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-03:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -3,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Montevideo_Standard_Time = new()
        {
            Id = 38,
            Value = "Montevideo Standard Time",
            DisplayName = "(UTC-03:00) Montevideo",
            StandardName = "Montevideo Standard Time",
            DaylightName = "Montevideo Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-03:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -3,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Magallanes_Standard_Time = new()
        {
            Id = 39,
            Value = "Magallanes Standard Time",
            DisplayName = "(UTC-03:00) Punta Arenas",
            StandardName = "Magallanes Standard Time",
            DaylightName = "Magallanes Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-03:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -3,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Saint_Pierre_Standard_Time = new()
        {
            Id = 40,
            Value = "Saint Pierre Standard Time",
            DisplayName = "(UTC-03:00) Saint Pierre and Miquelon",
            StandardName = "Saint Pierre Standard Time",
            DaylightName = "Saint Pierre Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-03:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -3,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Bahia_Standard_Time = new()
        {
            Id = 41,
            Value = "Bahia Standard Time",
            DisplayName = "(UTC-03:00) Salvador",
            StandardName = "Bahia Standard Time",
            DaylightName = "Bahia Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-03:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -3,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation UTC_02 = new()
        {
            Id = 42,
            Value = "UTC-02",
            DisplayName = "(UTC-02:00) Coordinated Universal Time-02",
            StandardName = "UTC-02",
            DaylightName = "UTC-02",
            BaseUtcOffset = TimeSpan.Parse("-02:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = -2,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Greenland_Standard_Time = new()
        {
            Id = 43,
            Value = "Greenland Standard Time",
            DisplayName = "(UTC-02:00) Greenland",
            StandardName = "Greenland Standard Time",
            DaylightName = "Greenland Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-02:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -2,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Mid_Atlantic_Standard_Time = new()
        {
            Id = 44,
            Value = "Mid-Atlantic Standard Time",
            DisplayName = "(UTC-02:00) Mid-Atlantic - Old",
            StandardName = "Mid-Atlantic Standard Time",
            DaylightName = "Mid-Atlantic Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-02:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -2,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Azores_Standard_Time = new()
        {
            Id = 45,
            Value = "Azores Standard Time",
            DisplayName = "(UTC-01:00) Azores",
            StandardName = "Azores Standard Time",
            DaylightName = "Azores Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-01:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = -1,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Cape_Verde_Standard_Time = new()
        {
            Id = 46,
            Value = "Cape Verde Standard Time",
            DisplayName = "(UTC-01:00) Cabo Verde Is.",
            StandardName = "Cabo Verde Standard Time",
            DaylightName = "Cabo Verde Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("-01:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = -1,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation UTC = new()
        {
            Id = 47,
            Value = "UTC",
            DisplayName = "(UTC) Coordinated Universal Time",
            StandardName = "Coordinated Universal Time",
            DaylightName = "Coordinated Universal Time",
            BaseUtcOffset = TimeSpan.Parse("00:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 0,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation GMT_Standard_Time = new()
        {
            Id = 48,
            Value = "GMT Standard Time",
            DisplayName = "(UTC+00:00) Dublin, Edinburgh, Lisbon, London",
            StandardName = "GMT Standard Time",
            DaylightName = "GMT Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("00:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 0,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Greenwich_Standard_Time = new()
        {
            Id = 49,
            Value = "Greenwich Standard Time",
            DisplayName = "(UTC+00:00) Monrovia, Reykjavik",
            StandardName = "Greenwich Standard Time",
            DaylightName = "Greenwich Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("00:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 0,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Sao_Tome_Standard_Time = new()
        {
            Id = 50,
            Value = "Sao Tome Standard Time",
            DisplayName = "(UTC+00:00) Sao Tome",
            StandardName = "Sao Tome Standard Time",
            DaylightName = "Sao Tome Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("00:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 0,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Morocco_Standard_Time = new()
        {
            Id = 51,
            Value = "Morocco Standard Time",
            DisplayName = "(UTC+01:00) Casablanca",
            StandardName = "Morocco Standard Time",
            DaylightName = "Morocco Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("00:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 0,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation W_Europe_Standard_Time = new()
        {
            Id = 52,
            Value = "W. Europe Standard Time",
            DisplayName = "(UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna",
            StandardName = "W. Europe Standard Time",
            DaylightName = "W. Europe Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("01:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 1,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Central_Europe_Standard_Time = new()
        {
            Id = 53,
            Value = "Central Europe Standard Time",
            DisplayName = "(UTC+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague",
            StandardName = "Central Europe Standard Time",
            DaylightName = "Central Europe Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("01:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 1,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Romance_Standard_Time = new()
        {
            Id = 54,
            Value = "Romance Standard Time",
            DisplayName = "(UTC+01:00) Brussels, Copenhagen, Madrid, Paris",
            StandardName = "Romance Standard Time",
            DaylightName = "Romance Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("01:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 1,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Central_European_Standard_Time = new()
        {
            Id = 55,
            Value = "Central European Standard Time",
            DisplayName = "(UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb",
            StandardName = "Central European Standard Time",
            DaylightName = "Central European Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("01:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 1,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation W_Central_Africa_Standard_Time = new()
        {
            Id = 56,
            Value = "W. Central Africa Standard Time",
            DisplayName = "(UTC+01:00) West Central Africa",
            StandardName = "W. Central Africa Standard Time",
            DaylightName = "W. Central Africa Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("01:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 1,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation GTB_Standard_Time = new()
        {
            Id = 57,
            Value = "GTB Standard Time",
            DisplayName = "(UTC+02:00) Athens, Bucharest",
            StandardName = "GTB Standard Time",
            DaylightName = "GTB Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("02:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 2,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Middle_East_Standard_Time = new()
        {
            Id = 58,
            Value = "Middle East Standard Time",
            DisplayName = "(UTC+02:00) Beirut",
            StandardName = "Middle East Standard Time",
            DaylightName = "Middle East Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("02:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 2,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Egypt_Standard_Time = new()
        {
            Id = 59,
            Value = "Egypt Standard Time",
            DisplayName = "(UTC+02:00) Cairo",
            StandardName = "Egypt Standard Time",
            DaylightName = "Egypt Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("02:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 2,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation E_Europe_Standard_Time = new()
        {
            Id = 60,
            Value = "E. Europe Standard Time",
            DisplayName = "(UTC+02:00) Chisinau",
            StandardName = "E. Europe Standard Time",
            DaylightName = "E. Europe Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("02:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 2,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation West_Bank_Standard_Time = new()
        {
            Id = 61,
            Value = "West Bank Standard Time",
            DisplayName = "(UTC+02:00) Gaza, Hebron",
            StandardName = "West Bank Gaza Standard Time",
            DaylightName = "West Bank Gaza Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("02:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 2,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation South_Africa_Standard_Time = new()
        {
            Id = 62,
            Value = "South Africa Standard Time",
            DisplayName = "(UTC+02:00) Harare, Pretoria",
            StandardName = "South Africa Standard Time",
            DaylightName = "South Africa Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("02:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 2,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation FLE_Standard_Time = new()
        {
            Id = 63,
            Value = "FLE Standard Time",
            DisplayName = "(UTC+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius",
            StandardName = "FLE Standard Time",
            DaylightName = "FLE Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("02:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 2,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Palastine_Standard_Time = new()
        {
            Id = 64,
            Value = "Israel Standard Time",
            DisplayName = "(UTC+02:00) Jerusalem",
            StandardName = "Jerusalem Standard Time",
            DaylightName = "Jerusalem Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("02:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 2,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation South_Sudan_Standard_Time = new()
        {
            Id = 65,
            Value = "South Sudan Standard Time",
            DisplayName = "(UTC+02:00) Juba",
            StandardName = "South Sudan Standard Time",
            DaylightName = "South Sudan Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("02:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 2,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Kaliningrad_Standard_Time = new()
        {
            Id = 66,
            Value = "Kaliningrad Standard Time",
            DisplayName = "(UTC+02:00) Kaliningrad",
            StandardName = "Russia TZ 1 Standard Time",
            DaylightName = "Russia TZ 1 Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("02:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 2,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Sudan_Standard_Time = new()
        {
            Id = 67,
            Value = "Sudan Standard Time",
            DisplayName = "(UTC+02:00) Khartoum",
            StandardName = "Sudan Standard Time",
            DaylightName = "Sudan Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("02:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 2,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Libya_Standard_Time = new()
        {
            Id = 68,
            Value = "Libya Standard Time",
            DisplayName = "(UTC+02:00) Tripoli",
            StandardName = "Libya Standard Time",
            DaylightName = "Libya Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("02:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 2,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Namibia_Standard_Time = new()
        {
            Id = 69,
            Value = "Namibia Standard Time",
            DisplayName = "(UTC+02:00) Windhoek",
            StandardName = "Namibia Standard Time",
            DaylightName = "Namibia Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("02:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 2,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Jordan_Standard_Time = new()
        {
            Id = 70,
            Value = "Jordan Standard Time",
            DisplayName = "(UTC+03:00) Amman",
            StandardName = "Jordan Standard Time",
            DaylightName = "Jordan Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("03:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 3,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Arabic_Standard_Time = new()
        {
            Id = 71,
            Value = "Arabic Standard Time",
            DisplayName = "(UTC+03:00) Baghdad",
            StandardName = "Arabic Standard Time",
            DaylightName = "Arabic Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("03:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 3,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Syria_Standard_Time = new()
        {
            Id = 72,
            Value = "Syria Standard Time",
            DisplayName = "(UTC+03:00) Damascus",
            StandardName = "Syria Standard Time",
            DaylightName = "Syria Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("03:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 3,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Turkey_Standard_Time = new()
        {
            Id = 73,
            Value = "Turkey Standard Time",
            DisplayName = "(UTC+03:00) Istanbul",
            StandardName = "Turkey Standard Time",
            DaylightName = "Turkey Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("03:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 3,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Arab_Standard_Time = new()
        {
            Id = 74,
            Value = "Arab Standard Time",
            DisplayName = "(UTC+03:00) Kuwait, Riyadh",
            StandardName = "Arab Standard Time",
            DaylightName = "Arab Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("03:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 3,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Belarus_Standard_Time = new()
        {
            Id = 75,
            Value = "Belarus Standard Time",
            DisplayName = "(UTC+03:00) Minsk",
            StandardName = "Belarus Standard Time",
            DaylightName = "Belarus Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("03:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 3,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Russian_Standard_Time = new()
        {
            Id = 76,
            Value = "Russian Standard Time",
            DisplayName = "(UTC+03:00) Moscow, St. Petersburg",
            StandardName = "Russia TZ 2 Standard Time",
            DaylightName = "Russia TZ 2 Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("03:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 3,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation E_Africa_Standard_Time = new()
        {
            Id = 77,
            Value = "E. Africa Standard Time",
            DisplayName = "(UTC+03:00) Nairobi",
            StandardName = "E. Africa Standard Time",
            DaylightName = "E. Africa Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("03:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 3,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Volgograd_Standard_Time = new()
        {
            Id = 78,
            Value = "Volgograd Standard Time",
            DisplayName = "(UTC+03:00) Volgograd",
            StandardName = "Volgograd Standard Time",
            DaylightName = "Volgograd Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("03:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 3,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Iran_Standard_Time = new()
        {
            Id = 79,
            Value = "Iran Standard Time",
            DisplayName = "(UTC+03:30) Tehran",
            StandardName = "Iran Standard Time",
            DaylightName = "Iran Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("03:30:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 3,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Arabian_Standard_Time = new()
        {
            Id = 80,
            Value = "Arabian Standard Time",
            DisplayName = "(UTC+04:00) Abu Dhabi, Muscat",
            StandardName = "Arabian Standard Time",
            DaylightName = "Arabian Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("04:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 4,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Astrakhan_Standard_Time = new()
        {
            Id = 81,
            Value = "Astrakhan Standard Time",
            DisplayName = "(UTC+04:00) Astrakhan, Ulyanovsk",
            StandardName = "Astrakhan Standard Time",
            DaylightName = "Astrakhan Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("04:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 4,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Azerbaijan_Standard_Time = new()
        {
            Id = 82,
            Value = "Azerbaijan Standard Time",
            DisplayName = "(UTC+04:00) Baku",
            StandardName = "Azerbaijan Standard Time",
            DaylightName = "Azerbaijan Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("04:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 4,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Russia_Time_Zone_3 = new()
        {
            Id = 83,
            Value = "Russia Time Zone 3",
            DisplayName = "(UTC+04:00) Izhevsk, Samara",
            StandardName = "Russia TZ 3 Standard Time",
            DaylightName = "Russia TZ 3 Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("04:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 4,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Mauritius_Standard_Time = new()
        {
            Id = 84,
            Value = "Mauritius Standard Time",
            DisplayName = "(UTC+04:00) Port Louis",
            StandardName = "Mauritius Standard Time",
            DaylightName = "Mauritius Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("04:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 4,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Saratov_Standard_Time = new()
        {
            Id = 85,
            Value = "Saratov Standard Time",
            DisplayName = "(UTC+04:00) Saratov",
            StandardName = "Saratov Standard Time",
            DaylightName = "Saratov Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("04:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 4,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Georgian_Standard_Time = new()
        {
            Id = 86,
            Value = "Georgian Standard Time",
            DisplayName = "(UTC+04:00) Tbilisi",
            StandardName = "Georgian Standard Time",
            DaylightName = "Georgian Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("04:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 4,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Caucasus_Standard_Time = new()
        {
            Id = 87,
            Value = "Caucasus Standard Time",
            DisplayName = "(UTC+04:00) Yerevan",
            StandardName = "Caucasus Standard Time",
            DaylightName = "Caucasus Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("04:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 4,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Afghanistan_Standard_Time = new()
        {
            Id = 88,
            Value = "Afghanistan Standard Time",
            DisplayName = "(UTC+04:30) Kabul",
            StandardName = "Afghanistan Standard Time",
            DaylightName = "Afghanistan Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("04:30:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 4,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation West_Asia_Standard_Time = new()
        {
            Id = 89,
            Value = "West Asia Standard Time",
            DisplayName = "(UTC+05:00) Ashgabat, Tashkent",
            StandardName = "West Asia Standard Time",
            DaylightName = "West Asia Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("05:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 5,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Qyzylorda_Standard_Time = new()
        {
            Id = 90,
            Value = "Qyzylorda Standard Time",
            DisplayName = "(UTC+05:00) Astana",
            StandardName = "Qyzylorda Standard Time",
            DaylightName = "Qyzylorda Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("05:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 5,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Ekaterinburg_Standard_Time = new()
        {
            Id = 91,
            Value = "Ekaterinburg Standard Time",
            DisplayName = "(UTC+05:00) Ekaterinburg",
            StandardName = "Russia TZ 4 Standard Time",
            DaylightName = "Russia TZ 4 Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("05:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 5,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Pakistan_Standard_Time = new()
        {
            Id = 92,
            Value = "Pakistan Standard Time",
            DisplayName = "(UTC+05:00) Islamabad, Karachi",
            StandardName = "Pakistan Standard Time",
            DaylightName = "Pakistan Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("05:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 5,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation India_Standard_Time = new()
        {
            Id = 93,
            Value = "India Standard Time",
            DisplayName = "(UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi",
            StandardName = "India Standard Time",
            DaylightName = "India Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("05:30:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 5,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Sri_Lanka_Standard_Time = new()
        {
            Id = 94,
            Value = "Sri Lanka Standard Time",
            DisplayName = "(UTC+05:30) Sri Jayawardenepura",
            StandardName = "Sri Lanka Standard Time",
            DaylightName = "Sri Lanka Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("05:30:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 5,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Nepal_Standard_Time = new()
        {
            Id = 95,
            Value = "Nepal Standard Time",
            DisplayName = "(UTC+05:45) Kathmandu",
            StandardName = "Nepal Standard Time",
            DaylightName = "Nepal Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("05:45:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 5,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Central_Asia_Standard_Time = new()
        {
            Id = 96,
            Value = "Central Asia Standard Time",
            DisplayName = "(UTC+06:00) Bishkek",
            StandardName = "Central Asia Standard Time",
            DaylightName = "Central Asia Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("06:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 6,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Bangladesh_Standard_Time = new()
        {
            Id = 97,
            Value = "Bangladesh Standard Time",
            DisplayName = "(UTC+06:00) Dhaka",
            StandardName = "Bangladesh Standard Time",
            DaylightName = "Bangladesh Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("06:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 6,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Omsk_Standard_Time = new()
        {
            Id = 98,
            Value = "Omsk Standard Time",
            DisplayName = "(UTC+06:00) Omsk",
            StandardName = "Omsk Standard Time",
            DaylightName = "Omsk Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("06:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 6,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Myanmar_Standard_Time = new()
        {
            Id = 99,
            Value = "Myanmar Standard Time",
            DisplayName = "(UTC+06:30) Yangon (Rangoon)",
            StandardName = "Myanmar Standard Time",
            DaylightName = "Myanmar Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("06:30:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 6,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation SE_Asia_Standard_Time = new()
        {
            Id = 100,
            Value = "SE Asia Standard Time",
            DisplayName = "(UTC+07:00) Bangkok, Hanoi, Jakarta",
            StandardName = "SE Asia Standard Time",
            DaylightName = "SE Asia Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("07:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 7,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Altai_Standard_Time = new()
        {
            Id = 101,
            Value = "Altai Standard Time",
            DisplayName = "(UTC+07:00) Barnaul, Gorno-Altaysk",
            StandardName = "Altai Standard Time",
            DaylightName = "Altai Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("07:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 7,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation W_Mongolia_Standard_Time = new()
        {
            Id = 102,
            Value = "W. Mongolia Standard Time",
            DisplayName = "(UTC+07:00) Hovd",
            StandardName = "W. Mongolia Standard Time",
            DaylightName = "W. Mongolia Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("07:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 7,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation North_Asia_Standard_Time = new()
        {
            Id = 103,
            Value = "North Asia Standard Time",
            DisplayName = "(UTC+07:00) Krasnoyarsk",
            StandardName = "Russia TZ 6 Standard Time",
            DaylightName = "Russia TZ 6 Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("07:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 7,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation N_Central_Asia_Standard_Time = new()
        {
            Id = 104,
            Value = "N. Central Asia Standard Time",
            DisplayName = "(UTC+07:00) Novosibirsk",
            StandardName = "Novosibirsk Standard Time",
            DaylightName = "Novosibirsk Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("07:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 7,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Tomsk_Standard_Time = new()
        {
            Id = 105,
            Value = "Tomsk Standard Time",
            DisplayName = "(UTC+07:00) Tomsk",
            StandardName = "Tomsk Standard Time",
            DaylightName = "Tomsk Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("07:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 7,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation China_Standard_Time = new()
        {
            Id = 106,
            Value = "China Standard Time",
            DisplayName = "(UTC+08:00) Beijing, Chongqing, Hong Kong, Urumqi",
            StandardName = "China Standard Time",
            DaylightName = "China Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("08:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 8,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation North_Asia_East_Standard_Time = new()
        {
            Id = 107,
            Value = "North Asia East Standard Time",
            DisplayName = "(UTC+08:00) Irkutsk",
            StandardName = "Russia TZ 7 Standard Time",
            DaylightName = "Russia TZ 7 Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("08:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 8,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Singapore_Standard_Time = new()
        {
            Id = 108,
            Value = "Singapore Standard Time",
            DisplayName = "(UTC+08:00) Kuala Lumpur, Singapore",
            StandardName = "Malay Peninsula Standard Time",
            DaylightName = "Malay Peninsula Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("08:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 8,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation W_Australia_Standard_Time = new()
        {
            Id = 109,
            Value = "W. Australia Standard Time",
            DisplayName = "(UTC+08:00) Perth",
            StandardName = "W. Australia Standard Time",
            DaylightName = "W. Australia Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("08:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 8,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Taipei_Standard_Time = new()
        {
            Id = 110,
            Value = "Taipei Standard Time",
            DisplayName = "(UTC+08:00) Taipei",
            StandardName = "Taipei Standard Time",
            DaylightName = "Taipei Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("08:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 8,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Ulaanbaatar_Standard_Time = new()
        {
            Id = 111,
            Value = "Ulaanbaatar Standard Time",
            DisplayName = "(UTC+08:00) Ulaanbaatar",
            StandardName = "Ulaanbaatar Standard Time",
            DaylightName = "Ulaanbaatar Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("08:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 8,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Aus_Central_W_Standard_Time = new()
        {
            Id = 112,
            Value = "Aus Central W. Standard Time",
            DisplayName = "(UTC+08:45) Eucla",
            StandardName = "Aus Central W. Standard Time",
            DaylightName = "Aus Central W. Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("08:45:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 8,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Transbaikal_Standard_Time = new()
        {
            Id = 113,
            Value = "Transbaikal Standard Time",
            DisplayName = "(UTC+09:00) Chita",
            StandardName = "Transbaikal Standard Time",
            DaylightName = "Transbaikal Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("09:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 9,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Tokyo_Standard_Time = new()
        {
            Id = 114,
            Value = "Tokyo Standard Time",
            DisplayName = "(UTC+09:00) Osaka, Sapporo, Tokyo",
            StandardName = "Tokyo Standard Time",
            DaylightName = "Tokyo Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("09:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 9,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation North_Korea_Standard_Time = new()
        {
            Id = 115,
            Value = "North Korea Standard Time",
            DisplayName = "(UTC+09:00) Pyongyang",
            StandardName = "North Korea Standard Time",
            DaylightName = "North Korea Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("09:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 9,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };


        public readonly static TimeZoneInformation Korea_Standard_Time = new()
        {
            Id = 116,
            Value = "Korea Standard Time",
            DisplayName = "(UTC+09:00) Seoul",
            StandardName = "Korea Standard Time",
            DaylightName = "Korea Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("09:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 9,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Yakutsk_Standard_Time = new()
        {
            Id = 117,
            Value = "Yakutsk Standard Time",
            DisplayName = "(UTC+09:00) Yakutsk",
            StandardName = "Russia TZ 8 Standard Time",
            DaylightName = "Russia TZ 8 Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("09:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 9,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Cen_Australia_Standard_Time = new()
        {
            Id = 118,
            Value = "Cen. Australia Standard Time",
            DisplayName = "(UTC+09:30) Adelaide",
            StandardName = "Cen. Australia Standard Time",
            DaylightName = "Cen. Australia Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("09:30:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 9,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation AUS_Central_Standard_Time = new()
        {
            Id = 119,
            Value = "AUS Central Standard Time",
            DisplayName = "(UTC+09:30) Darwin",
            StandardName = "AUS Central Standard Time",
            DaylightName = "AUS Central Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("09:30:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 9,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation E_Australia_Standard_Time = new()
        {
            Id = 120,
            Value = "E. Australia Standard Time",
            DisplayName = "(UTC+10:00) Brisbane",
            StandardName = "E. Australia Standard Time",
            DaylightName = "E. Australia Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("10:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 10,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation AUS_Eastern_Standard_Time = new()
        {
            Id = 121,
            Value = "AUS Eastern Standard Time",
            DisplayName = "(UTC+10:00) Canberra, Melbourne, Sydney",
            StandardName = "AUS Eastern Standard Time",
            DaylightName = "AUS Eastern Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("10:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 10,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation West_Pacific_Standard_Time = new()
        {
            Id = 122,
            Value = "West Pacific Standard Time",
            DisplayName = "(UTC+10:00) Guam, Port Moresby",
            StandardName = "West Pacific Standard Time",
            DaylightName = "West Pacific Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("10:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 10,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Tasmania_Standard_Time = new()
        {
            Id = 123,
            Value = "Tasmania Standard Time",
            DisplayName = "(UTC+10:00) Hobart",
            StandardName = "Tasmania Standard Time",
            DaylightName = "Tasmania Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("10:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 10,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Vladivostok_Standard_Time = new()
        {
            Id = 124,
            Value = "Vladivostok Standard Time",
            DisplayName = "(UTC+10:00) Vladivostok",
            StandardName = "Russia TZ 9 Standard Time",
            DaylightName = "Russia TZ 9 Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("10:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 10,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Lord_Howe_Standard_Time = new()
        {
            Id = 125,
            Value = "Lord Howe Standard Time",
            DisplayName = "(UTC+10:30) Lord Howe Island",
            StandardName = "Lord Howe Standard Time",
            DaylightName = "Lord Howe Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("10:30:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 10,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Bougainville_Standard_Time = new()
        {
            Id = 126,
            Value = "Bougainville Standard Time",
            DisplayName = "(UTC+11:00) Bougainville Island",
            StandardName = "Bougainville Standard Time",
            DaylightName = "Bougainville Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("11:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 11,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Russia_Time_Zone_10 = new()
        {
            Id = 127,
            Value = "Russia Time Zone 10",
            DisplayName = "(UTC+11:00) Chokurdakh",
            StandardName = "Russia TZ 10 Standard Time",
            DaylightName = "Russia TZ 10 Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("11:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 11,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Magadan_Standard_Time = new()
        {
            Id = 128,
            Value = "Magadan Standard Time",
            DisplayName = "(UTC+11:00) Magadan",
            StandardName = "Magadan Standard Time",
            DaylightName = "Magadan Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("11:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 11,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Norfolk_Standard_Time = new()
        {
            Id = 129,
            Value = "Norfolk Standard Time",
            DisplayName = "(UTC+11:00) Norfolk Island",
            StandardName = "Norfolk Standard Time",
            DaylightName = "Norfolk Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("11:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 11,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Sakhalin_Standard_Time = new()
        {
            Id = 130,
            Value = "Sakhalin Standard Time",
            DisplayName = "(UTC+11:00) Sakhalin",
            StandardName = "Sakhalin Standard Time",
            DaylightName = "Sakhalin Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("11:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 11,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Central_Pacific_Standard_Time = new()
        {
            Id = 131,
            Value = "Central Pacific Standard Time",
            DisplayName = "(UTC+11:00) Solomon Is., New Caledonia",
            StandardName = "Central Pacific Standard Time",
            DaylightName = "Central Pacific Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("11:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 11,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Russia_Time_Zone_11 = new()
        {
            Id = 132,
            Value = "Russia Time Zone 11",
            DisplayName = "(UTC+12:00) Anadyr, Petropavlovsk-Kamchatsky",
            StandardName = "Russia TZ 11 Standard Time",
            DaylightName = "Russia TZ 11 Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("12:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 12,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation New_Zealand_Standard_Time = new()
        {
            Id = 133,
            Value = "New Zealand Standard Time",
            DisplayName = "(UTC+12:00) Auckland, Wellington",
            StandardName = "New Zealand Standard Time",
            DaylightName = "New Zealand Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("12:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 12,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation UTC_12 = new()
        {
            Id = 134,
            Value = "UTC+12",
            DisplayName = "(UTC+12:00) Coordinated Universal Time+12",
            StandardName = "UTC+12",
            DaylightName = "UTC+12",
            BaseUtcOffset = TimeSpan.Parse("12:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 12,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Fiji_Standard_Time = new()
        {
            Id = 135,
            Value = "Fiji Standard Time",
            DisplayName = "(UTC+12:00) Fiji",
            StandardName = "Fiji Standard Time",
            DaylightName = "Fiji Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("12:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 12,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Kamchatka_Standard_Time = new()
        {
            Id = 136,
            Value = "Kamchatka Standard Time",
            DisplayName = "(UTC+12:00) Petropavlovsk-Kamchatsky - Old",
            StandardName = "Kamchatka Standard Time",
            DaylightName = "Kamchatka Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("12:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 12,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Chatham_Islands_Standard_Time = new()
        {
            Id = 137,
            Value = "Chatham Islands Standard Time",
            DisplayName = "(UTC+12:45) Chatham Islands",
            StandardName = "Chatham Islands Standard Time",
            DaylightName = "Chatham Islands Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("12:45:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 12,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation UTC_13 = new()
        {
            Id = 138,
            Value = "UTC+13",
            DisplayName = "(UTC+13:00) Coordinated Universal Time+13",
            StandardName = "UTC+13",
            DaylightName = "UTC+13",
            BaseUtcOffset = TimeSpan.Parse("13:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 13,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Tonga_Standard_Time = new()
        {
            Id = 139,
            Value = "Tonga Standard Time",
            DisplayName = "(UTC+13:00) Nuku'alofa",
            StandardName = "Tonga Standard Time",
            DaylightName = "Tonga Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("13:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 13,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Samoa_Standard_Time = new()
        {
            Id = 140,
            Value = "Samoa Standard Time",
            DisplayName = "(UTC+13:00) Samoa",
            StandardName = "Samoa Standard Time",
            DaylightName = "Samoa Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("13:00:00"),
            SupportsDaylightSavingTime = true,
            UtcOffset = 13,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        public readonly static TimeZoneInformation Line_Islands_Standard_Time = new()
        {
            Id = 141,
            Value = "Line Islands Standard Time",
            DisplayName = "(UTC+14:00) Kiritimati Island",
            StandardName = "Line Islands Standard Time",
            DaylightName = "Line Islands Daylight Time",
            BaseUtcOffset = TimeSpan.Parse("14:00:00"),
            SupportsDaylightSavingTime = false,
            UtcOffset = 14,
            CreatedBy = "1",
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        /// <summary>
        /// Retrieves all <see cref="TimeZoneInformation"/> instances defined as static fields in this class.
        /// </summary>
        /// <returns>
        /// A list of <see cref="TimeZoneInformation"/> objects obtained from the static fields of this class.
        /// </returns>
        public static List<TimeZoneInformation> GetAll()
        {
            return
            [
                Dateline_Standard_Time,
                UTC_11,
                Aleutian_Standard_Time,
                Hawaiian_Standard_Time,
                Marquesas_Standard_Time,
                Alaskan_Standard_Time,
                UTC_09,
                Pacific_Standard_Time_Mexico,
                UTC_08,
                Pacific_Standard_Time,
                US_Mountain_Standard_Time,
                Mountain_Standard_Time_Mexico,
                Mountain_Standard_Time,
                Yukon_Standard_Time,
                Central_America_Standard_Time,
                Central_Standard_Time,
                Easter_Island_Standard_Time,
                Central_Standard_Time_Mexico,
                Canada_Central_Standard_Time,
                SA_Pacific_Standard_Time,
                Eastern_Standard_Time_Mexico,
                Eastern_Standard_Time,
                Haiti_Standard_Time,
                Cuba_Standard_Time,
                US_Eastern_Standard_Time,
                Turks_And_Caicos_Standard_Time,
                Paraguay_Standard_Time,
                Atlantic_Standard_Time,
                Venezuela_Standard_Time,
                Central_Brazilian_Standard_Time,
                SA_Western_Standard_Time,
                Pacific_SA_Standard_Time,
                Newfoundland_Standard_Time,
                Tocantins_Standard_Time,
                E_South_America_Standard_Time,
                SA_Eastern_Standard_Time,
                Argentina_Standard_Time,
                Montevideo_Standard_Time,
                Magallanes_Standard_Time,
                Saint_Pierre_Standard_Time,
                Bahia_Standard_Time,
                UTC_02,
                Greenland_Standard_Time,
                Mid_Atlantic_Standard_Time,
                Azores_Standard_Time,
                Cape_Verde_Standard_Time,
                UTC,
                GMT_Standard_Time,
                Greenwich_Standard_Time,
                Sao_Tome_Standard_Time,
                Morocco_Standard_Time,
                W_Europe_Standard_Time,
                Central_Europe_Standard_Time,
                Romance_Standard_Time,
                Central_European_Standard_Time,
                W_Central_Africa_Standard_Time,
                GTB_Standard_Time,
                Middle_East_Standard_Time,
                Egypt_Standard_Time,
                E_Europe_Standard_Time,
                West_Bank_Standard_Time,
                South_Africa_Standard_Time,
                FLE_Standard_Time,
                Palastine_Standard_Time,
                South_Sudan_Standard_Time,
                Kaliningrad_Standard_Time,
                Sudan_Standard_Time,
                Libya_Standard_Time,
                Namibia_Standard_Time,
                Jordan_Standard_Time,
                Arabic_Standard_Time,
                Syria_Standard_Time,
                Turkey_Standard_Time,
                Arab_Standard_Time,
                Belarus_Standard_Time,
                Russian_Standard_Time,
                E_Africa_Standard_Time,
                Volgograd_Standard_Time,
                Iran_Standard_Time,
                Arabian_Standard_Time,
                Astrakhan_Standard_Time,
                Azerbaijan_Standard_Time,
                Russia_Time_Zone_3,
                Mauritius_Standard_Time,
                Saratov_Standard_Time,
                Georgian_Standard_Time,
                Caucasus_Standard_Time,
                Afghanistan_Standard_Time,
                West_Asia_Standard_Time,
                Qyzylorda_Standard_Time,
                Ekaterinburg_Standard_Time,
                Pakistan_Standard_Time,
                India_Standard_Time,
                Sri_Lanka_Standard_Time,
                Nepal_Standard_Time,
                Central_Asia_Standard_Time,
                Bangladesh_Standard_Time,
                Omsk_Standard_Time,
                Myanmar_Standard_Time,
                SE_Asia_Standard_Time,
                Altai_Standard_Time,
                W_Mongolia_Standard_Time,
                North_Asia_Standard_Time,
                N_Central_Asia_Standard_Time,
                Tomsk_Standard_Time,
                China_Standard_Time,
                North_Asia_East_Standard_Time,
                Singapore_Standard_Time,
                W_Australia_Standard_Time,
                Taipei_Standard_Time,
                Ulaanbaatar_Standard_Time,
                Aus_Central_W_Standard_Time,
                Transbaikal_Standard_Time,
                Tokyo_Standard_Time,
                    North_Korea_Standard_Time,
                Korea_Standard_Time,
                Yakutsk_Standard_Time,
                Cen_Australia_Standard_Time,
                AUS_Central_Standard_Time,
                E_Australia_Standard_Time,
                AUS_Eastern_Standard_Time,
                West_Pacific_Standard_Time,
                Tasmania_Standard_Time,
                Vladivostok_Standard_Time,
                Lord_Howe_Standard_Time,
                Bougainville_Standard_Time,
                Russia_Time_Zone_10,
                Magadan_Standard_Time,
                Norfolk_Standard_Time,
                Sakhalin_Standard_Time,
                Central_Pacific_Standard_Time,
                Russia_Time_Zone_11,
                New_Zealand_Standard_Time,
                UTC_12,
                Fiji_Standard_Time,
                Kamchatka_Standard_Time,
                Chatham_Islands_Standard_Time,
                UTC_13,
                Tonga_Standard_Time,
                Samoa_Standard_Time,
                Line_Islands_Standard_Time,
            ];
        }
    }
}
