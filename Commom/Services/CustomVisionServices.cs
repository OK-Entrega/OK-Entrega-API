using Microsoft.AspNetCore.Http;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Commom.Services
{
    public class CustomVisionServices
    {
        private string Endpoint { get; set; }
        private string PredictionKey { get; set; }
        private string TrainingKey { get; set; }
        private string PublishedModelName { get; set; }
        private CustomVisionPredictionClient PredictionApi { get; set; }
        private CustomVisionTrainingClient TrainingApi { get; set; }
        private Project Project { get; set; }

        public CustomVisionServices()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            Endpoint = configuration.GetSection("CustomVision")["Endpoint"];
            PredictionKey = configuration.GetSection("CustomVision")["PredictionKey"];
            TrainingKey = configuration.GetSection("CustomVision")["TrainingKey"];
            PublishedModelName = configuration.GetSection("CustomVision")["PublishedModelName"];

            PredictionApi = AuthenticatePrediction(Endpoint, PredictionKey);
            TrainingApi = AuthenticateTraining(Endpoint, TrainingKey);

            Project = GetProject(TrainingApi, configuration.GetSection("CustomVision")["ProjectId"]);
        }

        public Dictionary<string, double> AnalyzeVoucher(string path)
        {
            using var stream = File.OpenRead(path);
            var result = (List<PredictionModel>)PredictionApi.DetectImage(Project.Id, PublishedModelName, stream).Predictions;

            var dictionary = new Dictionary<string, double>();

            foreach (var c in result.Select(r => new { r.TagName, r.Probability }))
            {
                if (dictionary.ContainsKey(c.TagName))
                    dictionary[c.TagName] = dictionary[c.TagName] < c.Probability ? Math.Round(c.Probability, 2) : Math.Round(dictionary[c.TagName], 2);
                else
                    dictionary.Add(c.TagName, Math.Round(c.Probability, 2));
            }

            return dictionary;
        }

        private CustomVisionTrainingClient AuthenticateTraining(string endpoint, string trainingKey)
        {
            CustomVisionTrainingClient trainingApi = new CustomVisionTrainingClient(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.ApiKeyServiceClientCredentials(trainingKey))
            {
                Endpoint = endpoint
            };
            return trainingApi;
        }

        private CustomVisionPredictionClient AuthenticatePrediction(string endpoint, string predictionKey)
        {
            CustomVisionPredictionClient predictionApi = new CustomVisionPredictionClient(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.ApiKeyServiceClientCredentials(predictionKey))
            {
                Endpoint = endpoint
            };

            return predictionApi;
        }

        private Project GetProject(CustomVisionTrainingClient trainingApi, string projectId)
        {
            return trainingApi.GetProject(Guid.Parse(projectId));
        }
    }
}
