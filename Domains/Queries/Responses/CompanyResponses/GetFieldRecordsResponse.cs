namespace Domains.Queries.Responses.CompanyResponses
{
    public class GetFieldRecordsResponse
    {
        public string DelivererName { get; set; }
        public int FinishedsWithSuccess { get; set; }
        public int FinishedsWithDevolution { get; set; }
        public int Occurrences { get; set; }

        public GetFieldRecordsResponse(
            string delivererName,
            int finishedsWithSuccess,
            int finishedsWithDevolution,
            int occurrences
        )
        {
            DelivererName = delivererName;
            FinishedsWithSuccess = finishedsWithSuccess;
            FinishedsWithDevolution = finishedsWithDevolution;
            Occurrences = occurrences;
        }
    }
}
