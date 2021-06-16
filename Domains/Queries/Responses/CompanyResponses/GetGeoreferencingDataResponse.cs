namespace Domains.Queries.Responses.CompanyResponses
{
    public class GetGeoreferencingDataResponse
    {
        public string DelivererName { get; set; }
        public string CellphoneNumber { get; set; }
        public string VehicleType { get; set; }
        public string VehiclePlate { get; set; }
        public string Date { get; set; }
        public string AccessKey { get; set; }
        public string Type { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public GetGeoreferencingDataResponse(
            string delivererName,
            string cellphoneNumber,
            string vehicleType,
            string vehiclePlate,
            string date,
            string accessKey,
            string type,
            decimal latitude,
            decimal longitude
        )
        {
            DelivererName = delivererName;
            CellphoneNumber = cellphoneNumber;
            VehicleType = vehicleType;
            VehiclePlate = vehiclePlate;
            Date = date;
            AccessKey = accessKey;
            Type = type;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
