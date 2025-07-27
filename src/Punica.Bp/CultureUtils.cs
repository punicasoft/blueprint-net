using System.Globalization;

namespace Punica.Bp
{
    public class CultureUtils
    {
        private static CultureUtils? _instance;
        private static readonly object Lock = new object();
        private readonly Dictionary<string, CurrencyInfo> _currencies;

        private CultureUtils()
        {
            _currencies = new Dictionary<string, CurrencyInfo>();
        }

        public static CultureUtils Instance
        {
            get
            {
                lock (Lock)
                {
                    if (_instance == null)
                    {
                        _instance = new CultureUtils();
                    }
                    return _instance;
                }
            }
        }

        public CurrencyInfo GetCurrencyInfo(string currencyCode)
        {
            if (!_currencies.TryGetValue(currencyCode, out var currency))
            {
                var region = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                    .Select(x => new RegionInfo(x.Name))
                    .FirstOrDefault(x => x.ISOCurrencySymbol == currencyCode);

                if (region == null)
                {
                    throw new ArgumentException("Invalid currency code");
                }

                currency = new CurrencyInfo
                {
                    Code = region.ISOCurrencySymbol,
                    Symbol = region.CurrencySymbol,
                    Name = region.CurrencyEnglishName
                };

                _currencies.Add(currencyCode, currency);
            }

            return currency;
        }

        public static CurrencyInfo GetCurrency(string currencyCode)
        {
            return CultureUtils.Instance.GetCurrencyInfo(currencyCode);
        }

        public static IEnumerable<RegionInfo> GetRegions()
        {
            return CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                 .Select(culture => new RegionInfo(culture.Name))
                .DistinctBy(x => x.TwoLetterISORegionName);
        }

        public static IOrderedEnumerable<CountryInfo> GetCountries()
        {
            return GetRegions()
                .Select(x=> new CountryInfo()
                {
                    Name = x.EnglishName,
                    Code = x.TwoLetterISORegionName,
                    ThreeLetterCode = x.ThreeLetterISORegionName,
                })
                .OrderBy(x => x.Name);
        }

        public static CountryInfo GetCountry(string twoLetterCountryCode)
        {
           var region = new RegionInfo(twoLetterCountryCode);

           return new CountryInfo()
           {
                Name = region.EnglishName,
                Code = region.TwoLetterISORegionName,
                ThreeLetterCode = region.ThreeLetterISORegionName,
            };
        }

        public static List<CultureInfo> GetCultures(string twoLetterCountryCode)
        {
            var cultures = new List<CultureInfo>();

            foreach (var culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                var region = new RegionInfo(culture.Name);
                if (region.TwoLetterISORegionName.Equals(twoLetterCountryCode, StringComparison.OrdinalIgnoreCase))
                {
                    cultures.Add(culture);
                }
            }

            if (cultures.Count == 0)
            {
                throw new ArgumentException("Invalid country code");
            }

            return cultures;
        }
    }

    public record CurrencyInfo
    {
        public required string Code { get; init; }
        public required string Symbol { get; init; }
        public required string Name { get; init; }
    }

    public record CountryInfo
    {
        public required string Code { get; init; }
        public required string Name { get; init; }
        public required string ThreeLetterCode { get; init; }
    }
}
