using System.Collections.Generic;

namespace Domains.Queries.Responses.CompanyResponses
{
    public class GetDashboardDataResponse
    {
        public string Id { get; set; }
        public string Color { get; set; }
        public List<GraphAreas> Data { get; set; }

        public GetDashboardDataResponse(
            string id,
            string color,
            List<GraphAreas> data
        )
        {
            Id = id;
            Color = color;
            Data = data;
        }
    }

    public class GraphAreas
    {
        public string X { get; set; }
        public int Y { get; set; }

        public GraphAreas(
            string x,
            int y
        )
        {
            X = x;
            Y = y;
        }

        public GraphAreas(){}
    }
}
