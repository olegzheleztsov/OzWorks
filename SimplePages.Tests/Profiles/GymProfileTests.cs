using System;
using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using SimplePages.Models.GymStats;
using SimplePages.Profiles;
using SimplePages.Services;
using SimplePages.ViewModels;
using Xunit;

namespace SimplePages.Tests.Profiles
{
    public class GymProfileTests
    {
        [Fact]
        public void Should_Correctly_Map_Training()
        {
            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new GymProfile(new ExerciseNames()));
            });
            

            var viewModel = new TrainingViewModel()
            {
                Id = Guid.NewGuid().ToString(),
                Date = DateTime.Now,
                Exercises = new List<PhysicalExerciseViewModel>()
                {
                    new PhysicalExerciseViewModel()
                    {
                        ExerciseId = 2,
                        Value = "15"
                    }
                }
            };

            var exerciseNameService = new ExerciseNames();
            var mapper = config.CreateMapper();
            var model = mapper.Map<Training>(viewModel);
            model.Should().NotBe(null);
            model.Date.Should().BeSameDateAs(viewModel.Date);
            Assert.NotNull(model);
            Assert.Equal(viewModel.Id, model.Id);
            Assert.Equal(viewModel.Date, model.Date);
            Assert.Equal(viewModel.Exercises.Count, model.Exercises.Count);
            Assert.Equal(viewModel.Exercises[0].Value, model.Exercises[0].Value);
            Assert.Equal(viewModel.Exercises[0].ExerciseId, exerciseNameService.GetExerciseName(model.Exercises[0].BodyPart, model.Exercises[0].Name).Id);
        }
    }
}