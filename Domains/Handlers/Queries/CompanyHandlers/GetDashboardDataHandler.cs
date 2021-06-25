using Commom.Enum;
using Commom.Queries;
using Domains.Queries.Requests.CompanyRequests;
using Domains.Queries.Responses.CompanyResponses;
using Domains.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Queries.CompanyHandlers
{
    public class GetDashboardDataHandler : IRequestHandler<GetDashboardDataRequest, GenericQueryResult>
    {
        private readonly IOrderRepository _orderRepository;

        public GetDashboardDataHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<GenericQueryResult> Handle(GetDashboardDataRequest request, CancellationToken cancellationToken)
        {
            try
            {
                string type;
                DateTime minDate;
                DateTime maxDate;

                var query = _orderRepository.Read(request.CompanyId);

                if (request.Year <= 2000 || request.Year > DateTime.Now.Year)
                    return Task.FromResult(new GenericQueryResult(400, "Ano inválido!", null));

                if (!query.Any())
                    return Task.FromResult(new GenericQueryResult(404, null, null));

                if (request.Month != null)
                {
                    minDate = new DateTime(request.Year, (int)request.Month, 1);
                    maxDate = new DateTime(request.Year, (int)request.Month, DateTime.DaysInMonth(request.Year, (int)request.Month));
                    type = "Month";
                }
                else
                {
                    minDate = new DateTime(request.Year, 1, 1);
                    maxDate = new DateTime(request.Year, 12, DateTime.DaysInMonth(request.Year, 12));
                    type = "Year";
                }

                var orders =
                    query
                    .Where(o => o.CreatedAt >= minDate && o.CreatedAt <= maxDate)
                    .OrderBy(o => o.CreatedAt)
                    .GroupBy(o => o.CreatedAt)
                    .Select(o => new
                    {
                        Date = o.Key,
                        Count = o.Count()
                    });

                var finishedsWithSuccess =
                    query
                    .Where(o => o.FinishOrder != null && o.FinishOrder.FinishType == EnFinishType.Success && (o.FinishOrder.FinishedAt >= minDate && o.FinishOrder.FinishedAt <= maxDate))
                    .OrderBy(o => o.FinishOrder.FinishedAt)
                    .GroupBy(o => o.FinishOrder.FinishedAt)
                    .Select(o => new
                    {
                        Date = o.Key,
                        Count = o.Count()
                    });

                var finishedsWithDevolution =
                    query
                    .Where(o => o.FinishOrder != null && o.FinishOrder.FinishType == EnFinishType.Devolution && (o.FinishOrder.FinishedAt >= minDate && o.FinishOrder.FinishedAt <= maxDate))
                    .OrderBy(o => o.FinishOrder.FinishedAt)
                    .GroupBy(o => o.FinishOrder.FinishedAt)
                    .Select(o => new
                    {
                        Date = o.Key,
                        Count = o.Count()
                    });

                var occurrences =
                    query
                    .SelectMany(o => o.Occurrences.Where(oc => oc.CreatedAt >= minDate && oc.CreatedAt <= maxDate))
                    .OrderBy(o => o.CreatedAt)
                    .GroupBy(o => o.CreatedAt)
                    .Select(o => new
                    {
                        Date = o.Key,
                        Count = o.Count()
                    });

                GetDashboardDataResponse ordersGraph = null;
                GetDashboardDataResponse finishedsWithSuccessGraph = null;
                GetDashboardDataResponse finishedsWithDevolutionGraph = null;
                GetDashboardDataResponse occurrencesGraph = null;

                if (type == "Month")
                {
                    ordersGraph = new GetDashboardDataResponse("notes", "hsl(0, 0%, 53%)", SetNoDataByWeek(GenerateGraphAreaByWeek(orders), DateTime.DaysInMonth(request.Year, (int) request.Month)));

                    finishedsWithSuccessGraph = new GetDashboardDataResponse("success", "hsl(145, 63%, 49%)", SetNoDataByWeek(GenerateGraphAreaByWeek(finishedsWithSuccess), DateTime.DaysInMonth(request.Year, (int) request.Month)));

                    finishedsWithDevolutionGraph = new GetDashboardDataResponse("devolution", "hsl(0, 100%, 50%)", SetNoDataByWeek(GenerateGraphAreaByWeek(finishedsWithDevolution), DateTime.DaysInMonth(request.Year, (int) request.Month)));

                    occurrencesGraph = new GetDashboardDataResponse("occurrence", "hsl(36, 99%, 56%)", SetNoDataByWeek(GenerateGraphAreaByWeek(occurrences), DateTime.DaysInMonth(request.Year, (int) request.Month)));
                }
                else if(type == "Year")
                {
                    ordersGraph = new GetDashboardDataResponse("notes", "hsl(0, 0%, 53%)", SetNoDataByMonth(GenerateGraphAreaByMonth(orders)));

                    finishedsWithSuccessGraph = new GetDashboardDataResponse("success", "hsl(145, 63%, 49%)", SetNoDataByMonth(GenerateGraphAreaByMonth(finishedsWithSuccess)));

                    finishedsWithDevolutionGraph = new GetDashboardDataResponse("devolution", "hsl(0, 100%, 50%)", SetNoDataByMonth(GenerateGraphAreaByMonth(finishedsWithDevolution)));

                    occurrencesGraph = new GetDashboardDataResponse("occurrence", "hsl(36, 99%, 56%)", SetNoDataByMonth(GenerateGraphAreaByMonth(occurrences)));
                }

                var notesCount = orders.Select(o => o.Count).Sum();
                var finishedsWithSuccessCount = finishedsWithSuccess.Select(o => o.Count).Sum();
                var finishedsWithSuccessPercentage = Math.Round( (decimal) finishedsWithSuccessCount * 100 / (notesCount == 0 ? 1 : notesCount)) + "%";
                var finishedsWithDevolutionCount = finishedsWithDevolution.Select(o => o.Count).Sum();
                var finishedsWithDevolutionPercentage = Math.Round( (decimal) finishedsWithDevolutionCount * 100 / (notesCount == 0 ? 1 : notesCount)) + "%";
                var occurrencesCount = occurrences.Select(o => o.Count).Sum();
                var occurrencesAverage = Math.Round((decimal) occurrencesCount / (notesCount == 0 ? 1 : notesCount), 2);

                return Task.FromResult(new GenericQueryResult(200, null, new { Graphs = new[] { ordersGraph, finishedsWithSuccessGraph, finishedsWithDevolutionGraph, occurrencesGraph }, notesCount, finishedsWithSuccessCount, finishedsWithSuccessPercentage, finishedsWithDevolutionCount, finishedsWithDevolutionPercentage, occurrencesCount, occurrencesAverage}));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericQueryResult(500, ex.Message, ex.InnerException));
            }
        }

        private List<GraphAreas> GenerateGraphAreaByMonth(IQueryable<dynamic> query)
        {
            var graphAreas = new List<GraphAreas>();
            var graphArea = new GraphAreas();

            foreach (var group in query)
            {
                var month = group.Date.Month.ToString();

                graphArea = graphAreas.FirstOrDefault(g => g.X == month);
                if (graphArea == null)
                {
                    graphArea = new GraphAreas(month, group.Count);
                    graphAreas.Add(graphArea);
                }
                else
                    graphArea.Y += group.Count;
            }

            return graphAreas;
        }

        private static List<GraphAreas> SetNoDataByMonth(List<GraphAreas> graphAreas)
        {
            for (int i = 1; i <= 12; i++)
            {
                if (!graphAreas.Any(g => g.X == i.ToString()))
                    graphAreas.Add(new GraphAreas(i.ToString(), 0));
            }

            return graphAreas.OrderBy(o => Convert.ToInt32(o.X)).ToList();
        }

        private List<GraphAreas> GenerateGraphAreaByWeek(IQueryable<dynamic> query)
        {
            //1-7
            //8-14
            //15-21
            //22-28
            //28- se houver

            var graphAreas = new List<GraphAreas>();
            var graphArea = new GraphAreas();

            foreach (var group in query)
            {
                var x = (decimal)group.Date.Day / 7;

                if (x <= 1)
                {
                    graphArea = graphAreas.FirstOrDefault(g => g.X == "1-7");
                    if (graphArea == null)
                    {
                        graphArea = new GraphAreas("1-7", group.Count);
                        graphAreas.Add(graphArea);
                    }
                    else
                        graphArea.Y += group.Count;
                }
                else if (x > 1 && x <= 2)
                {
                    graphArea = graphAreas.FirstOrDefault(g => g.X == "8-14");
                    if (graphArea == null)
                    {
                        graphArea = new GraphAreas("8-14", group.Count);
                        graphAreas.Add(graphArea);
                    }
                    else
                        graphArea.Y += group.Count;
                    
                }
                else if (x > 2 && x <= 3)
                {
                    graphArea = graphAreas.FirstOrDefault(g => g.X == "15-21");
                    if (graphArea == null)
                    {
                        graphArea = new GraphAreas("15-21", group.Count);
                        graphAreas.Add(graphArea);
                    }
                    else
                        graphArea.Y += group.Count;
                    
                }
                else if (x > 3 && x <= 4)
                {
                    graphArea = graphAreas.FirstOrDefault(g => g.X == "22-28");
                    if (graphArea == null)
                    {
                        graphArea = new GraphAreas("22-28", group.Count);
                        graphAreas.Add(graphArea);
                    }
                    else
                        graphArea.Y += group.Count;
                }
                else
                {
                    graphArea = graphAreas.FirstOrDefault(g => g.X == $"29-{DateTime.DaysInMonth(group.Date.Year, group.Date.Month)}");
                    if (graphArea == null)
                    {
                        graphArea = new GraphAreas($"29-{DateTime.DaysInMonth(group.Date.Year, group.Date.Month)}", group.Count);
                        graphAreas.Add(graphArea);
                    }
                    else
                        graphArea.Y += group.Count;
                }
            }

            return graphAreas;
        }

        private static List<GraphAreas> SetNoDataByWeek(List<GraphAreas> graphAreas, int totalDays)
        {
            if (!graphAreas.Any(g => g.X == "1-7"))
                graphAreas.Add(new GraphAreas("1-7", 0));
            if (!graphAreas.Any(g => g.X == "8-14"))
                graphAreas.Add(new GraphAreas("8-14", 0));
            if (!graphAreas.Any(g => g.X == "15-21"))
                graphAreas.Add(new GraphAreas("15-21", 0));
            if (!graphAreas.Any(g => g.X == "21-28"))
                graphAreas.Add(new GraphAreas("21-28", 0));
            if (totalDays > 28)
                if (!graphAreas.Any(g => g.X == $"28-{totalDays}"))
                    graphAreas.Add(new GraphAreas($"28-{totalDays}", 0));

            return graphAreas.OrderBy(o => Convert.ToInt32(o.X.Split("-")[0])).ToList();
        }
    }
}
