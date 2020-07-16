using System;
using System.Collections.Generic;

namespace Dadata.Model
{
    public class FiasAddress
    {
        public string source { get; set; }
        public string result { get; set; }

        public string postal_code { get; set; }

        public string region_fias_id { get; set; }
        public string region_kladr_id { get; set; }
        public string region_with_type { get; set; }
        public string region_type { get; set; }
        public string region_type_full { get; set; }
        public string region { get; set; }

        public string area_fias_id { get; set; }
        public string area_kladr_id { get; set; }
        public string area_with_type { get; set; }
        public string area_type { get; set; }
        public string area_type_full { get; set; }
        public string area { get; set; }

        public string city_fias_id { get; set; }
        public string city_kladr_id { get; set; }
        public string city_with_type { get; set; }
        public string city_type { get; set; }
        public string city_type_full { get; set; }
        public string city { get; set; }

        public string city_district_fias_id { get; set; }
        public string city_district_kladr_id { get; set; }
        public string city_district_with_type { get; set; }
        public string city_district_type { get; set; }
        public string city_district_type_full { get; set; }
        public string city_district { get; set; }

        public string settlement_fias_id { get; set; }
        public string settlement_kladr_id { get; set; }
        public string settlement_with_type { get; set; }
        public string settlement_type { get; set; }
        public string settlement_type_full { get; set; }
        public string settlement { get; set; }

        public string planning_structure_fias_id { get; set; }
        public string planning_structure_kladr_id { get; set; }
        public string planning_structure_with_type { get; set; }
        public string planning_structure_type { get; set; }
        public string planning_structure_type_full { get; set; }
        public string planning_structure { get; set; }

        public string street_fias_id { get; set; }
        public string street_kladr_id { get; set; }
        public string street_with_type { get; set; }
        public string street_type { get; set; }
        public string street_type_full { get; set; }
        public string street { get; set; }

        public string house_fias_id { get; set; }
        public string house_kladr_id { get; set; }
        public string house_type { get; set; }
        public string house { get; set; }

        public string block { get; set; }
        public string building_type { get; set; }
        public string building { get; set; }

        public string cadastral_number { get; set; }

        public string fias_code { get; set; }
        public string fias_level { get; set; }
        public string fias_actuality_state { get; set; }
        public string kladr_id { get; set; }
        public string capital_marker { get; set; }

        public string okato { get; set; }
        public string oktmo { get; set; }
        public string tax_office { get; set; }
        public string tax_office_legal { get; set; }

        public IList<string> history_values { get; set; }
    }
}
