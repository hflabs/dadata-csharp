using System.Collections.Generic;

namespace Dadata.Model
{
    public class Address : IDadataEntity
    {
        public string source { get; set; }
        public string result { get; set; }

        public string postal_code { get; set; }
        public string country { get; set; }
        public string country_iso_code { get; set; }
        public string federal_district { get; set; }

        public string region_fias_id { get; set; }
        public string region_kladr_id { get; set; }
        public string region_iso_code { get; set; }
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

        public string sub_area_fias_id { get; set; }
        public string sub_area_kladr_id { get; set; }
        public string sub_area_with_type { get; set; }
        public string sub_area_type { get; set; }
        public string sub_area_type_full { get; set; }
        public string sub_area { get; set; }

        public string city_fias_id { get; set; }
        public string city_kladr_id { get; set; }
        public string city_with_type { get; set; }
        public string city_type { get; set; }
        public string city_type_full { get; set; }
        public string city { get; set; }

        public string city_area { get; set; }

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

        public string street_fias_id { get; set; }
        public string street_kladr_id { get; set; }
        public string street_with_type { get; set; }
        public string street_type { get; set; }
        public string street_type_full { get; set; }
        public string street { get; set; }

        public string house_fias_id { get; set; }
        public string house_kladr_id { get; set; }
        public string house_cadnum { get; set; }
        public string house_flat_count { get; set; }
        public string house_with_type { get; set; }
        public string house_type { get; set; }
        public string house_type_full { get; set; }
        public string house { get; set; }

        public string block_type { get; set; }
        public string block_type_full { get; set; }
        public string block { get; set; }

        public string entrance { get; set; }
        public string floor { get; set; }

        public string flat_fias_id { get; set; }
        public string flat_cadnum { get; set; }
        public string flat_type { get; set; }
        public string flat_type_full { get; set; }
        public string flat { get; set; }

        public string room_fias_id { get; set; }
        public string room_cadnum { get; set; }
        public string room_type { get; set; }
        public string room_type_full { get; set; }
        public string room { get; set; }

        public string stead_fias_id { get; set; }
        public string stead_kladr_id { get; set; }
        public string stead_type { get; set; }
        public string stead_type_full { get; set; }
        public string stead { get; set; }

        public string flat_area { get; set; }
        public string square_meter_price { get; set; }
        public string flat_price { get; set; }

        public string postal_box { get; set; }
        public string fias_id { get; set; }
        public string fias_code { get; set; }
        public string fias_level { get; set; }
        public string fias_actuality_state { get; set; }
        public string kladr_id { get; set; }
        public string geoname_id { get; set; }
        public string capital_marker { get; set; }

        public string okato { get; set; }
        public string oktmo { get; set; }
        public string tax_office { get; set; }
        public string tax_office_legal { get; set; }
        public string timezone { get; set; }

        public string geo_lat { get; set; }
        public string geo_lon { get; set; }
        public string beltway_hit { get; set; }
        public string beltway_distance { get; set; }

        public string qc_geo { get; set; }
        public string qc_complete { get; set; }
        public string qc_house { get; set; }
        public string qc { get; set; }

        public string unparsed_parts { get; set; }

        public List<string> history_values { get; set; }
        public List<AddressMetro> metro { get; set; }
        public AddressDivisions divisions { get; set; }

        public StructureType structure_type
        {
            get { return StructureType.ADDRESS; }
        }

        public override string ToString()
        {
            return string.Format(
                "[AddressData: source={0}, postal_code={1}, result={2}, qc={3}]",
                source, postal_code, result, qc
            );
        }
    }

    public class AddressMetro
    {
        public string name { get; set; }
        public string line { get; set; }
        public decimal distance { get; set; }
    }

    public class AddressDivisions
    {
        public AddressAdministrative administrative { get; set; }
        public AddressMunicipal municipal { get; set; }
    }

    public class AddressAdministrative
    {
        public AddressPart area { get; set; }
        public AddressPart city { get; set; }
        public AddressPart city_district { get; set; }
        public AddressPart settlement { get; set; }
        public AddressPart planning_structure { get; set; }
    }

    public class AddressMunicipal
    {
        public AddressPart area { get; set; }
        public AddressPart sub_area { get; set; }
        public AddressPart city { get; set; }
        public AddressPart settlement { get; set; }
        public AddressPart planning_structure { get; set; }
    }

    public class AddressPart
    {
        public string fias_id { get; set; }
        public string kladr_id { get; set; }
        public string type { get; set; }
        public string type_full { get; set; }
        public string name { get; set; }
        public string name_with_type { get; set; }
    }
}
