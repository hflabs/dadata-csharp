using System;
using System.Collections.Generic;

namespace dadatacsharp {

    /// <summary>
    /// DaData data entity (address, phone etc).
    /// </summary>
    public interface IDadataEntity {
        StructureType structure_type { get; }
    }

    /// <summary>
    /// Postal address.
    /// </summary>
    public class AddressData: IDadataEntity {
        public string source                { get; set; }
        public string result                { get; set; }
        public string postal_code           { get; set; }
        public string country               { get; set; }
        public string region_type           { get; set; }
        public string region_type_full      { get; set; }
        public string region                { get; set; }
        public string area_type             { get; set; }
        public string area_type_full        { get; set; }
        public string area                  { get; set; }
        public string city_type             { get; set; }
        public string city_type_full        { get; set; }
        public string city                  { get; set; }
        public string settlement_type       { get; set; }
        public string settlement_type_full  { get; set; }
        public string settlement            { get; set; }
        public string city_district         { get; set; }
        public string street_type           { get; set; }
        public string street_type_full      { get; set; }
        public string street                { get; set; }
        public string house_type            { get; set; }
        public string house_type_full       { get; set; }
        public string house                 { get; set; }
        public string block_type            { get; set; }
        public string block_type_full       { get; set; }
        public string block                 { get; set; }
        public string flat_type             { get; set; }
        public string flat                  { get; set; }
        public string flat_area             { get; set; }
        public string square_meter_price    { get; set; }
        public string flat_price            { get; set; }
        public string postal_box            { get; set; }
        public string fias_id               { get; set; }
        public string fias_level            { get; set; }
        public string kladr_id              { get; set; }
        public string capital_marker        { get; set; }
        public string okato                 { get; set; }
        public string oktmo                 { get; set; }
        public string tax_office            { get; set; }
        public string tax_office_legal      { get; set; }
        public string timezone              { get; set; }
        public string geo_lat               { get; set; }
        public string geo_lon               { get; set; }
        public string qc_geo                { get; set; }
        public string qc_complete           { get; set; }
        public string qc_house              { get; set; }
        public string qc                    { get; set; }
        public string unparsed_parts        { get; set; }

        public StructureType structure_type {
            get { return StructureType.ADDRESS; }
        }

        public override string ToString() {
            return string.Format(
                "[AddressData: source={0}, region={1}, area={2}, city={3}, settlement={4}, street={5}, house={6}, qc={7}]", 
                source, region, area, city, settlement, street, house, qc
            );
        }
    }

    /// <summary>
    /// "As is" entity.
    /// </summary>
    public class AsIsData: IDadataEntity {
        public string source { get; set; }

        public StructureType structure_type {
            get { return StructureType.AS_IS; }
        }

        public override string ToString() {
            return string.Format("[AsIsData: source={0}]", source);
        }
    }

    public class BirthdateData: IDadataEntity {
        public string source    { get; set; }
        public string birthdate { get; set; }
        public string qc        { get; set; }

        public StructureType structure_type {
            get { return StructureType.BIRTHDATE; }
        }

        public override string ToString() {
            return string.Format("[BirthdateData: source={0}, birthdate={1}, qc={2}]", source, birthdate, qc);
        }
    }

    public class EmailData: IDadataEntity {
        public string source    { get; set; }
        public string email     { get; set; }
        public string qc        { get; set; }

        public StructureType structure_type {
            get { return StructureType.EMAIL; }
        }

        public override string ToString() {
            return string.Format("[EmailData: source={0}, email={1}, qc={2}]", source, email, qc);
        }
    }

    /// <summary>
    /// Fullname.
    /// </summary>
    public class NameData: IDadataEntity {
        public string source        { get; set; }
        public string result        { get; set; }
        public string surname       { get; set; }
        public string name          { get; set; }
        public string patronymic    { get; set; }
        public string gender        { get; set; }
        public string qc            { get; set; }

        public StructureType structure_type {
            get { return StructureType.NAME; }
        }

        public override string ToString() {
            return string.Format("[FioData: source={0}, surname={1}, name={2}, patronymic={3}, qc={4}]", 
                source, surname, name, patronymic, qc);
        }
    }

    /// <summary>
    /// Phone.
    /// </summary>
    public class PhoneData: IDadataEntity {
        public string source        { get; set; }
        public string type          { get; set; }
        public string phone         { get; set; }
        public string country_code  { get; set; }
        public string city_code     { get; set; }
        public string number        { get; set; }
        public string extension     { get; set; }
        public string provider      { get; set; }
        public string region        { get; set; }
        public string timezone      { get; set; }
        public string qc_conflict   { get; set; }
        public string qc            { get; set; }

        public StructureType structure_type {
            get { return StructureType.PHONE; }
        }
        
        public override string ToString() {
            return string.Format("[PhoneData: source={0}, phone={1}, qc={2}]", 
                source, phone, qc);
        }
    }
    
    /// <summary>
    /// Passport.
    /// </summary>
    public class PassportData: IDadataEntity {
        public string source    { get; set; }
        public string series    { get; set; }
        public string number    { get; set; }
        public string qc        { get; set; }

        public StructureType structure_type {
            get { return StructureType.PASSPORT; }
        }
        
        public override string ToString() {
            return string.Format("[PassportData: source={0}, series={1}, number={2}, qc={3}]", 
                source, series, number, qc);
        }
    }
    
    /// <summary>
    /// Vehicle.
    /// </summary>
    public class VehicleData: IDadataEntity {
        public string source    { get; set; }
        public string result    { get; set; }
        public string brand     { get; set; }
        public string model     { get; set; }
        public string qc        { get; set; }

        public StructureType structure_type {
            get { return StructureType.VEHICLE; }
        }
        
        public override string ToString() {
            return string.Format("[VehicleData: source={0}, result={1}, qc={2}]", 
                source, result, qc);
        }
    }

    /// <summary>
    /// Entity types, as supported by DaData.
    /// </summary>
    public enum StructureType {
        ADDRESS,
        AS_IS,
        BIRTHDATE,
        EMAIL,
        IGNORE,
        NAME,
        PASSPORT,
        PHONE,
        VEHICLE
    }

    /// <summary>
    /// Clean request.
    /// </summary>
    public class CleanRequest {
        public IEnumerable<StructureType> structure { get; set; }
        public IEnumerable<IEnumerable<string>> data { get; set; }

        public CleanRequest(IEnumerable<StructureType> structure, IEnumerable<IEnumerable<string>> data) {
            this.structure = structure;
            this.data = data;
        }

        public override string ToString() {
            return string.Format("[CleanQuery: structure={0}, data={1}]", string.Join(", ", structure), string.Join("\n", data));
        }
    }

    /// <summary>
    /// Clean response.
    /// </summary>
    public class CleanResponse {
        public IList<StructureType> structure { get; set; }
        public IList<IList<IDadataEntity>> data { get; set; }
    }
}
