using InventorySystem.Shared.Entities.Core;

namespace InventorySystem.Shared.Entities.Enumerations.SeedEnumeration
{
    /// <summary>
    /// Contains predefined languages for the application.
    /// </summary>
    public static class LanguagesEnum
    {
        public readonly static Language Arabic = new() { Id = 1, Code = "ar", CreatedBy = "Init Seed", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, Name = "Arabic", NativeName = "العربية", NameAr = "العربية" };

        public readonly static Language Bengali = new() { Id = 2, Code = "bn", CreatedBy = "Init Seed", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, Name = "Bengali", NativeName = "বাংলা", NameAr = "البنغالية" };

        public readonly static Language English = new() { Id = 3, Code = "en", CreatedBy = "Init Seed", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, Name = "English", NativeName = "English", NameAr = "الإنجليزية" };

        public readonly static Language Spanish = new() { Id = 4, Code = "es", CreatedBy = "Init Seed", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, Name = "Spanish", NativeName = "Español", NameAr = "الإسبانية" };

        public readonly static Language Persian = new() { Id = 5, Code = "fa", CreatedBy = "Init Seed", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, Name = "Persian", NativeName = "فارسی", NameAr = "الفارسية" };

        public readonly static Language French = new() { Id = 6, Code = "fr", CreatedBy = "Init Seed", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, Name = "French", NativeName = "Français", NameAr = "الفرنسية" };

        public readonly static Language Hindi = new() { Id = 7, Code = "hi", CreatedBy = "Init Seed", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, Name = "Hindi", NativeName = "हिन्दी", NameAr = "الهندية" };

        public readonly static Language Malayalam = new() { Id = 8, Code = "ml", CreatedBy = "Init Seed", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, Name = "Malayalam", NativeName = "മലയാളം", NameAr = "الماليالامية" };

        public readonly static Language Nepali = new() { Id = 9, Code = "ne", CreatedBy = "Init Seed", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, Name = "Nepali", NativeName = "नेपाली", NameAr = "النيبالية" };

        public readonly static Language Pashto = new() { Id = 10, Code = "ps", CreatedBy = "Init Seed", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, Name = "Pashto", NativeName = "پښتو", NameAr = "البشتو" };

        public readonly static Language Russian = new() { Id = 11, Code = "ru", CreatedBy = "Init Seed", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, Name = "Russian", NativeName = "Русский", NameAr = "الروسية" };

        public readonly static Language Sinhala = new() { Id = 12, Code = "si", CreatedBy = "Init Seed", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, Name = "Sinhala", NativeName = "සිංහල", NameAr = "السنهالية" };

        public readonly static Language Tamil = new() { Id = 13, Code = "ta", CreatedBy = "Init Seed", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, Name = "Tamil", NativeName = "தமிழ்", NameAr = "التاميلية" };

        public readonly static Language Telugu = new() { Id = 14, Code = "te", CreatedBy = "Init Seed", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, Name = "Telugu", NativeName = "తెలుగు", NameAr = "التيلجو" };

        public readonly static Language Tagalog = new() { Id = 15, Code = "tl", CreatedBy = "Init Seed", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, Name = "Tagalog", NativeName = "Tagalog", NameAr = "التاغالوغية" };

        public readonly static Language Turkish = new() { Id = 16, Code = "tr", CreatedBy = "Init Seed", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, Name = "Turkish", NativeName = "Türkçe", NameAr = "التركية" };

        public readonly static Language Urdu = new() { Id = 17, Code = "ur", CreatedBy = "Init Seed", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, Name = "Urdu", NativeName = "اردو", NameAr = "الأردية" };

        public readonly static Language Chinese = new() { Id = 18, Code = "zh", CreatedBy = "Init Seed", CreatedDate = DateTime.UtcNow, IsActive = true, IsDeleted = false, Name = "Chinese", NativeName = "中文", NameAr = "الصينية" };

        /// <summary>
        /// Retrieves all predefined languages as a list.
        /// </summary>
        /// <returns>A list of all <see cref="Language"/> objects defined in this class.</returns>
        public static List<Language> GetAll()
        {
            return
            [
                Arabic,
                Bengali,
                English,
                Spanish,
                Persian,
                French,
                Hindi,
                Malayalam,
                Nepali,
                Pashto,
                Russian,
                Sinhala,
                Tamil,
                Telugu,
                Tagalog,
                Turkish,
                Urdu,
                Chinese
            ];
        }
    }
}
